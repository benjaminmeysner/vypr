// <copyright file="VyprCoreCrudLayoutBase" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Base
{
    /// <summary>
    /// Vypr Core Crud Layout (Create/Update/Delete) base class.
    /// </summary>
    public abstract class VyprCrudLayoutBase : VyprComponentBase
    {
        public const string CrudFlyOutOpen = "vypr-crud-flyout";
        public const string CrudContainer = "vypr-crud-container";
        public const string CrudGridFullWidthClass = "vypr-crud-grid-full";
        public const string CrudGridActionsClass = "vypr-datagrid-actions";
        public const string CrudGridPartialWidthClass = "vypr-crud-grid-partial";

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprCrudLayoutBase"/> class.
        /// </summary>
        public VyprCrudLayoutBase() : base(nameof(VyprCrudLayoutBase))
        {
            CrudFlyOutContainer = CrudFlyOutOpen;
            CrudGridContainer = CrudGridFullWidthClass;
        }

        public string CrudGridContainer { get; set; }

        public string CrudFlyOutContainer { get; set; }

        public void OpenFlyOut() => CrudGridContainer = CrudGridPartialWidthClass;

        public void CloseFlyOut() => CrudGridContainer = CrudGridFullWidthClass;
    }
}
