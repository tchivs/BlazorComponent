﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataTableHeaderSelectAll<TDataTableHeader> where TDataTableHeader : IDataTableHeader
    {
        public RenderFragment DataTableSelectContent => Component.DataTableSelectContent;
    }
}
