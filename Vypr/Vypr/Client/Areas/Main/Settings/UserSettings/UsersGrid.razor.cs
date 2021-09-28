// <copyright file="UsersGrid.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace Vypr.Client.Areas.Main.Settings.UserSettings
{
    using System.Threading.Tasks;
    using Vypr.Client.Api;
    using VyprCore.Client.Api;
    using VyprCore.Models.Models;
    using VyprCore.Models.ViewModels;

    /// <summary>
    /// Users grid.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.ComponentBase" />
    public partial class UsersGrid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersGrid"/> class.
        /// </summary>
        public UsersGrid()
        {
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected override async Task LoadData(LoadDataArgs args = null)
        {
            var gridQuery = await EntityApi.GridQueryAsync<VyprUserApiRoute, VyprUserViewModel>(args, new HttpCodeResponseIntercept());
            await SetDataAsync(gridQuery);
        }
    }
}