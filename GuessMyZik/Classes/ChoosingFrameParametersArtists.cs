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
using GuessMyZik.Classes.CategoryClasses;
using Windows.UI.Xaml.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Uwp.UI;


namespace GuessMyZik.Classes
{
    class ChoosingFrameParametersArtists
    {
        public Frame choosingFrame { get; set; }
        public List<Artist> listArtist { get; set; }
        public Tracks listMusicArtist { get; set; }
        public List<Track> listMusicChoosing { get; set; }
        public TextBox textMusics { get; set; }

        public ChoosingFrameParametersArtists(Frame frame, List<Artist> listArtist, Tracks listMusicArtist, List<Track> listMusicChoosing, TextBox textMusics)
        {
            this.choosingFrame = frame;
            this.listArtist = listArtist;
            this.listMusicChoosing = listMusicChoosing;
            this.textMusics = textMusics;
        }

    }
}
