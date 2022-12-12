using AAIC.API.CLient.Sage100.Mobile.App.ViewModel;

namespace AAIC.API.CLient.Sage100.Mobile.App.View;

public partial class EntitlementSelectionPage : ContentPage
{
	public EntitlementSelectionPage(EntitlementSelectionPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		
		
	}

}

