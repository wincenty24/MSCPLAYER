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
using System.Threading;
using System.IO;
using System.Windows.Threading;

namespace MSCPLAYER
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public static string Path_Music = @"C:\muzyka\";
        MediaPlayer mediaPlayer = new MediaPlayer();//objekt  związany z muzyką 
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load_music_to_list();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            PlayMusic(0);
            mediaPlayer.Play();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (music_list.Items.Count > 0)
            {
                if (mediaPlayer.Source != null)
                {

                }
            }
        }
        private void Load_music_to_list()
        {
            music_list.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(@"" + Path_Music);// tworzy obiekt di i otwiera zawartość pliku
            foreach (var fi in di.GetFiles("*.mp3*"))// ,,filtruje" bądź znajduje tylko pliki mp3 w folderze
            {
                music_list.Items.Add(fi.Name);//zapisuje ów przefiltrowane pliki do listy z muzyką
            }
        }
        private void PlayMusic(int index)
        {
            if (System.IO.File.Exists(@"" + Path_Music + music_list.Items[index].ToString()))
            {
              mediaPlayer.Open(new Uri(@"" + Path_Music + music_list.Items[index].ToString()));
            }
            else
            {
                MessageBox.Show("this path does not exist");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Music_list_ItemDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Music_list_add_listbox_contextmenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Music_list_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }

        private void Music_list_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Music_list_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Music_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Toggle_button_play_stop_Checked(object sender, RoutedEventArgs e)
        {
            //comment
            toggle_button_play_stop.Content = "➤";
            mediaPlayer.Pause();
        }

        private void Toggle_button_play_stop_Unchecked(object sender, RoutedEventArgs e)
        {
            toggle_button_play_stop.Content = "||";
            mediaPlayer.Play();
        }
    }
}
