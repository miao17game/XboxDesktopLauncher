using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using muxc = Microsoft.UI.Xaml.Controls;
using pages = XboxDesktopLauncher.Pages;

namespace XboxDesktopLauncher {

    internal class Parameter {

        public string Tag { get; set; }

        public Parameter(string tag) {
            Tag = tag;
        }
    }

    public sealed partial class MainPage : Page {

        private readonly Dictionary<string, Type> routes = new Dictionary<string, Type> {
            {"home" , typeof(pages.HomePage) },
            {"games" , typeof(pages.GamesPage) }
        };

        private Type DefaultRoute { get { return routes["home"]; } }

        private string Current { get; set; }

        public MainPage() {
            InitializeComponent();
            ContentFrame.Navigated += OnNavigated;
        }

        public void NavigationBarItemInvoked(muxc.NavigationView _, muxc.NavigationViewItemInvokedEventArgs args) {
            var tag = args.InvokedItemContainer?.Tag.ToString();
            var current = (muxc.NavigationViewItem)NavigationBar.SelectedItem;
            if (Current == current.Tag.ToString()) return;
            ContentFrame.NavigateToType(UsePage(tag), new Parameter(tag), new FrameNavigationOptions {
                TransitionInfoOverride = args.RecommendedNavigationTransitionInfo
            });
        }

        private Type UsePage(string tag) {
            return routes[tag ?? "default"] ?? DefaultRoute;
        }

        private void OnNavigated(object sender, NavigationEventArgs args) {
            Current = ((Parameter)args.Parameter)?.Tag;
        }
    }
}
