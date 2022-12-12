using AAIC.API.CLient.Sage100.Mobile.App.ViewModel;

namespace AAIC.API.CLient.Sage100.Mobile.App.View;

public partial class DataSetSelectionPage : ContentPage
{
	public DataSetSelectionPage(DataSetSelectionPageViewModel viewModel)
	{
		
		InitializeComponent();
		BindingContext = viewModel;
	}
}