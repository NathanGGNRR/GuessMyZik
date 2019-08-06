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
    public sealed partial class ThirdStepMultiFrame : Page
    {
        #region Variables
        private GameFrameMultiParameters gameFrameParameters;

        private Frame gameFrame;
        private int? classType;
        private APIConnect apiConnect = new APIConnect();


        private int pageFound = 0;
        private int pageAdd = 0;
        #endregion

        public ThirdStepMultiFrame()
        {
            this.InitializeComponent();
            this.DataContext = this; 
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            gameFrameParameters = (GameFrameMultiParameters)e.Parameter;
            gameFrame = gameFrameParameters.secondFrame;
            classType = gameFrameParameters.classTypeSelected;
            InitializeUIControl();
        }

        private void InitializeUIControl()
        {
            if (classType == 1)
            {
                textFound.Text = "Artist(s) found: (can scroll name)";
                textAdd.Text = "Artist(s) add: (scrollable too)";
                DataTemplate dataTemplateFound = this.Resources["templateArtistFound"] as DataTemplate;
                listViewFound.ItemTemplate = dataTemplateFound;
                DataTemplate dataTemplateAdd = this.Resources["templateArtistAdd"] as DataTemplate;
                listViewAdd.ItemTemplate = dataTemplateAdd;
            }
            else if (classType == 2)
            {
                textFound.Text = "Album(s) found: (can scroll name)";
                textAdd.Text = "Album(s) add: (scrollable too)";
                DataTemplate dataTemplateFound = this.Resources["templateAlbumFound"] as DataTemplate;
                listViewFound.ItemTemplate = dataTemplateFound;
                DataTemplate dataTemplateAdd = this.Resources["templateAlbumAdd"] as DataTemplate;
                listViewAdd.ItemTemplate = dataTemplateAdd;
            }
            else
            {
                textFound.Text = "Track(s) found: (can scroll name)";
                textAdd.Text = "Track(s) add: (min 5 / max 20)";
                DataTemplate dataTemplateFound = this.Resources["templateTrackFound"] as DataTemplate;
                listViewFound.ItemTemplate = dataTemplateFound;
                DataTemplate dataTemplateAdd = this.Resources["templateTrackAdd"] as DataTemplate;
                listViewAdd.ItemTemplate = dataTemplateAdd;
            }
        }

        #region Global
      

        #region Event Search
        private void BtnSearch_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathSearch, (sender as Button), "SecondaryColor");
        }

        private void BtnSearch_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathSearch, (sender as Button), "WriteColor");
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            chooseClickSearch();
        }


        private void textSearch_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                chooseClickSearch();
            }
        }

        private void chooseClickSearch()
        {
            if (classType == 1)
            {
                clickOnSearch("https://api.deezer.com/search/artist?q=");
            }
            else if (classType == 2)
            {
                clickOnSearch("https://api.deezer.com/search/album?q=");
            }
            else
            {
                clickOnSearch("https://api.deezer.com/search/track?q=");
            }
        }
        #endregion

        #region Event Button
        private void BtnValid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathValid, (sender as Button), "LastColor");
        }

        private void BtnValid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathValid, (sender as Button), "WriteColor");
        }

        private void BtnBack_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathBack, (sender as Button), "ExitColor");
        }

        private void BtnBack_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathBack, (sender as Button), "WriteColor");
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            gameFrame.GoBack();
        }

        private async void BtnValid_Click(object sender, RoutedEventArgs e)
        {
            if(classType == 1)
            {
                gameFrameParameters.listSelected[0] = listArtistsAdd;
                gameFrame.Navigate(typeof(FourStepMultiFrame), gameFrameParameters, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            } else if (classType == 2)
            {
                gameFrameParameters.listSelected[0] = listAlbumsAdd;
                gameFrame.Navigate(typeof(FourStepMultiFrame), gameFrameParameters, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            } else
            {
                gameFrameParameters.game_duel = 0;
                gameFrameParameters.number_tracks = listTracksAdd.data.Count();
                gameFrameParameters.listTrack = listTracksAdd.data;
                if (gameFrameParameters.connectedUser != null)
                {
                    gameFrameParameters.party_id =  await StockDatabase();
                }
                gameFrame.Navigate(typeof(FourStepMultiFrame), gameFrameParameters, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            }
        }

        private async Task<int> StockDatabase()
        {
            Party partyStocked = new Party(gameFrameParameters.connectedUser.username, DateTime.Today.ToShortDateString(), gameFrameParameters.classTypeSelected, gameFrameParameters.number_tracks, gameFrameParameters.game_duel, gameFrameParameters.listTrack);
            string response = await apiConnect.PostAsJsonAsync(partyStocked, "http://localhost/api/party/stockparty.php");
            return Convert.ToInt16(response);
        }

        private void BtnBackFound_Click(object sender, RoutedEventArgs e)
        {
            chooseClassNext(false, true);
        }

        private void BtnNextFound_Click(object sender, RoutedEventArgs e)
        {
            chooseClassNext(true, true);
        }

        private void BtnBackAdd_Click(object sender, RoutedEventArgs e)
        {
            chooseClassNext(false, false);
        }

        private void BtnNextAdd_Click(object sender, RoutedEventArgs e)
        {
            chooseClassNext(true, false);
        }

        private void EnableValidButton()
        {
            btnValid.IsEnabled = true;
        }


        private void DisableValidButton()
        {
            btnValid.IsEnabled = false;
        }


        private void chooseClass()
        {
            if (classType == 1)
            {
                LoadArtistsFound();
            }
            else if (classType == 2)
            {
                LoadAlbumsFound();
            }
            else
            {
                LoadTracksFound();
            }
        }

        private void chooseClassNext(bool action, bool listViewFound)
        {
            if (classType == 1)
            {
                if (listViewFound)
                {
                    LoadArtistsFound(action);
                }
                else
                {
                    LoadArtistsAdd(action);
                }
            }
            else if (classType == 2)
            {
                if (listViewFound)
                {
                    LoadAlbumsFound(action);
                }
                else
                {
                    LoadAlbumsAdd(action);
                }
            }
            else
            {
                if (listViewFound)
                {
                    LoadTracksFound(action);
                }
                else
                {
                    LoadTracksAdd(action);
                }
            }
        }


        #endregion

        #region Event ListView
        private void ListViewFound_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (classType == 1)
            {
                listViewFoundClickArtist(e);
            }
            else if (classType == 2)
            {
                listViewFoundClickAlbum(e);
            }
            else
            {
                listViewFoundClickTrack(e);
            }
        }


        private void ListViewAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (classType == 1)
            {
                listViewAddClickArtist(e);
            }
            else if (classType == 2)
            {
                listViewAddClickAlbum(e);
            }
            else
            {
                listViewAddClickTrack(e);
            }
        }
        #endregion

        #region Animation
        private void ProgressAnimation(bool action, ProgressRing progressRing, TextBlock textEmpty)
        {
            if (action)
            {
                textEmpty.Visibility = Visibility.Collapsed;
                progressRing.Visibility = Visibility.Visible;
                progressRing.IsActive = true;
            }
            else
            {
                progressRing.Visibility = Visibility.Collapsed;
                progressRing.IsActive = false;
            }
        }

        private void ShowListView(bool action, Grid grid, TextBlock textBlock)
        {

            if (action)
            {
                textBlock.Visibility = Visibility.Collapsed;
                grid.Visibility = Visibility.Visible;
            }
            else
            {
                grid.Visibility = Visibility.Collapsed;
                textBlock.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Called when the animation of the ErrorFirstTextBoxColor storyboard is finished.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to the target object.</param>
        private void ErrorTextBoxColor_Completed(object sender, object e)
        {
            errorTextBoxStoryboard.Stop(); //Stop the storyboard
        }
        #endregion

        private async void clickOnSearch(string url)
        {
            if (textSearch.Text != "")
            {
                ProgressAnimation(true, progressFound, textEmptyFound);
                string response = await apiConnect.GetAsJsonAsync(url + textSearch.Text);
                if (classType == 1)
                {
                    listArtists = deserializeArtist(response); //Deserialize the response of the php files to class Artist.
                    ProgressAnimation(false, progressFound, textEmptyFound);
                    if (listArtists.data.Count != 0)
                    {
                        if(listArtistsAdd.data.Count != 0)
                        {
                            List<Artist> listArtistCleared = listArtists.data;
                            foreach(Artist artistListArtist in listArtists.data.ToList())
                            {
                                foreach (Artist artistListArtistAdd in listArtistsAdd.data)
                                {
                                    if (artistListArtist.id == artistListArtistAdd.id)
                                    {
                                        listArtistCleared.Remove(artistListArtist);
                                    }
                                }
                            }
                            listArtists.data = listArtistCleared;     
                        }
                        ShowListView(true, gridFound, textEmptyFound);
                        chooseClass();
                    }
                    else
                    {
                        ShowListView(false, gridFound, textEmptyFound);
                        textEmptyFound.Text = "NO RESULT FOUND";
                    }
                }
                else if (classType == 2)
                {
                    listAlbums = deserializeAlbum(response); //Deserialize the response of the php files to class Album.
                    ProgressAnimation(false, progressFound, textEmptyFound);
                    if (listAlbums.data.Count != 0)
                    {
                        if (listAlbumsAdd.data.Count != 0)
                        {
                            List<Album> listAlbumsCleared = listAlbums.data;
                            foreach (Album albumListAlbum in listAlbums.data.ToList())
                            {
                                foreach (Album albumListAlbumAdd in listAlbumsAdd.data)
                                {
                                    if (albumListAlbum.id == albumListAlbumAdd.id)
                                    {
                                        listAlbumsCleared.Remove(albumListAlbum);
                                    }
                                }
                            }
                            listAlbums.data = listAlbumsCleared;
                        }
                        ShowListView(true, gridFound, textEmptyFound);
                        chooseClass();
                    }
                    else
                    {
                        ShowListView(false, gridFound, textEmptyFound);
                        textEmptyFound.Text = "NO RESULT FOUND";
                    }
                }
                else
                {
                    listTracks = deserializeTrack(response); //Deserialize the response of the php files to class Track.
                    ProgressAnimation(false, progressFound, textEmptyFound);
                    if (listTracks.data.Count != 0)
                    {
                        if (listTracksAdd.data.Count != 0)
                        {
                            List<Track> listTracksCleared = listTracks.data;
                            foreach (Track trackListTrack in listTracks.data.ToList())
                            {
                                foreach (Track trackListTrackAdd in listTracksAdd.data)
                                {
                                    if (trackListTrack.id == trackListTrackAdd.id)
                                    {
                                        listTracksCleared.Remove(trackListTrack);
                                    }
                                }
                            }
                            listTracks.data = listTracksCleared;
                        }
                        ShowListView(true, gridFound, textEmptyFound);
                        chooseClass();
                    }
                    else
                    {
                        ShowListView(false, gridFound, textEmptyFound);
                        textEmptyFound.Text = "NO RESULT FOUND";
                    }
                }

            }
            else
            {
                errorTextBoxStoryboard.Begin();
            }
        }

        #endregion

        #region Artist

        #region Variables
        private Artists listArtists;
        private Artists listArtistsAdd = new Artists();
        private ObservableCollection<Artist> artistShowing;
        private ObservableCollection<Artist> artistAdd = new ObservableCollection<Artist>();
        #endregion

        #region LoadingFound
        public void LoadArtistsFound()
        {
            if (this.listArtists.data.Count > 5)
            {
                if ((pageFound * 5) + 5 > this.listArtists.data.Count)
                {
                    int count = this.listArtists.data.Count - (pageFound * 5);
                    artistShowing = new ObservableCollection<Artist>(this.listArtists.data.GetRange(pageFound * 5, count));
                    btnNextFound.IsEnabled = false;
                }
                else
                {
                    artistShowing = new ObservableCollection<Artist>(this.listArtists.data.GetRange(pageFound * 5, 5));
                    if ((pageFound * 5) + 5 == this.listArtists.data.Count)
                    {
                        btnNextFound.IsEnabled = false;
                    }
                    else
                    {
                        btnNextFound.IsEnabled = true;
                    }
                }
                listViewFound.ItemsSource = artistShowing;
            }
            else
            {
                btnNextFound.IsEnabled = false;
                artistShowing = new ObservableCollection<Artist>(this.listArtists.data.GetRange(pageFound * 5, this.listArtists.data.Count));
                listViewFound.ItemsSource = artistShowing;
            }
        }

        public void LoadArtistsFound(int count)
        {
            artistShowing = new ObservableCollection<Artist>(this.listArtists.data.GetRange(pageFound * 5, count));
            listViewFound.ItemsSource = artistShowing;
        }

        private void LoadArtistsFound(bool nextPage)
        {
            if (nextPage)
            {
                if (((pageFound + 1) * 5) + 5 >= this.listArtists.data.Count)
                {
                    int count = this.listArtists.data.Count - (pageFound * 5);
                    pageFound++;
                    LoadArtistsFound();
                    btnNextFound.IsEnabled = false;

                }
                else
                {
                    pageFound++;
                    LoadArtistsFound();
                    btnNextFound.IsEnabled = true;
                }
                btnBackFound.IsEnabled = true;
            }
            else
            {
                if (pageFound - 1 >= 0)
                {
                    btnNextFound.IsEnabled = true;
                    pageFound--;
                    if (pageFound == 0)
                    {
                        btnBackFound.IsEnabled = false;
                    }
                    LoadArtistsFound();
                }
                else
                {
                    btnBackFound.IsEnabled = false;
                }
            }
        }
        #endregion

        #region LoadingAdd
        public async void LoadArtistsAdd()
        {
            await Task.Delay(1);
            if (this.listArtistsAdd.data.Count > 5)
            {
                if ((pageAdd * 5) + 5 > this.listArtistsAdd.data.Count)
                {
                    int count = this.listArtistsAdd.data.Count - (pageAdd * 5);
                    artistAdd = new ObservableCollection<Artist>(this.listArtistsAdd.data.GetRange(pageAdd * 5, count));
                    btnNextAdd.IsEnabled = false;
                }
                else
                {
                    artistAdd = new ObservableCollection<Artist>(this.listArtistsAdd.data.GetRange(pageAdd * 5, 5));
                    if ((pageAdd * 5) + 5 == this.listArtistsAdd.data.Count)
                    {
                        btnNextAdd.IsEnabled = false;
                    }
                    else
                    {
                        btnNextAdd.IsEnabled = true;
                    }
                }
                listViewAdd.ItemsSource = artistAdd;
            }
            else
            {
                if (pageAdd == 1)
                {
                    pageAdd--;
                }
                btnNextAdd.IsEnabled = false;
                artistAdd = new ObservableCollection<Artist>(this.listArtistsAdd.data.GetRange(pageAdd * 5, this.listArtistsAdd.data.Count));
                listViewAdd.ItemsSource = artistAdd;
            }
        }

        public void LoadArtistsAdd(int count)
        {
            artistAdd = new ObservableCollection<Artist>(this.listArtistsAdd.data.GetRange(pageAdd * 5, count));
            listViewAdd.ItemsSource = artistAdd;
        }

        private void LoadArtistsAdd(bool nextPage)
        {
            if (nextPage)
            {
                if (((pageAdd + 1) * 5) + 5 >= this.listArtistsAdd.data.Count)
                {
                    int count = this.listArtistsAdd.data.Count - (pageAdd * 5);
                    pageAdd++;
                    LoadArtistsAdd();
                    btnNextAdd.IsEnabled = false;

                }
                else
                {
                    pageAdd++;
                    LoadArtistsAdd();
                    btnNextAdd.IsEnabled = true;
                }
                btnBackAdd.IsEnabled = true;
            }
            else
            {
                if (pageAdd - 1 >= 0)
                {
                    btnNextAdd.IsEnabled = true;
                    pageAdd--;
                    if (pageAdd == 0)
                    {
                        btnBackAdd.IsEnabled = false;
                    }
                    LoadArtistsAdd();
                }
                else
                {  
                    btnBackAdd.IsEnabled = false;
                }
            }
        }
        #endregion

        private Artists deserializeArtist(string response)
        {
            return this.listArtists = JsonConvert.DeserializeObject<Artists>(response); //Deserialize the response of the php files to a dictionary.
        }

        private void listViewFoundClickArtist(ItemClickEventArgs e)
        {
            artistAdd.Insert(0, e.ClickedItem as Artist);
            listArtistsAdd.data.Insert(0, e.ClickedItem as Artist);
            artistShowing.Remove(e.ClickedItem as Artist);
            listArtists.data.Remove(e.ClickedItem as Artist);
            LoadArtistsFound();
            LoadArtistsAdd();
            if (artistShowing.Count == 0 && listArtists.data.Count > 0)
            {
                if (pageFound > 0)
                {
                    LoadArtistsFound(false);
                }
            }
            if (listArtistsAdd.data.Count > 5)
            {
                btnNextAdd.IsEnabled = true;
            }
            else
            {
                btnBackAdd.IsEnabled = false;
                btnNextAdd.IsEnabled = false;
                if (listArtistsAdd.data.Count == 1)
                {
                    EnableValidButton();
                    ShowListView(true, gridAdd, textEmptyAdd);
                }
            }
        }

        private void listViewAddClickArtist(ItemClickEventArgs e)
        {
            artistShowing.Insert(0, e.ClickedItem as Artist);
            listArtists.data.Insert(0, e.ClickedItem as Artist);
            artistAdd.Remove(e.ClickedItem as Artist);
            listArtistsAdd.data.Remove(e.ClickedItem as Artist);
            LoadArtistsFound();
            LoadArtistsAdd();

            if (listArtistsAdd.data.Count > 5)
            {
                btnNextAdd.IsEnabled = true;
            }
            else
            {
                btnNextAdd.IsEnabled = false;
                btnBackAdd.IsEnabled = false;
                if (listArtistsAdd.data.Count == 0)
                {
                    DisableValidButton();
                    ShowListView(false, gridAdd, textEmptyAdd);
                }
            }
        }

        #endregion

        #region Album

        #region Variables
        private Albums listAlbums;
        private Albums listAlbumsAdd = new Albums();
        private ObservableCollection<Album> albumShowing;
        private ObservableCollection<Album> albumAdd = new ObservableCollection<Album>();
        #endregion

        #region LoadingFound
        public void LoadAlbumsFound()
        {
            if (this.listAlbums.data.Count > 5)
            {
                if ((pageFound * 5) + 5 > this.listAlbums.data.Count)
                {
                    int count = this.listAlbums.data.Count - (pageFound * 5);
                    albumShowing = new ObservableCollection<Album>(this.listAlbums.data.GetRange(pageFound * 5, count));
                    btnNextFound.IsEnabled = false;
                }
                else
                {
                    albumShowing = new ObservableCollection<Album>(this.listAlbums.data.GetRange(pageFound * 5, 5));
                    if ((pageFound * 5) + 5 == this.listAlbums.data.Count)
                    {
                        btnNextFound.IsEnabled = false;
                    }
                    else
                    {
                        btnNextFound.IsEnabled = true;
                    }
                }
                listViewFound.ItemsSource = albumShowing;
            }
            else
            {
                btnNextFound.IsEnabled = false;
                albumShowing = new ObservableCollection<Album>(this.listAlbums.data.GetRange(pageFound * 5, this.listAlbums.data.Count));
                listViewFound.ItemsSource = albumShowing;
            }
        }

        public void LoadAlbumsFound(int count)
        {
            albumShowing = new ObservableCollection<Album>(this.listAlbums.data.GetRange(pageFound * 5, count));
            listViewFound.ItemsSource = albumShowing;
        }

        private void LoadAlbumsFound(bool nextPage)
        {
            if (nextPage)
            {
                if (((pageFound + 1) * 5) + 5 >= this.listAlbums.data.Count)
                {
                    int count = this.listAlbums.data.Count - (pageFound * 5);
                    pageFound++;
                    LoadAlbumsFound();
                    btnNextFound.IsEnabled = false;

                }
                else
                {
                    pageFound++;
                    LoadAlbumsFound();
                    btnNextFound.IsEnabled = true;
                }
                btnBackFound.IsEnabled = true;
            }
            else
            {
                if (pageFound - 1 >= 0)
                {
                    btnNextFound.IsEnabled = true;
                    pageFound--;
                    if (pageFound == 0)
                    {
                        btnBackFound.IsEnabled = false;
                    }
                    LoadAlbumsFound();
                }
                else
                {
                    btnBackFound.IsEnabled = false;
                }
            }
        }
        #endregion

        #region LoadingAdd
        public async void LoadAlbumsAdd()
        {
            await Task.Delay(1);
            if (this.listAlbumsAdd.data.Count > 5)
            {
                if ((pageAdd * 5) + 5 > this.listAlbumsAdd.data.Count)
                {
                    int count = this.listAlbumsAdd.data.Count - (pageAdd * 5);
                    albumAdd = new ObservableCollection<Album>(this.listAlbumsAdd.data.GetRange(pageAdd * 5, count));
                    btnNextAdd.IsEnabled = false;
                }
                else
                {
                    albumAdd = new ObservableCollection<Album>(this.listAlbumsAdd.data.GetRange(pageAdd * 5, 5));
                    if ((pageAdd * 5) + 5 == this.listAlbumsAdd.data.Count)
                    {
                        btnNextAdd.IsEnabled = false;
                    }
                    else
                    {
                        btnNextAdd.IsEnabled = true;
                    }
                }
                listViewAdd.ItemsSource = albumAdd;
            }
            else
            {
                if (pageAdd == 1)
                {
                    pageAdd--;
                }
                btnNextAdd.IsEnabled = false;
                albumAdd = new ObservableCollection<Album>(this.listAlbumsAdd.data.GetRange(pageAdd * 5, this.listAlbumsAdd.data.Count));
                listViewAdd.ItemsSource = albumAdd;
            }
        }

        public void LoadAlbumsAdd(int count)
        {
            albumAdd = new ObservableCollection<Album>(this.listAlbumsAdd.data.GetRange(pageAdd * 5, count));
            listViewAdd.ItemsSource = albumAdd;
        }

        private void LoadAlbumsAdd(bool nextPage)
        {
            if (nextPage)
            {
                if (((pageAdd + 1) * 5) + 5 >= this.listAlbumsAdd.data.Count)
                {
                    int count = this.listAlbumsAdd.data.Count - (pageAdd * 5);
                    pageAdd++;
                    LoadAlbumsAdd();
                    btnNextAdd.IsEnabled = false;

                }
                else
                {
                    pageAdd++;
                    LoadAlbumsAdd();
                    btnNextAdd.IsEnabled = true;
                }
                btnBackAdd.IsEnabled = true;
            }
            else
            {
                if (pageAdd - 1 >= 0)
                {
                    btnNextAdd.IsEnabled = true;
                    pageAdd--;
                    if (pageAdd == 0)
                    {
                        btnBackAdd.IsEnabled = false;
                    }
                    LoadAlbumsAdd();
                }
                else
                {
                    btnBackAdd.IsEnabled = false;
                }
            }
        }
        #endregion

        private Albums deserializeAlbum(string response)
        {
            return this.listAlbums = JsonConvert.DeserializeObject<Albums>(response); //Deserialize the response of the php files to a dictionary.
        }

        private void listViewFoundClickAlbum(ItemClickEventArgs e)
        {
            
            albumAdd.Insert(0, e.ClickedItem as Album);
            listAlbumsAdd.data.Insert(0, e.ClickedItem as Album);
            albumShowing.Remove(e.ClickedItem as Album);
            listAlbums.data.Remove(e.ClickedItem as Album);
            LoadAlbumsFound();
            LoadAlbumsAdd(); 
            if (albumShowing.Count == 0 && listAlbums.data.Count > 0)
            {
                if (pageFound > 0)
                {
                    LoadAlbumsFound(false);
                }
            }
            if (listAlbumsAdd.data.Count > 5)
            {

                btnNextAdd.IsEnabled = true;
            }
            else
            {
                btnBackAdd.IsEnabled = false;
                btnNextAdd.IsEnabled = false;
                if (listAlbumsAdd.data.Count == 1)
                {
                    EnableValidButton();
                    ShowListView(true, gridAdd, textEmptyAdd);
                }
            }
        }

        private void listViewAddClickAlbum(ItemClickEventArgs e)
        {
            albumShowing.Insert(0, e.ClickedItem as Album);
            listAlbums.data.Insert(0, e.ClickedItem as Album);
            albumAdd.Remove(e.ClickedItem as Album);
            listAlbumsAdd.data.Remove(e.ClickedItem as Album);
            LoadAlbumsFound();
            LoadAlbumsAdd();

            if (listAlbumsAdd.data.Count > 5)
            {
                btnNextAdd.IsEnabled = true;
            }
            else
            {
                btnNextAdd.IsEnabled = false;
                btnBackAdd.IsEnabled = false;
                if (listAlbumsAdd.data.Count == 0)
                {
                    DisableValidButton();
                    ShowListView(false, gridAdd, textEmptyAdd);
                }
            }
        }

        #endregion

        #region Track

        #region Variables
        private Tracks listTracks;
        private Tracks listTracksAdd = new Tracks();
        private ObservableCollection<Track> trackShowing;
        private ObservableCollection<Track> trackAdd = new ObservableCollection<Track>();
        #endregion

        #region LoadingFound
        public void LoadTracksFound()
        {
            if (this.listTracks.data.Count > 5)
            {
                if ((pageFound * 5) + 5 > this.listTracks.data.Count)
                {
                    int count = this.listTracks.data.Count - (pageFound * 5);
                    trackShowing = new ObservableCollection<Track>(this.listTracks.data.GetRange(pageFound * 5, count));
                    btnNextFound.IsEnabled = false;
                }
                else
                {
                    trackShowing = new ObservableCollection<Track>(this.listTracks.data.GetRange(pageFound * 5, 5));
                    if ((pageFound * 5) + 5 == this.listTracks.data.Count)
                    {
                        btnNextFound.IsEnabled = false;
                    }
                    else
                    {
                        btnNextFound.IsEnabled = true;
                    }
                }
                listViewFound.ItemsSource = trackShowing;
            }
            else
            {
                btnNextFound.IsEnabled = false;
                trackShowing = new ObservableCollection<Track>(this.listTracks.data.GetRange(pageFound * 5, this.listTracks.data.Count));
                listViewFound.ItemsSource = trackShowing;
            }
        }

        public void LoadTracksFound(int count)
        {
            trackShowing = new ObservableCollection<Track>(this.listTracks.data.GetRange(pageFound * 5, count));
            listViewFound.ItemsSource = trackShowing;
        }

        private void LoadTracksFound(bool nextPage)
        {
            if (nextPage)
            {
                if (((pageFound + 1) * 5) + 5 >= this.listTracks.data.Count)
                {
                    int count = this.listTracks.data.Count - (pageFound * 5);
                    pageFound++;
                    LoadTracksFound();
                    btnNextFound.IsEnabled = false;

                }
                else
                {
                    pageFound++;
                    LoadTracksFound();
                    btnNextFound.IsEnabled = true;
                }
                btnBackFound.IsEnabled = true;
            }
            else
            {
                if (pageFound - 1 >= 0)
                {
                    btnNextFound.IsEnabled = true;
                    pageFound--;
                    if (pageFound == 0)
                    {
                        btnBackFound.IsEnabled = false;
                    }
                    LoadTracksFound();
                }
                else
                {
                    btnBackFound.IsEnabled = false;
                }
            }
        }
        #endregion

        #region LoadingAdd
        public async void LoadTracksAdd()
        {
            await Task.Delay(1);
            if (this.listTracksAdd.data.Count > 5)
            {
                if ((pageAdd * 5) + 5 > this.listTracksAdd.data.Count)
                {
                    int count = this.listTracksAdd.data.Count - (pageAdd * 5);
                    trackAdd = new ObservableCollection<Track>(this.listTracksAdd.data.GetRange(pageAdd * 5, count));
                    btnNextAdd.IsEnabled = false;
                }
                else
                {
                    trackAdd = new ObservableCollection<Track>(this.listTracksAdd.data.GetRange(pageAdd * 5, 5));
                    if ((pageAdd * 5) + 5 == this.listTracksAdd.data.Count)
                    {
                        btnNextAdd.IsEnabled = false;
                    }
                    else
                    {
                        btnNextAdd.IsEnabled = true;
                    }
                }
                listViewAdd.ItemsSource = trackAdd;
            }
            else
            {
                if (pageAdd == 1)
                {
                    pageAdd--;
                }
                btnNextAdd.IsEnabled = false;
                trackAdd = new ObservableCollection<Track>(this.listTracksAdd.data.GetRange(pageAdd * 5, this.listTracksAdd.data.Count));
                listViewAdd.ItemsSource = trackAdd;
            }
        }

        public void LoadTracksAdd(int count)
        {
            trackAdd = new ObservableCollection<Track>(this.listTracksAdd.data.GetRange(pageAdd * 5, count));
            listViewAdd.ItemsSource = trackAdd;
        }

        private void LoadTracksAdd(bool nextPage)
        {
            if (nextPage)
            {
                if (((pageAdd + 1) * 5) + 5 >= this.listTracksAdd.data.Count)
                {
                    int count = this.listTracksAdd.data.Count - (pageAdd * 5);
                    pageAdd++;
                    LoadTracksAdd();
                    btnNextAdd.IsEnabled = false;

                }
                else
                {
                    pageAdd++;
                    LoadTracksAdd();
                    btnNextAdd.IsEnabled = true;
                }
                btnBackAdd.IsEnabled = true;
            }
            else
            {
                if (pageAdd - 1 >= 0)
                {
                    btnNextAdd.IsEnabled = true;
                    pageAdd--;
                    if (pageAdd == 0)
                    {
                        btnBackAdd.IsEnabled = false;
                    }
                    LoadTracksAdd();
                }
                else
                {
                    btnBackAdd.IsEnabled = false;
                }
            }
        }
        #endregion

        private Tracks deserializeTrack(string response)
        {
            return this.listTracks = JsonConvert.DeserializeObject<Tracks>(response); //Deserialize the response of the php files to a dictionary.
        }

        private void listViewFoundClickTrack(ItemClickEventArgs e)
        {
            trackAdd.Insert(0, e.ClickedItem as Track);
            listTracksAdd.data.Insert(0, e.ClickedItem as Track);
            trackShowing.Remove(e.ClickedItem as Track);
            listTracks.data.Remove(e.ClickedItem as Track);
            LoadTracksFound();
            LoadTracksAdd();
            if (trackShowing.Count == 0 && listTracks.data.Count > 0)
            {
                if (pageFound > 0)
                {
                    LoadTracksFound(false);
                }
            }
            if (listTracksAdd.data.Count > 5)
            {
                if (listTracksAdd.data.Count > 20)
                {
                    DisableValidButton();
                }
                btnNextAdd.IsEnabled = true;
            }
            else
            {
                btnBackAdd.IsEnabled = false;
                btnNextAdd.IsEnabled = false;
                if (listTracksAdd.data.Count == 1)
                {
                    ShowListView(true, gridAdd, textEmptyAdd);
                }
                if (listTracksAdd.data.Count >= 5)
                {            
                    EnableValidButton();
                }
            }
        }

        private void listViewAddClickTrack(ItemClickEventArgs e)
        {
            trackShowing.Insert(0, e.ClickedItem as Track);
            listTracks.data.Insert(0, e.ClickedItem as Track);
            trackAdd.Remove(e.ClickedItem as Track);
            listTracksAdd.data.Remove(e.ClickedItem as Track);
            LoadTracksFound();
            LoadTracksAdd();

            if (listTracks.data.Count > 5)
            {
                btnNextAdd.IsEnabled = true;
            }
            else
            {
                btnNextAdd.IsEnabled = false;
                btnBackAdd.IsEnabled = false;
                if (listTracksAdd.data.Count == 0)
                {
                    ShowListView(false, gridAdd, textEmptyAdd);
                }
                if(listTracksAdd.data.Count < 5)
                {
                    DisableValidButton();
                }
            }
        }

        #endregion

       
    }
}
