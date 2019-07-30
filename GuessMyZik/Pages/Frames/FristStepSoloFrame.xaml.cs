using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System.Threading.Tasks;
using GuessMyZik.Classes;
using Windows.UI.Xaml.Media.Animation;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.Security.Cryptography;
using Windows.UI.Popups;
using System.Diagnostics;
using Windows.Web.Http;
using System.Net.NetworkInformation;
using Windows.UI.Xaml.Shapes;
using Windows.UI;

namespace GuessMyZik.Pages.Frames
{
    /// <summary>
    /// An empty page can be used alone or as a landing page within a frame.
    /// </summary>
    public sealed partial class FristStepSoloFrame : Page
    {

        private FrameParameters frameParameters;

        private Frame rootFrame;
        private Frame gameFrame;

        private Users connectedUser;

        public FristStepSoloFrame()
        {
            this.InitializeComponent();
           
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            frameParameters = (FrameParameters)e.Parameter;
            rootFrame = frameParameters.rootFrame;
            gameFrame = frameParameters.secondFrame;
            connectedUser = frameParameters.connectedUser;
        }

        private void BtnMusic_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathMusic, (sender as Button), "WriteColor");
        }

        private void BtnMusic_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathMusic, (sender as Button), "ThirdColor");
        }

        private void BtnCategory_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathCategory, (sender as Button), "WriteColor");
        }

        private void BtnCategory_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathCategory, (sender as Button), "ThirdColor");
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

        private void BtnCategory_Click(object sender, RoutedEventArgs e)
        {
            gameFrame.Navigate(typeof(SecondStepSoloCategoryFrame), new FrameParameters(rootFrame, gameFrame, connectedUser));
        }

        private void BtnMusic_Click(object sender, RoutedEventArgs e)
        {
            gameFrame.Navigate(typeof(SecondStepSoloOtherFrame), new GameFrameParameters(rootFrame, gameFrame, connectedUser, 3), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void BtnAlbum_Click(object sender, RoutedEventArgs e)
        {
            gameFrame.Navigate(typeof(SecondStepSoloOtherFrame), new GameFrameParameters(rootFrame, gameFrame, connectedUser, 2), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void BtnArtist_Click(object sender, RoutedEventArgs e)
        {
            gameFrame.Navigate(typeof(SecondStepSoloOtherFrame), new GameFrameParameters(rootFrame, gameFrame, connectedUser, 1), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
    }
}
