﻿using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    [EventHandler("onmouseleave", typeof(MouseEventArgs), true, true)]
    [EventHandler("onmouseenter", typeof(MouseEventArgs), true, true)]
    [EventHandler("onexmousedown", typeof(ExMouseEventArgs), true, true)]
    [EventHandler("onexmousemove", typeof(ExMouseEventArgs), true, true)]
    [EventHandler("onexclick", typeof(ExMouseEventArgs), true, true)]
    [EventHandler("onexmouseleave", typeof(ExMouseEventArgs), true, true)]
    [EventHandler("onexmouseenter", typeof(ExMouseEventArgs), true, true)]
    public static class EventHandlers
    {
    }
}
