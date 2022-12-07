using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace WIFIER
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string password;
        public string Hot_spot_name;

        host_model em;



        public object FormWindowState { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            load_creds();
            btn__1.Visibility = Visibility.Hidden;
            btn__2.Visibility = Visibility.Hidden;

            em = new host_model();
            turn_off();


            check_state();

        }



        public void check_state()
        {
            show_hosts();
            Regex rx = new Regex(" Status                 : Not started");

            if (rx.IsMatch(txt_myinput.Text))
            {
                turn_off();
            }


            Regex rxt = new Regex(" Status                 : Started");

            if (rxt.IsMatch(txt_myinput.Text))
            {
                turn_on();

            }

        }

        public bool return_state_going_off()
        {
            Regex rxt = new Regex(" Status                 : Started");

            return rxt.IsMatch(txt_myinput.Text);

        }





        public void turn_on()
        {
            img_state_off.Visibility = Visibility.Hidden;
            img_state_on.Visibility = Visibility.Visible;

        }

        public void turn_off()
        {
            img_state_off.Visibility = Visibility.Visible;
            img_state_on.Visibility = Visibility.Hidden;

        }



        public void save_creds(string name, string password)
        {


            em.setname(name);
            em.setpasssword(password);

            Stream output;
            BinaryFormatter bf = new BinaryFormatter();
            output = File.Open("host.ken", FileMode.OpenOrCreate);

            try
            {
                bf.Serialize(output, em);

                ///       MessageBox.Show("SEALISATION SUCCESSFUL");

            }
            catch (SerializationException)
            {
                //  MessageBox.Show("SEALISATION FAILED" + ex.Message);


            }

            output.Close();



        }


        public void load_creds()
        {

            FileStream input;
            BinaryFormatter bf = new BinaryFormatter();
            input = File.Open("host.ken", FileMode.OpenOrCreate);

            try
            {
                em = (host_model)bf.Deserialize(input);
                txt_hotspotname.Text = em.get_name_;
                txt_hotspote_password.Text = em.get_pass_;

            }
            catch (SerializationException)
            {
                //      MessageBox.Show("DESEALISATION FAILED" + ex.Message);

            }
            input.Close();



        }







        private void Pressme_Click(object sender, RoutedEventArgs e)
        {

            if (txt_hotspote_password.Text != "" && txt_hotspotname.Text != "")
            {

                password = txt_hotspote_password.Text.ToString();
                Hot_spot_name = txt_hotspotname.Text.ToString();

                save_creds(Hot_spot_name, password);

                Process process = new Process();
                process.StartInfo.FileName = "powershell.exe";
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                process.StandardInput.WriteLine("netsh wlan set hostednetwork mode = allow  ssid = " + Hot_spot_name + " KEY = " + password + "");
                process.StandardInput.WriteLine("NETSH WLAN START HOSTEDNETWORK");
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();
                // Console.WriteLine(process.StandardOutput.ReadToEnd());
                // Console.ReadKey();
                txt_myinput.Text = process.StandardOutput.ReadToEnd();
                //pressme.Background = Color.;
                btn__1.Visibility = Visibility.Hidden;
                btn__2.Visibility = Visibility.Hidden;
                check_state();
                //  turn_on();
                //    set_on();

            }
            else
            {
                if (txt_hotspote_password.Text == "")
                {
                    btn__2.Visibility = Visibility.Visible;
                    txt_hotspote_password.Focus();
                }
                else
                {

                    btn__2.Visibility = Visibility.Hidden;
                }

                if (txt_hotspotname.Text == "")
                {

                    btn__1.Visibility = Visibility.Visible;
                    txt_hotspotname.Focus();
                }
                else
                {
                    btn__1.Visibility = Visibility.Hidden;
                }

            }


        }

        private void Txt_myinput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            kill_hotspot();

        }

        public void kill_hotspot()
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.StandardInput.WriteLine("NETSH WLAN STOP HOSTEDNETWORK");
            process.StandardInput.Flush();
            process.StandardInput.Close();
            process.WaitForExit();
            // Console.WriteLine(process.StandardOutput.ReadToEnd());
            // Console.ReadKey();
            txt_myinput.Text = process.StandardOutput.ReadToEnd();
            check_state();
            //  turn_off();
            //   set_off();

        }







        private void Cmd_show_hosts1_Click(object sender, RoutedEventArgs e)
        {

            show_hosts();

        }

        public void show_hosts()
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.StandardInput.WriteLine("NETSH WLAN Show HOSTEDNETWORK");
            process.StandardInput.Flush();
            process.StandardInput.Close();
            process.WaitForExit();
            // Console.WriteLine(process.StandardOutput.ReadToEnd());
            // Console.ReadKey();
            txt_myinput.Text = process.StandardOutput.ReadToEnd();


        }




        private void minimise(object sender, RoutedEventArgs e)
        {


            this.WindowState = WindowState.Minimized;



        }

        private void when_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (return_state_going_off())
            {
                MessageBoxResult result = MessageBox.Show("You seem to have left your hotspot ON , Do you wish to turn it OFF ? ", "Hey Buddy!!!", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        kill_hotspot();
                        break;
                    case MessageBoxResult.No:

                        break;

                }
            }


        }
    }
}
