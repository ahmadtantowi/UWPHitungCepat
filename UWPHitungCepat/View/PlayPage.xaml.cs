using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.Phone.Devices.Notification;
using Windows.Foundation.Metadata;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPHitungCepat.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayPage : Page
    {
        int val1 = 0, val2 = 0, result = 0;
        char[] opr = { '+', '-', 'x', '/', '?' };
        bool isEnd = false;
        int level = Model.DataPlayer.Level;

        Random rnd = new Random();

        public PlayPage()
        {
            this.InitializeComponent();
            Model.DataPlayer.MyScore = 0;
            Model.DataPlayer.TotalQuestion = 0;

            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();

            GetQuestion();
            //StartTimer();
            StartCountdownHealth(new TimeSpan(0,0,0,0,1));
            labelAnswer.Focus(FocusState.Keyboard);
        }

        async void StartTimer()
        {
            int sec = 0, min = 0;
            while (!isEnd)
            {
                labelTime.Text = (sec++).ToString();
                await Task.Delay(1000);
            }
        }

        async void StartCountdownHealth(TimeSpan interval)
        {
            while ((int)prgHealth.Value >= 0 && isEnd == false)
            {
                prgHealth.Value = (double)prgHealth.Value - 0.05;   // value 0.05
                await Task.Delay(interval);

                if (prgHealth.Value == 0)
                {
                    isEnd = true;
                    EndGame();
                }
            }
        }

        void GetQuestion()
        {
            // level 0=mudah, 1=sedang, 2=sulit. ditambah dua untuk ambil operator (opr) aritmatika secara acak
            opr[4] = (char)opr.GetValue(rnd.Next(0, level + 2));

            if (opr[4] == '+')
            {
                val1 = rnd.Next(1, 100);
                val2 = rnd.Next(1, 100);
                result = val1 + val2;
            }
            else if (opr[4] == '-')
            {
                val1 = rnd.Next(1, 100);
                val2 = rnd.Next(1, val1 + 1);
                result = val1 - val2;
            }
            else if (opr[4] == 'x')
            {
                val1 = rnd.Next(1, 100);
                val2 = rnd.Next(1, 10);
                result = val1 * val2;
            }
            else if (opr[4] == '/')
            {
                val1 = rnd.Next(1, 100);
                val2 = rnd.Next(1, 10);
                result = val1 / val2;
            }
            Model.DataPlayer.TotalQuestion++;
            Debug.WriteLine("Pertanyaan ke-{0} ", Model.DataPlayer.TotalQuestion);

            labelQuest.Text = val1.ToString() + " " + opr[4] + " " + val2.ToString();
            labelAnswer.Text = "";
            labelAnswer.Focus(FocusState.Keyboard);
        }

        void GetResult()
        {
            float currentHealth = (float)prgHealth.Value;
            var settings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            if (labelAnswer.Text == result.ToString())
            {
                prgHealth.Value = currentHealth + 10;
                Model.DataPlayer.MyScore++;
            }
            else
            {
                // jika perangkat merupakan smartphone
                if ((string)settings.Values["currentVibrate"] == "on" && ApiInformation.IsTypePresent("Windows.Phone.Devices.Notification.VibrationDevice"))
                    VibrationDevice.GetDefault().Vibrate(TimeSpan.FromMilliseconds(300));
            }
            Debug.WriteLine("Skor sementara {0}", Model.DataPlayer.MyScore);

            GetQuestion();
        }

        async void EndGame()
        {
            isEnd = true;
            var settings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            //MessageDialog msg = new MessageDialog("Permainan berakhir. Skor Anda: " + Model.DataPlayer.MyScore);
            //msg.Commands.Add(new UICommand("Yes") { Id = 0});

            //var result = await msg.ShowAsync();
            //msg.DefaultCommandIndex = 0;

            //if((int) result.Id == 0)
            //{

            //}

            if ((string)settings.Values["currentVibrate"] == "on" && ApiInformation.IsTypePresent("Windows.Phone.Devices.Notification.VibrationDevice"))
                VibrationDevice.GetDefault().Vibrate(TimeSpan.FromMilliseconds(1000));
            pagePlay.IsEnabled = false;
            await Task.Delay(2000);

            try
            {
                this.Frame.Navigate(typeof(ResultPage));
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            } 
        }

        private void labelAnswer_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter)
            {
                btnOK_Click(sender, e);
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            labelAnswer.Text += btn.Content.ToString();
            labelAnswer.Focus(FocusState.Keyboard);
        }

        private void btnGiveUp_Click(object sender, RoutedEventArgs e)
        {
            EndGame();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            GetResult();
            labelAnswer.Text = "";
            labelAnswer.Focus(FocusState.Keyboard);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            labelAnswer.Text = "";
            labelAnswer.Focus(FocusState.Keyboard);
        }
    }
}
