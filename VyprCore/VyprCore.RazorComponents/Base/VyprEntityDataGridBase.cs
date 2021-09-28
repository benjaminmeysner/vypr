// <copyright file="VyprEntityDataGridBase.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Base
{
    using Microsoft.AspNetCore.Components;
    using VyprCore.Interfaces.Client;
    using VyprCore.Interfaces.Entity;
    using VyprCore.Interfaces.Model;
    using VyprCore.Interfaces.Repository;
    using VyprCore.Models.Models;
    using VyprCore.RazorComponents.DataGrid;
    using VyprCore.Utilities.Helpers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Base class for implementing the VyprEntityDataGrid on the UI with entities.
    /// </summary>
    /// <seealso cref="ComponentBase" />
    public abstract class VyprEntityDataGridBase<TViewModel> : VyprComponentBase
        where TViewModel : IEntityViewModel, ICloneableViewModel<TViewModel>, new()
    {
        protected readonly string _cssGridWidth;

        protected int _entityCount;
        protected List<TViewModel> _entities;
        private IList<TViewModel> _selectedEntities;
        protected VyprDataGrid<TViewModel> _internalGrid;

        public VyprEntityDataGridBase() : base(nameof(VyprEntityDataGridBase<TViewModel>))
        {
            _entities = new List<TViewModel>();

            // Width of page - 75px (thin sidebar-default) - leftmargin - rightmargin
            // TODO: When sidebar expands, this style needs to reflect the new width!
            _cssGridWidth = "max-width: calc(100vw - 75px - 2rem - 1.5rem);";
        }

        [Parameter]
        public IList<TViewModel> SelectedEntities
        {
            get => _selectedEntities;
            set
            {
                _selectedEntities = value;
                AnyEntitiesSelected = ObjectHelpers.ValueOrFalse(_selectedEntities?.Any());

                SelectedEntitiesChanged.InvokeAsync(_selectedEntities);
            }
        }

        [Parameter]
        public EventCallback<IList<TViewModel>> SelectedEntitiesChanged { get; set; }

        public bool AnyEntitiesSelected { get; private set; }

        public int? Count => _internalGrid?.Count;

        [Inject]
        public IEntityApi EntityApi { get; set; }

        /// <summary>
        /// Reloads the grid asynchronous.
        /// </summary>
        /// <param name="clearSelectedUsers">if set to <c>true</c> [clear selected users].</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task ReloadGridAsync(bool clearSelectedUsers = false) => await _internalGrid?.ReloadAsync(clearSelectedUsers);

        /// <summary>
        /// Edits the row asynchronous.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public void UpdateEntity(TViewModel row)
        {
            _internalGrid?.UpdateRow(row);
        }

        /// <summary>
        /// Loads the user data.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected abstract Task LoadData(LoadDataArgs args = null);

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="gridQuery">The grid query.</param>
        protected async Task SetDataAsync(IQueryResult<TViewModel> gridQuery)
        {
            _entities = gridQuery?.Data;
            _entityCount = gridQuery?.Count ?? 0;
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Creates the entity copy.
        /// </summary>
        /// <returns></returns>
        public TViewModel CreateEntityCopy() => _internalGrid.CreateSelectedRowModelCopy();
    }
}
