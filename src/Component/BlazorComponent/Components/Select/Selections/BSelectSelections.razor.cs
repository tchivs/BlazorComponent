﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSelectSelections<TItem, TItemValue, TValue, TInput> where TInput : ISelect<TItem, TItemValue, TValue>
    {
        protected bool HasChips => Component.HasChips;

        protected RenderFragment<SelectSelectionProps<TItem>> SelectionContent => Component.SelectionContent;

        protected IList<TItem> SelectedItems => Component.SelectedItems;
    }
}
