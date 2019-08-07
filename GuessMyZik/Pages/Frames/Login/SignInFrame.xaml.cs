using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System.Threading.Tasks;
using GuessMyZik.Classes;
using Windows.UI.Xaml.Media.Animation;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.Security.Cryptography;
using Windows.UI.Popups;
using System.Diagnostics;
using Windows.Web.Http;
using System.Net.NetworkInformation;
using GuessMyZik.Classes.FrameParameters;

namespace GuessMyZik.Pages.Frames.Login
{
    /// <summary>
    /// An empty page can be used alone or as a landing page within a frame.
    /// </summary>
    public sealed partial class SignInFrame : Page
    {

        private Frame rootFrame;
        private readonly SymmetricEncryption encryptionProvider;
        private APIConnect apiConnect = new APIConnect();

        public SignInFrame()
        {
            this.InitializeComponent();
            encryptionProvider = new SymmetricEncryption(); //Instantiates the SymmetricEncryption class
        }

        /// <summary>
        /// Called when a keyboard key is pressed into textbox.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to KeyRoutedEventArgs.</param>
        private void enableConnect_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if(passwordBox.Password != "" && textUserOrMail.Text != "") //Check if all the textbox are empty or not.
            {
                btnConnect.IsEnabled = true; //Enabled the button btnRegister.
                if (e.Key == Windows.System.VirtualKey.Enter)
                {
                    ClickSignIn();
                }
            } else
            {
                btnConnect.IsEnabled = false; //Disabled the button btnRegister.
            }
            this.resetErrorTextBox(); //Reset all error messages
        }

        /// <summary>
        /// Called when the animation of the ErrorFirstTextBoxColor storyboard is finished.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to the target object.</param>
        private void ErrorFirstTextBoxColor_Completed(object sender, object e)
        {
            errorFirstTextBoxStoryboard.Stop(); //Stop the storyboard
        }

        /// <summary>
        /// Called when the animation of the ErrorSecondTextBoxColor storyboard is finished.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to the target object.</param>
        private void ErrorSecondTextBoxColor_Completed(object sender, object e)
        {
            errorSecondTextBoxStoryboard.Stop(); //Stop the storyboard
        }

        /// <summary>
        /// Called when you click on the btnRegister button.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to RoutedEventArgs.</param>
        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            ClickSignIn();  
        }

        private async void ClickSignIn()
        {
            progressConnect.IsActive = true;
            backgroundWaiting.Visibility = Visibility.Visible;
            string userLogin = textUserOrMail.Text;
            string response = await apiConnect.PostAsJsonAsync(userLogin, "http://localhost/api/auth/login.php"); //HttpRequest to the URL with the user's information and recover the return.
            if (response != "NO") //If the username or the mail is in the database.
            {
                Users user = JsonConvert.DeserializeObject<Users>(response); //Deserialize the response of the php files to a dictionary.
                string passwordEncrypted = encryptionProvider.Decrypt(user.password); //Decrypt the password from the database.
                if (passwordEncrypted == passwordBox.Password) //Check if the decrypted password and the password from the PasswordBox match.
                {
                    rootFrame.Navigate(typeof(MainPage), new FrameParameters(rootFrame, null, user), new DrillInNavigationTransitionInfo()); //Close this page and open MainPage.
                }
                else
                {
                    progressConnect.IsActive = false;
                    backgroundWaiting.Visibility = Visibility.Collapsed;
                    VerificationAuth.AnimationErrorTextBox(textUserOrMail, errorFirstTextBoxStoryboard, errorFirstTextBoxColor, btnConnect); //Launch error animation on textUsername TextBox.
                    VerificationAuth.AnimationErrorTextBox(passwordBox, errorSecondTextBoxStoryboard, errorSecondTextBoxColor, btnConnect); //Launch error animation on textUsername PasswordBox.
                    errorPassword.Text = "Your identifiants are invalid.";
                }
            }
            else
            {
                progressConnect.IsActive = false;
                backgroundWaiting.Visibility = Visibility.Collapsed;
                VerificationAuth.AnimationErrorTextBox(textUserOrMail, errorFirstTextBoxStoryboard, errorFirstTextBoxColor, btnConnect); //Launch error animation on textUsername TextBox.
                errorUserOrMail.Text = "Your username or your email is invalid.";
            }
        }

        /// <summary>
        /// Function that empties all the TextBox.
        /// </summary>
        private void resetErrorTextBox()
        {
            errorPassword.Text = "";
            errorUserOrMail.Text = "";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            rootFrame = (Frame)e.Parameter;
        }

       
    }
}
