using AAIC.API.CLient.Sage100.Mobile.App.ViewModel;


namespace AAIC.API.CLient.Sage100.Mobile.App.View;

public partial class PEMPage : ContentPage
{
	public PEMPage(PEMPageViewModel viewModel)
	{
		
        //InitializeComponent();
        BindingContext = viewModel;
    }
}