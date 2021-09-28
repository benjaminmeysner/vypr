// <copyright file="VyprCoreComponentBase" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Base
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Radzen.Blazor;
    using VyprCore.Interfaces.Entity;
    using VyprCore.Interfaces.Model;
    using VyprCore.Models.Models;
    using VyprCore.Models.Resources;

    /// <summary>
    /// Vypr Component base class.
    /// Logically bundles common functionality between Vypr Components.
    /// This class inherits <see cref="Microsoft.AspNetCore.Components.ComponentBase"/>.
    /// </summary>
    public abstract class VyprDataGridBase<TViewModel> : VyprComponentBase
        where TViewModel : ICloneableViewModel<TViewModel>, IEntityViewModel, new()
    {
        private bool _initialLoadComplete;
        private List<TViewModel> _dataSource;
        private IList<TViewModel> _selectedRowModels;

        protected RadzenDataGrid<TViewModel> GridInternal;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprDataGridBase"/> class.
        /// </summary>
        public VyprDataGridBase(string instanceName) : base(instanceName)
        {
            DataLoading = false;
            _initialLoadComplete = false;
        }

        [Parameter]
        public IList<TViewModel> SelectedRowModels
        {
            get => _selectedRowModels;
            set
            {
                // Equality checks important to stop possible multiple API calls!
                // Only allow a single selection for now!
                // This is just a early protection mechanism, and will need removing
                // if such that multi-selection is needed.
                if (value is null || value.Count > 1 || _selectedRowModels == value)
                {
                    return;
                }

                _selectedRowModels = value;
                SelectedRowModelsChanged.InvokeAsync(value);
            }
        }

        /// <summary>
        /// Reloads the asynchronous.
        /// </summary>
        public async Task ReloadAsync(bool clearSelectedRows = false)
        {
            DataLoading = true;
            await LoadData.InvokeAsync(new LoadDataArgs { Take = PageSize });

            if (clearSelectedRows)
            {
                // Clear() doesn't fire off property change.
                // As it modifies the underlying member.
                SelectedRowModels = new List<TViewModel>();
            }

            DataLoading = false;
            await GridInternal.FirstPage(false);
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Updates the row asynchronous.
        /// This will also set the current selection back
        /// to what it was before the update. This will only work if 
        /// the selection mode is single.
        /// </summary>
        /// <param name="row">The row.</param>
        public void UpdateRow(TViewModel row)
        {
            if (SelectionMode.HasValue
                && SelectionMode.Value == VyprDataGridSelectionMode.Multiple)
            {
                return;
            }

            DataSource[_dataSource.FindIndex(x => x.Id == row.Id)] = row;
            SelectedRowModels = new[] { row };
        }

        /// <summary>
        /// Edits the row asynchronous.
        /// </summary>
        /// <param name="row">The row.</param>
        public async Task EditRowAsync(TViewModel row)
        {
            await GridInternal.EditRow(row);
        }

        /// <summary>
        /// Cancels the edit row.
        /// </summary>
        /// <param name="row">The row.</param>
        public void CancelEditRow(TViewModel row)
        {
            GridInternal.CancelEditRow(row);
        }

        /// <summary>
        /// Adds the row asynchronous.
        /// </summary>
        public async Task AddRowAsync()
        {
            await GridInternal.InsertRow(new TViewModel());
        }

        /// <summary>
        /// Selects the first available model.
        /// Will clear any selection made and bind to the first row in the data source.
        /// </summary>
        public void SelectRow(TViewModel row)
        {
            if (row != null)
            {
                SelectedRowModels = new[] { row };
            }
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// Override this method if you will perform an asynchronous operation and
        /// want the component to refresh when that operation is completed.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            DataLoading = true;

            await LoadData.InvokeAsync(new LoadDataArgs { Take = PageSize });

            _initialLoadComplete = true;

            DataLoading = false;
        }

        /// <summary>
        /// Called when [load data].
        /// </summary>
        /// <param name="args">The arguments.</param>
        protected async Task OnLoadDataAsync(Radzen.LoadDataArgs args)
        {
            await LoadData.InvokeAsync(
                new LoadDataArgs { Filter = args?.Filter, OrderBy = args?.OrderBy, Skip = args?.Skip, Take = args?.Top });
        }

        [Parameter]
        public List<TViewModel> DataSource
        {
            get => _dataSource;
            set => _dataSource = value;
        }

        public bool InitialLoadComplete => _initialLoadComplete;

        public string ComponentId => _componentId;

        public int PageSize { get; set; } = 20;

        public List<KeyValuePair<string, int>> PageValues { get; } = new List<KeyValuePair<string, int>>() 
        { 
            KeyValuePair.Create<string, int>("10", 10),  
            KeyValuePair.Create<string, int>("20", 20),  
            KeyValuePair.Create<string, int>("40", 40),  
            KeyValuePair.Create<string, int>("60", 60),  
            KeyValuePair.Create<string, int>("All", int.MaxValue) 
        };

        public TViewModel CreateSelectedRowModelCopy() => _selectedRowModels[0].Clone();

        [Parameter]
        public bool DataLoading { get; set; }

        [Parameter]
        public EventCallback<LoadDataArgs> LoadData { get; set; }

        [Parameter]
        public bool IsVisible { get; set; } = true;

        [Parameter]
        public int Count { get; set; }

        [Parameter]
        public string EmptyText { get; set; } = StandardText.NoRecordsToDisplay;

        [Parameter]
        public bool AllowSorting { get; set; } = true;

        [Parameter]
        public bool AllowFiltering { get; set; } = true;

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool AllowPaging { get; set; } = true;

        [Parameter]
        public string InLineStyle { get; set; }

        [Parameter]
        public EventCallback<IList<TViewModel>> SelectedRowModelsChanged { get; set; }

        [Parameter]
        public VyprDataGridSelectionMode? SelectionMode { get; set; }

        [Parameter]
        public EventCallback<VyprDataGridSelectionMode?> SelectionModeChanged { get; set; }
    }
}
