using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using System.Net.Mail;

namespace GuessMyZik.Classes
{
    class VerificationAuth
    {
        /// <summary>
        /// Check if the both password match. If match sent true otherwise false.
        /// </summary>
        /// <param name="password">First password to check.</param>
        /// <param name="passwordConfirm">Second password to check.</param>
        public static bool IsValidPassword(string password, string passwordConfirm)
        {
            if(password == passwordConfirm)
            {
                return true;    
            }
            return false;
        }

        /// <summary>
        /// Check if the email is valid: if contains @ and a point.
        /// </summary>
        /// <param name="email">Email to check.</param>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email); //If it's possible, it's a valid email.
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Function launching animation color of TextBox.
        /// </summary>
        /// <param name="textBoxName">TextBox to animate.</param>
        /// <param name="errorTextBoxStoryboard">Storyboard to launch.</param>
        /// <param name="color">The chosen color.</param>
        /// <param name="btnDisabled">Button that will be disabled.</param>
        public static void AnimationErrorTextBox(TextBox textBoxName, Storyboard errorTextBoxStoryboard, ColorAnimation color, Button btnDisabled)
        {
            Storyboard.SetTargetName(color, textBoxName.Name); //Change TargetName of the Storyboard by the TextBox
            errorTextBoxStoryboard.Begin(); //Start the animation
            btnDisabled.IsEnabled = false; //Disable the button
        }

        /// <summary>
        /// Function launching animation color of PasswordBox.
        /// </summary>
        /// <param name="passwordBox">PasswordBox to animate.</param>
        /// <param name="errorTextBoxStoryboard">Storyboard to launch.</param>
        /// <param name="color">The chosen color.</param>
        /// <param name="btnDisabled">Button that will be disabled.</param>
        public static void AnimationErrorTextBox(PasswordBox passwordBox, Storyboard errorTextBoxStoryboard, ColorAnimation color, Button btnDisabled)
        {
            Storyboard.SetTargetName(color, passwordBox.Name); //Change TargetName of the Storyboard by the PasswordBox
            errorTextBoxStoryboard.Begin(); //Start the animation
            btnDisabled.IsEnabled = false; //Disable the button
        }

    }
}
