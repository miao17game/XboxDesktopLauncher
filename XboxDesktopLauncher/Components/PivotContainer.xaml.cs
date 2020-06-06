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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using XboxDesktopLauncher.Utils;

namespace XboxDesktopLauncher.Components {

    public sealed partial class PivotContainer : UserControl {

        public IList<RouteTag> Tags { get { return ApplicationRouter.Tags; } }

        public RouteTag Selected;

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

        public PivotContainer() {
            Selected = Tags[0];
            InitializeComponent();
        }

        public static readonly DependencyProperty ContentFrameProperty =
            DependencyProperty.Register("ContentFrame", typeof(Frame), typeof(PivotContainer), null);

        private void OnNavigated(object sender, NavigationEventArgs args) { }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var pre = Selected;
            Selected = (RouteTag)e.AddedItems[0];
            ContentFrame.NavigateToRoute(Selected.Type, TransitionHelper.DecideSlideAnimation(pre, Selected));
        }
    }
}
