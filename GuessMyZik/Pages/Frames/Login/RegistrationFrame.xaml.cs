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
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;
using System.Text;
using Windows.Storage;
using GuessMyZik.Classes.FrameParameters;

namespace GuessMyZik.Pages.Frames.Login
{
    /// <summary>
    /// An empty page can be used alone or as a landing page within a frame.
    /// </summary>
    public sealed partial class RegistrationFrame : Page
    {

        private Frame rootFrame;
        private readonly SymmetricEncryption encryptionProvider;
        private APIConnect apiConnect = new APIConnect();

        public RegistrationFrame()
        {
            this.InitializeComponent();
            encryptionProvider = new SymmetricEncryption(); //Instantiates the SymmetricEncryption class
        }

        /// <summary>
        /// Called when a keyboard key is pressed into textbox.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to KeyRoutedEventArgs.</param>
        private void registerEnable_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (passwordBox.Password != "" && passwordConfirmBox.Password != "" && textMail.Text != "" && textUsername.Text != "") //Check if all the textbox are empty or not.
            {
                btnRegister.IsEnabled = true; //Enabled the button btnRegister.
                if (e.Key == Windows.System.VirtualKey.Enter)
                {
                    ClickRegister();
                }
            }
            else
            {
                btnRegister.IsEnabled = false; //Disabled the button btnRegister.
            }
            this.resetErrorTextBox(); //Reset all error messages
        }

        /// <summary>
        /// Called when you click on the btnRegister button.
        /// </summary>
        /// <param name="sender">Element on which the event is launched.</param>
        /// <param name="e">Event details to RoutedEventArgs.</param>
        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            ClickRegister();
        }

        private async void ClickRegister()
        {
            if (VerificationAuth.IsValidPassword(passwordBox.Password, passwordConfirmBox.Password)) //Check if both password match.
            {
                if (VerificationAuth.IsValidEmail(textMail.Text)) //Check if mail is valid.
                {
                    progressRegister.IsActive = true;
                    backgroundWaiting.Visibility = Visibility.Visible;
                    string passwordEncrypted = encryptionProvider.Encrypt(passwordBox.Password); //Encrypt the password.
                    Users registration = new Users(textUsername.Text, passwordEncrypted, textMail.Text, "1", "0"); //Create new Users with information.
                    string response = await apiConnect.PostAsJsonAsync(registration, "http://localhost/api/auth/registration.php"); //HttpRequest to the URL with the user's information and recover the return.
                    if (response == "YES") //Registration good.
                    {
                        rootFrame.Navigate(typeof(MainPage), new FrameParameters(rootFrame, null, registration), new DrillInNavigationTransitionInfo()); //Close this page and open MainPage.
                    }
                    else
                    {
                        progressRegister.IsActive = false;
                        backgroundWaiting.Visibility = Visibility.Collapsed;
                        Dictionary<string, string> dicoJSON = JsonConvert.DeserializeObject<Dictionary<string, string>>(response); //Deserialize the response of the php files to a dictionary.
                        if (dicoJSON.ContainsKey("username")) //If the dictionary contains the key username.
                        {
                            VerificationAuth.AnimationErrorTextBox(textUsername, errorFirstTextBoxStoryboard, errorFirstTextBoxColor, btnRegister); //Launch error animation on textUsername TextBox.
                            errorUsername.Text = "Your username is already taken.";
                        }
                        if (dicoJSON.ContainsKey("mail")) //If the dictionary contains the key mail.
                        {
                            VerificationAuth.AnimationErrorTextBox(textMail, errorSecondTextBoxStoryboard, errorSecondTextBoxColor, btnRegister); //Launch error animation on textMail TextBox.
                            errorMail.Text = "Your email is already taken.";
                        }
                    }


                }
                else
                {
                    VerificationAuth.AnimationErrorTextBox(textMail, errorFirstTextBoxStoryboard, errorFirstTextBoxColor, btnRegister); //Launch error animation on textMail TextBox.
                    errorMail.Text = "Your email is invalid.";
                }
            }
            else
            {
                VerificationAuth.AnimationErrorTextBox(passwordBox, errorFirstTextBoxStoryboard, errorFirstTextBoxColor, btnRegister); //Launch error animation on passwordBox PasswordBox.
                VerificationAuth.AnimationErrorTextBox(passwordConfirmBox, errorSecondTextBoxStoryboard, errorSecondTextBoxColor, btnRegister); //Launch error animation on passwordConfirmBox PasswordBox.
                errorPassword.Text = "The passwords do not match.";
            }
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
        /// Function that empties all the TextBox.
        /// </summary>
        private void resetErrorTextBox()
        {
            errorPassword.Text = "";
            errorMail.Text = "";
            errorUsername.Text = "";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            rootFrame = (Frame)e.Parameter;
        }


    }
}
