using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GuessMyZik.Pages;

namespace GuessMyZik
{
    /// <summary>
    /// Provides application-specific behavior to complete the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of the code created
        /// to be executed. It therefore corresponds to the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is normally launched by the end user.  Other entry points
        /// will be used, for example, when launching the application to open a specific file.
        /// </summary>
        /// <param name="e">Details about the request and the launch process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat the initialization of the application when the window already contains content,
            // just make sure the window is active.
            if (rootFrame == null)
            {
                // Create a Frame that can be used as a navigation context and navigate to the first page.
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: load the status of the previously suspended application.
                }

                // Place the frame in the active window.
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack is not restored, go to the first page,
                    // then configure the new page by sending the required information as
                    // setting.
                    rootFrame.Navigate(typeof(LoginPage), e.Arguments);
                }
                // Check that the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Called when navigation to a given page fails.
        /// </summary>
        /// <param name="sender">Frame causing navigation failure.</param>
        /// <param name="e">Details of navigation failure.</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Called when application execution is suspended.  The status of the application is recorded
        /// without knowing if the application will be able to close or resume without damaging
        /// the content of the memory.
        /// </summary>
        /// <param name="sender">Source of the request for suspension.</param>
        /// <param name="e">Details of the request for suspension.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: record the status of the application and stop all background activity.
            deferral.Complete();
        }
    }
}
