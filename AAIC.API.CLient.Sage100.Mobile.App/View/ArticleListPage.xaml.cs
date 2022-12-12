using AAIC.API.CLient.Sage100.Mobile.App.ViewModel;

namespace AAIC.API.CLient.Sage100.Mobile.App.View;

public partial class ArticleListPage : ContentPage
{
	public ArticleListPage(ArticleListPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}