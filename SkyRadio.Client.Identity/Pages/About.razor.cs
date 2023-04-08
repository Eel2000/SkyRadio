using Microsoft.AspNetCore.Components;
using SkyRadio.Client.Identity.Services.Interfaces;

namespace SkyRadio.Client.Identity.Pages
{
    public partial class About : ComponentBase
    {
        [Parameter] public string Name { get; set; }
        [Inject] public ILoginService LoginService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }

        int Counter = 0;
        protected override async Task OnInitializedAsync()
        {
            await LoginService.LoginAsync(Name, "password");
            StateHasChanged();
        }

        protected override void OnParametersSet()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                Navigation.NavigateTo("/");
            }
        }

        void Count()
        {
            Counter++;
            StateHasChanged();
        }
    }
}
