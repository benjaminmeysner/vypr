// <copyright file="Index.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Client.Areas
{
    using System.Threading.Tasks;

    /// <summary>
    /// Index epod.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public partial class Index
    {
        protected const string RelativePath = "/";

        /// <summary>
        /// Initializes a new instance of the <see cref="Index"/> class.
        /// </summary>
        public Index()
        {
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}