using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml.Controls;

namespace XboxDesktopLauncher.Utils {
    public static class CapabilityHelper {

        public static async Task<bool> CheckBroadFileSystemAccess() {
            try {
                StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(@"C:\");
                return true;
            } catch {
                return false;
            }
        }

        public static async void SubmitBroadFileSystemAccess() {
            var result = await new ContentDialog {
                Title = "需要授权完整的文件系统访问权限",
                Content = "将自动为你打开文件系统权限管理设置，请找到当前App并为其打开文件系统访问权限，并重启本应用。",
                PrimaryButtonText = "好的",
                CloseButtonText = "算了"
            }.ShowAsync();
            if (result == ContentDialogResult.Primary) {
                await Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-broadfilesystemaccess"));
            } else {
                Notification.ShowTip("你拒绝了文件系统访问的申请", "部分功能可能无法正常使用。");
            }
        }

    }
}
