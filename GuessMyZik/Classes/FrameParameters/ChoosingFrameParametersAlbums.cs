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


namespace GuessMyZik.Classes.FrameParameters
{
    class ChoosingFrameParametersAlbums
    {
        public Frame choosingFrame { get; set; }
        public Albums listAlbum { get; set; }
        public Tracks listMusicAlbum { get; set; }
        public List<Track> listMusicChoosing { get; set; }
        public TextBox textMusics { get; set; }

        public ChoosingFrameParametersAlbums(Frame frame, Albums listAlbum, List<Track> listMusicChoosing, TextBox textMusics)
        {
            this.choosingFrame = frame;
            this.listAlbum = listAlbum;
            this.listMusicChoosing = listMusicChoosing;
            this.textMusics = textMusics;
        }

    }
}
