using Microsoft.AspNetCore.Components;

namespace conferencePlannerApp.Services.RoleAutherization
{
    public class RedirectToHomePage : ComponentBase
    {

        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            NavigationManager?.NavigateTo("/");
        }

    }
}
