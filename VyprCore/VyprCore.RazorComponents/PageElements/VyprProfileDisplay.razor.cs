// <copyright file="VyprCoreProfileDisplay" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.PageElements
{
    using Microsoft.AspNetCore.Components;
    using VyprCore.Client.Api;
    using System.Threading.Tasks;

    /// <summary>
    /// Vypr core profile display.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public partial class VyprProfileDisplay : ComponentBase
    {
        private string _userName;
        private string _userNameHash;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprProfileDisplay"/> class.
        /// </summary>
        public VyprProfileDisplay()
        {
        }

        [Inject]
        public AccountManageApi AccountApi { get; set; }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            var profileDisplayModel = await AccountApi.GetProfileDisplayAsync(new HttpCodeResponseIntercept());

            _userName = profileDisplayModel.UserName;
            _userNameHash = profileDisplayModel.UserNameHash;

            await base.OnInitializedAsync();
        }
    }
}