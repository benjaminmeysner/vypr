// <copyright file="FidoStrategy.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Server.Data.Strategy
{
    using AutoMapper;
    using Fido2NetLib;
    using Fido2NetLib.Objects;
    using Microsoft.AspNetCore.Http;
    using Vypr.Server.Authentication.Managers;
    using Vypr.Server.Classes.Context;
    using VyprCore.Interfaces.Logging;
    using VyprCore.Interfaces.Repository;
    using VyprCore.Models;
    using VyprCore.Models.Domain;
    using VyprCore.Models.Resources;
    using VyprCore.Models.ViewModels;
    using VyprCore.Utilities.Exceptions;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static Fido2NetLib.Fido2;
    using Vypr.Server.Data.Repository;

    /// <summary>
    /// FidoIdentity Auth/WebAuthn strategy.
    /// </summary>
    /// <seealso cref="BaseStrategy{FidoCredential, FidoCredentialViewModel, WebAuthnRepository{TDbContext}}" />
    public class WebAuthnStrategy : BaseStrategy<VyprWebAuthnCredential, WebAuthnCredentialViewModel, WebAuthnRepository>
    {
        /// <summary>
        /// Web authn strategy.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        /// <param name="webAuthn"></param>
        /// <param name="logger"></param>
        /// <param name="userManager"></param>
        /// <param name="applicationContext"></param>
        public WebAuthnStrategy(
            IRepository<VyprWebAuthnCredential> repository,
            IMapper mapper,
            IVyprLogger logger,
            ApplicationContext applicationContext)
            : base(repository, mapper, logger, applicationContext)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="WebAuthnStrategy{TDbContext}"/> class.</summary>
        public WebAuthnStrategy()
        {
        }

        /// <summary>
        /// Get the existing user keys.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        /// <summary>
        /// Makes the webauthn credential options.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static async Task<CredentialCreateOptions> MakeCredentialOptions(HttpContext context, WebAuthnCredentialOptionsViewModel viewModel, VyprUserManager userManager, IRepository<VyprWebAuthnCredential> repo, WebAuthn webAuthn, IMapper mapper)
        {
            try
            {
                var vypruser = await userManager.FindByEmailAsync(viewModel.UserName) ?? await userManager.FindByNameAsync(viewModel.UserName);
                var keys = await GetExistingKeysForUserAsync(vypruser.UserName, repo);

                var authenticatorSelection = new AuthenticatorSelection
                {
                    RequireResidentKey = true,
                    UserVerification = UserVerificationRequirement.Required,
                    AuthenticatorAttachment = AuthenticatorAttachment.Platform // No USB Keys, Windows Hello, Yubi-Keys etc. Only fingerprints.
                };

                var exts = new AuthenticationExtensionsClientInputs() { Extensions = true, UserVerificationIndex = true, Location = true, UserVerificationMethod = true, BiometricAuthenticatorPerformanceBounds = new AuthenticatorBiometricPerfBounds { FAR = float.MaxValue, FRR = float.MaxValue } };
                var newCred = webAuthn.RequestNewCredential(WebAuthn.CreateWebAuthnUser(vypruser.UserName, vypruser.UserName, vypruser.Id), keys.ToList(), authenticatorSelection, AttestationConveyancePreference.Direct, exts);
                newCred.Rp.Icon = $"{context.Request.Scheme}://{context.Request.Host}/";
                return newCred;
            }
            catch
            {
                throw new ArgumentException($"{StandardText.WebAuthnLoginProblem}");
            }
        }

        /// <summary>
        /// Create assertion options.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userManager"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        public static async Task<AssertionOptions> MakeAssertionOptions(string userName, VyprUserManager userManager, IRepository<VyprWebAuthnCredential> repository, WebAuthn webAuthn)
        {
            try
            {
                var vypruser = await userManager.FindByEmailAsync(userName);
                var keys = await GetExistingKeysForUserAsync(vypruser.UserName, repository);                
                var exts = new AuthenticationExtensionsClientInputs() { SimpleTransactionAuthorization = "FIDO", GenericTransactionAuthorization = new TxAuthGenericArg { ContentType = "text/plain", Content = new byte[] { 0x46, 0x49, 0x44, 0x4F } }, UserVerificationIndex = true, Location = true, UserVerificationMethod = true };
                var options = webAuthn.GetAssertionOptions(keys, UserVerificationRequirement.Required, exts);
                return options;
            }
            catch
            {
                throw new ArgumentException($"{StandardText.WebAuthnLoginProblem}");
            }
        }

        /// <summary>
        /// Make the credentials.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>the new model.</retu
        public async Task<CredentialMakeResult> MakeCredential(WebAuthnCreateCredentialViewModel viewModel, VyprSignInManager signInManager, VyprUserManager userManager, IRepository<VyprWebAuthnCredential> repository, WebAuthn webAuthn, IMapper mapper)
        {
            try
            {
                var vypruser = await userManager.FindByEmailAsync(viewModel.UserName);
                if (vypruser is null || !await userManager.CheckPasswordAsync(vypruser, viewModel.Password))
                {
                    throw new ArgumentException(StandardText.WebAuthnVerifyProblem);
                }

                IsCredentialIdUniqueToUserAsyncDelegate callback = async (IsCredentialIdUniqueToUserParams args) =>
                {
                    var users = await GetUsersByCredentialIdAsync(args.CredentialId, userManager, repository);
                    if (users.Count > 0) return false;
                    return true;
                };

                var success = await webAuthn.MakeNewCredentialAsync(viewModel.DeviceResponse, viewModel.CreateCredentialOptions, callback);
                if (success.Status.ToUpper() == "OK")
                {
                    await AddCredential(new WebAuthnCredentialViewModel
                    {
                        UserId = viewModel.CreateCredentialOptions.User.Id,
                        Username = viewModel.CreateCredentialOptions.User.Name,
                        PublicKey = success.Result.PublicKey,
                        UserHandle = success.Result.User.Id,
                        SignatureCounter = success.Result.Counter,
                        CredType = success.Result.CredType,
                        RegDate = DateTime.Now,
                        AaGuid = success.Result.Aaguid,
                        DescriptorJson = System.Text.Json.JsonSerializer.Serialize(new PublicKeyCredentialDescriptor(success.Result.CredentialId)),
                    }, mapper, repository);

                    await signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, isPersistent: true, lockoutOnFailure: false);
                }

                return success;
            }
            catch (NoTenantFoundException)
            {
                throw new Exception("The device link was created but could not log in due to not finding a tenant for this origin (URL).");
            }
            catch (Exception)
            {
                throw new Exception(StandardText.WebAuthnCreateCredentialProblem);
            }
        }

        /// <summary>
        /// Adds the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>the new model.</returns>
        public static async Task AddCredential(WebAuthnCredentialViewModel viewModel, IMapper mapper, IRepository<VyprWebAuthnCredential> repo)
        {
            var entity = mapper.Map<VyprWebAuthnCredential>(viewModel);
            await repo.Add(entity);
            await repo.SaveChanges();
        }

        /// <summary>
        /// Make the assertion.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userManager"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        public static async Task<AssertionVerificationResult> MakeAssertion(WebAuthnAuthenticatorAssertionRawResponseViewModel viewModel, VyprSignInManager signInManager, VyprUserManager userManager, IRepository<VyprWebAuthnCredential> repository, WebAuthn webAuthn)
        {
            var credential  = await GetKeyById(viewModel.Response.Id, repository);

            IsUserHandleOwnerOfCredentialIdAsync callback = async (args) =>
            {
                var storedCreds = await GetCredentialsByUserHandleAsync(args.UserHandle, repository);
                return storedCreds.Exists(c => c.Descriptor.Id.SequenceEqual(args.CredentialId));
            };

            var res = await webAuthn.MakeAssertionAsync(viewModel.Response, viewModel.AssertionOptions, credential.PublicKey, credential.SignatureCounter, callback);
            if (res.Status.ToUpper() == "OK")
            {
                await UpdateCounter(res.CredentialId, res.Counter, repository);
                var vypruser = await userManager.FindByNameAsync(credential.Username);
                await signInManager.SignInAsync(vypruser, isPersistent: false);
            }

            return res;
        }

        /// <summary>
        /// Gets the fido credentials of the user by unique username.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        private static async Task<List<PublicKeyCredentialDescriptor>> GetExistingKeysForUserAsync(string userName, IRepository<VyprWebAuthnCredential> repo)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return new();
            }

            var keys = await repo.Find(x => x.Username == userName);

            return keys.Select(x => x.Descriptor).ToList();
        }

        /// <summary>
        /// Gets the fido credentials of the user by unique username.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        private static async Task<VyprWebAuthnCredential> GetKeyById(byte[] id, IRepository<VyprWebAuthnCredential> repo)
        {
            var credentialIdString = Convert.ToBase64String(id);
            var cred = await repo.Find(x => x.DescriptorJson.Contains(credentialIdString));
            return cred.FirstOrDefault();
        }

        /// <summary>
        /// [GetCredentialsByUserHandleAsync]
        /// </summary>
        /// <param name="userHandle"></param>
        /// <param name="repo"></param>
        /// <returns></returns>
        private static async Task<List<VyprWebAuthnCredential>> GetCredentialsByUserHandleAsync(byte[] userHandle, IRepository<VyprWebAuthnCredential> repo)
        {
            return await (await repo.Find(c => c.UserHandle.SequenceEqual(userHandle))).ToListAsync();
        }

        /// <summary>
        /// [UpdateCounter]
        /// </summary>
        /// <param name="credentialId"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        private static async Task UpdateCounter(byte[] credentialId, uint counter, IRepository<VyprWebAuthnCredential> repo)
        {
            var credentialIdString = Convert.ToBase64String(credentialId);
            var cred = (await repo.Find(x => x.DescriptorJson.Contains(credentialIdString))).FirstOrDefault();
            cred.SignatureCounter = counter;
            await repo.Update(cred);
            await repo.SaveChanges();
        }

        /// <summary>
        /// Get users by credential id async.
        /// </summary>
        /// <param name="credentialId"></param>
        /// <param name="repo"></param>
        /// <returns></returns>
        private static async Task<List<Fido2User>> GetUsersByCredentialIdAsync(byte[] credentialId, VyprUserManager userManager, IRepository<VyprWebAuthnCredential> repo)
        {
            var credentialIdString = Base64Url.Encode(credentialId);
            var cred = (await repo.Find(x => x.DescriptorJson.Contains(credentialIdString))).FirstOrDefault();

            if (cred == null)
            {
                return new();
            }

            var users = await userManager.Users.Where(x => Encoding.UTF8.GetBytes(x.UserName).SequenceEqual(cred.UserId)).Select(x => WebAuthn.CreateWebAuthnUser(x.UserName, x.UserName, x.Id)).ToListAsync();
            return users;
        }
    }
}
