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

namespace GuessMyZik.Pages.Frames
{
    /// <summary>
    /// An empty page can be used alone or as a landing page within a frame.
    /// </summary>
    public sealed partial class RegistrationFrame : Page
    {
        public RegistrationFrame()
        {
            this.InitializeComponent();
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
            }
            else
            {
                btnRegister.IsEnabled = false; //Disabled the button btnRegister.
            }
        }
    }
}
