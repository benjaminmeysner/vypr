// <copyright file="VyprToolTipService" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Services
{
    using Microsoft.AspNetCore.Components;
    using Radzen;
    using VyprCore.Interfaces.Client;
    using VyprCore.RazorComponents.Helpers;

    /// <summary>
    /// Vypr Toast Service.
    /// </summary>
    public class VyprToolTipService : IToolTipService
    {
        private readonly TooltipService _radzenToolTipService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprToastService"/> class.
        /// </summary>
        /// <param name="notificationService">The notification service.</param>
        public VyprToolTipService(TooltipService radzenToolTipService)
        {
            _radzenToolTipService = radzenToolTipService;
        }

        public void Open(ElementReference element, string message, IToolTipPosition position)
        {
            _radzenToolTipService.Open(element, message, new TooltipOptions { Position = VyprComponentHelpers.VyprToolTipPosToRadzenTooltipPos(position.Position) });
        }
    }
}
