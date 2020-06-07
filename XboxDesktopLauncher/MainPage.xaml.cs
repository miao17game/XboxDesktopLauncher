using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using XboxDesktopLauncher.Utils;

namespace XboxDesktopLauncher {

    public sealed partial class MainPage : Page {

        public MainPage() {
            InitializeComponent();
            Notification.OnTipChanged += (_, args) => {
                GlobalTip.Title = args.Title;
                GlobalTip.Subtitle = args.Message;
                GlobalTip.IsOpen = args.Show;
            };
        }

        public void OnNavigated(object _, NavigationEventArgs args) { }
    }
}
