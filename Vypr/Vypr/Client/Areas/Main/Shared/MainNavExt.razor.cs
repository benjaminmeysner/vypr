// <copyright file="MainNavExt.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Client.Areas.Main.Shared
{
    using Microsoft.AspNetCore.Components;
    using Vypr.Client.Enums;

    /// <summary>
    /// Main nav ext.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public partial class MainNavExt
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainNavExt"/> class.
        /// </summary>
        public MainNavExt()
        {
        }

        [Parameter]
        public MenuNavTarget NavTarget {get; set;}
    }
}