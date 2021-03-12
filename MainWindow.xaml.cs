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
    /// /// cobuvbulbh

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
            music_list.Items.MoveCurrentToPosition(0);

            PlayMusic(); 
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if ((music_list.Items.Count > 0) && (mediaPlayer.Source != null))
            {
                Time_Slider.Value = mediaPlayer.Position.TotalSeconds;
                if (mediaPlayer.NaturalDuration.HasTimeSpan)
                {
                    if ((int)mediaPlayer.Position.TotalSeconds >= (int)mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds)
                    {
                        if ((int)music_list.Items.CurrentPosition < ((int)music_list.Items.Count - 1))
                        {
                            move_to_next_song();
                        }
                        else if ((int)music_list.Items.CurrentPosition >= ((int)music_list.Items.Count - 1))
                        {
                            move_to_first_song();
                        }
                    }
                }
            }
        }
       public void move_to_previous_song()
        {
            int previous_position = music_list.Items.CurrentPosition - 1;
            music_list.Items.MoveCurrentToPosition(previous_position);
            Time_Slider.Value = 0;
            PlayMusic();
        }
        public void move_to_first_song()
        {
            music_list.Items.MoveCurrentToPosition(0);
            PlayMusic();
        }

        public void move_to_next_song()
        {
            int next_position = music_list.Items.CurrentPosition + 1;
            music_list.Items.MoveCurrentToPosition(next_position);
            Time_Slider.Value = 0;
            PlayMusic();
        }

        private void Load_music_to_list()
        {
            music_list.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(@"" + Path_Music);
            foreach (var fi in di.GetFiles("*.mp3*"))
            {
                music_list.Items.Add(fi.Name);
            }
        }
        private void PlayMusic()
        {
            if (System.IO.File.Exists(@"" + Path_Music + music_list.Items[music_list.Items.CurrentPosition].ToString()))
            {
                mediaPlayer.Open(new Uri(@"" + Path_Music + music_list.Items[music_list.Items.CurrentPosition].ToString()));
                mediaPlayer.Play();
                while (true)
                {
                    if (mediaPlayer.NaturalDuration.HasTimeSpan)
                    {
                        Time_Slider.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                        break;
                    }
                }
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
            toggle_button_play_stop.Content = "▶️";
            mediaPlayer.Pause();
        }

        private void Toggle_button_play_stop_Unchecked(object sender, RoutedEventArgs e)
        {
            toggle_button_play_stop.Content = "⏸️";
            mediaPlayer.Play();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Time_Slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            mediaPlayer.Position = TimeSpan.FromSeconds(Time_Slider.Value);
            mediaPlayer.Play();

        }

        private void Time_Slider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            mediaPlayer.Pause(); 
        }

        private void Time_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           
        }

        private void Volume_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Volume = (double)Volume_Slider.Value;
        }

        private void Previous_music_button_Click(object sender, RoutedEventArgs e)
        {
            if ((int)music_list.Items.CurrentPosition >= ((int)music_list.Items.Count - 1))
            {
                move_to_previous_song();
            }
            else if ((int)music_list.Items.CurrentPosition < ((int)music_list.Items.Count - 1) || (int)music_list.Items.CurrentPosition > 0)
            {
                move_to_first_song();
            }
            
        }

        private void Stop_music_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Next_music_button_Click(object sender, RoutedEventArgs e)
        {
            if ((int)music_list.Items.CurrentPosition < ((int)music_list.Items.Count - 1))
            {
                move_to_next_song();
            }
            else if ((int)music_list.Items.CurrentPosition >= ((int)music_list.Items.Count - 1))
            {
                move_to_first_song();
            }
            
            
        }

        private void Mix_music_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Expander_playlist_Expanded(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Listbox_playlist_menuitem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Listbox_playlist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Listbox_playlist_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
