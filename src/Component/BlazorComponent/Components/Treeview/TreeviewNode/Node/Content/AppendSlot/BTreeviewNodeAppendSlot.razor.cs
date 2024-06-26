﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTreeviewNodeAppendSlot<TItem, TKey, TTreeviewNode> where TTreeviewNode : ITreeviewNode<TItem, TKey>
    {
        public RenderFragment<TreeviewItem<TItem>> AppendContent => Component.AppendContent;

        public RenderFragment ComputedAppendContent => AppendContent?.Invoke(new TreeviewItem<TItem>()
        {
            Item = Component.Item,
            Leaf = Component.IsLeaf,
            Selected = Component.IsSelected,
            Indeterminate = Component.IsIndeterminate,
            Active = Component.IsActive,
            Open = Component.IsOpen
        });
    }
}