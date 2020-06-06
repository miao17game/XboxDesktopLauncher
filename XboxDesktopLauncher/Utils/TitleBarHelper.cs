using System;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace XboxDesktopLauncher.Utils {
    public static class TitleBarHelper {

        public static ApplicationViewTitleBar TitleBar {
            get {
                return ApplicationView.GetForCurrentView().TitleBar;
            }
        }

        public static Color GetBrushColor(string name) {
            return ((SolidColorBrush)Application.Current.Resources["System" + name + "Brush"]).Color;
        }

        public static Color GetThemeColor(string name) {
            return ((Color)Application.Current.Resources["System" + name + "Color"]);
        }

        public static Color GetThemeColor(string name, string level) {
            return ((Color)Application.Current.Resources["System" + name + "Color" + level]);
        }

        public static ApplicationViewTitleBar UseExtendTitleBar() {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            UseCustomColorSolutions();
            return TitleBar;
        }

        private static void UseCustomColorSolutions() {
            // Set active window colors
            TitleBar.ForegroundColor = Colors.White;
            TitleBar.BackgroundColor = Colors.Transparent;
            TitleBar.ButtonForegroundColor = Colors.White;
            TitleBar.ButtonBackgroundColor = Colors.Transparent;
            TitleBar.ButtonHoverForegroundColor = Colors.White;
            TitleBar.ButtonHoverBackgroundColor = GetThemeColor("Accent");
            TitleBar.ButtonPressedForegroundColor = Colors.White;
            TitleBar.ButtonPressedBackgroundColor = GetThemeColor("Accent", "Dark2");

            // Set inactive window colors
            TitleBar.InactiveForegroundColor = Colors.Gray;
            TitleBar.InactiveBackgroundColor = Colors.Transparent;
            TitleBar.ButtonInactiveForegroundColor = Colors.Gray;
            TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }

    }
}
