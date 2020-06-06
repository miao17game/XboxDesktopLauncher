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
using muxc = Microsoft.UI.Xaml.Controls;

namespace XboxDesktopLauncher.Components {
    public sealed partial class PivotContainer : UserControl {

        public Frame ContentFrame {
            get { return (Frame)GetValue(ContentFrameProperty); }
            set {
                if (ContentFrame != null) {
                    ContentFrame.Navigated -= OnNavigated;
                }
                SetValue(ContentFrameProperty, value);
                value.Navigated += OnNavigated;
            }
        }

        public PivotContainer() {
            InitializeComponent();
        }

        public static readonly DependencyProperty ContentFrameProperty =
            DependencyProperty.Register("ContentFrame", typeof(Frame), typeof(PivotContainer), null);

        public void ItemInvoke(muxc.NavigationView _, muxc.NavigationViewItemInvokedEventArgs args) {
            ContentFrame.NavigateToRoute(args.InvokedItemContainer?.Tag.ToString(), args.RecommendedNavigationTransitionInfo);
        }

        private void OnNavigated(object sender, NavigationEventArgs args) { }
    }
}
