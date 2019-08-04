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


namespace GuessMyZik.Pages.DialogContent
{
    public sealed partial class NextContentDialog : ContentDialog
    {

        private DispatcherTimer dispatcherTimer;

        public NextContentDialog()
        {
            this.InitializeComponent();
            InitializeNewTimer();
        }

        private void InitializeNewTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, object e)
        {
            if (NextWaiting.Value + 1 < 4)
            {
                NextWaiting.Value = NextWaiting.Value + 1;
                textNextTimer.Text = (Convert.ToInt16(textNextTimer.Text) - 1).ToString();
            }
            else
            {
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
                dispatcherTimer.Stop();
                this.Hide();
            }
        }

    }
}
