﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BInputIcon<TValue, TInput> : ComponentAbstractBase<TInput>
        where TInput : IInput<TValue>
    {
        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public string Type { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (Icon == null)
            {
                throw new ArgumentNullException(nameof(Icon));
            }

            if (Type == null)
            {
                throw new ArgumentNullException(nameof(Type));
            }
        }
    }
}
