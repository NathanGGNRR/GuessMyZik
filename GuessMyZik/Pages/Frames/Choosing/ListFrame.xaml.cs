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

namespace GuessMyZik.Pages.Frames.Choosing
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ListFrame : Page
    {
        public ListFrame()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        private ChoosingFrameParameters choosingFrameParameters;
        private APIConnect apiConnect = new APIConnect();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            choosingFrameParameters = (ChoosingFrameParameters)e.Parameter;
            if (choosingFrameParameters.artists != null)
            {
                listViewTracks.ItemsSource = choosingFrameParameters.artists.listMusicArtist.data;
                if(choosingFrameParameters.artists.listMusicChoosing.Count != 0)
                {
                    SelectMusicChoosing();
                }
            } else
            {
                listViewTracks.ItemsSource = choosingFrameParameters.albums.listMusicAlbum.data;
                if (choosingFrameParameters.albums.listMusicChoosing.Count != 0)
                {
                    SelectMusicChoosing();
                }
            }
    }

        private void SelectMusicChoosing()
        {
            foreach(Track item in listViewTracks.Items)
            {
                if (choosingFrameParameters.artists != null)
                {
                    foreach (Track itemTwo in choosingFrameParameters.artists.listMusicChoosing)
                    {
                        if (item.id == itemTwo.id)
                        {
                            listViewTracks.SelectedItems.Add(item);
                            break;
                        }
                    }
                } else
                {
                    foreach (Track itemTwo in choosingFrameParameters.albums.listMusicChoosing)
                    {
                        if (item.id == itemTwo.id)
                        {
                            listViewTracks.SelectedItems.Add(item);
                            break;
                        }
                    }
                }
            }
        }

        private void ListViewTracks_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool itemSelected = false;
            int count = listViewTracks.SelectedItems.Count;
            for (int i = 0; i < count; i++)
            {
                if (listViewTracks.SelectedItems[i] as Track == e.ClickedItem as Track)
                {
                    itemSelected = true;
                    break;
                }
            }

            if (choosingFrameParameters.artists != null)
            {
                if (itemSelected)
                { 
                    foreach (Track track in choosingFrameParameters.artists.listMusicChoosing)
                    {
                        if (track.id == (e.ClickedItem as Track).id)
                        {
                            choosingFrameParameters.artists.listMusicChoosing.Remove(track);
                            break;
                        }

                    }
                    choosingFrameParameters.artists.textMusics.Text = choosingFrameParameters.artists.listMusicChoosing.Count.ToString();
                }
                else
                {
                    choosingFrameParameters.artists.listMusicChoosing.Add(e.ClickedItem as Track);
                    choosingFrameParameters.artists.textMusics.Text = choosingFrameParameters.artists.listMusicChoosing.Count.ToString();
                }
            } else
            {
                if (itemSelected)
                {
                    foreach (Track track in choosingFrameParameters.albums.listMusicChoosing)
                    {
                        if (track.id == (e.ClickedItem as Track).id)
                        {
                            choosingFrameParameters.albums.listMusicChoosing.Remove(track);
                            break;
                        }

                    }
                    choosingFrameParameters.albums.textMusics.Text = choosingFrameParameters.albums.listMusicChoosing.Count.ToString();
                }
                else
                {
                    choosingFrameParameters.albums.listMusicChoosing.Add(e.ClickedItem as Track);
                    choosingFrameParameters.albums.textMusics.Text = choosingFrameParameters.albums.listMusicChoosing.Count.ToString();
                }
            }
        }

    }
}
