﻿using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BPicker : BDomComponentBase
    {
        [Parameter]
        public RenderFragment TitleContent { get; set; }

        [Parameter]
        public RenderFragment ActionsContent { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

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
    }
}
