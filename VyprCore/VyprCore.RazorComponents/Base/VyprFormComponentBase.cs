// <copyright file="VyprCoreFormComponentBase" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Base
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using VyprCore.RazorComponents.Helpers;
    using VyprCore.Utilities.Helpers;

    /// <summary>
    /// Vypr Core Component base class.
    /// Logically bundles common functionality between Vypr Core Components.
    /// This class inherits <see cref="Microsoft.AspNetCore.Components.ComponentBase"/>.
    /// </summary>
    public abstract class VyprFormComponentBase<TValue> : VyprComponentBase
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        private TValue _value { set; get; } = default(TValue)!;

        /// <summary>
        /// The field identifier for this component.
        /// </summary>
        private FieldIdentifier _fieldIdentifier;

        /// <summary>
        /// The instance name.
        /// </summary>
        private readonly string _instanceName;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprFormComponentBase"/> class.
        /// </summary>
        public VyprFormComponentBase(string instanceName) : base(instanceName)
        {
            _instanceName = instanceName;
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// </summary>
        protected override void OnInitialized()
        {
            if (!(ValueExpression is null))
            {
                _fieldIdentifier = FieldIdentifier.Create(ValueExpression);

                // If we have not been explicitly provided a label, we can look it up 
                // via a displayattribute.
                Label = Label ?? ValueExpression.GetDisplayAttributeValue() ?? string.Empty;
                IsRequired = IsRequired ?? ValueExpression.GetRequiredAsterixValue();
            }

            if (!string.IsNullOrEmpty(Label) && ShowLabel)
            {
                LabelRenderTree = VyprComponentHelpers.CreateLabelRenderTree(Label, IsRequired ?? false);
            }
        }

        /// <summary>
        /// Gets or sets the value expression.
        /// </summary>
        /// <value>
        /// The value expression.
        /// </value>
        [Parameter]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public Expression<Func<TValue>>? ValueExpression { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        /// <summary>
        /// Gets or sets the value changed.
        /// </summary>
        /// <value>
        /// The value changed.
        /// </value>
        [Parameter]
        public EventCallback<TValue> ValueChanged { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [Parameter]
        public TValue Value
        {
            get => _value;
            set
            {
                // Check if we have made a change in the value input
                // If not, then just skip over as we don't need to update.
                if (VyprComponentHelpers.Compare(_value, value))
                {
                    return;
                }

                _value = value;
                ValueChanged.InvokeAsync(_value);

                // Notify bound property that the value has changed.
                if (ValueExpression != null)
                {
                    EditContext?.NotifyFieldChanged(_fieldIdentifier);
                }
            }
        }

        /// <summary>
        /// Gets or sets the field identifier.
        /// </summary>
        /// <value>
        /// The field identifier.
        /// </value>
        public FieldIdentifier FieldIdentifier => _fieldIdentifier;

        /// <summary>
        /// Gets or sets the cascading edit context.
        /// </summary>
        /// <value>
        /// The edit context.
        /// </value>
        [CascadingParameter]
        public EditContext EditContext { get; set; } = default!;

        /// <summary>
        /// Somes the base method.
        /// </summary>
        /// <returns></returns>
        public string ComponentId => _componentId;

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        [Parameter]
        public string Label { get; set; } = default;

        /// <summary>
        /// Gets or sets a value indicating whether [show title].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show title]; otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool ShowLabel { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="VyprTextBoxFor"/> is editable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if editable; otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool IsReadOnly { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is required; otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool? IsRequired { get; set; } = default!;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
        [Parameter]
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// The label render fragment.
        /// </summary>
        protected RenderFragment LabelRenderTree;
    }
}
