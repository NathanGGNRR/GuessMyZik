using System;
using System.Collections.Generic;
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
using GuessMyZik.Classes;
using GuessMyZik.Classes.ArtistClasses;
using GuessMyZik.Classes.AlbumClasses;
using GuessMyZik.Classes.TrackClasses;
using GuessMyZik.Classes.CategoryClasses;
using Windows.UI.Xaml.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GuessMyZik.Pages
{
    /// <summary>
    /// An empty page can be used alone or as a landing page within a frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private Frame rootFrame;
        private Users connectedUser;

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            navigationSolo.Navigate(typeof(FristStepSoloFrame), new FrameParameters(rootFrame, navigationSolo, connectedUser));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            FrameParameters parameter = (FrameParameters)e.Parameter;
            rootFrame = parameter.rootFrame;
            connectedUser = parameter.connectedUser;
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
        /// Called when the mouse entered on the grid element gridBtnSettings. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnSettings_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            btnSettingsPointerEntered.Begin(); //Launch the storyboard called btnSettingsPointerEntered, animate color of the button BtnSettings with the color resource SecondaryDarkColor.
        }

        /// <summary>
        /// Called when the mouse exited the grid element gridBtnSettings. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnSettings_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            btnSettingsPointerExited.Begin(); //Launch the storyboard called btnSettingsPointerExited, animate color of the button BtnSettings with the color resource SecondaryColor.
        }


        /// <summary>
        /// Called when the mouse entered on the grid element gridBtnStats. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnStats_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            btnStatsPointerEntered.Begin(); //Launch the storyboard called btnStatsPointerEntered, animate color of the button BtnStats with the color resource SecondaryDarkColor.
        }

        /// <summary>
        /// Called when the mouse exited the grid element gridBtnStats. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnStats_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            btnStatsPointerExited.Begin(); //Launch the storyboard called btnStatsPointerExited, animate color of the button BtnStats with the color resource SecondaryColor.
        }

        /// <summary>
        /// Called when the mouse entered on the grid element gridBtnDisconnectGuest. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnDisconnectGuest_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            btnDisconnectGuestPointerEntered.Begin(); //Launch the storyboard called btnDisconnectGuestPointerEntered, animate color of the button BtnDisconnect with the color resource ExitColor.
        }

        /// <summary>
        /// Called when the mouse exited the grid element gridBtnDisconnectGuest. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnDisconnectGuest_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            btnDisconnectGuestPointerExited.Begin(); //Launch the storyboard called btnDisconnectGuestPointerExited, animate color of the button BtnDisconnect with the color resource SecondaryColor.
        }

        /// <summary>
        /// Called when the mouse entered on the grid element gridBtnDisconnectUser. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnDisconnectUser_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            btnDisconnectUserPointerEntered.Begin(); //Launch the storyboard called btnDisconnectUserPointerEntered, animate color of the button BtnDisconnect with the color resource ExitColor.
        }

        /// <summary>
        /// Called when the mouse exited the grid element gridBtnDisconnectUser. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnDisconnectUser_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            btnDisconnectUserPointerExited.Begin(); //Launch the storyboard called btnDisconnectUserPointerExited, animate color of the button BtnDisconnect with the color resource SecondaryColor.
        }


        /// <summary>
        /// Called when you click on the grid element gridBtnSettings.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to TappedRoutedEventArgs.</param>
        private void BtnSettings_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }


        /// <summary>
        /// Called when you click on the grid element gridBtnStats.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to TappedRoutedEventArgs.</param>
        private void BtnStats_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        /// <summary>
        /// Called when you click on the grid element gridBtnDisconnectUser.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to TappedRoutedEventArgs.</param>
        private void BtnDisconnectUser_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        /// <summary>
        /// Called when you click on the grid element gridBtnDisconnectGuest.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to TappedRoutedEventArgs.</param>
        private void BtnDisconnectGuest_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        

    }
}
