// <copyright file="WebAuthnApiController.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.Foundation.Identity.Fido.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Blazored.LocalStorage;
    using Fido2NetLib;
    using Fido2NetLib.Objects;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VyprCore.Foundation.Authentication.Managers;
    using VyprCore.Foundation.BaseClasses.Controller;
    using VyprCore.Foundation.Identity.Fido;
    using VyprCore.Interfaces.Repository;
    using VyprCore.Interfaces.Strategy;
    using VyprCore.Models;
    using VyprCore.Models.Domain;
    using VyprCore.Models.ViewModels;
    using VyprCore.Utilities.Exceptions;
    using VyprCore.Utilities.Helpers;
    using static Fido2NetLib.Fido2;

    /// <summary>
    /// Fido credentials controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [AllowAnonymous]
    [Route("api/webAuthn")]
    public class WebAuthnApiController : BaseApiController<VyprWebAuthnCredential, WebAuthnCredentialViewModel, WebAuthnRepository, WebAuthnStrategy>
    {
        private readonly IMapper _mapper;
        protected readonly WebAuthn _webAuthn;
        protected readonly VyprUserManager _userManager;
        protected readonly VyprSignInManager _signInManager;
        protected readonly ILocalStorageService _localStorage;
        protected readonly IRepository<VyprWebAuthnCredential> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFidoCredentialsController"/> class.
        /// This controller is responsible for the routing and interaction of fido2 services.
        /// </summary>
        /// <param name="webAuthn">The fido/webAuthn instance.</param>
        /// <param name="userManager">The user manager instance.</param>
        /// <param name="signInManager">The sign-in manager instance.</param>
        /// <param name="strategy">The strategy.</param>
        public WebAuthnApiController(
            WebAuthn webAuthn,
            IMapper mapper,
            VyprUserManager userManager,
            VyprSignInManager signInManager,
            ILocalStorageService localStorage,
            IRepository<VyprWebAuthnCredential> repository,
            IStrategy<VyprWebAuthnCredential, WebAuthnCredentialViewModel, WebAuthnRepository> strategy) : base(strategy)
        {
            _mapper = mapper;
            _webAuthn = webAuthn;
            _userManager = userManager;
            _signInManager = signInManager;
            _localStorage = localStorage;
            // Not quite sure why WebAuthnStrategy cannot inject stuff in?
            _repository = repository;
        }

        /// <summary>
        /// Makes the fido credential options.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>Json Result.</returns>
        [HttpPost("credential/createoptions")]
        public async Task<ActionResult<CredentialCreateOptions>> GetUserKeys([FromBody] WebAuthnCredentialOptionsViewModel viewModel)
        {
            try
            {
                return Ok(await WebAuthnStrategy.MakeCredentialOptions(HttpContext, viewModel, _userManager, _repository, _webAuthn, _mapper));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Makes the fido credential options.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>Json Result.</returns>
        [HttpGet("assertion/createoptions")]
        public async Task<ActionResult<AssertionOptions>> MakeAssertionOptions(string userName)
        {
            try
            {
                return Ok(await WebAuthnStrategy.MakeAssertionOptions(userName, _userManager, _repository, _webAuthn));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Makes the assertion.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>Json Result.</returns>
        [HttpPost("assertion/make")]
        public async Task<ActionResult<AssertionVerificationResult>> MakeAssertion([FromBody] WebAuthnAuthenticatorAssertionRawResponseViewModel viewModel)
        {
            try
            {
                return new JsonResult(await WebAuthnStrategy.MakeAssertion(viewModel, _signInManager, _userManager, _repository, _webAuthn));
            }
            catch (NoTenantFoundException)
            {
                return new StatusCodeResult(202);
            }
            catch (Exception ex)
            {
                return BadRequest(StringHelpers.FormatException(ex));
            }
        }

        /// <summary>
        /// Makes the assertion.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>Json Result.</returns>
        [HttpPost("credential/make")]
        public async Task<ActionResult<CredentialMakeResult>> MakeCredential([FromBody] WebAuthnCreateCredentialViewModel viewModel)
        {
            try 
            {
                return new JsonResult(await _strategy.MakeCredential(viewModel, _signInManager, _userManager, _repository, _webAuthn, _mapper));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Verify the user.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>Json Result.</returns>
        [HttpPost("credential/verify")]
        public async Task<ActionResult<bool>> VerifyUser([FromBody] WebAuthnCredentialVerifyViewModel viewModel)
        {
            try
            {
                return Ok(await _userManager.CheckPasswordAsync(await _userManager.FindByEmailAsync(viewModel.UserName), viewModel.Password));
            }
            catch (Exception ex)
            {
                return BadRequest(new AssertionVerificationResult { Status = "error", ErrorMessage = StringHelpers.FormatException(ex) });
            }
        }
    }
}