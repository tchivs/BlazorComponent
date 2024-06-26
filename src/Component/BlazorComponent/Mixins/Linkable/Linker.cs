﻿using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent;

public class Linker : ILinkable
{
    public bool Linkage { get; set; }
    
    public bool Exact { get; set; }
    
    public NavigationManager NavigationManager { get; set; }

    public Linker(ILinkable linkable)
    {
        Linkage = linkable.Linkage;
        Exact = linkable.Exact;
        NavigationManager = linkable.NavigationManager;
    }

    public bool MatchRoute(string href)
    {
        var relativePath = "/" + NavigationManager.ToBaseRelativePath(NavigationManager.Uri);

        if (Exact)
        {
            href += "$";
        }
        
        return Regex.Match(relativePath, href, RegexOptions.IgnoreCase).Success;
    }
}