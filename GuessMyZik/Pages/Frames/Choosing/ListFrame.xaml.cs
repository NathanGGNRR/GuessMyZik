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
    public sealed partial class ListFrame : Page
    {
        public ListFrame()
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
            listViewTracks.ItemsSource = choosingFrameParameters.listMusicArtist.data;
            if(choosingFrameParameters.listMusicChoosing.Count != 0)
            {
                SelectMusicChoosing();
            }
        }

        private void SelectMusicChoosing()
        {
            foreach(Track item in listViewTracks.Items)
            {
                foreach(Track itemTwo in choosingFrameParameters.listMusicChoosing)
                if(item.id == itemTwo.id)
                {
                    listViewTracks.SelectedItems.Add(item);
                    break;
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
           
            if (itemSelected)
            {

                foreach(Track track in choosingFrameParameters.listMusicChoosing)
                {
                    if(track.id == (e.ClickedItem as Track).id)
                    {
                        choosingFrameParameters.listMusicChoosing.Remove(track);
                        break;
                    }

                }
                choosingFrameParameters.textMusics.Text = choosingFrameParameters.listMusicChoosing.Count.ToString();
            } else
            {
                choosingFrameParameters.listMusicChoosing.Add(e.ClickedItem as Track);
                choosingFrameParameters.textMusics.Text = choosingFrameParameters.listMusicChoosing.Count.ToString();
            }
        }

    }
}
