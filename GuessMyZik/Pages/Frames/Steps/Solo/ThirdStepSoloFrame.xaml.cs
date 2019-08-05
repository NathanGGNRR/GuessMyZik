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
using Windows.UI.Xaml.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Uwp.UI;
using GuessMyZik.Classes.FrameParameters;

namespace GuessMyZik.Pages.Frames.Steps.Solo
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ThirdStepSoloFrame : Page
    {
        #region Variables
        private GameFrameSoloParameters gameFrameParameters;
        private RandomClasse randomClasse = new RandomClasse();
        private Frame gameFrame;
        private APIConnect apiConnect = new APIConnect();

        #endregion

        public ThirdStepSoloFrame()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            gameFrameParameters = (GameFrameSoloParameters)e.Parameter;
            gameFrame = gameFrameParameters.secondFrame;
          
            if (gameFrameParameters.classTypeSelected != 4)
            {
                fromStepTwo.Visibility = Visibility.Visible;
                if (gameFrameParameters.connectedUser != null)
                {
                    checkBoxRandom.Visibility = Visibility.Visible;
                    checkBoxChoosing.Visibility = Visibility.Visible;
                }
            } else
            {
                fromStepOne.Visibility = Visibility.Visible;
                if (gameFrameParameters.connectedUser != null)
                {
                    checkBoxStepOne.Visibility = Visibility.Visible;
                }
            }
        }

        private void BtnValidRandom_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathValidRandom, (sender as Button), "LastColor");
        }

        private void BtnValidRandom_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathValidRandom, (sender as Button), "WriteColor");
        }

        private void BtnValidChoosing_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathValidChoosing, (sender as Button), "LastColor");
        }

        private void BtnValidChoosing_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathValidChoosing, (sender as Button), "WriteColor");
        }

        private void BtnBackChoosing_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathBackChoosing, (sender as Button), "ExitColor");
        }

        private void BtnBackChoosing_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathBackChoosing, (sender as Button), "WriteColor");
        }

        private void BtnBackRandom_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathBackRandom, (sender as Button), "ExitColor");
        }

        private void BtnBackRandom_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathBackRandom, (sender as Button), "WriteColor");
        }

        private void BtnValidChoosing_Click(object sender, RoutedEventArgs e)
        {
            if(checkBoxChoosing.IsChecked == true)
            {
                gameFrameParameters.game_duel = 1;
            } else
            {
                gameFrameParameters.game_duel = 0;
            }
            gameFrameParameters.number_tracks = Convert.ToInt16(sliderChoosing.Value);
            gameFrame.Navigate(typeof(FourStepSoloFrame), new GameFrameParameters(gameFrameParameters, null), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private async void BtnValidRandom_Click(object sender, RoutedEventArgs e)
        {
            progressMusics.IsActive = true;
            backgroundWaiting.Visibility = Visibility.Visible;
            if (checkBoxRandom.IsChecked == true)
            {
                gameFrameParameters.game_duel = 1;
            } else
            {
                gameFrameParameters.game_duel = 0;
            }
            gameFrameParameters.number_tracks = Convert.ToInt16(sliderRandom.Value);
            if(gameFrameParameters.classTypeSelected == 1)
            {
                gameFrameParameters.listTrack = await randomClasse.AllTracksArtist(gameFrameParameters.listSelected[0] as Artists, Convert.ToInt16(gameFrameParameters.number_tracks));
            } else {
                gameFrameParameters.listTrack = await randomClasse.AllTracksAlbum(gameFrameParameters.listSelected[0] as Albums, Convert.ToInt16(gameFrameParameters.number_tracks));
            }
            progressMusics.IsActive = false;
            backgroundWaiting.Visibility = Visibility.Collapsed;
            if(gameFrameParameters.listTrack.Count != gameFrameParameters.number_tracks)
            {
                gameFrameParameters.number_tracks = gameFrameParameters.listTrack.Count;
            }
            if (gameFrameParameters.connectedUser != null)
            {
                StockDatabase();
            }
            gameFrameParameters.rootFrame.Navigate(typeof(GamePage), new GameFrameParameters(gameFrameParameters, null), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private async void StockDatabase()
        {
            Party partyStocked = new Party(gameFrameParameters.connectedUser.username, DateTime.Today.ToShortDateString(), gameFrameParameters.number_tracks, gameFrameParameters.game_duel, gameFrameParameters.listTrack);
            string response = await apiConnect.PostAsJsonAsync(partyStocked, "http://localhost/api/stockparty.php");
            gameFrameParameters.party_id = Convert.ToInt16(response);
        }

        private void BtnBackChoosing_Click(object sender, RoutedEventArgs e)
        {
            gameFrame.GoBack();
        }

        private void BtnBackRandom_Click(object sender, RoutedEventArgs e)
        {
            gameFrame.GoBack();
        }

        private void RandomExpander_Expanded(object sender, EventArgs e)
        {
            choosingExpander.IsExpanded = false;
        }

        private void ChoosingExpander_Expanded(object sender, EventArgs e)
        {
            randomExpander.IsExpanded = false;
        }

        private void BtnBackStepOne_Click(object sender, RoutedEventArgs e)
        {
            gameFrame.GoBack();
        }

        private void BtnBackStepOne_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathBackStepOne, (sender as Button), "ExitColor");

        }

        private void BtnBackStepOne_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathBackStepOne, (sender as Button), "WriteColor");
        }

        private void BtnValidStepOne_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathValidStepOne, (sender as Button), "LastColor");
        }

        private void BtnValidStepOne_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathValidStepOne, (sender as Button), "WriteColor");
        }

        private async void BtnValidStepOne_Click(object sender, RoutedEventArgs e)
        {
            progressMusics.IsActive = true;
            backgroundWaiting.Visibility = Visibility.Visible;
            if (checkBoxStepOne.IsChecked == true)
            {
                gameFrameParameters.game_duel = 1;
            } else
            {
                gameFrameParameters.game_duel = 0;
            }
            gameFrameParameters.number_tracks = Convert.ToInt16(sliderStepOne.Value);
            gameFrameParameters.listTrack = await randomClasse.RandomTracks(Convert.ToInt16(gameFrameParameters.number_tracks));
            progressMusics.IsActive = false;
            backgroundWaiting.Visibility = Visibility.Collapsed;
            if (gameFrameParameters.connectedUser != null)
            {
                StockDatabase();
            }
            gameFrameParameters.rootFrame.Navigate(typeof(GamePage), new GameFrameParameters(gameFrameParameters, null), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
    }
}
