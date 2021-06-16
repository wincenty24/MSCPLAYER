using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using VideoLibrary;
namespace MSCPLAYER
{
   
    class MP
    {

       public MediaPlayer media_player = new MediaPlayer();
       
        public MP()
        {
       
        }
        public bool is_video_downloading = false;
        public void load_to_list(ref List<string> list, ref ListBox musiclist, Sort_TYPE.Sort sort, string music_path)
       {
            DirectoryInfo di = new DirectoryInfo(@"" + music_path);
            list.Clear();
            foreach (var fi in di.GetFiles("*.mp3*"))
            {
                list.Add(fi.Name);
            }
            list.Sort();
            Checker check = new Checker();
            check.check_music_in_path(list, ref musiclist, sort);
       }
        public void play_music(string music_direction, ref Slider slider)
        {
            if (System.IO.File.Exists(@"" + music_direction))
            {
                media_player.Open(new Uri(@"" + music_direction));
                media_player.Play();
                while (true)
                {
                    if (media_player.NaturalDuration.HasTimeSpan)
                    {
                        slider.Maximum = media_player.NaturalDuration.TimeSpan.TotalSeconds;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("this path does not exist");
            }
        }

        public void assigne_mp_time(Slider slider)
        {
            slider.Value = media_player.Position.TotalSeconds;
        }
       
        public void mix_music_in_listbox(ref ListBox listbox)
        {
            if (listbox.Items.Count > 0)
            {
                int curret_pos = listbox.Items.CurrentPosition;
                int count = listbox.Items.Count;
                Random randomiser = new Random();
                object item;

                for (int index = curret_pos + 1; index <= count - 2; index++)
                {
                    item = listbox.Items[randomiser.Next(index, count)];
                    listbox.Items.Remove(item);
                    listbox.Items.Insert(index, item);
                }
            }
        }

        public void move_to_previous_song(ref ListBox listbox, ref Slider slider, string path)
        {
            int previous_position = listbox.Items.CurrentPosition - 1;
            listbox.Items.MoveCurrentToPosition(previous_position);
            slider.Value = 0;
            string current_song = listbox.Items[previous_position].ToString();
            this.play_music(@"" + path + current_song, ref slider);
        }

        public void move_to_first_song(ref ListBox listbox, ref Slider slider, string direction)
        {
            listbox.Items.MoveCurrentToPosition(0);
            slider.Value = 0;
            this.play_music(@"" + direction, ref slider);
        }
        public void move_to_next_song(ref ListBox listbox, ref Slider slider, string path)//path
        {
            int next_position = listbox.Items.CurrentPosition + 1;
            listbox.Items.MoveCurrentToPosition(next_position);
            slider.Value = 0;
            string current_song = listbox.Items[next_position].ToString();
            this.play_music(@"" + path + current_song, ref slider);
        }
        public void download_mp4(string url, string dir_video,string dir_mp3)
        {

            try
            {

                is_video_downloading = true;
                var youTube = YouTube.Default; // starting point for YouTube actions
                var video = youTube.GetVideo(url); // gets a Video object with info about the video
                File.WriteAllBytes($@"{dir_video}" + video.FullName, video.GetBytes());
                try
                {
                    string music_namee = video.FullName.Replace("- YouTube.mp4", "");
                    var convert = new NReco.VideoConverter.FFMpegConverter();
                    convert.ConvertMedia(dir_video + video.FullName, dir_mp3 + music_namee + ".mp3", "mp3");
                    MessageBox.Show("Succeed");
                }
                catch
                {
                    MessageBox.Show("error->convert");
                }

            }
            catch
            {
                MessageBox.Show("Video has not downloaded ");
            }
            finally
            {
                

                is_video_downloading = false;
            }
        }

    }
}
