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


namespace GuessMyZik.Pages.Frames.Steps.Multi
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class FirstStepMultiFrame : Page
    {

        private GameFrameParameters gameFrameParameters;

        public FirstStepMultiFrame()
        {
            this.InitializeComponent();
        }
       

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            gameFrameParameters = (GameFrameParameters)e.Parameter;
            if(gameFrameParameters.connectedUser != null)
            {
                btnDuel.Visibility = Visibility.Visible;
            } else
            {
                Grid.SetColumnSpan(btnLocal, 3);
                btnLocal.FontSize = 50;
                btnDuel.Visibility = Visibility.Collapsed;
            }

        }

        private void BtnLocal_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathLocal, (sender as Button), "WriteColor");
        }

        private void BtnLocal_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathLocal, (sender as Button), "SecondaryColor");
        }

        private void BtnDuel_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathDuel, (sender as Button), "WriteColor");
        }

        private void BtnDuel_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathDuel, (sender as Button), "SecondaryColor");
        }

       

        private void BtnLocal_Click(object sender, RoutedEventArgs e)
        {
            //gameFrameParameters.rootFrame.Navigate(typeof(SecondStepMulti), gameFrameParameters, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void BtnDuel_Click(object sender, RoutedEventArgs e)
        {
            //gameFrameParameters.rootFrame.Navigate(typeof(Duelpage), gameFrameParameters, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

       
    }
}
