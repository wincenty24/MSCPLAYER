using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows;

namespace MSCPLAYER
{
    class Checker
    {
        public void check_music_in_path(List<string> list_A, ref ListBox musiclist, Sort_TYPE.Sort sort)
        {
             switch (sort)
             {
                case Sort_TYPE.Sort.BY_ALPHABET_FROM_PATH:
                    if(list_A.Count() != musiclist.Items.Count)
                    {                       
                        string current_music = musiclist.Items[musiclist.Items.CurrentPosition].ToString();
                        musiclist.Items.Clear();
                        foreach(string m_name in list_A)
                        {
                            musiclist.Items.Add(m_name);
                        }
                        int current_song_position = musiclist.Items.IndexOf(current_music);
                        musiclist.Items.MoveCurrentToPosition(current_song_position);
                    }
                break;
             }
        }

        public bool is_playlist_exist(List<PlayList> playlists, string name_playlist)
        {
            bool status = false;
            //Debug.WriteLine("playlistname " + name_playlist);
            if (playlists.Count > 0)
            {
                
                for (int index =0; index<playlists.Count; index++)
                {
                    
                    if (playlists[index].playlist_name != name_playlist)
                    {
                       // Debug.WriteLine("status1 " + playlists[index].playlist_name);
                        status = true;
                        
                    }
                    else
                    {
                       // Debug.WriteLine("status2 " + status);
                        status = false;
                    }
                }
            }
            else if (playlists.Count == 0)
            {
                //Debug.WriteLine("status3 " + status);
                status = true;
            }
            else
            {
                //Debug.WriteLine("status4 " + status);
                status = false;
            }
           // Debug.WriteLine("status5 " + status);
            return status;
        }
    }
}
