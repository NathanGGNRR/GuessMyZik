using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GuessMyZik.Pages
{
    ///// <summary>
    ///// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    ///// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            logoOpacityStoryBoard.Begin(); //Start the storyboard called logoOpacityStoryBoard on HomePage.xaml: animate opacity of the logo from 0 to 1 in 2 seconds and then returns to 0.
        }

        /// <summary>
        /// Function started at the end of the animation of the logo. 
        /// </summary>
        private void AnimationHomePage(object sender, object e)
        {
            for (int i = 0; i < gridHomePage.ColumnDefinitions.Count; i+=2)
            {
                gridHomePage.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star); //Change the values of the first and the last ColumnDefinitions of the gridHomePage to 1. The basic value is 4.
            }
            gridWelcome.Visibility = Visibility.Visible; //Make the visibility of gridWelcome on Visible.
            homePageOpacityStoryBoard.Begin(); //Start the storyboard called homePageOpacityStoryBoard on HomePage.xaml: animate opacity of the gridWelcome from 0 to 1 in 2 seconds and the blink animation on "CLICK TO START".
        }

        /// <summary>
        /// Function started when the mouse entered on the platine picture. platine.wav is started.
        /// </summary>
        private void PlatineLogoHome_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            platineSound.Play(); //Play the platine.wav file in Assets/Music, platineSound is the name of the MediaElement on HomePage.xaml.
        }

        /// <summary>
        /// Function started at the click, the storyboard called loginPageOpacityStoryBoard on HomePage.xaml: animate opacity of the entire page from 1 to 0 in 1 second.
        /// </summary>
        private void HomePage_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            loginPageOpacityStoryBoard.Begin(); //Start the storyboard called loginPageOpacityStoryBoard on HomePage.xaml: animate opacity of the entire page from 1 to 0 in 1 second.
        }

        /// <summary>
        /// Function started at the end of the animation of the storyboard loginPageOpacityStoryBoard.
        /// </summary>
        private void AnimationLoginPage(object sender, object e)
        {
            this.Frame.Navigate(typeof(LoginPage)); //Close this page and open LoginPage.
        }
    }
}
