using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GuessMyZik
{
    ///// <summary>
    ///// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    ///// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            logoOpacityStoryBoard.Begin(); //Start the storyboard called logoOpacityStoryBoard on MainPage.xaml: animates opacity of the logo from 0 to 1 in 2 seconds and then returns to 0
        }

        /// <summary>
        /// Function started at the end of the animation of the logo. 
        /// </summary>
        private void AnimationHomePage(object sender, object e)
        {
            List<int> gridWidthColumnDefinition = new List<int>(new int[] { 1, 6, 1 });
            for (int i = 0; i < gridWidthColumnDefinition.Count; i++)
            {
                gridHome.ColumnDefinitions[i].Width = new GridLength(gridWidthColumnDefinition[i], GridUnitType.Star);
            }
            gridWelcome.Visibility = Visibility.Visible;
            homePageOpacityStoryBoard.Begin();
        }

        private void PlatineLogoHome_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            platineSound.Play();
        }
    }
}
