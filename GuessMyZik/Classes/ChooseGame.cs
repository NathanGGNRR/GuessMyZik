using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using System.Net.Mail;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using Windows.UI.Xaml;

namespace GuessMyZik.Classes
{
    class ChooseGame
    {
       
        /// <summary>
        /// Function launching animation color of path when PointerEntered.
        /// </summary>
        /// <param name="path">Path to animate.</param>
        /// <param name="button">Button where the storyboard will be stock.</param>
        public static void AnimationColorBtnSoloEntered(Path path, Button button, string colorRessource)
        {
            ColorAnimation colorAnimation = new ColorAnimation();
            colorAnimation.To = (Color)Application.Current.Resources[colorRessource];
            colorAnimation.Duration = new TimeSpan(0, 0, 0, 0, 500);
            Storyboard storyboard = new Storyboard();
            Storyboard.SetTargetName(colorAnimation, path.Name);
            Storyboard.SetTargetProperty(colorAnimation, "(Path.Fill).(SolidColorBrush.Color)");
            storyboard.Children.Add(colorAnimation);
            button.Resources.Add(new KeyValuePair<object, object>("storyboardEntered", storyboard));
            storyboard.Begin();
            button.Resources.Remove("storyboardEntered");
        }

        /// <summary>
        /// Function launching animation color of path when PointerExited.
        /// </summary>
        /// <param name="path">Path to animate.</param>
        /// <param name="button">Button where the storyboard will be stock.</param>
        public static void AnimationColorBtnSoloExited(Path path, Button button, string colorRessource)
        {
            ColorAnimation colorAnimation = new ColorAnimation();
            colorAnimation.To = (Color)Application.Current.Resources[colorRessource];
            colorAnimation.Duration = new TimeSpan(0, 0, 0, 0, 500);
            Storyboard storyboard = new Storyboard();
            Storyboard.SetTargetName(colorAnimation, path.Name);
            Storyboard.SetTargetProperty(colorAnimation, "(Path.Fill).(SolidColorBrush.Color)");
            storyboard.Children.Add(colorAnimation);
            button.Resources.Add(new KeyValuePair<object, object>("storyboardExited", storyboard));
            storyboard.Begin();
            button.Resources.Remove("storyboardExited");
        }

    }
}
