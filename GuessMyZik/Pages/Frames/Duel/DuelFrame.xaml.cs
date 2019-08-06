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

namespace GuessMyZik.Pages.Frames.Duel
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class DuelFrame : Page
    {
        #region Variables
        private GameFrameMultiParameters gameFrameParameters;
        private APIConnect apiConnect = new APIConnect();

        #endregion

        public DuelFrame()
        {
            this.InitializeComponent();       
        }

        private async void InitializeDuelFrame()
        {
            string response = await apiConnect.PostAsJsonAsync(gameFrameParameters.connectedUser.username, "http://localhost/api/party/getparty.php");
            List<Party> parties = JsonConvert.DeserializeObject<List<Party>>(response);
            listViewTracks.ItemsSource = parties;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            gameFrameParameters = (GameFrameMultiParameters)e.Parameter;
            InitializeDuelFrame();
        }

        private void BtnBackDuel_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathBackDuel, (sender as Button), "ExitColor");
        }

        private void BtnBackDuel_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathBackDuel, (sender as Button), "WriteColor");
        }

        private void BtnBackDuel_Click(object sender, RoutedEventArgs e)
        {
            gameFrameParameters.secondFrame.GoBack();
        }



        private void BtnDuel_Click(object sender, RoutedEventArgs e)
        {
            Party party = (sender as Button).Tag as Party;
            gameFrameParameters.rootFrame.Navigate(typeof(GamePage), new GameFrameParameters(new GameFrameSoloParameters(gameFrameParameters.rootFrame, gameFrameParameters.secondFrame, gameFrameParameters.connectedUser, party.classType, null, party.game_duel, party.nb_tracks, party), null), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }
    }
}
