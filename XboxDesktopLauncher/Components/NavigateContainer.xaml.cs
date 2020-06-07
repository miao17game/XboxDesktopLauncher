using System;
using System.Collections.Generic;
using System.Diagnostics;
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

using XboxDesktopLauncher.Utils;

namespace XboxDesktopLauncher.Components {
    public sealed partial class NavigateContainer : UserControl {

        public IList<RouteTag> Tags { get { return ApplicationRouter.Tags; } }

        public RouteTag Selected;

        public NavigationViewItemBase SelectedContainer;

        public Frame ContentFrame {
            get { return (Frame)GetValue(ContentFrameProperty); }
            set {
                if (ContentFrame != null) {
                    ContentFrame.Navigated -= OnNavigated;
                }
                SetValue(ContentFrameProperty, value);
                value.Navigated += OnNavigated;
                value.NavigateToRoute(Selected.Type, null);
            }
        }

        public NavigateContainer() {
            Selected = Tags[0];
            InitializeComponent();
        }

        public static readonly DependencyProperty ContentFrameProperty =
            DependencyProperty.Register("ContentFrame", typeof(Frame), typeof(NavigateContainer), null);

        private void OnNavigated(object sender, NavigationEventArgs args) {
            Debug.WriteLine("navigated");
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e) {
            foreach (var tag in Tags) {
                NavView.MenuItems.Add(new NavigationViewItem {
                    Content = tag.Text,
                    Icon = new SymbolIcon((Symbol)0xF1AD),
                    Tag = tag.Type
                });
            }
            Selected = ApplicationRouter.GetRouteTag(Tags[0].Type);
            SelectedContainer = (NavigationViewItemBase)(NavView.SelectedItem = NavView.MenuItems[0]);
        }

        private void NavView_ItemInvoked(NavigationView _, NavigationViewItemInvokedEventArgs args) {
            var tag = (RouteType)args.InvokedItemContainer.Tag;
            var pre = Selected;
            Selected = ApplicationRouter.GetRouteTag(tag);
            SelectedContainer = args.InvokedItemContainer;
            ContentFrame.NavigateToRoute(tag, TransitionHelper.DecideSlideAnimation(pre, Selected));
        }
    }
}
