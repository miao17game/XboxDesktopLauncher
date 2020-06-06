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

    public class Tag {
        public readonly string Text;

        public readonly string Value;

        public readonly int Index;

        public Tag(string display, string tag, int index) {
            Text = display;
            Value = tag;
            Index = index;
        }
    }

    static class TransitionHelper {
        public readonly static SlideNavigationTransitionInfo Next = new SlideNavigationTransitionInfo {
            Effect = SlideNavigationTransitionEffect.FromRight
        };

        public readonly static SlideNavigationTransitionInfo Previous = new SlideNavigationTransitionInfo {
            Effect = SlideNavigationTransitionEffect.FromLeft
        };

        public static SlideNavigationTransitionInfo DecideAnimas(Tag pre, Tag current) {
            return pre.Index > current.Index ? Previous : Next;
        }
    }



    public sealed partial class PivotContainer : UserControl {

        public List<Tag> Tags = new List<Tag> { new Tag("Home", "home", 1), new Tag("Games", "games", 2) };

        public Tag Selected;

        public Frame ContentFrame {
            get { return (Frame)GetValue(ContentFrameProperty); }
            set {
                if (ContentFrame != null) {
                    ContentFrame.Navigated -= OnNavigated;
                }
                SetValue(ContentFrameProperty, value);
                value.Navigated += OnNavigated;
                value.NavigateToRoute(Selected.Value, TransitionHelper.Next);
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
            Selected = (Tag)e.AddedItems[0];
            ContentFrame.NavigateToRoute(Selected.Value, TransitionHelper.DecideAnimas(pre, Selected));
        }
    }
}
