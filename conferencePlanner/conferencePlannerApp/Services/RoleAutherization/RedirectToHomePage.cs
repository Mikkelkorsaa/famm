using Microsoft.AspNetCore.Components;

namespace conferencePlannerApp.Services.RoleAutherization
{
    public class RedirectToHomePage : ComponentBase
    {
        private NavigationManager NavigationManager { get; set; }

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
