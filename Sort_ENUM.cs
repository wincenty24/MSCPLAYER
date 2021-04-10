using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCPLAYER
{
    class Sort_TYPE
    {
        public enum Sort
        {
            BY_TITLE,
            BY_AUTHOR,
            MIX_FROM_PATH,
            BY_ALPHABET_FROM_PATH,
            OTHER
        }

        public string ToSTR(Sort sort)
        {
            switch (sort)
            {
                case Sort.BY_TITLE:
                    return "BY_TITLE";
                case Sort.BY_AUTHOR:
                    return "BY_AUTHOR";
                case Sort.MIX_FROM_PATH:
                    return "MIX_FROM_PATH";
                case Sort.BY_ALPHABET_FROM_PATH:
                    return "FROM_PATH";
                default:
                    return "OTHER";
            }
        }

        public void toSort(ref Sort sort ,string sort_name)
        {
            switch (sort_name)
            {
                case "BY_TITLE":
                    sort = Sort.BY_TITLE;
                    break;
                case "BY_AUTHOR":
                    sort = Sort.BY_AUTHOR;
                    break;
                case "MIX_FROM_PATH":
                    sort = Sort.MIX_FROM_PATH;
                    break;
                case "BY_ALPHABET_FROM_PATH":
                    sort = Sort.BY_ALPHABET_FROM_PATH;
                    break;
                case "OTHER":
                    sort = Sort.OTHER;
                    break;
            }
        }
        
    }
}
