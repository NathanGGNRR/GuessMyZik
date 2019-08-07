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
using GuessMyZik.Pages.DialogContent;
using GuessMyZik.Pages.Frames.Steps.Solo;
using GuessMyZik.Pages.Frames.Steps.Multi;
using GuessMyZik.Pages.Frames.Choosing;
using GuessMyZik.Pages.Frames.Game;
using Windows.ApplicationModel.Core;
using Windows.UI;
using GuessMyZik.Classes;
using GuessMyZik.Classes.ArtistClasses;
using GuessMyZik.Classes.AlbumClasses;
using GuessMyZik.Classes.TrackClasses;
using Windows.UI.Xaml.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Uwp.UI;
using GuessMyZik.Classes.FrameParameters;

namespace GuessMyZik.Pages
{
    /// <summary>
    /// An empty page can be used alone or as a landing page within a frame.
    /// </summary>
    public sealed partial class StatsPage : Page
    {

        private FrameParameters parameter;
        private Users connectedUser;
        private APIConnect apiConnect = new APIConnect();


        public StatsPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            parameter = (FrameParameters)e.Parameter;
            connectedUser = parameter.connectedUser;
            InitializeUser();
            InitializeStats();
        }

        private async void InitializeStats()
        {
            string response = await apiConnect.PostAsJsonAsync(connectedUser.username,"http://localhost/api/stats/getkdr.php");
            List<string> kdr = JsonConvert.DeserializeObject<List<string>>(response);
            textVictory.Text = kdr[0] + " victory(ies)";
            textDefeat.Text = kdr[1] + " defeat(s)";
            string responseTwo = await apiConnect.PostAsJsonAsync(connectedUser.username, "http://localhost/api/stats/bestelement.php");
            textCategory.Text = "Best element: " + responseTwo[0].ToString().ToUpper() + responseTwo.Substring(1);
            string responseThree = await apiConnect.PostAsJsonAsync(connectedUser.username, "http://localhost/api/party/getparty.php");
            var message = new MessageDialog(responseThree);
            await message.ShowAsync();
            List<Party> parties = JsonConvert.DeserializeObject<List<Party>>(responseThree);
            listViewParty.ItemsSource = parties;
            string responseFour = await apiConnect.PostAsJsonAsync(connectedUser.username, "http://localhost/api/stats/nbtrackguessed.php");
            textTrack.Text = responseFour + " track(s) guessed";
            string responseFive = await apiConnect.PostAsJsonAsync(connectedUser.username, "http://localhost/api/lastguessed.php");
            List<Track> tracksGuessed = JsonConvert.DeserializeObject<List<Track>>(responseFive);
            listViewTracks.ItemsSource = tracksGuessed;

        }

        private async void InitializeUser()
        {
            textWelcomeUser.Text = "Welcome " + connectedUser.username;
            personPicture.Initials = (connectedUser.username[0].ToString() + connectedUser.username[1].ToString()).ToUpper();
            string response = await apiConnect.GetAsJsonAsync("http://localhost/api/level/level.php");
            List<Levels> levels = JsonConvert.DeserializeObject<List<Levels>>(response);
            double amountActualLevel = Convert.ToDouble(levels[Convert.ToInt16(connectedUser.level_id) - 1].amount_xp);
            double amountNextLevel = 38400;
            if (connectedUser.level_id != "10")
            {
                amountNextLevel = Convert.ToDouble(levels[Convert.ToInt16(connectedUser.level_id)].amount_xp);
            }
            double calcul = Convert.ToDouble(((Convert.ToDouble(connectedUser.xp) - amountActualLevel) / (amountNextLevel - amountActualLevel)) * 100);
            double value = Math.Round(calcul);
            progressUser.Value = value;
            textLvl.Text = "Lvl " + connectedUser.level_id;
            string responseTwo = await apiConnect.PostAsJsonAsync(connectedUser.username, "http://localhost/api/stats/getkdr.php");
            List<string> kdr = JsonConvert.DeserializeObject<List<string>>(responseTwo);
            textVictories.Text = "Win(s): " + kdr[0];
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
        private void BtnHome_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            btnHomePointerEntered.Begin(); //Launch the storyboard called btnHomePointerEntered, animate color of the button BtnHome with the color resource SecondaryDarkColor.
        }

        /// <summary>
        /// Called when the mouse exited the grid element gridBtnStats. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnHome_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            btnHomePointerExited.Begin(); //Launch the storyboard called btnHomePointerEntered, animate color of the button BtnHome with the color resource SecondaryColor.
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
            parameter.rootFrame.Navigate(typeof(SettingPage), new FrameParameters(parameter.rootFrame, null, connectedUser), new DrillInNavigationTransitionInfo());
        }


        /// <summary>
        /// Called when you click on the grid element gridBtnStats.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to TappedRoutedEventArgs.</param>
        private void BtnHome_Tapped(object sender, TappedRoutedEventArgs e)
        {
            parameter.rootFrame.Navigate(typeof(MainPage), new FrameParameters(parameter.rootFrame, null, connectedUser), new DrillInNavigationTransitionInfo());
        }

        /// <summary>
        /// Called when you click on the grid element gridBtnDisconnectUser.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to TappedRoutedEventArgs.</param>
        private void BtnDisconnectUser_Tapped(object sender, TappedRoutedEventArgs e)
        {
            parameter.rootFrame.Navigate(typeof(LoginPage), parameter.rootFrame);
        }

    }
}
