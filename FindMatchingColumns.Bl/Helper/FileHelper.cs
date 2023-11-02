using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FindMatchingColumns.BL.Helper
{
    public static class FileHelper
    {
        public static FileStream CreateOrAppendFileStream(string path,FileCreatingOptions option)
        {
            if (option==FileCreatingOptions.Create) 
            {
                return new FileStream(path, FileMode.CreateNew);
            }
            else if (option==FileCreatingOptions.Append)
            {
                return new FileStream(path, FileMode.Append);
            }
            return null;
        }
        
        public static void CloseFileStream(FileStream file) 
        {
            file.Close();
        }
        public static StreamWriter CreateStreamWriter(FileStream file)
        {
            return new StreamWriter(file);
        }
       
        public static void CloseStreamWriter(StreamWriter writer)
        {
            writer.Close();
        }

        public static void CloseFileStreamAndStreamWriter(FileStream file,StreamWriter writer)
        {
            writer.Close();
            file.Close();
        }
    }
}
