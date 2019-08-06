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
using GuessMyZik.Pages.Frames.Game;
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
    public sealed partial class EndUserContentDialogSolo : ContentDialog
    {

        private Users userConnected;
        private Frame rootFrame;
        private int points;
        private double amountlevelUpNext;
        private APIConnect apiConnect = new APIConnect();

        public EndUserContentDialogSolo(object userConnected, int points, Frame rootFrame)
        {
            this.InitializeComponent();
            this.userConnected = userConnected as Users;
            this.points = points;
            this.rootFrame = rootFrame;
            InitializeContentDialog();
        }

        public EndUserContentDialogSolo(object userConnected, int points, Frame rootFrame, int statusDuel)
        {
            this.InitializeComponent();
            this.userConnected = userConnected as Users;
            this.points = points;
            this.rootFrame = rootFrame;
            InitializeContentDialog();
            InitializeDuel(statusDuel);
        }

        private void InitializeDuel(int statusDuel)
        {
            stackDuel.Visibility = Visibility.Visible;
            if(statusDuel == 1)
            {
                textDuel.Text = "You win !";
                textDuel.Foreground = new SolidColorBrush((Color)Application.Current.Resources["LastColor"]);
            } else if(statusDuel == 2)
            {
                textDuel.Text = "You made a tie.";
                textDuel.Foreground = new SolidColorBrush((Color)Application.Current.Resources["SecondaryColor"]);
            } else
            {
                textDuel.Text = "You lost !";
                textDuel.Foreground = new SolidColorBrush((Color)Application.Current.Resources["ExitColor"]);
            }
        }

        private async void InitializeContentDialog()
        {
            textCongratsUser.Text = "Congratulations " + userConnected.username;
            personPicture.Initials = (userConnected.username[0].ToString() + userConnected.username[1].ToString()).ToUpper();
            if(userConnected.level_id != "10")
            {
                textLvlActual.Text = "Lvl " + userConnected.level_id;
                textLvlNext.Text = "Lvl " + (Convert.ToInt16(userConnected.level_id) + 1).ToString();
            } else
            {
                textLvlActual.Text = "Lvl " + userConnected.level_id;
                textLvlNext.Text = "Lvl " + userConnected.level_id;
            }
            string response = await apiConnect.GetAsJsonAsync("http://localhost/api/level/level.php");
            List<Levels> levels = JsonConvert.DeserializeObject<List<Levels>>(response);
            double amountActualLevel = Convert.ToDouble(levels[Convert.ToInt16(userConnected.level_id) - 1].amount_xp);
            progressUser.Minimum = amountActualLevel;
            double amountNextLevel = 38400;
            if (userConnected.level_id != "10")
            {
                amountNextLevel = Convert.ToDouble(levels[Convert.ToInt16(userConnected.level_id)].amount_xp);
                progressUser.Maximum = amountNextLevel;
                amountlevelUpNext = 0;
                if (userConnected.level_id != "9")
                {
                    amountlevelUpNext = Convert.ToDouble(levels[Convert.ToInt16(userConnected.level_id) + 1].amount_xp);
                }
            }
            double value = amountActualLevel + Convert.ToDouble(userConnected.xp);
            progressUser.Value = value;
            await Task.Delay(2000);
            if (userConnected.level_id != "10")
            {
                AddXp(amountNextLevel, Convert.ToDouble(userConnected.xp));
            }
        }

        private async void AddXp(double amountNextLevel, double xpUser)
        {
            if (Convert.ToDouble(points * 10) != 0)
            {
                List<string> parameterXp = new List<string>();
                if (xpUser + Convert.ToDouble(points * 10) >= amountNextLevel)
                {
                    (storyBoardProgress.Children[0] as DoubleAnimation).To = amountNextLevel;
                    storyBoardProgress.Begin(); 
                    await Task.Delay(3100);
                    storyBoardProgress.Stop();
                    storyBoardProgress.Children.RemoveAt(0);
                    if (userConnected.level_id != "9")
                    {
                        textLvlActual.Text = "Lvl " + (Convert.ToInt16(userConnected.level_id) + 1).ToString();
                        textLvlNext.Text = "Lvl " + (Convert.ToInt16(userConnected.level_id) + 2).ToString();
                        progressUser.Visibility = Visibility.Collapsed;
                        progressUserTwo.Minimum = amountNextLevel;
                        progressUserTwo.Value = amountNextLevel;
                        progressUserTwo.Maximum = amountlevelUpNext;
                        progressUserTwo.Visibility = Visibility.Visible;
                        if((xpUser + Convert.ToDouble(points * 10)) - amountNextLevel != 0)
                        {
                            (storyBoardProgressTwo.Children[0] as DoubleAnimation).To = xpUser + (points * 10);
                            storyBoardProgressTwo.Begin();                      
                        }
                    }
                    await Task.Delay(3100);
                    BtnMainPage.IsEnabled = true;
                    userConnected.xp = Math.Round(progressUserTwo.Value).ToString();
                    userConnected.level_id = (Convert.ToInt16(userConnected.level_id) + 1).ToString();
                    parameterXp.Add(userConnected.username);
                    parameterXp.Add(userConnected.level_id);
                    parameterXp.Add(userConnected.xp);
                    UpdateLevel(parameterXp);
                }
                else
                {
                    (storyBoardProgress.Children[0] as DoubleAnimation).To = Math.Round(progressUser.Value) + (points * 10);
                    storyBoardProgress.Begin();
                    await Task.Delay(3100);
                    BtnMainPage.IsEnabled = true;
                    userConnected.xp = Math.Round(progressUser.Value).ToString();
                    parameterXp.Add(userConnected.username);
                    parameterXp.Add(userConnected.xp);
                    UpdateOnlyXp(parameterXp);
                }
            }
        }

        private async void UpdateOnlyXp(List<string> list)
        {
            await apiConnect.PostAsJsonAsync(list, "http://localhost/api/level/xp.php");
        }

        private async void UpdateLevel(List<string> list)
        {
            await apiConnect.PostAsJsonAsync(list, "http://localhost/api/level/updatelevel.php");
        }

        private void BtnMainPage_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            rootFrame.GoBack();
        }
    }
}
