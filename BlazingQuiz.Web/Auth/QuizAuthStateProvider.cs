using BlazingQuiz.Shared.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace BlazingQuiz.Web.Auth
{
    public class QuizAuthStateProvider : AuthenticationStateProvider
    {
        private const string AuthType = "quiz-auth";
        private const string UserDateKey = "udata";
        private Task<AuthenticationState> _authenticationStateTask;
        private readonly IJSRuntime _jsRuntine;
        public QuizAuthStateProvider(IJSRuntime jSRuntime)
        {
            _jsRuntine = jSRuntime;
            SetAuthStateTask();
        }
        public override Task<AuthenticationState> GetAuthenticationStateAsync() => _authenticationStateTask;

        public LoggedUser User { get; private set; }
        public bool IsLogged => User?.Id >0;
        public async Task SetLoginAsync(LoggedUser loggedUser)
        {
            User = loggedUser;
            SetAuthStateTask();
            NotifyAuthenticationStateChanged(_authenticationStateTask);
            await _jsRuntine.InvokeVoidAsync("localStorage.setItem", UserDateKey, loggedUser.ToJson());
        }
        public async Task SetLogOutAsync()
        {
            User = null;
            SetAuthStateTask();
            NotifyAuthenticationStateChanged(_authenticationStateTask);
            await _jsRuntine.InvokeVoidAsync("localStorage.removeItem", UserDateKey);
        }

        public bool IsInitializated { get; private set; } = true;
        public async Task InitializeAsync()
        {
            try
            {
                var uData = await _jsRuntine.InvokeAsync<string?>("localStorage.getItem", UserDateKey);
                if (string.IsNullOrWhiteSpace(uData))
                {
                    //User data/state is not in the browser's storage
                    return;
                }

                var user = LoggedUser.LoadFromJson(uData);
                if (user == null || user.Id == 0)
                {
                    //User data/state is not valid
                    return;
                }
                await SetLoginAsync(user);
            }
            catch
            {
                //TODO:Fix this error
                //SetLoginAsync from this InitializeAsync method throws Collection eas notified - Enumeration has changed on the NotifyAuthenticationStateChanged in the setLoginasync method
            }
            finally
            {
                IsInitializated = false;
            }
        }
        private void SetAuthStateTask()
        {
            if (IsLogged)
            {
                var identity = new ClaimsIdentity(User.ToClaim(), AuthType);
                var principal = new ClaimsPrincipal(identity);
                var authState = new AuthenticationState(principal);

                _authenticationStateTask = Task.FromResult(authState);
            }
            else
            {
                var identity = new ClaimsIdentity();
                var principal = new ClaimsPrincipal(identity);
                var authState = new AuthenticationState(principal);
                _authenticationStateTask = Task.FromResult(authState);
            }
        }
      
    }
}
