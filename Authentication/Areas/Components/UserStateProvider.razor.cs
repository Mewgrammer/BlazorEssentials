﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BlazingComponents.Authentication.Helpers;
using BlazingComponents.Authentication.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazingComponents.Authentication.Areas.Components
{

    public class UserStateProviderBase : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        public UserState CurrentUser { get; private set; }

        public bool IsLoggedIn => CurrentUser?.IsLoggedIn ?? false;

        protected override async Task OnInitAsync()
        {
            var url = HttpClient.BaseAddress.AbsoluteUri + "user";
            try
            {
                var response = await HttpClient.GetJsonAsync<UserState>(url);
                CurrentUser = response;
            }
            catch(Exception ex)
            {
                CurrentUser = new UserState() { IsLoggedIn = false };
            }

        }

        public async Task SignIn(UserCredentials credentials)
        {
            var url = HttpClient.BaseAddress.AbsoluteUri + "login";
            var newUserState = await Authenticator.BasicAuth(url, credentials);
            CurrentUser = newUserState;
            StateHasChanged();
        }

        public async Task SignOut()
        {
            // Transition to "loading" state synchronously, then asynchronously update
            CurrentUser = null;
            StateHasChanged();
            var url = HttpClient.BaseAddress.AbsoluteUri + "logout";
            CurrentUser = await HttpClient.PutJsonAsync<UserState>(url, null);
            StateHasChanged();
        }

        public async Task Register(UserCredentials credentials)
        {
            var url = HttpClient.BaseAddress.AbsoluteUri + "register";
            CurrentUser = await HttpClient.PostJsonAsync<UserState>(url, credentials);
            StateHasChanged();
        }
    }
}
