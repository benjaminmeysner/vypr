// <copyright file="VyprCoreFieldClassProvider.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.Helpers
{
    using Microsoft.AspNetCore.Components.Forms;
    using Microsoft.CSharp.RuntimeBinder;
    using System;
    using System.Linq;

    /// <summary>
    /// Vypr Field Class Provider. Provides custom CSS mapping logic to fields used
    /// within an VyprForm.
    /// </summary>
    public class VyprFieldClassProvider : FieldCssClassProvider
    {
        /// <summary>
        /// Gets a string that indicates the status of the specified field as a CSS class.
        /// </summary>
        /// <param name="editContext">The <see cref="T:Microsoft.AspNetCore.Components.Forms.EditContext" />.</param>
        /// <param name="fieldIdentifier">The <see cref="T:Microsoft.AspNetCore.Components.Forms.FieldIdentifier" />.</param>
        /// <returns>
        /// A CSS class name string.
        /// </returns>
        public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier)
        {
            if (editContext is null)
            {
                throw new ArgumentNullException(nameof(editContext));
            }

            try
            {
                // The field identifier here is the Vypr component field and not 
                // the property field. This is because we have have a component heirarchy (ie. Form->VyprText->Radzen).
                // We need to map to the correct property using the components value expression which is auto bound using @bind-Value.
                var modelObj = (dynamic)fieldIdentifier.Model;

                var childFieldIdentifier = (FieldIdentifier)modelObj.FieldIdentifier;

                if (!VyprComponentHelpers.ValidFieldIdentifier(childFieldIdentifier))
                {
                    return string.Empty;
                }

                bool isValid = !editContext.GetValidationMessages(childFieldIdentifier).Any();

                return isValid ? "vypr-valid-field" : "vypr-invalid-field";
            }
            // Possibly a case where we cannot find a field identifier for the component.
            catch (RuntimeBinderException)
            {
                return string.Empty;
            }
        }
    }
}
