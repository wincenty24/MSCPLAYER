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
using System.Diagnostics;
namespace MSCPLAYER
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// /// cobuvbulbh

    public partial class MainWindow : Window
    {
        public static string Path_Music = @"C:\muzyka\";
        //MediaPlayer mediaPlayer = new MediaPlayer();//objekt  związany z muzyką 
        DispatcherTimer timer = new DispatcherTimer();
        List<string> all_music_list = new List<string>();
        Sort_TYPE.Sort sort_type = new Sort_TYPE.Sort();
        MP mp = new MP();
        List<PlayList> playlist = new List<PlayList>();
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

            mp.play_music(Path_Music + music_list.Items[music_list.Items.CurrentPosition].ToString(), ref Time_Slider);

        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            try
            {

                mp.load_to_list(ref all_music_list, ref music_list, sort_type , Path_Music);
            }
            catch
            {

            }
            if ((music_list.Items.Count > 0) && (mp.media_player.Source != null))
            {
               // Debug.WriteLine(music_list.Items.CurrentItem);
                //Debug.WriteLine(music_list.Items.CurrentPosition);
                //Debug.WriteLine("=================================================================================");
                mp.assigne_mp_time(Time_Slider);
                //Time_Slider.Value = mediaPlayer.Position.TotalSeconds;
                if (mp.media_player.NaturalDuration.HasTimeSpan)
                {
                    if ((int)mp.media_player.Position.TotalSeconds >= (int)mp.media_player.NaturalDuration.TimeSpan.TotalSeconds)
                    {
                        if ((int)music_list.Items.CurrentPosition < ((int)music_list.Items.Count - 1))
                        {
                            mp.move_to_next_song(ref music_list, ref Time_Slider, Path_Music);
                        }
                        else if ((int)music_list.Items.CurrentPosition >= ((int)music_list.Items.Count - 1))
                        {
                            mp.move_to_first_song(ref music_list, ref Time_Slider, Path_Music + music_list.Items[music_list.Items.CurrentPosition].ToString());
                        }
                    }
                }
            }
        }
        
        private void Load_music_to_list()
        {
            
            sort_type = Sort_TYPE.Sort.BY_ALPHABET_FROM_PATH;
            music_list.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(@"" + Path_Music);
            foreach (var fi in di.GetFiles("*.mp3*"))
            {
                //all_music_list.Add(fi.Name);
                music_list.Items.Add(fi.Name);
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
            string selected_song = music_list.SelectedItem.ToString();

            string selected_playlist;
            try
            {
                selected_playlist = music_list_add_listbox_contextmenu.SelectedItem.ToString();
                for (int i = 0; i < playlist.Count; i++)
                {
                    if (playlist[i].playlist_name == selected_playlist)
                    {
                        Debug.WriteLine($"selected playlis{selected_playlist} {playlist[i].playlist_name}");
                        playlist[i].playlist.Add(selected_song);
                        Debug.WriteLine($"item in a list {playlist[i].playlist.Count}");
                        foreach (string dupa in playlist[i].playlist)
                        {
                            Debug.WriteLine(dupa);
                        }
                    }
                }
            }
            catch
            {

            }
            
            //music_list_add_listbox_contextmenu.SelectedItem = -1;
            music_list_add_listbox_contextmenu.UnselectAll();
            //sprawdzanie która jest pozycja w liscie playlist
            //zrób funkcje




            Debug.WriteLine("end");
        }

        private void Music_list_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }

        private void Music_list_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int selected_index = music_list.SelectedIndex;
            string selected_item = music_list.SelectedItem.ToString();
            mp.play_music(Path_Music+selected_item, ref Time_Slider);
            music_list.Items.MoveCurrentToPosition(selected_index);
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
            mp.media_player.Pause();
        }

        private void Toggle_button_play_stop_Unchecked(object sender, RoutedEventArgs e)
        {
            toggle_button_play_stop.Content = "⏸️";
            mp.media_player.Play();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Time_Slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            mp.media_player.Position = TimeSpan.FromSeconds(Time_Slider.Value);
            mp.media_player.Play();

        }

        private void Time_Slider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            mp.media_player.Pause(); 
        }

        private void Time_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           
        }

        private void Volume_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mp.media_player.Volume = (double)Volume_Slider.Value;
        }

        private void Previous_music_button_Click(object sender, RoutedEventArgs e)
        {
            mp.media_player.Position = TimeSpan.FromSeconds(Time_Slider.Value = 0);
            mp.media_player.Play();
        }
        private void Previous_music_button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
         if ((int)music_list.Items.CurrentPosition > 0)
            {
                mp.move_to_previous_song(ref music_list ,ref Time_Slider, Path_Music);
            }
        }
        private void Stop_music_button_Click(object sender, RoutedEventArgs e)
        {
           
        }
       
        private void Next_music_button_Click(object sender, RoutedEventArgs e)
        {
            if ((int)music_list.Items.CurrentPosition < ((int)music_list.Items.Count - 1))
            {
                mp.move_to_next_song(ref music_list, ref Time_Slider, Path_Music);
            }
            else if ((int)music_list.Items.CurrentPosition >= ((int)music_list.Items.Count - 1))
            {
                mp.move_to_first_song(ref music_list, ref Time_Slider, Path_Music + music_list.Items[music_list.Items.CurrentPosition].ToString());
            }
        }

        private void Mix_music_button_Click(object sender, RoutedEventArgs e)
        {
            mp.mix_music_in_listbox(ref music_list);
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

        private void By_title_sort_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void By_author_sort_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void add_Listbox_playlist_menuitem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void add_Listbox_playlist_menuitem_Click_1(object sender, RoutedEventArgs e)
        {
           //tworzenie nowej playlisty
           //zrób z tego funckje w klasie plylist
            Checker check = new Checker();
            if (plya_list_name_creator_listbox.Text != "" && check.is_playlist_exist(playlist, plya_list_name_creator_listbox.Text))
            {
                //Debug.WriteLine("creating playlist status"+ check.is_playlist_exist(playlist, plya_list_name_creator_listbox.Text));
                playlist.Add(new PlayList(plya_list_name_creator_listbox.Text));
                Listbox_playlist.Items.Add(plya_list_name_creator_listbox.Text);
                music_list_add_listbox_contextmenu.Items.Add(plya_list_name_creator_listbox.Text);
                //playlist[playlist.Count - 1].playlist_name = plya_list_name_creator_listbox.Text;
            }
        }

        private void add_menucontext_musiclist_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
