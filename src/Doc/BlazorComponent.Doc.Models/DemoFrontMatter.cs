﻿using YamlDotNet.Serialization;

namespace BlazorComponent.Doc.Models;

public class DemoFrontMatter
{
    [YamlMember(Alias = "category")]
    public string Category { get; set; }

    [YamlMember(Alias = "cover")]
    public string Cover { get; set; }

    [YamlMember(Alias = "cols")]
    public int Cols { get; set; }

    [YamlMember(Alias = "related")]
    public string[] Related { get; set; }

    [YamlMember(Alias = "subtitle")]
    public string Subtitle { get; set; }

    [YamlMember(Alias = "title")]
    public string Title { get; set; }

    [YamlMember(Alias = "type")]
    public string Type { get; set; }
}