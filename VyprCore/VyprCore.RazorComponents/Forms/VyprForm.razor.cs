// <copyright file="VyprCoreForm.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Forms
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using VyprCore.RazorComponents.Helpers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Vypr Core Form.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.Forms.EditForm" />
    public partial class VyprForm<TModel>
    {
        private TModel _model;
        private bool _disableSubmit;
        private EditContext _editContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprForm"/> class.
        /// </summary>
        public VyprForm() : base(typeof(VyprForm<>).Name)
        {
            _disableSubmit = false;
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// </summary>
        protected override void OnInitialized()
        {
            _editContext = new EditContext(Model);
            _editContext.OnFieldChanged += OnFieldChanged;
            _editContext.SetFieldCssClassProvider(new VyprFieldClassProvider());

            base.OnInitialized();
        }

        /// <summary>
        /// Handles the valid submit.
        /// </summary>
        private async Task HandleValidSubmit()
        {
            await OnValidSubmit.InvokeAsync((TModel)_editContext.Model);
        }

        /// <summary>
        /// Called when [field changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="FieldChangedEventArgs"/> instance containing the event data.</param>
        private void OnFieldChanged(object sender, FieldChangedEventArgs e)
        {
            _disableSubmit = _editContext.Validate() ? false : true;
            StateHasChanged();
        }

        /// <summary>
        /// Specifies the top-level model object for the form. An edit context will
        /// be constructed for this model. If using this parameter, do not also supply
        /// a value for <see cref="P:Microsoft.AspNetCore.Components.Forms.EditForm.EditContext" />.
        /// </summary>
        [Parameter]
        public TModel Model
        {
            get => _model;
            set
            {
                if (!EqualityComparer<TModel>.Default.Equals(_model, value))
                {
                    _model = value;
                    _editContext = new EditContext(value);
                    _editContext.OnFieldChanged += OnFieldChanged;
                    _editContext.SetFieldCssClassProvider(new VyprFieldClassProvider());
                }
            }
        }

        [Parameter]
        public EventCallback<TModel> OnValidSubmit { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string SubmitText { get; set; } = "Submit";

        [Parameter]
        public bool ShowResetButton { get; set; } = true;

        [Parameter]
        public bool DisableSubmit
        {
            get => _disableSubmit;
            set => _disableSubmit = value;
        }

        [Parameter]
        public bool WaitingOnResponse { get; set; } = false;

        [Parameter]
        public EventCallback<bool> WaitingOnResponseChanged { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is finished.
        /// We can use this whether or not to hide the submit/reset buttons.
        /// The buttons 
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is finished; otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool ShowButtonsBindTo { get; set; } = false;
    }
}