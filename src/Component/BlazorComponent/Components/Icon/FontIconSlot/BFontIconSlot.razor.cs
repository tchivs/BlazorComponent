﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace BlazorComponent
{
    public partial class BFontIconSlot<TIcon> : ComponentPartBase<TIcon> where TIcon : IIcon
    {
        public bool Disabled => Component.Disabled;

        public ElementReference Ref
        {
            get => Ref;
            set => Component.Ref = value;
        }

        public string Tag => Component.Tag;

        public string NewChildren => Component.NewChildren;

        public EventCallback<MouseEventArgs> OnClick => Component.OnClick;

        public IDictionary<string, object> Attrs => Component.Attrs;
    }
}