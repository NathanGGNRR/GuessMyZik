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
    public sealed partial class SignInFrame : Page
    {
        public SignInFrame()
        {
            this.InitializeComponent();
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
            } else
            {
                btnConnect.IsEnabled = false; //Disabled the button btnRegister.
            }
        }
    }
}
