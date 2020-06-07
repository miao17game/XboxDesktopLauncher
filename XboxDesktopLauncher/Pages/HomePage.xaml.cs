using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Management.Deployment;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using XboxDesktopLauncher.Utils;

namespace XboxDesktopLauncher.Pages {

    public class WinPackage {
        public string Logo;
        public string Name;
    }

    public sealed partial class HomePage : Page {

        private readonly PackageManager manager = new PackageManager();
        private bool hasBroadFileSystemAccess = false;

        public ObservableCollection<WinPackage> Packages = new ObservableCollection<WinPackage> { };

        public HomePage() {
            NavigationCacheMode = NavigationCacheMode.Enabled;
            InitializeComponent();
            InitPageData();
        }

        private Task LoadPackages() {
            return Task.Run(async () => {
                var packages = manager.FindPackagesForUser("")
                    .OrderBy((p) => p.DisplayName)
                    .Where(p =>
                        p.SignatureKind == PackageSignatureKind.Store &&
                        !p.IsDevelopmentMode &&
                        !p.IsResourcePackage &&
                        !p.Status.NotAvailable)
                    .Select(pack => new WinPackage {
                        Name = pack.DisplayName ?? pack.Id.Name,
                        Logo = pack.Logo.AbsoluteUri
                    })
                    .ToList();
                await UI.UpdateAsync(() => {
                    Packages.Clear();
                    foreach (var package in packages) {
                        Packages.Add(package);
                    }
                });
            });
        }

        private async void InitPageData() {
            hasBroadFileSystemAccess = await CapabilityHelper.CheckBroadFileSystemAccess();
            if (!hasBroadFileSystemAccess) CapabilityHelper.SubmitBroadFileSystemAccess();
            await LoadPackages();
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e) {
            if (!hasBroadFileSystemAccess) return;
            FixImageLoadFailed((Image)sender, ((BitmapImage)((Image)sender).Source).UriSource.AbsoluteUri);
        }

        private void Image_ImageOpened(object sender, RoutedEventArgs e) {

        }

        private async void FixImageLoadFailed(Image image, string path) {
            var file = await StorageFile.GetFileFromPathAsync(path);
            using (var stream = await file.OpenAsync(FileAccessMode.Read)) {
                var bitmap = new BitmapImage();
                bitmap.SetSource(stream);
                image.Source = bitmap;
            }
        }
    }
}
