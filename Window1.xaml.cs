using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;
using System.Xml;
using System.Media;
using System.Net;
using System.Net.NetworkInformation;
using System.ComponentModel;
 
namespace WPFTaskbarNotifier
{
    /// <summary>
    /// An example window that adds content to the TaskbarNotifier and changes
    /// its settings.
    /// </summary>
    public partial class Window1 : System.Windows.Window
    {
        private bool reallyCloseWindow = false;

        public Window1()
        {
            try
            {
              
                conex_state = GetIsConnAvail();
              
                this.taskbarNotifier = new ExampleTaskbarNotifier();

                InitializeComponent();
             
            }
            catch (Exception e)
            {
                MessageBox.Show(":" + "المرجو ابلاغنا بهذا المشكل " + "\n" +e+ "\n" + "nibrass.dev@gmail.com");
            }
        }

        private ExampleTaskbarNotifier taskbarNotifier;
        Random rnd1 = new Random();
        String conex_state ="no";
        int counter_line = 1;//contour des images
            String title = "100";
            bool is_upted = false;
            String folder = AppDomain.CurrentDomain.BaseDirectory + "azkar";
             DispatcherTimer dispatcherTimer = new DispatcherTimer();


        public ExampleTaskbarNotifier TaskbarNotifier
        {
            get { return this.taskbarNotifier; }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs args)
        {
           
            if (!this.reallyCloseWindow)
            {
                // Don't close, just Hide.
                args.Cancel = true;
                // Trying to hide
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, (DispatcherOperationCallback)delegate(object o)
                {
                    this.Hide();
                    return null;
                }, null);
            }
            else
            {
                // Actually closing window.

                this.NotifyIcon.Visibility = Visibility.Collapsed;

                // Close the taskbar notifier too.
                if (this.taskbarNotifier != null)
                    this.taskbarNotifier.Close();
            }
        }

   
        public int count_file(String folder)
        {
            int fileCount = 1;
              fileCount = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories).Length;
            return fileCount;
        }

        public void Add_Click(object sender, EventArgs e)
        {
            try
            {
              // 
                //MessageBox.Show(slider1.Value.ToString());

                this.taskbarNotifier.Show();
            //    if (radioButton1.IsChecked == true)
                {
                   //
                    this.taskbarNotifier.Large = Convert.ToInt32(slider1.Value);

                    this.taskbarNotifier.Hauteur = Convert.ToInt32(slider2.Value); 
                    //
                   // this.taskbarNotifier.Large = 550;
                    this.taskbarNotifier.NotifyContent.Add(new NotifyObject(AppDomain.CurrentDomain.BaseDirectory + "startup.jpg", "450", "500"));
                   
                }
  
                this.ClearTextBoxes();

                // Tell the TaskbarNotifier to open.
                this.taskbarNotifier.Notify();


                //  this.taskbarNotifier.NotifyContent.Clear();

                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                int sec = 10;
                int h = 0;
                int min = 0;
                sec = int.Parse(textBox4.Text);
                min = int.Parse(textBox5.Text);
                h = int.Parse(textBox6.Text);
                dispatcherTimer.Interval = new TimeSpan(0, h, min, sec, 0);
                // Use TimeSpan constructor to specify: days, hours, minutes, seconds, milliseconds.
                // ... The TimeSpan returned has those values.
         
                dispatcherTimer.Start();
                this.Close();    
            
            }
            catch (Exception f)
            {
                MessageBox.Show(":" + "المرجو ابلاغنا بهذا المشكل " + "\n" + f + "\n" + "nibrass.dev@gmail.com");
            }
        
            //dispatcherTimer.Interval.
        }

        private void ClearTextBoxes()
        {
            //this.TitleTextBox.Text = string.Empty;
            //  this.MessageTextBox.Text = string.Empty;
        }

        private void ClearAll_Click(object sender, EventArgs e)
        {
            // Clear all content in the TaskbarNotifier
            this.taskbarNotifier.NotifyContent.Clear();
        }

        private void NotifyIcon_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                // Open the TaskbarNotifier
                this.taskbarNotifier.Notify();
            }
        }

        private void NotifyIconOpen_Click(object sender, RoutedEventArgs e)
        {
            // Open the TaskbarNotifier
            this.taskbarNotifier.Notify();
        }

        private void NotifyIconConfigure_Click(object sender, RoutedEventArgs e)
        {
            // Show this window
            this.Show();
            this.Activate();
        }

        private void NotifyIconExit_Click(object sender, RoutedEventArgs e)
        {
            // Close this window.
            this.reallyCloseWindow = true;
            this.Close();
        
        }

      
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ThisControl_Loaded(object sender, RoutedEventArgs e)
        {

           
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

       public  String folder_target()
        {
            String f = AppDomain.CurrentDomain.BaseDirectory;
           string folder=f+"azkar"; 
            if(this.comboBox1.SelectedIndex ==0)
                  //title = "ذِكـــر";  
                folder=f+"azkar";
                else if(this.comboBox1.SelectedIndex ==1)
                folder = f+"melange";   //title= " دعـــــــاء" ;
                else if (this.comboBox1.SelectedIndex == 2)
                               //title = "حديــــــــث";
            folder = f+"chiear";
                 else if (this.comboBox1.SelectedIndex == 3)
                folder = f+"ahadith";

           // MessageBox.Show(f);
            return folder;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            folder = folder_target();
            string image_name;
            //
            int    max= count_file(folder);;
          //  MessageBox.Show(image_name);
            if (this.comboBox3.SelectedIndex == 0)
            {
           
                counter_line = rnd1.Next(1, max);
                  image_name = folder + "\\" + counter_line + ".jpg";
         
 
                    if (this.comboBox2.SelectedIndex == 0)
                    {
                        
                        SoundPlayer simpleSound = new SoundPlayer(WPFTaskbarNotifier.Properties.Resources.ir_inter);
                        simpleSound.Play();
                    }
                    this.taskbarNotifier.NotifyContent.Clear();


                 //   if (radioButton1.IsChecked == true)
                    {
                        this.taskbarNotifier.Large = Convert.ToInt32(slider1.Value);

                        this.taskbarNotifier.Hauteur = Convert.ToInt32(slider2.Value); 
                        //
                        this.taskbarNotifier.NotifyContent.Add(new NotifyObject(image_name, "350", "500"));
                      
                    }
              
               
                    this.ClearTextBoxes();
 
                    // Tell the TaskbarNotifier to open.
                    this.taskbarNotifier.Notify();
 
            }
            else
            {

                  image_name = folder + "\\" + counter_line + ".jpg";
                if (counter_line <= count_file(folder))
                {
                    if (this.comboBox2.SelectedIndex == 0)
                    {
                        SoundPlayer simpleSound = new SoundPlayer(WPFTaskbarNotifier.Properties.Resources.ir_inter);
                        simpleSound.Play();
                    }
                    this.taskbarNotifier.NotifyContent.Clear();

                    {
                        this.taskbarNotifier.Large = Convert.ToInt32(slider1.Value);

                        this.taskbarNotifier.Hauteur = Convert.ToInt32(slider2.Value);
                        //
                        this.taskbarNotifier.NotifyContent.Add(new NotifyObject(image_name, "350", "500"));

                    }   
                 
                    // Clear the textboxes.
                    this.ClearTextBoxes();

                    // Tell the TaskbarNotifier to open.
                    this.taskbarNotifier.Notify();


                    counter_line++;

                }
                else
                {
                    MessageBox.Show("إنتهى التذكير");
                    dispatcherTimer.Stop();
                    counter_line = 1;
                }
            }
        }

      
        public void send(String s1)
        {
            string url = "http://almoundir.zzl.org/target.php";
            string data =s1;

            using (WebClient client = new WebClient())
            {
                client.UploadString(url, data);
            }
       
        }
        public static String read()
        {
          
            string url = "http://nibrass.zzl.org/target.php";
            string responseFromServer = "مشكل في الاتصال ";
          
                WebRequest request = WebRequest.Create(url);
                Stream dataStream;
                WebResponse response;
                StreamReader reader;





                response = request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                reader.Close();

                dataStream.Close();

                response.Close();
                if (responseFromServer == null)
                    responseFromServer = "error";
         
         return responseFromServer;
          
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            label14.Content = "تم الإرسال شكرا ";
            try
            {
                send(textBox1.Text + "\n" + textBox2.Text);
            }
            catch (Exception )
            {
                label14.Content = "مشكل في الاتصال ";
            }
            
        }





        private void button1_Click(object sender, RoutedEventArgs e)
        {  
       String s = "1";
            string ver = "1";
            int vv = 0;
            int ss = 0;
         try{  
             
             s = read().Trim();
       ver=version().Trim();
       vv = Int32.Parse(ver);
       ss = Int32.Parse(s);

           //    
             if ( vv==ss)
             {
                 
                 label15.Content = "لا يوجد تحديث لهذه النسخة";
             
             }
             else
             {
                 button1.IsEnabled = false;
                 //
                 progressBar1.Visibility = Visibility.Visible;
                 down(0, "f");
         
             }
         }
         catch (Exception  )
         {
             MessageBox.Show("مشكل في الاتصال ");
         } 
        }



      

        String version()
        {
            string text = System.IO.File.ReadAllText(@"version.txt");
            return text;
        }

        void down(int i, string flder)
        {

            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

            string url = @"http://nibrass.zzl.org/update.zip";
            // Starts the download
            tabItem1.BorderBrush = Brushes.Red;
            client.DownloadFileAsync(new Uri(url), @"update\update.zip");

            label18.Content = "جاري التحميل ... ";

        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = (bytesIn / (totalBytes )) * 100;

            progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                Zipo.UnZipTo(AppDomain.CurrentDomain.BaseDirectory + "\\update\\update.zip", AppDomain.CurrentDomain.BaseDirectory + "\\update\\");

                String f =AppDomain.CurrentDomain.BaseDirectory;



                // 
                Copier.move_pic(f + "update\\azkar", f + "azkar", count_file(f + "azkar"));

                // 
                Copier.move_pic(f + "update\\ahadith", f + "ahadith", count_file(f + "ahadith"));
                Copier.move_pic(f + "update\\melange", f + "melange", count_file(f + "melange"));
                //
                Copier.move_pic(f + "update\\chiear", f + "chiear", count_file(f + "chiear"));
                Copier.move_exe_dll(f + "update", f);

                
                
                
                
                
                label18.Content = "إنتهـــاء التحديث";
            button1.IsEnabled = true;
            //System.Diagnostics.Process.Start("update.bat");
          
         //  MessageBox.Show("سيتم إعادة تشغيل البرنامج لتأكيد التحديث");

           // Close this window.
            is_upted = true;
           this.reallyCloseWindow = true;
           this.Close();
           if (is_upted)
           {
               TextWriter tw = new StreamWriter("version.txt");

               // write a line of text to the file
               tw.WriteLine(Int32.Parse(read().Trim()));

               // close the stream
               tw.Close();
           }
           if (File.Exists("Nibrass_new.exe"))
          System.Diagnostics.Process.Start("Remplace.exe");
            }
            catch (IOException) {
                is_upted = false;
                MessageBox.Show("فشل التحديث"); }  //  down(0, "f");

          
        }

     
        public static String GetIsConnAvail()
        {
            String success = "no";
            string StatusMsg = "wlo";
            if (NetworkInterface.GetIsNetworkAvailable() == false)
            {
                return success;
                
            }
            string Mysite = "nibrass.zzl.org";
            try
            {
                using (var ping = new Ping())
                {

                    var replyMsg = ping.Send(Mysite, 3500);//tmp de atent repns de ping
                    if (replyMsg.Status == IPStatus.Success)
                    {
                        success = "yes";
                        StatusMsg = "Connection Found...";
                       
                        //break;
                    }
                    else
                    {
                        StatusMsg = "Internet Connection not Found on your machine...";
                    }


                }
            }

            catch (Exception ex)
            {
                //Trace.WriteLine(ex.Message);
            }
            return success;
        }

   
       public void frame_page()
       {
           
           
       if (conex_state == "yes")
       {
           Uri u = new Uri("http://nibrass.zzl.org/f.html");
           //"206.254.25.36//f.html";
           frame1.Source = u;
           label1.Content = "جاري الإتصال";//
           button3.Visibility = Visibility.Hidden;
       }
       else
       {

           label1.Content = "  فشل الإتصال بالموقع";//
           button3.Visibility=Visibility.Visible;
       }
       }

 private void ThisControl_Loaded1(object sender, RoutedEventArgs e)
        {
            try
            {
                frame_page();
               
            }
            catch (Exception g)
            {
                //MessageBox.Show("فشل الإتصال بالموقع"); //
                label1.Content = "مشكل في الاتصال";//
               
            }
        }

 private void button3_Click(object sender, RoutedEventArgs e)
 {
     frame_page();
 }

 private void tabItem1_Loaded(object sender, RoutedEventArgs e)
 { 
     if (conex_state == "yes")
     {
         try
         {
             string s = read().Trim();
             string ver = version().Trim();
             int vv = Int32.Parse(ver);
             int ss = Int32.Parse(s);
             if (vv != ss)
             {
                 tabItem1.Background = Brushes.Tomato;
                 label15.Content = "يوجد تحديث للبرنامج :)";
             }
         }
         catch
         {
             label14.Content = "مشكل في الاتصال ";

         }
     }
 }

 
 





    }




}