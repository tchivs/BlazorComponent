﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSnackbarWrapper<TSnackbar> where TSnackbar : ISnackbar
    {
        bool Value => Component.Value;
    }
}
