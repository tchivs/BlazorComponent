﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public partial class BDialog : BActivatable, IAsyncDisposable
    {
        private bool _valueChangedToTrue;
        private int _stackMinZIndex = 200;

        protected virtual string AttachSelector { get; set; }

        public ElementReference ContentRef { get; set; }

        public ElementReference DialogRef { get; set; }

        protected object Overlay { get; set; }

        protected ElementReference? OverlayRef => ((BOverlay)Overlay)?.Ref;

        public virtual bool ShowContent { get; set; }

        protected int ZIndex { get; set; }

        [Parameter]
        public string Attach { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Fullscreen { get; set; }

        [Parameter]
        public bool HideOverlay { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnOutsideClick { get; set; }

        [Parameter]
        public bool Persistent { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return CascadingIsDark;
            }
        }

        public override bool Value
        {
            get => base.Value;
            set
            {
                if (base.Value == false && value)
                    _valueChangedToTrue = true;

                base.Value = value;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            await ShowLazyContent();

            if (_valueChangedToTrue)
            {
                ZIndex = await ActiveZIndex(true);
                await Show();
                await InvokeStateHasChangedAsync();
                _valueChangedToTrue = false;
            }
        }

        protected bool ShowOverlay => !Fullscreen && !HideOverlay;

        private async Task Show()
        {
            // TODO: previousActiveElement
            // https://github.com/vuetifyjs/vuetify/blob/master/packages/vuetify/src/components/VDialog/VDialog.ts#L185

            var contains = await JsInvokeAsync<bool?>(JsInteropConstants.ContainsActiveElement, ContentRef);
            if (contains.HasValue && !contains.Value)
            {
                await JsInvokeAsync(JsInteropConstants.Focus, ContentRef);
            }
        }

        protected override async Task Close()
        {
            if (Persistent) return;

            await base.Close();
        }

        private async Task<int> ActiveZIndex(bool isActive)
        {
            return !isActive ? await JsInvokeAsync<int>(JsInteropConstants.GetZIndex, ContentRef) : await GetMaxZIndex() + 2;
        }

        private async Task<int> GetMaxZIndex()
        {
            var maxZindex = await JsInvokeAsync<int>(JsInteropConstants.GetMenuOrDialogMaxZIndex, new List<ElementReference> { ContentRef }, Ref);

            return maxZindex > _stackMinZIndex ? maxZindex : _stackMinZIndex;
        }

        protected virtual Task ShowLazyContent()
        {
            return Task.CompletedTask;
        }

        protected virtual async Task DeleteContent()
        {
            try
            {
                if (ContentRef.Context != null)
                {
                    await JsInvokeAsync(JsInteropConstants.DelElementFrom, ContentRef, AttachSelector);
                }

                if (OverlayRef?.Context != null)
                {
                    await JsInvokeAsync(JsInteropConstants.DelElementFrom, OverlayRef, AttachSelector);
                }
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        public async ValueTask DisposeAsync()
        {
            await DeleteContent();
        }
    }
}