using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Colors = Windows.UI.Colors;
using TitleHelper = XboxDesktopLauncher.Utils.TitleBarHelper;

namespace XboxDesktopLauncher {

    sealed partial class App : Application {

        public App() {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e) {
            // 不要在窗口已包含内容时重复应用程序初始化，只需确保窗口处于活动状态
            if (!(Window.Current.Content is Frame rootFrame)) {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;
                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated) {
                    //TODO: 从之前挂起的应用程序加载状态
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false) {
                if (rootFrame.Content == null) {
                    // 当导航堆栈尚未还原时，导航到第一页，并通过将所需信息作为导航参数传入来配置参数
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // 确保当前窗口处于活动状态
                Window.Current.Activate();
            }

            UseCustomNavigationTitleBar();
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e) {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e) {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: 保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }

        private void UseCustomNavigationTitleBar() {
            var titleBar = TitleHelper.UseExtendTitleBar();

            // Set active window colors
            titleBar.ForegroundColor = Colors.White;
            titleBar.BackgroundColor = Colors.Transparent;
            titleBar.ButtonForegroundColor = Colors.White;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonHoverForegroundColor = Colors.White;
            titleBar.ButtonHoverBackgroundColor = TitleHelper.GetThemeColor("Accent");
            titleBar.ButtonPressedForegroundColor = Colors.White;
            titleBar.ButtonPressedBackgroundColor = TitleHelper.GetThemeColor("Accent", "Dark2");

            // Set inactive window colors
            titleBar.InactiveForegroundColor = Colors.Gray;
            titleBar.InactiveBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveForegroundColor = Colors.Gray;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }
    }

}
