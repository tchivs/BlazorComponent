﻿using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace BlazorComponent.Doc.Shared
{
    public partial class NavMenu : ComponentBase, IDisposable
    {
        protected override async Task OnInitializedAsync()
        {
            MenuItems = await DemoService.GetCurrentMenuItems();

            LanguageService.LanguageChanged += OnLanguageChanged;
            NavigationManager.LocationChanged += OnLocationChanged;

            StateHasChanged();

            await base.OnInitializedAsync();
        }

        private async void OnLanguageChanged(object sender, CultureInfo args)
        {
            MenuItems = await DemoService.GetCurrentMenuItems();
            await InvokeAsync(StateHasChanged);
        }

        private async void OnLocationChanged(object sender, LocationChangedEventArgs args)
        {
            MenuItems = await DemoService.GetCurrentMenuItems();
            StateHasChanged();
        }

        public void Dispose()
        {
            LanguageService.LanguageChanged -= OnLanguageChanged;
            NavigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}
