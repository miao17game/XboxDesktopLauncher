using System;
using System.Linq;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Animation;
using XboxDesktopLauncher.Pages;

namespace XboxDesktopLauncher.Utils {
    internal class FrameContext {
        public RouteType? Current { get; set; } = null;
    }

    public class RouteData {
        public string Text;
        public Type Page;
        public RouteData() { }
    }

    public class RouteTag : RouteData {
        public RouteType Type;
        public RouteTag(RouteType type, RouteData data) {
            Type = type;
            Text = data.Text;
            Page = data.Page;
        }
    }

    public enum RouteType {
        Default = 0,
        Home = 1,
        Games = 2
    }

    public static class ApplicationRouter {

        readonly static IDictionary<RouteType, RouteData> tags = new Dictionary<RouteType, RouteData> {
            {RouteType.Home , new RouteData { Text = "Home", Page = typeof(HomePage) } },
            {RouteType.Games , new RouteData { Text = "Games", Page = typeof(GamesPage) } }
        };

        public static IList<RouteTag> Tags = tags.Keys.Select(k => new RouteTag(k, tags[k])).ToList();

        readonly static IDictionary<Frame, FrameContext> contexts = new Dictionary<Frame, FrameContext> { };

        static Type GetRoute(RouteType tag) {
            if (!tags.TryGetValue(tag, out var matched)) {
                return DefaultRoute;
            }
            return matched.Page;
        }

        public static Type DefaultRoute { get { return tags[RouteType.Home].Page; } }

        public static bool NavigateTo(Frame frame, RouteType tag, NavigationTransitionInfo transition) {
            return InnerNavigateTo(frame, tag, transition);
        }

        public static bool NavigateToRoute(this Frame frame, RouteType tag, NavigationTransitionInfo transition) {
            return InnerNavigateTo(frame, tag, transition);
        }

        static bool InnerNavigateTo(Frame frame, RouteType tag, NavigationTransitionInfo transition) {
            if (!contexts.TryGetValue(frame, out var context)) {
                context = contexts[frame] = new FrameContext();
            }
            if (context.Current == tag) return false;
            context.Current = tag;
            return frame.NavigateToType(GetRoute(tag), null, new FrameNavigationOptions {
                TransitionInfoOverride = transition ?? TransitionHelper.DefaultSlide
            });
        }

    }
}
