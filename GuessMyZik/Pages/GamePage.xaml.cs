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
    public sealed partial class GamePage : Page
    {

        private Frame rootFrame;
        private Users connectedUser;
        private int? classType;
        private int? nbVisiteur;
        private APIConnect apiConnect = new APIConnect();
        private GameFrameSoloParameters gameFrameSoloParameters;
        private GameFrameMultiParameters gameFrameMultiParameters;

        private List<Track> beforeTrack = new List<Track>();
        private List<TextBlock> textGuessed = new List<TextBlock>();
        private int points = 0;

        public GamePage()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(((GameFrameParameters)e.Parameter).solo != null)
            {
                gameFrameSoloParameters = ((GameFrameParameters)e.Parameter).solo;
                rootFrame = gameFrameSoloParameters.rootFrame;
                connectedUser = gameFrameSoloParameters.connectedUser;
                classType = gameFrameSoloParameters.classTypeSelected;
                if (connectedUser != null)
                {
                    if (gameFrameSoloParameters.listTrack != null)
                    {
                        navigationGame.Navigate(typeof(TrackSoloGame), new GameFrame(gameFrameSoloParameters.rootFrame, navigationGame, connectedUser, gameFrameSoloParameters.listTrack[0], gameFrameSoloParameters.listTrack, beforeTrack, listViewTracksBefore, textGuessed, classType, points, null, gameFrameSoloParameters.party), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
                    } else
                    {
                        navigationGame.Navigate(typeof(TrackSoloGame), new GameFrame(gameFrameSoloParameters.rootFrame, navigationGame, connectedUser, gameFrameSoloParameters.party.listTrack[0], gameFrameSoloParameters.party.listTrack, beforeTrack, listViewTracksBefore, textGuessed, gameFrameSoloParameters.party.classType, points, null, gameFrameSoloParameters.party), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
                    }
                } else
                {
                    nbVisiteur = gameFrameSoloParameters.nbVisiteur;
                    navigationGame.Navigate(typeof(TrackSoloGame), new GameFrame(gameFrameSoloParameters.rootFrame, navigationGame, null, gameFrameSoloParameters.listTrack[0], gameFrameSoloParameters.listTrack, beforeTrack, listViewTracksBefore, textGuessed, classType, points, null, null), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
                }
            } else
            {
                gameFrameMultiParameters = ((GameFrameParameters)e.Parameter).multi;
                rootFrame = gameFrameMultiParameters.rootFrame;
                connectedUser = gameFrameMultiParameters.connectedUser;
                classType = gameFrameMultiParameters.classTypeSelected;
                if (connectedUser != null)
                {
                    navigationGame.Navigate(typeof(TrackMultiGame), new GameFrame(gameFrameMultiParameters.rootFrame, navigationGame, connectedUser, gameFrameMultiParameters.listTrack[0], gameFrameMultiParameters.listTrack, beforeTrack, listViewTracksBefore, textGuessed, classType, points, gameFrameMultiParameters.players, null), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
                } else
                {
                    nbVisiteur = gameFrameMultiParameters.nbVisiteur;
                    navigationGame.Navigate(typeof(TrackMultiGame), new GameFrame(gameFrameMultiParameters.rootFrame, navigationGame, null, gameFrameMultiParameters.listTrack[0], gameFrameMultiParameters.listTrack, beforeTrack, listViewTracksBefore, textGuessed, classType, points, gameFrameMultiParameters.players, null), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
                }
            }
            InitializeGamePage();
        }

        

        private  void InitializeGamePage()
        {  
            if (connectedUser != null)
            {
                gridUser.Visibility = Visibility.Visible;
                btnsUser.Visibility = Visibility.Visible;
                InitializeUser();
            }
            else
            {
                gridGuest.Visibility = Visibility.Visible;
                btnsGuest.Visibility = Visibility.Visible;
                InitializeGuest();
            }
            InitializeElementToFound();
        }

        private void InitializeElementToFound()
        {
            if(classType == 1)
            {
                stackTrack.Visibility = Visibility.Visible;
                textGuessed.Add(textTrackName);
                stackAlbum.Visibility = Visibility.Visible;
                textGuessed.Add(textAlbumName);
            } else if (classType == 2)
            {
                stackTrack.Visibility = Visibility.Visible;
                textGuessed.Add(textTrackName);
                stackArtist.Visibility = Visibility.Visible;
                textGuessed.Add(textArtistName);
            } else if (classType == 3)
            {
                stackArtist.Visibility = Visibility.Visible;
                textGuessed.Add(textArtistName);
                stackAlbum.Visibility = Visibility.Visible;
                textGuessed.Add(textAlbumName);
            } else
            {
                stackTrack.Visibility = Visibility.Visible;
                textGuessed.Add(textTrackName);
                stackArtist.Visibility = Visibility.Visible;
                textGuessed.Add(textArtistName);
                stackAlbum.Visibility = Visibility.Visible;
                textGuessed.Add(textAlbumName);
            }
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
        }

        private void InitializeGuest()
        {
            textNbVisiteur.Text = nbVisiteur.ToString();
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
            rootFrame.Navigate(typeof(SettingPage), new FrameParameters(rootFrame, null, connectedUser), new DrillInNavigationTransitionInfo());
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

            rootFrame.GoBack();
        }

        /// <summary>
        /// Called when you click on the grid element gridBtnDisconnectGuest.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to TappedRoutedEventArgs.</param>
        private void BtnDisconnectGuest_Tapped(object sender, TappedRoutedEventArgs e)
        {
            apiConnect.PostAsJson(nbVisiteur.ToString(), "http://localhost/api/auth/guest.php");
            rootFrame.GoBack();
        }

        private void ListViewTracksBefore_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            textEmptyFound.Visibility = Visibility.Collapsed;
        }

    }
}
