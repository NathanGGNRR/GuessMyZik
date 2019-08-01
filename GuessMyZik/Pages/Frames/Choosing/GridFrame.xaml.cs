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

namespace GuessMyZik.Pages.Frames.Choosing
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class GridFrame : Page
    {
        public GridFrame()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        private ChoosingFrameParametersArtists choosingFrameParameters;
        private APIConnect apiConnect = new APIConnect();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            choosingFrameParameters = (ChoosingFrameParametersArtists)e.Parameter;
            gridViewArtist.ItemsSource = choosingFrameParameters.listArtist;
        }

        private async Task<Albums> RemoveDuplicateAlbum(ItemClickEventArgs e)
        {
            string response = await apiConnect.GetAsJsonAsync("https://api.deezer.com/artist/" + (e.ClickedItem as Artist).id + "/albums?limit=1000");
            Albums albums = JsonConvert.DeserializeObject<Albums>(response);
            List<string> allTitle = new List<string>();
            foreach (Album album in albums.data)
            {
                allTitle.Add(album.title);
            }
            foreach (string title in allTitle)
            {
                int count = 0;
                foreach (Album album in albums.data.ToList())
                {
                    if (album.title.Contains(title))
                    {
                        count++;
                        if (count > 1)
                        {
                            albums.data.Remove(album);
                        }
                    }
                }
            }
            return albums;
        }

        private async Task<Tracks> StoreAllTracks(Albums albums)
        {
            Tracks tracks = new Tracks();
            foreach (Album album in albums.data)
            {
                string responseTwo = await apiConnect.GetAsJsonAsync(album.tracklist + "?limit=25");
                Tracks tracksAlbum = JsonConvert.DeserializeObject<Tracks>(responseTwo);
                foreach (Track trackAlbum in tracksAlbum.data)
                {
                    trackAlbum.album = album;
                }
                tracks.data.AddRange(tracksAlbum.data);
            }
            return tracks;
        }

        private async void GridViewArtist_ItemClick(object sender, ItemClickEventArgs e)
        {
            progressMusics.IsActive = true;
            backgroundWaiting.Visibility = Visibility.Visible;
            Albums albums = await RemoveDuplicateAlbum(e);           
            choosingFrameParameters.listMusicArtist = await StoreAllTracks(albums);
            progressMusics.IsActive = false;
            backgroundWaiting.Visibility = Visibility.Collapsed;
            choosingFrameParameters.choosingFrame.Navigate(typeof(ListFrame), choosingFrameParameters);
        }


    }
}
