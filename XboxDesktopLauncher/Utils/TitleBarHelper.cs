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
            return TitleBar;
        }

    }
}
