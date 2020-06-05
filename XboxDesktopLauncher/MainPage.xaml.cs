using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using XboxDesktopLauncher.Utils;

using muxc = Microsoft.UI.Xaml.Controls;

namespace XboxDesktopLauncher {

    internal struct Parameter {

        public readonly string Tag;

        public Parameter(string tag) {
            Tag = tag;
        }
    }

    public sealed partial class MainPage : Page {

        private string CurrentTag { get; set; }

        public MainPage() {
            InitializeComponent();
            ContentFrame.Navigated += OnContentFrameNavigated;
        }

        public void NavigationBarItemInvoked(muxc.NavigationView _, muxc.NavigationViewItemInvokedEventArgs args) {
            var tag = args.InvokedItemContainer?.Tag.ToString();
            var current = (muxc.NavigationViewItem)NavigationBar.SelectedItem;
            if (CurrentTag == current.Tag.ToString()) return;
            ContentFrame.NavigateToType(ApplicationRouter.GetRoute(tag), new Parameter(tag), new FrameNavigationOptions {
                TransitionInfoOverride = args.RecommendedNavigationTransitionInfo
            });
        }

        private void OnContentFrameNavigated(object sender, NavigationEventArgs args) {
            CurrentTag = ((Parameter?)args.Parameter)?.Tag;
        }
    }
}
