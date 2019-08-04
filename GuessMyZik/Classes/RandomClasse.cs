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
using GuessMyZik.Classes.TrackRandomClasses;
using Windows.UI.Xaml.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Uwp.UI;
using GuessMyZik.Classes.FrameParameters;

namespace GuessMyZik.Classes
{
    class RandomClasse
    {

        private APIConnect apiConnect = new APIConnect();

        public RandomClasse() { }


        public async Task<List<Track>> AllTracksArtist(Artists artists, int nbMusicChoosen)
        {
            Tracks allTracks = new Tracks();
            foreach (Artist artist in artists.data)
            {
                string response = await apiConnect.GetAsJsonAsync("https://api.deezer.com/artist/" + artist.id + "/albums?limit=1000");
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
                Tracks tracksArtist = await StoreAllTracks(albums);
                allTracks.data.AddRange(tracksArtist.data);
            }
            Tracks tracks = new Tracks();
            int countTotal = nbMusicChoosen;
            if (allTracks.data.Count < nbMusicChoosen)
            {
                countTotal = allTracks.data.Count;
            }
            for (int i = 0; i < countTotal; i++)
            {
                bool action = false;
                while (!action)
                {
                    Random rnd = new Random();
                    int index = rnd.Next(allTracks.data.Count);
                    if (!tracks.data.Contains(allTracks.data[index]) && allTracks.data[index].readable == true)
                    {
                        tracks.data.Add(allTracks.data[index]);
                        action = true;
                    }
                }
            }
            return tracks.data;
        }


        public async Task<List<Track>> AllTracksAlbum(Albums albums, int nbMusicChoosen)
        {
            Tracks allTracks = await StoreAllTracks(albums);
            Tracks tracks = new Tracks();
            int countTotal = nbMusicChoosen;
            if (allTracks.data.Count < nbMusicChoosen)
            {
                countTotal = allTracks.data.Count;
            }
            for (int i = 0; i < countTotal; i++)
            {
                bool action = false;
                while (!action)
                {
                    Random rnd = new Random();
                    int index = rnd.Next(allTracks.data.Count);
                    if (!tracks.data.Contains(allTracks.data[index]) && allTracks.data[index].readable == true)
                    {
                        tracks.data.Add(allTracks.data[index]);
                        action = true;
                    }
                }
            }
            return tracks.data;
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

        public async Task<List<Track>> RandomTracks(int nbMusicChoosen)
        {
            List<Track> tracks = new List<Track>();
            for (int i = 0; i < nbMusicChoosen; i++)
            {
                bool action = false;
                while (!action) { 
                    Random rnd = new Random();
                    int randomNumber = rnd.Next(2000001, 999999999);
                    string response = await apiConnect.GetAsJsonAsync("https://api.deezer.com/track/" + randomNumber.ToString());
                    try
                    {
                        TrackRandom tracksAlbum = JsonConvert.DeserializeObject<TrackRandom>(response);
                        if (tracksAlbum.readable == true)
                        {
                            Track newTrack = new Track(tracksAlbum.id, tracksAlbum.readable, tracksAlbum.title, tracksAlbum.title_short, tracksAlbum.title_version, tracksAlbum.link, tracksAlbum.duration, tracksAlbum.rank.ToString(), tracksAlbum.explicit_lyrics, tracksAlbum.explicit_content_lyrics, tracksAlbum.explicit_content_cover, tracksAlbum.preview, tracksAlbum.artist, tracksAlbum.album, tracksAlbum.type);
                            if (!tracks.Contains(newTrack))
                            {
                                tracks.Add(newTrack);
                                action = true;
                            }
                        }
                    } catch {
                       
                    }
                }   
            }
            return tracks;
        }
    }
}
