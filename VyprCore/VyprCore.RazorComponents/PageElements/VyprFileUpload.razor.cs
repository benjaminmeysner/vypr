// <copyright file="VyprFileUpload.razor.cs" company="Vypr Systems">
// Copyright (c) Vypr Systems. All rights reserved.
// </copyright>

namespace VyprCore.RazorComponents.PageElements
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using Microsoft.JSInterop;
    using VyprCore.Models.Resources;
    using VyprCore.RazorComponents.Helpers;
    using VyprCore.Utilities.Helpers;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Single File upload.
    /// </summary>
    public partial class VyprFileUpload
    {
        private string _fileName;

        private string _default => PlaceHolder ?? StandardText.SelectFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="VyprFileUpload"/> class.
        /// </summary>
        public VyprFileUpload() : base(nameof(VyprFileUpload))
        {
        }

        [Parameter]
        public string Accept { get; set; }

        [Parameter]
        public string PlaceHolder { get; set; }

        [Parameter]
        public string Icon { get; set; } = "file_upload";

        [Parameter]
        public bool Multiple { get; set; } = false;

        [Parameter]
        public int MaxFileCount { get; set; } = 1;

        [Parameter]
        public bool ButtonOnly { get; set; } = false;

        [Parameter]
        public bool Required { get; set; } = false;

        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public EventCallback<InputFileChangeEventArgs> Change { get; set; }

        [Inject]
        public IJSRuntime JS { get; set; }

        public bool FilesSelected { get; private set; }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public async Task ResetAsync()
        {
            // clearInput is the name of the javascript function
            // {_componentId} is the id given to the InputFile element
            await JS.InvokeVoidAsync(VyprJSFunctions.ClearInput, _componentId);
            _fileName = _default;
            FilesSelected = false;

            await Change.InvokeAsync();
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            _fileName = _default;

            if (Multiple)
            {
                MaxFileCount = 4;
            }
        }

        private async Task OnFileChangeAsync(InputFileChangeEventArgs e)
        {
            if (e.FileCount > MaxFileCount)
            {
                // Just send the response back.
                await Change.InvokeAsync(e);
                return;
            }

            if (Multiple)
            {
                _fileName = StringHelpers.CommaSeperatedParameters(e.GetMultipleFiles(MaxFileCount).Select(x => x.Name).ToArray()) ?? PlaceHolder ?? StandardText.SelectFile;
            }
            else
            {
                _fileName = e.File.Name ?? _default;
            }

            FilesSelected = e.FileCount > 0;
            await Change.InvokeAsync(e);
        }
    }
}