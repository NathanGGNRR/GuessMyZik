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
using Microsoft.Toolkit.Uwp.UI;

namespace GuessMyZik.Pages.Frames.Steps
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ThirdStepSoloFrame : Page
    {
        #region Variables
        private GameFrameParameters gameFrameParameters;

        private Frame gameFrame;

        #endregion

        public ThirdStepSoloFrame()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            gameFrameParameters = (GameFrameParameters)e.Parameter;
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
            gameFrameParameters.game_duel = checkBoxChoosing.IsChecked;
            gameFrameParameters.number_tracks = Convert.ToInt16(sliderChoosing.Value);
            gameFrame.Navigate(typeof(FourStepSoloFrame), gameFrameParameters, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void BtnValidRandom_Click(object sender, RoutedEventArgs e)
        {
            gameFrameParameters.game_duel = checkBoxRandom.IsChecked;
            gameFrameParameters.number_tracks = Convert.ToInt16(sliderRandom.Value);

            //Fonction RANDOM

            //gameFrame.Navigate(typeof(PageGame), gameFrameParameters, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
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

        private void BtnValidStepOne_Click(object sender, RoutedEventArgs e)
        {
            gameFrameParameters.game_duel = checkBoxRandom.IsChecked;
            gameFrameParameters.number_tracks = Convert.ToInt16(sliderStepOne.Value);
            
            //Fonction RANDOM

            //gameFrame.Navigate(typeof(PageGame), gameFrameParameters, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
    }
}
