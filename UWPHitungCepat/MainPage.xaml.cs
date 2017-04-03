using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPHitungCepat
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public string GetAppVersion()
        {
            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            return string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Revision);
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(View.PlayPage));
        }

        private void cboxLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbox = sender as ComboBox;
            Model.DataPlayer.Level = cbox.SelectedIndex;
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(View.SettingPage));
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msg = new MessageDialog("\n\bHitung Cepat \nVersi: \t\t" + GetAppVersion() + " \nPengembang: \tAhmad Tantowi \nKontak: \t\tahmadtantowi@outlook.com");
            msg.Commands.Add(new UICommand("OK") { Id = 0 });
            msg.Title = "Tentang";
            msg.ShowAsync();
            msg.DefaultCommandIndex = 0;
        }
    }
}
