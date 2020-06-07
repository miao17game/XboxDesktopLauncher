using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XboxDesktopLauncher.Utils {

    public class GlobalTipChangeEvent {
        public bool Show;
        public string Title;
        public string Message;
    }

    public static class Notification {

        public static event EventHandler<GlobalTipChangeEvent> OnTipChanged;

        public static void ShowTip(string title, string message) {
            OnTipChanged?.Invoke(null, new GlobalTipChangeEvent {
                Title = title ?? "",
                Message = message ?? "",
                Show = true,
            });
        }

        public static void HideTip() {
            OnTipChanged?.Invoke(null, new GlobalTipChangeEvent {
                Title = "",
                Message = "",
                Show = false,
            });
        }

    }
}
