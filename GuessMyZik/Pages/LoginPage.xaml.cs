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

namespace GuessMyZik.Pages
{
    /// <summary>
    /// An empty page can be used alone or as a landing page within a frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {

        private Frame rootFrame;
        private RegistrationFrame registrationFrame = new RegistrationFrame();
        

        public LoginPage()
        {
            this.InitializeComponent();
            loginPageOpacityStoryBoard.Begin(); //Start the storyboard called loginPageOpacityStoryBoard on LoginPage.xaml: animate opacity of the gridLoginPage from 0 to 1 in 1 second.
            navigationLogin.Navigate(typeof(WelcomeLoginFrame)); //Display the page WelcomeLoginFrame into the frame navigationLogin.
        }

        /// <summary>
        /// Called when you click on the grid element gridBtnExit.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to TappedRoutedEventArgs.</param>
        private void BtnExit_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CoreApplication.Exit(); //Close the application
        }

        /// <summary>
        /// Called when the mouse entered on the grid element gridBtnExit. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnExit_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            btnExitPointerEntered.Begin(); //Launch the storyboard called btnExitPointerEntered, animate color of the button BtnExit with the color resource ExitColor.
        }

        /// <summary>
        /// Called when the mouse exited the grid element gridBtnExit. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnExit_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            btnExitPointerExited.Begin(); //Launch the storyboard called btnExitPointerEntered, animate color of the button BtnExit with the color resource WriteColor.
        }

        /// <summary>
        /// Called when you click on the button btnRight.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to RoutedEventArgs.</param>
        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.ToString() == "Registration") //Check if the content of the button BtnRight is equal to Registration.
            {
                navigationLogin.Navigate(typeof(RegistrationFrame), rootFrame, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight }); //Display the page RegistrationFrame into the frame navigationLogin with an animation (Page come with slide from right).
                (sender as Button).Content = "Back"; //Change the content of the button btnRight to Back.
                btnLeft.IsEnabled = false; //Disabled the button btnLeft.
            }
            else
            {
                navigationLogin.GoBack(); //Back to the page WelcomeLoginFrame into the frame navigationLogin with an animation (Page come back).
                (sender as Button).Content = "Registration"; //Change the content of the button btnRight to Registration.
                btnLeft.IsEnabled = true; //Enabled the button btnLeft.
            }
        }

        /// <summary>
        /// Called when you click on the button btnLeft.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to RoutedEventArgs.</param>
        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.ToString() == "Sign In") //Check if the content of the button BtnLeft is equal to Sign In.
            {
                navigationLogin.Navigate(typeof(SignInFrame), rootFrame, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft }); //Display the page SignInFrame into the frame navigationLogin with an animation (Page come with slide from left).
                (sender as Button).Content = "Back"; //Change the content of the button btnLeft to Back.
                btnRight.IsEnabled = false;  //Disabled the button btnLeft.
            }
            else
            {
                navigationLogin.GoBack(); //Back to the page WelcomeLoginFrame into the frame navigationLogin with an animation (Page come back).
                (sender as Button).Content = "Sign In"; //Change the content of the button btnLeft to Sign In.
                btnRight.IsEnabled = true; //Enabled the button btnLeft.
            }
        }

        /// <summary>
        /// Called when you click on the button btnGuest.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to RoutedEventArgs.</param>
        private void BtnGuest_Click(object sender, RoutedEventArgs e)
        {
            rootFrame.Navigate(typeof(MainPage), null, new DrillInNavigationTransitionInfo()); //Close this page and open MainPage.
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            rootFrame = (Frame)e.Parameter;
        }

    }
}
 