using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using pages = XboxDesktopLauncher.Pages;

namespace XboxDesktopLauncher.Utils {
    public static class ApplicationRouter {

        public static Dictionary<string, Type> routes = new Dictionary<string, Type> {
            {"home" , typeof(pages.HomePage) },
            {"games" , typeof(pages.GamesPage) }
        };

        public static Type DefaultRoute { get { return routes["home"]; } }

        public static Type GetRoute(string tag) {
            return routes[tag ?? "default"] ?? DefaultRoute;
        }

    }
}
