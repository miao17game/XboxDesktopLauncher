using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Animation;
using XboxDesktopLauncher.Pages;

namespace XboxDesktopLauncher.Utils {
    internal class FrameContext {
        public string Current { get; set; } = null;
    }

    public static class ApplicationRouter {

        readonly static Dictionary<string, Type> routes = new Dictionary<string, Type> {
            {"home" , typeof(HomePage) },
            {"games" , typeof(GamesPage) }
        };

        readonly static Dictionary<Frame, FrameContext> contexts = new Dictionary<Frame, FrameContext> { };

        static Type GetRoute(string tag) {
            return routes[tag ?? "default"] ?? DefaultRoute;
        }

        public static Type DefaultRoute { get { return routes["home"]; } }

        public static bool NavigateTo(Frame frame, string tag, NavigationTransitionInfo transition) {
            return InnerNavigateTo(frame, tag, transition);
        }

        public static bool NavigateToRoute(this Frame frame, string tag, NavigationTransitionInfo transition) {
            return InnerNavigateTo(frame, tag, transition);
        }

        static bool InnerNavigateTo(Frame frame, string tag, NavigationTransitionInfo transition) {
            if (!contexts.TryGetValue(frame, out var context)) {
                context = contexts[frame] = new FrameContext();
            }
            if (context.Current == tag) return false;
            context.Current = tag;
            return frame.NavigateToType(GetRoute(tag), null, new FrameNavigationOptions {
                TransitionInfoOverride = transition
            });
        }

    }
}
