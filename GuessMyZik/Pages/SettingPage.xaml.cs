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

namespace GuessMyZik.Pages
{
    /// <summary>
    /// An empty page can be used alone or as a landing page within a frame.
    /// </summary>
    public sealed partial class SettingPage : Page
    {

        private FrameParameters parameter;
        private Users connectedUser;
        private APIConnect apiConnect = new APIConnect();
        private readonly SymmetricEncryption encryptionProvider;


        public SettingPage()
        {
            this.InitializeComponent();
            encryptionProvider = new SymmetricEncryption(); //Instantiates the SymmetricEncryption class
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            parameter = (FrameParameters)e.Parameter;
            connectedUser = parameter.connectedUser;
            InitializeUser();
        }
        
        private async void InitializeUser()
        {
            textWelcomeUser.Text = "Welcome " + connectedUser.username;
            personPicture.Initials = (connectedUser.username[0].ToString() + connectedUser.username[1].ToString()).ToUpper();
            string response = await apiConnect.GetAsJsonAsync("http://localhost/api/level/level.php");
            List<Levels> levels = JsonConvert.DeserializeObject<List<Levels>>(response);
            double amountActualLevel = Convert.ToDouble(levels[Convert.ToInt16(connectedUser.level_id) - 1].amount_xp);
            double amountNextLevel = 38400;
            if (connectedUser.level_id != "10")
            {
                amountNextLevel = Convert.ToDouble(levels[Convert.ToInt16(connectedUser.level_id)].amount_xp);
            }
            double calcul = Convert.ToDouble(((Convert.ToDouble(connectedUser.xp) - amountActualLevel) / (amountNextLevel - amountActualLevel)) * 100);
            double value = Math.Round(calcul);
            progressUser.Value = value;
            textLvl.Text = "Lvl " + connectedUser.level_id;
        }


        /// <summary>
        /// Called when you click on the grid element gridBtnExit.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to TappedRoutedEventArgs.</param>
        private void BtnExit_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CoreApplication.Exit(); //Close the application
        }

        /// <summary>
        /// Called when the mouse entered on the grid element gridBtnExit. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnExit_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            btnExitPointerEntered.Begin(); //Launch the storyboard called btnExitPointerEntered, animate color of the button BtnExit with the color resource ExitColor.
        }

        /// <summary>
        /// Called when the mouse exited the grid element gridBtnExit. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnExit_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            btnExitPointerExited.Begin(); //Launch the storyboard called btnExitPointerEntered, animate color of the button BtnExit with the color resource WriteColor.
        }

        /// <summary>
        /// Called when the mouse entered on the grid element gridBtnSettings. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnHome_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            btnHomePointerEntered.Begin(); //Launch the storyboard called btnSettingsPointerEntered, animate color of the button BtnHome with the color resource SecondaryDarkColor.
        }

        /// <summary>
        /// Called when the mouse exited the grid element gridBtnSettings. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnHome_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            btnHomePointerExited.Begin(); //Launch the storyboard called btnSettingsPointerExited, animate color of the button BtnHome with the color resource SecondaryColor.
        }


        /// <summary>
        /// Called when the mouse entered on the grid element gridBtnStats. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnStats_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            btnStatsPointerEntered.Begin(); //Launch the storyboard called btnStatsPointerEntered, animate color of the button BtnStats with the color resource SecondaryDarkColor.
        }

        /// <summary>
        /// Called when the mouse exited the grid element gridBtnStats. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnStats_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            btnStatsPointerExited.Begin(); //Launch the storyboard called btnStatsPointerExited, animate color of the button BtnStats with the color resource SecondaryColor.
        }

     
        /// <summary>
        /// Called when the mouse entered on the grid element gridBtnDisconnectUser. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnDisconnectUser_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            btnDisconnectUserPointerEntered.Begin(); //Launch the storyboard called btnDisconnectUserPointerEntered, animate color of the button BtnDisconnect with the color resource ExitColor.
        }

        /// <summary>
        /// Called when the mouse exited the grid element gridBtnDisconnectUser. 
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to PointerRoutedEventArgs.</param>
        private void BtnDisconnectUser_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            btnDisconnectUserPointerExited.Begin(); //Launch the storyboard called btnDisconnectUserPointerExited, animate color of the button BtnDisconnect with the color resource SecondaryColor.
        }


        /// <summary>
        /// Called when you click on the grid element gridBtnSettings.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to TappedRoutedEventArgs.</param>
        private void BtnHome_Tapped(object sender, TappedRoutedEventArgs e)
        {
            parameter.rootFrame.Navigate(typeof(MainPage), new FrameParameters(parameter.rootFrame, null, connectedUser), new DrillInNavigationTransitionInfo());
        }


        /// <summary>
        /// Called when you click on the grid element gridBtnStats.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to TappedRoutedEventArgs.</param>
        private void BtnStats_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        /// <summary>
        /// Called when you click on the grid element gridBtnDisconnectUser.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to TappedRoutedEventArgs.</param>
        private void BtnDisconnectUser_Tapped(object sender, TappedRoutedEventArgs e)
        {

            parameter.rootFrame.Navigate(typeof(LoginPage), parameter.rootFrame);
        }

        /// <summary>
        /// Called when a keyboard key is pressed into textbox.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to KeyRoutedEventArgs.</param>
        private void passwordEnable_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (passwordActualBox.Password != "" && newPasswordBox.Password != "" && newPasswordConfirmBox.Password != "") //Check if all the password box are empty or not.
            {
                btnChange.IsEnabled = true; //Enabled the button btnChange.
                if (e.Key == Windows.System.VirtualKey.Enter)
                {
                    ClickSetting();
                }
            }
            else
            {
                btnChange.IsEnabled = false; //Disabled the button btnChange.
            }
            this.resetErrorTextBox(); //Reset all error messages
        }

        /// <summary>
        /// Called when you click on the btnRegister button.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to RoutedEventArgs.</param>
        private void BtnChange_Click(object sender, RoutedEventArgs e)
        {
            ClickSetting();
        }

        private async void ClickSetting()
        {
            if (VerificationAuth.IsValidPassword(newPasswordBox.Password, newPasswordConfirmBox.Password)) //Check if both password match.
            {
                progressSetting.IsActive = true;
                backgroundWaiting.Visibility = Visibility.Visible;
                string ActualPassword = encryptionProvider.Decrypt(connectedUser.password);
                if (passwordActualBox.Password == ActualPassword)
                {
                    connectedUser.password = encryptionProvider.Encrypt(newPasswordBox.Password);//Encrypt the password.
                    await apiConnect.PostAsJsonAsync(connectedUser, "http://localhost/api/auth/changepassword.php");
                    progressSetting.IsActive = false;
                    backgroundWaiting.Visibility = Visibility.Collapsed;
                    ChangePasswordContentDialog dialog = new ChangePasswordContentDialog(parameter.rootFrame, parameter.connectedUser);
                    await dialog.ShowAsync();
                }
                else
                {
                    progressSetting.IsActive = false;
                    backgroundWaiting.Visibility = Visibility.Collapsed;
                    VerificationAuth.AnimationErrorTextBox(passwordActualBox, errorSecondPasswordBoxStoryboard, errorSecondPasswordBoxColor, btnChange); //Launch error animation on textMail TextBox.
                    errorActualPassword.Text = "Your actual password do not match.";
                }
            }
            else
            {
                progressSetting.IsActive = false;
                backgroundWaiting.Visibility = Visibility.Collapsed;
                VerificationAuth.AnimationErrorTextBox(newPasswordBox, errorFirstPasswordBoxStoryboard, errorFirstPasswordBoxColor, btnChange); //Launch error animation on passwordBox PasswordBox.
                VerificationAuth.AnimationErrorTextBox(newPasswordConfirmBox, errorSecondPasswordBoxStoryboard, errorSecondPasswordBoxColor, btnChange); //Launch error animation on passwordConfirmBox PasswordBox.
                errorNewPassword.Text = "The new passwords do not match.";
            }
        }

        /// <summary>
        /// Called when the animation of the ErrorFirstTextBoxColor storyboard is finished.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to the target object.</param>
        private void ErrorFirstPasswordBoxColor_Completed(object sender, object e)
        {
            errorFirstPasswordBoxStoryboard.Stop(); //Stop the storyboard
        }

        /// <summary>
        /// Called when the animation of the ErrorSecondTextBoxColor storyboard is finished.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to the target object.</param>
        private void ErrorSecondPasswordBoxColor_Completed(object sender, object e)
        {
            errorSecondPasswordBoxStoryboard.Stop(); //Stop the storyboard
        }

        /// <summary>
        /// Function that empties all the TextBox.
        /// </summary>
        private void resetErrorTextBox()
        {
            errorActualPassword.Text = "";
            errorNewPassword.Text = "";
            errorNewPasswordConfirm.Text = "";
        }



    }
}
