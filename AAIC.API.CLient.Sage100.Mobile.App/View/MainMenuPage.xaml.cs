using AAIC.API.CLient.Sage100.Mobile.App.ViewModel;

namespace AAIC.API.CLient.Sage100.Mobile.App.View;

public partial class MainMenuPage : ContentPage
{
    public MainMenuPage(MainMenuPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}