﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ISwitch : IInput<bool>, ISelectable, IRippleable
    {
        bool IsLoading { get; }

        string LeftText { get; }

        string RightText { get; }
    }
}