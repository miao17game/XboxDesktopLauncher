using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

namespace XboxDesktopLauncher.Utils {

    public static class TransitionHelper {
        public readonly static SlideNavigationTransitionInfo SlideNext = new SlideNavigationTransitionInfo {
            Effect = SlideNavigationTransitionEffect.FromRight
        };

        public readonly static SlideNavigationTransitionInfo SlidePrevious = new SlideNavigationTransitionInfo {
            Effect = SlideNavigationTransitionEffect.FromLeft
        };

        public static SlideNavigationTransitionInfo DefaultSlide { get { return SlideNext; } }

        public static SlideNavigationTransitionInfo DecideSlideAnimation(RouteTag pre, RouteTag current) {
            return pre.Type > current.Type ? SlidePrevious : SlideNext;
        }
    }
}
