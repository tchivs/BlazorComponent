﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent.Doc.Highlight
{
    public interface IPrismHighlighter
    {
        ValueTask<MarkupString> HighlightAsync(string code, string language);

        Task HighlightAllAsync();
    }
}