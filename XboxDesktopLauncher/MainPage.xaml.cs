using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using XboxDesktopLauncher.Utils;

using muxc = Microsoft.UI.Xaml.Controls;

namespace XboxDesktopLauncher {

    public sealed partial class MainPage : Page {

        public MainPage() {
            InitializeComponent();
            ContentFrame.Navigated += OnContentFrameNavigated;
        }

        public void NavigationBarItemInvoked(muxc.NavigationView _, muxc.NavigationViewItemInvokedEventArgs args) {
            ContentFrame.NavigateToRoute(args.InvokedItemContainer?.Tag.ToString(), args.RecommendedNavigationTransitionInfo);
        }

        private void OnContentFrameNavigated(object sender, NavigationEventArgs args) { }
    }

}
