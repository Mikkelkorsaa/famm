using Microsoft.AspNetCore.Components;

namespace conferencePlannerApp.Services.RoleAutherization
{
    public class RedirectToHomePage : ComponentBase
    {
        private readonly NavigationManager NavigationManager;

        public RedirectToHomePage(NavigationManager navigationManager)
        {
            NavigationManager = navigationManager;
        }

        protected override void OnInitialized()
        {
            NavigationManager?.NavigateTo("/");
        }

    }
}
