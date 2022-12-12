using AAIC.API.CLient.Sage100.Mobile.App.View;
using AAIC.API.CLient.Sage100.Mobile.App.ViewModel;
using System.Xaml;


namespace AAIC.API.CLient.Sage100.Mobile.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Eine Route besteht immer aus einem Pfad(String) und dem Typen der Klasse, die aufgerufen werden soll.
            Routing.RegisterRoute(nameof(EntitlementSelectionPage),typeof(EntitlementSelectionPage));

            Routing.RegisterRoute(nameof(DataSetSelectionPage),typeof(DataSetSelectionPage));

            Routing.RegisterRoute(nameof(MainMenuPage), typeof(MainMenuPage));

            Routing.RegisterRoute(nameof(ArticleListPage), typeof(ArticleListPage));

            Routing.RegisterRoute(nameof(PEMPage), typeof(PEMPage));
        }
    }
}