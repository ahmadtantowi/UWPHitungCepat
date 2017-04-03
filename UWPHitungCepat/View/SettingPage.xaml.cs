using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPHitungCepat.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        string currentBgm = null;
        string currentSfx = null;
        string currentVibrate = null;
        string currentTheme = null;

        public SettingPage()
        {
            this.InitializeComponent();
            toggleVibrate.IsEnabled = ApiInformation.IsTypePresent("Windows.Phone.Devices.Notification.VibrationDevice");

            var settings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            currentBgm = settings.Values["currentBgm"] as string;
            currentSfx = settings.Values["currentSfx"] as string;
            currentVibrate = settings.Values["currentVibrate"] as string;
            currentTheme = settings.Values["currentTheme"] as string;

            toggleBgMusic.IsOn = currentBgm == "on" ? true : false;
            toggleSfx.IsOn = currentSfx == "on" ? true : false;
            toggleVibrate.IsOn = currentVibrate == "on" ? true : false;
            toggleTheme.IsOn = currentTheme == "dark" ? true : false;

            Debug.WriteLine("Background music: " + currentBgm + " | Sound effect: " + currentSfx + " | Vibrate: " + currentVibrate + " | Theme: " + currentTheme);
        }

        private void toggleBgMusic_Toggled(object sender, RoutedEventArgs e)
        {
            var settings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            if (settings.Values.ContainsKey("currentBgm"))
            {
                currentBgm = settings.Values["currentBgm"] as string;
                settings.Values.Remove("currentBgm");
            }
            settings.Values["currentBgm"] = toggleBgMusic.IsOn ? "on" : "off";

            Debug.WriteLine("Background music: " + settings.Values["currentBgm"] as string);
        }

        private void toggleSfx_Toggled(object sender, RoutedEventArgs e)
        {
            var settings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            if (settings.Values.ContainsKey("currentSfx"))
            {
                currentSfx = settings.Values["currentSfx"] as string;
                settings.Values.Remove("currentSfx");
            }
            settings.Values["currentSfx"] = toggleSfx.IsOn ? "on" : "off";

            Debug.WriteLine("Sound effect: " + settings.Values["currentSfx"] as string);
        }

        private void toggleVibrate_Toggled(object sender, RoutedEventArgs e)
        {
            var settings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            if (settings.Values.ContainsKey("currentVibrate"))
            {
                currentVibrate = settings.Values["currentVibrate"] as string;
                settings.Values.Remove("currentVibrate");
            }
            settings.Values["currentVibrate"] = toggleVibrate.IsOn ? "on" : "off";

            Debug.WriteLine("Vibrate: " + settings.Values["currentVibrate"] as string);
        }

        private void toggleTheme_Toggled(object sender, RoutedEventArgs e)
        {
            var settings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            if (settings.Values.ContainsKey("currentTheme"))
            {
                currentTheme = settings.Values["currentTheme"] as string;
                settings.Values.Remove("currentTheme");
            }
            settings.Values["currentTheme"] = toggleTheme.IsOn ? "dark" : "light";

            Debug.WriteLine("Theme: " + settings.Values["currentTheme"] as string);
            labelNotification.Text = "Mengubah tema membutuhkan memulai ulang aplikasi";
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
