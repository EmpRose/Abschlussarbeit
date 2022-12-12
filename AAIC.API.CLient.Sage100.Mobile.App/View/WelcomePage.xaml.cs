using AAIC.API.CLient.Sage100.Mobile.App.ViewModel;

namespace AAIC.API.CLient.Sage100.Mobile.App.View;

public partial class WelcomePage : ContentPage
{
	public WelcomePage(WelcomePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}