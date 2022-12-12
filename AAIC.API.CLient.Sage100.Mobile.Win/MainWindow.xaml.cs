using AAIC.API.CLient.Sage100.Mobile.Lib.Model;
using AAIC.API.CLient.Sage100.Mobile.Lib.OpenId;
using AAIC.API.CLient.Sage100.Mobile.Lib.Extensions;
using System;
using System.Windows;


namespace AAIC.API.CLient.Sage100.Mobile.Win
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ISageIdClient sageIdClient;
        public MainWindow(ISageIdClient sageIdClient) // Konstruktor
        {
            InitializeComponent();
            this.sageIdClient = sageIdClient;
        }

        private async void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            SageIdRequestResult loginResult;                                                    // Objekt loginResult anhand der Klasse SageIdRequestResult 
            try
            {
                loginResult = await sageIdClient.LoginAsync();                                  // erstellter Client, versucht sich anzumelden
            }
            catch (Exception exception)
            {
                TextBlockMessage.Text = $"Error: {exception.Message}";                          // Wenn eine "Ausnahme" erhalten wird , diese im TextBlock ausgeben
                return;
            }
            if (loginResult.IsError)                                                            // Wenn der Versuch sich anzumelden schief läuft - Informiere über den TextBlock
            {
                TextBlockMessage.Text = loginResult.Error;
            }
            else
            {
                TextBlockMessage.Text = loginResult.Token.AccessToken.FromSecureString();       // Wenn Anmeldung erfolgreich, übergib entschlüsselten Token an den TextBlock
            }
        }

        private async void Button_Logout_Click (object sender, RoutedEventArgs e)
        {
            await sageIdClient.LogoutAsync();                                                   // warte bis zum Ende der Abmelde Methode
            TextBlockMessage.Text = "";                                                         // Gib nichts aus
                
        }
    }
}
