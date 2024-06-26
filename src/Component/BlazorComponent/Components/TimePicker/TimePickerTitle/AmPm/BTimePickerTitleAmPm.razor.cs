﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BTimePickerTitleAmPm<TTimePickerTitle> where TTimePickerTitle : ITimePickerTitle
    {
        public bool AmPmReadonly => Component.AmPmReadonly;

        public TimePeriod Period=>Component.Period;

        public EventCallback<MouseEventArgs> OnAmClick=>CreateEventCallback<MouseEventArgs>(Component.HandleOnAmClickAsync);

        public EventCallback<MouseEventArgs> OnPmClick => CreateEventCallback<MouseEventArgs>(Component.HandleOnPmClickAsync);
    }
}
