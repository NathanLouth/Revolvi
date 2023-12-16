using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace Revolvi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool var_refresh = true;
        bool var_rotate = true;
        bool var_inactive = true;
        int var_delay = 60;
        bool var_notRunning = true;
        private CancellationTokenSource _cts;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (DelaySecLabel != null)
            {
                int roundedValue = (int)Math.Round(e.NewValue);
                DelaySecLabel.Content = roundedValue.ToString();
                var_delay = roundedValue;
            }
        }

        private void AboutClicked(object sender, MouseButtonEventArgs e)
        {
            System.Windows.MessageBox.Show("V1.0.1                                 ", "About Revolvi", MessageBoxButton.OK);
        }

        private void Item_Refresh_CheckedChanged(object sender, EventArgs e)
        {
            if (Item_Refresh.IsChecked == true)
            {
                var_refresh = true;
            }
            else
            {
                var_refresh = false;
            }
        }

        private void Item_Rotate_CheckedChanged(object sender, EventArgs e)
        {
            if (Item_Rotate.IsChecked == true)
            {
                var_rotate = true;
            }
            else
            {
                var_rotate = false;
            }

        }

        private void Item_Inactive_CheckedChanged(object sender, EventArgs e)
        {
            if (Item_Inactive.IsChecked == true)
            {
                var_inactive = true;
            }
            else
            {
                var_inactive = false;
            }

        }

        public static async Task BackGroundTask(CancellationToken token, bool Rf, bool Tb, bool Ina, int dly)
        {

            while (!token.IsCancellationRequested)
            {
                if (Ina)
                {
                    if ((IdleTimeFinder.GetIdleTime() / 1000) > (dly - 7))
                    {
                        if (Tb)
                        {
                            SendKeys.SendWait("^{PGDN}");
                        }
                        if (Rf)
                        {
                            SendKeys.SendWait("{F5}");
                        }
                    }
                }
                else
                {
                    if (Tb)
                    {
                        SendKeys.SendWait("^{PGDN}");
                    }
                    if (Rf)
                    {
                        SendKeys.SendWait("{F5}");
                    }

                }

                await Task.Delay(dly * 1000, token);
            }
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {

            if (var_notRunning)
            {
                _cts = new CancellationTokenSource();
                BackGroundTask(_cts.Token, var_refresh, var_rotate, var_inactive, var_delay);
                Item_Refresh.IsEnabled = false;
                Item_Rotate.IsEnabled = false;
                DelaySlider.IsEnabled = false;
                Item_Inactive.IsEnabled = false;
                MainButton.Content = "Stop";
                var_notRunning = false;
            }
            else
            {
                if (_cts != null && !_cts.IsCancellationRequested)
                {
                    _cts.Cancel();
                }
                Item_Refresh.IsEnabled = true;
                Item_Rotate.IsEnabled = true;
                DelaySlider.IsEnabled = true;
                Item_Inactive.IsEnabled = true;
                MainButton.Content = "Start";
                var_notRunning = true;
            }
        }
    }

    internal struct LASTINPUTINFO
    {
        public uint cbSize;

        public uint dwTime;
    }

    public class IdleTimeFinder
    {
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("Kernel32.dll")]
        private static extern uint GetLastError();

        public static uint GetIdleTime()
        {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
            GetLastInputInfo(ref lastInPut);

            return ((uint)Environment.TickCount - lastInPut.dwTime);
        }
        /// <summary>
        /// Get the Last input time in milliseconds
        /// </summary>
        /// <returns></returns>
        public static long GetLastInputTime()
        {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
            if (!GetLastInputInfo(ref lastInPut))
            {
                throw new Exception(GetLastError().ToString());
            }
            return lastInPut.dwTime;
        }
    }
}