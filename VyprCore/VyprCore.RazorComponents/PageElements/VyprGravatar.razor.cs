// <copyright file="VyprCoreConnectionStatus.razor" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.PageElements
{
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Vypr Gravatar.
    /// </summary>
    public partial class VyprGravatar
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VyprGravatar"/> class.
        /// </summary>
        public VyprGravatar() : base(nameof(VyprGravatar))
        {
        }

        [Parameter]
        public string EmailHash { get; set; }

        [Parameter]
        public bool Visible { get; set; } = true;

        protected string Url
        {
            get
            {
                var style = "retro";
                var width = "36";

                return $"https://secure.gravatar.com/avatar/{EmailHash}?d={style}&s={width}";
            }
        }
    }
}