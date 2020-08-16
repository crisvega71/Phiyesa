using System;
using System.Collections.Generic;
using System.Text;

namespace MiscellClassLib
{
    public class FileUtility
    {
        public bool ValidImageExtension(string file_ext)
        {
            if (file_ext == ".jpg" || file_ext == ".png")
            {
                return true;
            }
            else
            {   return false; }
        } //--
    }
}
