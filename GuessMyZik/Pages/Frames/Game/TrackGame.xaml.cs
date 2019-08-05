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

namespace GuessMyZik.Pages.Frames.Game
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class TrackGame : Page
    {

        private DispatcherTimer dispatcherTimer;
        private APIConnect apiConnect = new APIConnect();
        private GameFrame parameter;
        private string trackName;
        private string artistName;
        private string albumName;
        private int pointTrack = 0;
        private int error = 0;
        private List<string> elementGuessed = new List<string>();

        public TrackGame()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Disabled;

        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            parameter = (GameFrame)e.Parameter;
            trackName = parameter.trackGuessed.title_short.ToString().ToLower().Replace(" ", string.Empty);
            artistName = parameter.trackGuessed.artist.name.ToString().ToLower().Replace(" ", string.Empty);
            albumName = parameter.trackGuessed.album.title.ToString().ToLower().Replace(" ", string.Empty);
            textTrackNumber.Text = "Track n°" + (parameter.listTrack.IndexOf(parameter.trackGuessed) + 1);
            if (parameter.listTrack.IndexOf(parameter.trackGuessed) == 0)
            {
                ReadyContentDialog dialog = new ReadyContentDialog();
                await dialog.ShowAsync();
            }
            InitializeNewSound();
            InitializeNewTimer();
        }

        private void InitializeNewTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }


        private void InitializeNewSound()
        {
            trackSound.Source = new Uri(parameter.trackGuessed.preview);
            trackSound.Play();
        }

        private void BtnValidTry_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathSearch, (sender as Button), "ThirdColor");
        }

        private void BtnValidTry_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathSearch, (sender as Button), "WriteColor");
        }

        private void BtnValidTry_Click(object sender, RoutedEventArgs e)
        {
            clickTry();
        }

        private void textTry_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (textTry.Text != "")
            {
                if (e.Key == Windows.System.VirtualKey.Enter)
                {
                    clickTry();
                }
                else
                {
                    trackSound.Pause();
                    dispatcherTimer.Stop();

                }
            } else {
                trackSound.Play();
                dispatcherTimer.Start();
            }
        }
        
        private void CheckElementGuessed(string textTry, string element, string elementToFound, string textFound)
        {
            if (!elementGuessed.Contains(element))
            {
                if (textTry == elementToFound)
                {
                    for (int i = 0; i < parameter.textGuess.Count; i++)
                    {
                        if (parameter.textGuess[i].Tag.ToString() == element)
                        {
                            ChangeOnGamePage(element, textFound, i);
                            if (parameter.connectedUser != null)
                            {
                                StockElementGuessed(element);
                            }
                            break;
                        }
                    }

                }
            }
        }

        private async void StockElementGuessed(string element_found)
        {
            ElementFound elementFound = new ElementFound(parameter.connectedUser.username, parameter.trackGuessed.id, element_found, DateTime.Now.ToString());
            await apiConnect.PostAsJsonAsync(elementFound, "http://localhost/api/stockelementguessed.php");
        }

        private void clickTry()
        {
            if(textTry.Text != "")
            {
                string textTryGuess = textTry.Text.ToLower().Replace(" ", string.Empty);
                if (parameter.classType == 1)
                {
                    CheckElementGuessed(textTryGuess, "track", trackName, "Name of track: " + parameter.trackGuessed.title_short);
                    CheckElementGuessed(textTryGuess, "album", albumName, "Title of album: " + parameter.trackGuessed.album.title);
                    if (textTryGuess != trackName && textTryGuess != albumName)
                    {
                        ErrorGame();
                    }
                }
                else if (parameter.classType == 2)
                {
                    CheckElementGuessed(textTryGuess, "track", trackName, "Name of track: " + parameter.trackGuessed.title_short);
                    CheckElementGuessed(textTryGuess, "artist", artistName, "Name of artist: " + parameter.trackGuessed.artist.name); 
                    if (textTryGuess != trackName && textTryGuess != artistName)
                    {
                        ErrorGame();
                    }
                }
                else if (parameter.classType == 3)
                {
                    CheckElementGuessed(textTryGuess, "album", albumName, "Title of album: " + parameter.trackGuessed.album.title);
                    CheckElementGuessed(textTryGuess, "artist", artistName, "Name of artist: " + parameter.trackGuessed.artist.name);
                    if (textTryGuess != artistName && textTryGuess != albumName)
                    {
                        ErrorGame();
                    }
                }
                else
                {
                    CheckElementGuessed(textTryGuess, "track", trackName, "Name of track: " + parameter.trackGuessed.title_short);
                    CheckElementGuessed(textTryGuess, "artist", artistName, "Name of artist: " + parameter.trackGuessed.artist.name);
                    CheckElementGuessed(textTryGuess, "album", albumName, "Title of album: " + parameter.trackGuessed.album.title);
                    if (textTryGuess != trackName && textTryGuess != artistName && textTryGuess != albumName)
                    {
                        ErrorGame();
                    }
                }
            }
            textTry.Text = "";
            trackSound.Play();
            dispatcherTimer.Start();
            if(pointTrack == parameter.textGuess.Count)
            {
                nextTrack();
            }
        }


        private void ChangeOnGamePage(string element, string text, int i)
        {
            parameter.textGuess[i].Foreground = new SolidColorBrush((Color)Application.Current.Resources["LastColor"]);
            parameter.textGuess[i].Text = text;
            pointTrack += 1;
            elementGuessed.Add(element);
            validSound.Play();
        }

        private void ErrorGame()
        {
            errorSound.Play();
            if (error == 0)
            {
                error++;
                pathError1.Visibility = Visibility.Visible;
            } else if (error == 1)
            {
                error++;
                pathError2.Visibility = Visibility.Visible;
            } else if (error == 2)
            {
                error++;
                pathError3.Visibility = Visibility.Visible;
                nextTrack();
            }
        }

        private void dispatcherTimer_Tick(object sender, object e)
        {
            if (RadialProgressBarWaiting.Value + 1 < 31)
            {
                RadialProgressBarWaiting.Value = RadialProgressBarWaiting.Value + 1;
                textTimer.Text = (Convert.ToInt16(textTimer.Text) - 1).ToString();
            }
            else
            {
                nextTrack();
            }
        }

        private async void nextTrack()
        {
            trackSound.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            dispatcherTimer.Stop();
            parameter.points += pointTrack;
            parameter.beforeTrack.Add(parameter.trackGuessed);
            parameter.listBeforeTrack.Items.Add(parameter.trackGuessed);
            if (parameter.listTrack.IndexOf(parameter.trackGuessed) != parameter.listTrack.Count - 1)
            {
                //NextContentDialog dialog = new NextContentDialog();
                //await dialog.ShowAsync();
            }
            for (int i = 0; i < parameter.textGuess.Count; i++)
            {
                parameter.textGuess[i].Foreground = new SolidColorBrush((Color)Application.Current.Resources["ExitColor"]);
                if (parameter.textGuess[i].Tag.ToString() == "track")
                {
                    parameter.textGuess[i].Text = "Name of track: NOT YET FOUND";
                } else if (parameter.textGuess[i].Tag.ToString() == "artist")
                {
                    parameter.textGuess[i].Text = "Name of artist: NOT YET FOUND";
                } else if (parameter.textGuess[i].Tag.ToString() == "album")
                {
                    parameter.textGuess[i].Text = "Title of album: NOT YET FOUND";
                }
            }
            if (parameter.listTrack.IndexOf(parameter.trackGuessed) != parameter.listTrack.Count - 1)
            {
                parameter.trackGuessed = parameter.listTrack[parameter.listTrack.IndexOf(parameter.trackGuessed) + 1];
                if (parameter.connectedUser != null)
                {
                    parameter.gameFrame.Navigate(typeof(TrackGame), new GameFrame(parameter.rootFrame, parameter.gameFrame, parameter.connectedUser, parameter.trackGuessed, parameter.listTrack, parameter.beforeTrack, parameter.listBeforeTrack, parameter.textGuess, parameter.classType, parameter.points), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
                } else
                {
                    parameter.gameFrame.Navigate(typeof(TrackGame), new GameFrame(parameter.rootFrame, parameter.gameFrame, null, parameter.trackGuessed, parameter.listTrack, parameter.beforeTrack, parameter.listBeforeTrack, parameter.textGuess, parameter.classType, parameter.points), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });

                }
                parameter.gameFrame.BackStack.RemoveAt(parameter.gameFrame.BackStack.Count - 1);
            }
            else
            {
                if (parameter.connectedUser != null)
                {
                    EndUserContentDialog dialog = new EndUserContentDialog(parameter.connectedUser, parameter.points, parameter.rootFrame);
                    await dialog.ShowAsync();
                } else
                {
                    EndGuestContentDialog dialog = new EndGuestContentDialog(parameter.rootFrame);
                    await dialog.ShowAsync();
                }
            }
        }

        

        private void TextTry_Loaded(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Focus(FocusState.Keyboard);
        }
    }
}
