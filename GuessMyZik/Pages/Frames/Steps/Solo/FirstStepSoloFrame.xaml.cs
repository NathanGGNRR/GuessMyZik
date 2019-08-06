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
using GuessMyZik.Classes.FrameParameters;

namespace GuessMyZik.Pages.Frames.Steps.Solo
{
    /// <summary>
    /// An empty page can be used alone or as a landing page within a frame.
    /// </summary>
    public sealed partial class FirstStepSoloFrame : Page
    {

        private GameFrameSoloParameters gameFrameParameters;

        private Frame rootFrame;
        private Frame gameFrame;

        private Users connectedUser;

        public FirstStepSoloFrame()
        {
            this.InitializeComponent();
           
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            gameFrameParameters = (GameFrameSoloParameters)e.Parameter;
            rootFrame = gameFrameParameters.rootFrame;
            gameFrame = gameFrameParameters.secondFrame;
            connectedUser = gameFrameParameters.connectedUser;
        }

        private void BtnMusic_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathMusic, (sender as Button), "WriteColor");
        }

        private void BtnMusic_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathMusic, (sender as Button), "ThirdColor");
        }

        private void BtnRandom_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathRandom, (sender as Button), "WriteColor");
        }

        private void BtnRandom_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathRandom, (sender as Button), "ThirdColor");
        }

        private void BtnAlbum_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathAlbum, (sender as Button), "WriteColor");
        }

        private void BtnAlbum_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathAlbum, (sender as Button), "ThirdColor");
        }

        private void BtnArtist_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathArtist, (sender as Button), "WriteColor");
        }

        private void BtnArtist_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathArtist, (sender as Button), "ThirdColor");
        }

        private void BtnRandom_Click(object sender, RoutedEventArgs e)
        {
            gameFrameParameters.classTypeSelected = 4;
            gameFrame.Navigate(typeof(ThirdStepSoloFrame), gameFrameParameters, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void BtnMusic_Click(object sender, RoutedEventArgs e)
        {
            gameFrameParameters.classTypeSelected = 3;
            gameFrame.Navigate(typeof(SecondStepSoloFrame), gameFrameParameters, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void BtnAlbum_Click(object sender, RoutedEventArgs e)
        {
            gameFrameParameters.classTypeSelected = 2;
            gameFrame.Navigate(typeof(SecondStepSoloFrame), gameFrameParameters, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void BtnArtist_Click(object sender, RoutedEventArgs e)
        {
            gameFrameParameters.classTypeSelected = 1;
            gameFrame.Navigate(typeof(SecondStepSoloFrame), gameFrameParameters, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
    }
}
