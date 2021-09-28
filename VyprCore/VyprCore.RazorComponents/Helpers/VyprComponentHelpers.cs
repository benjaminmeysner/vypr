// <copyright file="VyprComponentHelpers" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Helpers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Radzen;
    using VyprCore.Interfaces.Client;
    using VyprCore.RazorComponents.Services;

    /// <summary>
    /// Provides helper methods for components.
    /// </summary>
    public static class VyprComponentHelpers
    {
        /// <summary>
        /// Missing WebAuthn credentials.
        /// </summary>
        public const string LOGIN_ECODE_MISSINGCREDENTIALS = "1";

        /// <summary>
        /// Compares the specified generic values using the default equality comparer for the underlying type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public static bool Compare<T>(T x, T y)
        {
            return EqualityComparer<T>.Default.Equals(x, y);
        }

        /// <summary>
        /// Validates the field identifier contains useful information.
        /// </summary>
        /// <param name="fieldIdentifier">The field identifier.</param>
        /// <returns></returns>
        public static bool ValidFieldIdentifier(FieldIdentifier fieldIdentifier)
        {
            return !string.IsNullOrEmpty(fieldIdentifier.FieldName) && !(fieldIdentifier.Model is null);
        }

        /// <summary>
        /// Vyprs the data grid selection mode to radzen.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns></returns>
        public static DataGridSelectionMode VyprDataGridSelectionModeToRadzen(VyprDataGridSelectionMode mode)
        {
            return mode switch
            {
                VyprDataGridSelectionMode.Single => DataGridSelectionMode.Single,
                _ => DataGridSelectionMode.Multiple
            };
        }

        /// <summary>
        /// Vyprs the tool tip position to radzen tooltip position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public static TooltipPosition VyprToolTipPosToRadzenTooltipPos(int position)
        {
            return position switch
            {
                0 => TooltipPosition.Bottom,
                1 => TooltipPosition.Left,
                2 => TooltipPosition.Top,
                3 => TooltipPosition.Right,
                _ => TooltipPosition.Top
            };
        }

        /// <summary>
        /// Creates the label render fragment for a vypr form component.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="isRequired">if set to <c>true</c> [is required].</param>
        /// <returns></returns>
        public static RenderFragment CreateLabelRenderTree(string label, bool isRequired) => builder =>
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "vypr-rz-componentlabel");

            builder.OpenElement(2, "h5");
            builder.AddContent(3, label);

            if (isRequired)
            {
                builder.OpenElement(4, "span");
                builder.AddAttribute(5, "class", "vypr-rz-componentrequired");
                builder.CloseElement();
            }

            builder.CloseElement();
            builder.CloseElement();
        };

        /// <summary>
        /// Creates the error diaglog render tree.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static RenderFragment CreateErrorDiaglogRenderTree(string message) => builder =>
        {
            builder.OpenElement(0, "div");

            builder.OpenElement(2, "p");
            builder.AddContent(3, message);

            builder.CloseElement();
            builder.CloseElement();
        };

        /// <summary>
        /// Creates the error diaglog render tree.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static RenderFragment CreateSuccessDiaglogRenderTree(string message) => builder =>
        {
            builder.OpenElement(0, "div");

            builder.OpenElement(2, "p");
            builder.AddContent(3, message);

            builder.CloseElement();
            builder.CloseElement();
        };

        /// <summary>
        /// Registers the framework razor component services.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void AddFrameworkRazorComponentServices(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<IDialogService, VyprDialogService>();

            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<IToastService, VyprToastService>();

            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<IToolTipService, VyprToolTipService>();
        }
    }
}
