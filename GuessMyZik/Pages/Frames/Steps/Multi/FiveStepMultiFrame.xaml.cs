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
using GuessMyZik.Pages.Frames.Steps;
using GuessMyZik.Pages.Frames.Choosing;
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

namespace GuessMyZik.Pages.Frames.Steps.Multi
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class FiveStepMultiFrame : Page
    {

        private List<Track> musicChoosen = new List<Track>();

        private GameFrameMultiParameters gameFrameParameters;

        public FiveStepMultiFrame()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            gameFrameParameters = (GameFrameMultiParameters)e.Parameter;
            ChoosingFrameParameters choosingParameters = new ChoosingFrameParameters();
            if (gameFrameParameters.classTypeSelected == 1)
            {
                textFound.Text = "Choose between 5 and 20 musics among all artists:";
                choosingParameters.artists = new ChoosingFrameParametersArtists(navigationChoosing, (gameFrameParameters.listSelected[0] as Artists).data, musicChoosen, textNumberMusics);
                navigationChoosing.Navigate(typeof(GridFrame), choosingParameters);
            } else
            {
                textFound.Text = "Choose between 5 and 20 musics among all albums:";
                choosingParameters.albums = new ChoosingFrameParametersAlbums(navigationChoosing, (gameFrameParameters.listSelected[0] as Albums), musicChoosen, textNumberMusics);
                navigationChoosing.Navigate(typeof(GridFrame), choosingParameters);
            }
            var message = new MessageDialog(gameFrameParameters.players[0].name);
            await message.ShowAsync();
        }

        private void BtnBack_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathBack, (sender as Button), "ExitColor");

        }

        private void BtnBack_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathBack, (sender as Button), "WriteColor");
        }

        private void BtnValid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathValid, (sender as Button), "LastColor");
        }

        private void BtnValid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathValid, (sender as Button), "WriteColor");
        }

        private void BtnValid_Click(object sender, RoutedEventArgs e)
        {
            gameFrameParameters.listTrack = musicChoosen;
            //gameFrame.Navigate(typeof(PageGame), gameFrameParameters, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                navigationChoosing.GoBack();
            }
            catch
            {
                gameFrameParameters.secondFrame.GoBack();
            }
        }

        private void TextNumberMusics_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (musicChoosen.Count >= 5)
            {
                if (musicChoosen.Count > gameFrameParameters.number_tracks)
                {
                    btnValid.IsEnabled = false;
                }
                else
                {
                    btnValid.IsEnabled = true;
                }
            }
            else
            {
                btnValid.IsEnabled = false;
            }
        }

        private void StockDatabase()
        {

        }
    }
}
