using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using GuessMyZik.Pages.Frames;
using Windows.ApplicationModel.Core;
using Windows.UI;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace GuessMyZik.Pages
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {

        private RegistrationFrame registrationFrame = new RegistrationFrame();

        public LoginPage()
        {
            this.InitializeComponent();
            loginPageOpacityStoryBoard.Begin(); //Start the storyboard called loginPageOpacityStoryBoard on LoginPage.xaml: animate opacity of the gridLoginPage from 0 to 1 in 1 second.
            navigationLogin.Navigate(typeof(SignInFrame)); //Display the page SignInFrame into the frame navigationLogin
        }

      
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit(); //Close the application
        }

        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.ToString() == "Registration") //Check if the content of the button BtnRight is equal to Registration
            {
                navigationLogin.Navigate(typeof(RegistrationFrame), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight }); //Display the page RegistrationFrame into the frame navigationLogin with an animation (Page come with slide from right)
                (sender as Button).Content = "Back";
                btnLeft.Content = "Confirm";
            }
            else
            {
                navigationLogin.GoBack();
                (sender as Button).Content = "Registration";
                btnLeft.Content = "Sign In";
            }
        }

        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.ToString() == "Sign In") //Check if the content of the button Btnleft is equal to Sign In
            {
                
            }
            else
            {
                
            }
        }
    }
}
 