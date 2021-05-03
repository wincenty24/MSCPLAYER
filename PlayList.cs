using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCPLAYER
{
    class PlayList
    {
       public List<string> playlist = new List<string>();
       public string playlist_name;
    
        public PlayList(string playlist_name)
        {
            this.playlist_name = playlist_name;
        }
       public void Create_PlayList(string playlist_name)
       {
            //playlist_name = this.playlist_name;
       }
    }   
}
