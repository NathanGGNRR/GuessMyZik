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
using System.Reflection;

namespace GuessMyZik.Pages.Frames.Steps.Multi
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class FourStepMultiFrame : Page
    {
        #region Variables
        private GameFrameMultiParameters gameFrameParameters;
        private RandomClasse randomClasse = new RandomClasse();
        private Frame gameFrame;
        private APIConnect apiConnect = new APIConnect();
        private List<string> keyAffectedRandom = new List<string>();
        private List<string> keyAffectedChoosing = new List<string>();
        private List<string> keyAffectedStepOne = new List<string>();


        #endregion

        public FourStepMultiFrame()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            gameFrameParameters = (GameFrameMultiParameters)e.Parameter;
            gameFrame = gameFrameParameters.secondFrame;
          
            if (gameFrameParameters.classTypeSelected != 4)
            {
                fromStepTwo.Visibility = Visibility.Visible;
            } else
            {
                fromStepOne.Visibility = Visibility.Visible;
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
            gameFrameParameters.game_duel = 0;
            gameFrameParameters.number_tracks = Convert.ToInt16(sliderChoosing.Value);
            List<Player> players = new List<Player>();
            foreach(StackPanel stackPanel in stackPlayerChoosing.Children)
            {

                string playerName = (stackPanel.Children[1] as TextBox).Text;
                if ((stackPanel.Children[1] as TextBox).Text == "")
                {
                    playerName = "Player " + (stackPlayerChoosing.Children.IndexOf(stackPanel) + 1).ToString();
                }
                players.Add(new Player(playerName, ((stackPanel.Children[2] as Button).Tag as KeyRoutedEventArgs).Key.ToString(),(stackPanel.Children[2] as Button).Tag as KeyRoutedEventArgs));
            }
            gameFrameParameters.players = players;
            gameFrame.Navigate(typeof(FiveStepMultiFrame), gameFrameParameters, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private async void BtnValidRandom_Click(object sender, RoutedEventArgs e)
        {
            progressMusics.IsActive = true;
            backgroundWaiting.Visibility = Visibility.Visible;
            gameFrameParameters.game_duel = 0;
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
            List<Player> players = new List<Player>();
            foreach (StackPanel stackPanel in stackPlayerRandom.Children)
            {

                string playerName = (stackPanel.Children[1] as TextBox).Text;
                if ((stackPanel.Children[1] as TextBox).Text == "")
                {
                    playerName = "Player " + (stackPlayerRandom.Children.IndexOf(stackPanel) + 1).ToString();
                }
                players.Add(new Player(playerName, ((stackPanel.Children[2] as Button).Tag as KeyRoutedEventArgs).Key.ToString(), (stackPanel.Children[2] as Button).Tag as KeyRoutedEventArgs));
            }
            gameFrameParameters.players = players;
            if (gameFrameParameters.connectedUser != null)
            {
                StockDatabase();
            }
            gameFrameParameters.rootFrame.Navigate(typeof(GamePage), new GameFrameParameters(null, gameFrameParameters), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
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
            gameFrameParameters.game_duel = 0;
            gameFrameParameters.number_tracks = Convert.ToInt16(sliderStepOne.Value);
            gameFrameParameters.listTrack = await randomClasse.RandomTracks(Convert.ToInt16(gameFrameParameters.number_tracks));
            progressMusics.IsActive = false;
            backgroundWaiting.Visibility = Visibility.Collapsed;
            List<Player> players = new List<Player>();
            foreach (StackPanel stackPanel in stackPlayerStepOne.Children)
            {
                string playerName = (stackPanel.Children[1] as TextBox).Text;
                if ((stackPanel.Children[1] as TextBox).Text == "")
                {
                    playerName = "Player " + (stackPlayerStepOne.Children.IndexOf(stackPanel) + 1).ToString();
                }
                players.Add(new Player(playerName, ((stackPanel.Children[2] as Button).Tag as KeyRoutedEventArgs).Key.ToString(), (stackPanel.Children[2] as Button).Tag as KeyRoutedEventArgs));
            }
            gameFrameParameters.players = players;
            if (gameFrameParameters.connectedUser != null)
            {
                StockDatabase();
            }
            gameFrameParameters.rootFrame.Navigate(typeof(GamePage), new GameFrameParameters(null, gameFrameParameters), new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        #region AddPlayerRandom
        private void BtnAddPlayerRandom_Click(object sender, RoutedEventArgs e)
        {
            if (stackPlayerRandom.Children.Count < 4) {
                StackPanel newStackPlayer = new StackPanel();
                newStackPlayer.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                newStackPlayer.Margin = new Thickness(0, 0, 15, 0);
                StackPanel newStackTextButton = new StackPanel();
                newStackTextButton.Width = 140;
                newStackTextButton.Orientation = Orientation.Horizontal;
                newStackTextButton.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
                TextBlock newTextBlockPlayer = new TextBlock();
                newTextBlockPlayer.Text = "Player " + (stackPlayerRandom.Children.Count + 1).ToString();
                newTextBlockPlayer.FontFamily = new FontFamily("Kaushan Script");
                newTextBlockPlayer.FontSize = 16;
                newTextBlockPlayer.Foreground = new SolidColorBrush((Color)Application.Current.Resources["SecondaryDarkColor"]);
                newStackTextButton.Children.Add(newTextBlockPlayer);
                Button newButtonLess = new Button();
                newButtonLess.Background = new SolidColorBrush((Color)Application.Current.Resources["ExitColor"]);
                newButtonLess.BorderBrush = new SolidColorBrush((Color)Application.Current.Resources["ExitDarkColor"]);
                newButtonLess.Foreground = new SolidColorBrush((Color)Application.Current.Resources["ExitColor"]);
                newButtonLess.FontFamily = new FontFamily("Kaushan Script");
                newButtonLess.Height = 7;
                newButtonLess.Margin = new Thickness(40, 0, 0, 0);
                newButtonLess.Width = 28;
                newButtonLess.Tag = stackPlayerRandom.Children.Count + 1;
                newButtonLess.BorderThickness = new Thickness(0);
                newButtonLess.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right;
                newButtonLess.Click += BtnRemovePlayerRandom_Click;
                newStackTextButton.Children.Add(newButtonLess);
                newStackPlayer.Children.Add(newStackTextButton);
                TextBox newTextBoxPlayer = new TextBox();
                newTextBoxPlayer.FontSize = 18;
                newTextBoxPlayer.Height = 35;
                newTextBoxPlayer.Width = 140;
                newTextBoxPlayer.FontFamily = new FontFamily("Trebuchet MS");
                newTextBoxPlayer.Style = (Style)App.Current.Resources["textBoxFocusThird"];
                newTextBoxPlayer.SelectionHighlightColor = new SolidColorBrush((Color)Application.Current.Resources["SecondaryDarkColor"]);
                newTextBoxPlayer.FontWeight = Windows.UI.Text.FontWeights.Bold;
                newTextBoxPlayer.Margin = new Thickness(0, 10, 0, 0);
                newTextBoxPlayer.PlaceholderText = "Name of player " + (stackPlayerRandom.Children.Count + 1).ToString();
                newTextBoxPlayer.PlaceholderForeground = new SolidColorBrush((Color)Application.Current.Resources["ThirdColor"]);
                newTextBoxPlayer.VerticalAlignment = VerticalAlignment.Center;
                newTextBoxPlayer.VerticalContentAlignment = VerticalAlignment.Center;
                newTextBoxPlayer.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                newStackPlayer.Children.Add(newTextBoxPlayer);
                Button newButtonPlayer = new Button();
                newButtonPlayer.Style = (Style)(App.Current.Resources["BtnSecondary"]);
                newButtonPlayer.Background =  new SolidColorBrush((Color)Application.Current.Resources["SecondaryColor"]);
                newButtonPlayer.BorderBrush = new SolidColorBrush((Color)Application.Current.Resources["SecondaryDarkColor"]);
                newButtonPlayer.Foreground = new SolidColorBrush((Color)Application.Current.Resources["SecondaryColor"]);
                newButtonPlayer.FontFamily = new FontFamily("Kaushan Script");
                newButtonPlayer.Margin = new Thickness(0, 10, 0, 0);
                newButtonPlayer.Content = "Click and affect key";
                newButtonPlayer.Height = 30;
                newButtonPlayer.FontSize = 15;
                newButtonPlayer.Width = 140;
                newButtonPlayer.Click += AffectKeyRandom_Click;
                newButtonPlayer.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                newStackPlayer.Children.Add(newButtonPlayer);
                stackPlayerRandom.Children.Add(newStackPlayer);
                checkAffectedKeyRandom();
                if (stackPlayerRandom.Children.Count == 4)
                {
                    btnAddPlayerRandom.IsEnabled = false;
                }
            }
        }

        private void BtnRemovePlayerRandom_Click(object sender, RoutedEventArgs e)
        {
            btnAddPlayerRandom.IsEnabled = true;
            Button button = (stackPlayerRandom.Children[Convert.ToInt16((sender as Button).Tag) - 1] as StackPanel).Children[2] as Button;
            if(button.Tag != null)
            {
                keyAffectedRandom.Remove((button.Tag as KeyRoutedEventArgs).Key.ToString());
            } 
            stackPlayerRandom.Children.RemoveAt(Convert.ToInt16((sender as Button).Tag) - 1);
            checkAffectedKeyRandom();
        }


        private void AffectKeyRandom_Click(object senderElement, RoutedEventArgs click)
        {
            (senderElement as Button).Content = "...";
            (senderElement as Button).KeyDown += BtnAffectKeyRandom_KeyDown;

        }

        private void BtnAffectKeyRandom_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (!keyAffectedRandom.Contains(e.Key.ToString()))
            {
                keyAffectedRandom.Add(e.Key.ToString());
                (sender as Button).Content = e.Key.ToString();
                (sender as Button).Tag = e;
                (sender as Button).KeyDown -= BtnAffectKeyRandom_KeyDown;
                checkAffectedKeyRandom();
            }

        }

        private void checkAffectedKeyRandom()
        {
            if (stackPlayerRandom.Children.Count == keyAffectedRandom.Count)
            {
                btnValidRandom.IsEnabled = true;
            }
            else
            {
                btnValidRandom.IsEnabled = false;
            }
        }
        #endregion

        #region AddPlayerChoosing
        private void BtnAddPlayerChoosing_Click(object sender, RoutedEventArgs e)
        {
            if (stackPlayerChoosing.Children.Count < 4)
            {
                StackPanel newStackPlayer = new StackPanel();
                newStackPlayer.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                newStackPlayer.Margin = new Thickness(0, 0, 15, 0);
                StackPanel newStackTextButton = new StackPanel();
                newStackTextButton.Width = 140;
                newStackTextButton.Orientation = Orientation.Horizontal;
                newStackTextButton.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
                TextBlock newTextBlockPlayer = new TextBlock();
                newTextBlockPlayer.Text = "Player " + (stackPlayerChoosing.Children.Count + 1).ToString();
                newTextBlockPlayer.FontFamily = new FontFamily("Kaushan Script");
                newTextBlockPlayer.FontSize = 16;
                newTextBlockPlayer.Foreground = new SolidColorBrush((Color)Application.Current.Resources["SecondaryDarkColor"]);
                newStackTextButton.Children.Add(newTextBlockPlayer);
                Button newButtonLess = new Button();
                newButtonLess.Background = new SolidColorBrush((Color)Application.Current.Resources["ExitColor"]);
                newButtonLess.BorderBrush = new SolidColorBrush((Color)Application.Current.Resources["ExitDarkColor"]);
                newButtonLess.Foreground = new SolidColorBrush((Color)Application.Current.Resources["ExitColor"]);
                newButtonLess.FontFamily = new FontFamily("Kaushan Script");
                newButtonLess.Height = 7;
                newButtonLess.Margin = new Thickness(40, 0, 0, 0);
                newButtonLess.Width = 28;
                newButtonLess.Tag = stackPlayerChoosing.Children.Count + 1;
                newButtonLess.BorderThickness = new Thickness(0);
                newButtonLess.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right;
                newButtonLess.Click += BtnRemovePlayerChoosing_Click;
                newStackTextButton.Children.Add(newButtonLess);
                newStackPlayer.Children.Add(newStackTextButton);
                TextBox newTextBoxPlayer = new TextBox();
                newTextBoxPlayer.FontSize = 18;
                newTextBoxPlayer.Height = 35;
                newTextBoxPlayer.Width = 140;
                newTextBoxPlayer.FontFamily = new FontFamily("Trebuchet MS");
                newTextBoxPlayer.Style = (Style)App.Current.Resources["textBoxFocusThird"];
                newTextBoxPlayer.SelectionHighlightColor = new SolidColorBrush((Color)Application.Current.Resources["SecondaryDarkColor"]);
                newTextBoxPlayer.FontWeight = Windows.UI.Text.FontWeights.Bold;
                newTextBoxPlayer.Margin = new Thickness(0, 10, 0, 0);
                newTextBoxPlayer.PlaceholderText = "Name of player " + (stackPlayerChoosing.Children.Count + 1).ToString();
                newTextBoxPlayer.PlaceholderForeground = new SolidColorBrush((Color)Application.Current.Resources["ThirdColor"]);
                newTextBoxPlayer.VerticalAlignment = VerticalAlignment.Center;
                newTextBoxPlayer.VerticalContentAlignment = VerticalAlignment.Center;
                newTextBoxPlayer.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                newStackPlayer.Children.Add(newTextBoxPlayer);
                Button newButtonPlayer = new Button();
                newButtonPlayer.Style = (Style)(App.Current.Resources["BtnSecondary"]);
                newButtonPlayer.Background = new SolidColorBrush((Color)Application.Current.Resources["SecondaryColor"]);
                newButtonPlayer.BorderBrush = new SolidColorBrush((Color)Application.Current.Resources["SecondaryDarkColor"]);
                newButtonPlayer.Foreground = new SolidColorBrush((Color)Application.Current.Resources["SecondaryColor"]);
                newButtonPlayer.FontFamily = new FontFamily("Kaushan Script");
                newButtonPlayer.Margin = new Thickness(0, 10, 0, 0);
                newButtonPlayer.Content = "Click and affect key";
                newButtonPlayer.Height = 30;
                newButtonPlayer.FontSize = 15;
                newButtonPlayer.Width = 140;
                newButtonPlayer.Click += AffectKeyChoosing_Click;
                newButtonPlayer.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                newStackPlayer.Children.Add(newButtonPlayer);
                stackPlayerChoosing.Children.Add(newStackPlayer);
                checkAffectedKeyChoosing();
                if (stackPlayerChoosing.Children.Count == 4)
                {
                    btnAddPlayerChoosing.IsEnabled = false;
                }
            }
        }

        private void BtnRemovePlayerChoosing_Click(object sender, RoutedEventArgs e)
        {
            btnAddPlayerChoosing.IsEnabled = true;
            Button button = (stackPlayerChoosing.Children[Convert.ToInt16((sender as Button).Tag) - 1] as StackPanel).Children[2] as Button;
            if (button.Tag != null)
            {
                keyAffectedChoosing.Remove((button.Tag as KeyRoutedEventArgs).Key.ToString());
            }
            stackPlayerChoosing.Children.RemoveAt(Convert.ToInt16((sender as Button).Tag) - 1);
            checkAffectedKeyChoosing();
        }


        private void AffectKeyChoosing_Click(object senderElement, RoutedEventArgs click)
        {
            (senderElement as Button).Content = "...";
            (senderElement as Button).KeyDown += BtnAffectKeyChoosing_KeyDown;

        }

        private void BtnAffectKeyChoosing_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (!keyAffectedChoosing.Contains(e.Key.ToString()))
            {
                keyAffectedChoosing.Add(e.Key.ToString());
                (sender as Button).Content = e.Key.ToString();
                (sender as Button).Tag = e;
                (sender as Button).KeyDown -= BtnAffectKeyChoosing_KeyDown;
                checkAffectedKeyChoosing();
            }

        }

        private void checkAffectedKeyChoosing()
        {
            if (stackPlayerChoosing.Children.Count == keyAffectedChoosing.Count)
            {
                btnValidChoosing.IsEnabled = true;
            }
            else
            {
                btnValidChoosing.IsEnabled = false;
            }
        }

        #endregion

        #region AddPlayerStepOne
        private void BtnAddPlayerStepOne_Click(object sender, RoutedEventArgs e)
        {
           
            if (stackPlayerStepOne.Children.Count < 4)
            {
                StackPanel newStackPlayer = new StackPanel();
                newStackPlayer.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                newStackPlayer.Margin = new Thickness(0, 0, 15, 0);
                newStackPlayer.Tag = "test" + stackPlayerStepOne.Children.Count + 1;
                StackPanel newStackTextButton = new StackPanel();
                newStackTextButton.Width = 140;
                newStackTextButton.Orientation = Orientation.Horizontal;
                newStackTextButton.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
                TextBlock newTextBlockPlayer = new TextBlock();
                newTextBlockPlayer.Text = "Player " + (stackPlayerStepOne.Children.Count + 1).ToString();
                newTextBlockPlayer.FontFamily = new FontFamily("Kaushan Script");
                newTextBlockPlayer.FontSize = 16;
                newTextBlockPlayer.Foreground = new SolidColorBrush((Color)Application.Current.Resources["SecondaryDarkColor"]);
                newStackTextButton.Children.Add(newTextBlockPlayer);
                Button newButtonLess = new Button();
                newButtonLess.Background = new SolidColorBrush((Color)Application.Current.Resources["ExitColor"]);
                newButtonLess.BorderBrush = new SolidColorBrush((Color)Application.Current.Resources["ExitDarkColor"]);
                newButtonLess.Foreground = new SolidColorBrush((Color)Application.Current.Resources["ExitColor"]);
                newButtonLess.FontFamily = new FontFamily("Kaushan Script");
                newButtonLess.Height = 7;
                newButtonLess.Margin = new Thickness(40, 0, 0, 0);
                newButtonLess.Width = 28;
                newButtonLess.Tag = stackPlayerStepOne.Children.Count + 1;
                newButtonLess.BorderThickness = new Thickness(0);
                newButtonLess.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Right;
                newButtonLess.Click += BtnRemovePlayerStepOne_Click;
                newStackTextButton.Children.Add(newButtonLess);
                newStackPlayer.Children.Add(newStackTextButton);
                TextBox newTextBoxPlayer = new TextBox();
                newTextBoxPlayer.FontSize = 18;
                newTextBoxPlayer.Height = 35;
                newTextBoxPlayer.Width = 140;
                newTextBoxPlayer.FontFamily = new FontFamily("Trebuchet MS");
                newTextBoxPlayer.Style = (Style)App.Current.Resources["textBoxFocusThird"];
                newTextBoxPlayer.SelectionHighlightColor = new SolidColorBrush((Color)Application.Current.Resources["SecondaryDarkColor"]);
                newTextBoxPlayer.FontWeight = Windows.UI.Text.FontWeights.Bold;
                newTextBoxPlayer.Margin = new Thickness(0, 10, 0, 0);
                newTextBoxPlayer.PlaceholderText = "Name of player " + (stackPlayerStepOne.Children.Count + 1).ToString();
                newTextBoxPlayer.PlaceholderForeground = new SolidColorBrush((Color)Application.Current.Resources["ThirdColor"]);
                newTextBoxPlayer.VerticalAlignment = VerticalAlignment.Center;
                newTextBoxPlayer.VerticalContentAlignment = VerticalAlignment.Center;
                newTextBoxPlayer.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                newStackPlayer.Children.Add(newTextBoxPlayer);
                Button newButtonPlayer = new Button();
                newButtonPlayer.Style = (Style)(App.Current.Resources["BtnSecondary"]);
                newButtonPlayer.Background = new SolidColorBrush((Color)Application.Current.Resources["SecondaryColor"]);
                newButtonPlayer.BorderBrush = new SolidColorBrush((Color)Application.Current.Resources["SecondaryDarkColor"]);
                newButtonPlayer.Foreground = new SolidColorBrush((Color)Application.Current.Resources["SecondaryColor"]);
                newButtonPlayer.FontFamily = new FontFamily("Kaushan Script");
                newButtonPlayer.Margin = new Thickness(0, 10, 0, 0);
                newButtonPlayer.Content = "Click and affect key";
                newButtonPlayer.Height = 30;
                newButtonPlayer.FontSize = 15;
                newButtonPlayer.Width = 140;
                newButtonPlayer.Click += AffectKeyStepOne_Click;
                newButtonPlayer.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                newStackPlayer.Children.Add(newButtonPlayer);
                stackPlayerStepOne.Children.Add(newStackPlayer);
                checkAffectedKeyStepOne();
                if (stackPlayerStepOne.Children.Count == 4)
                {
                    btnAddPlayerStepOne.IsEnabled = false;
                }
            }
        }

        private void BtnRemovePlayerStepOne_Click(object sender, RoutedEventArgs e)
        {
            btnAddPlayerStepOne.IsEnabled = true;
            Button button;
            try
            {
                button = (stackPlayerStepOne.Children[Convert.ToInt16((sender as Button).Tag) - 1] as StackPanel).Children[2] as Button;
            } catch
            {
                button = (stackPlayerStepOne.Children[2] as StackPanel).Children[2] as Button;
            }
            if (button.Tag != null)
            {
                keyAffectedStepOne.Remove((button.Tag as KeyRoutedEventArgs).Key.ToString());
            }
            try
            {
                stackPlayerStepOne.Children.RemoveAt(Convert.ToInt16((sender as Button).Tag) - 1);
            } catch
            {
                stackPlayerStepOne.Children.RemoveAt(2);
            }
            checkAffectedKeyStepOne();
        }


        private void AffectKeyStepOne_Click(object senderElement, RoutedEventArgs click)
        {
            (senderElement as Button).Content = "...";
            (senderElement as Button).KeyDown += BtnAffectKeyStepOne_KeyDown;

        }

        private void BtnAffectKeyStepOne_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (!keyAffectedStepOne.Contains(e.Key.ToString()))
            {
                keyAffectedStepOne.Add(e.Key.ToString());
                (sender as Button).Content = e.Key.ToString();
                (sender as Button).Tag = e;
                (sender as Button).KeyDown -= BtnAffectKeyStepOne_KeyDown;
                checkAffectedKeyStepOne();
            }

        }

        private void checkAffectedKeyStepOne()
        {
            if (stackPlayerStepOne.Children.Count == keyAffectedStepOne.Count)
            {
                btnValidStepOne.IsEnabled = true;
            }
            else
            {
                btnValidStepOne.IsEnabled = false;
            }
        }
        #endregion

        private async void StockDatabase()
        {
            Party partyStocked = new Party(gameFrameParameters.connectedUser.username, DateTime.Today.ToShortDateString(), gameFrameParameters.number_tracks, gameFrameParameters.game_duel, gameFrameParameters.listTrack);
            string response = await apiConnect.PostAsJsonAsync(partyStocked, "http://localhost/api/stockparty.php");
            gameFrameParameters.party_id = Convert.ToInt16(response);
        }
    }
}
