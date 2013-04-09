using System;
using System.IO;

namespace Kids.Utility.Excell
{

    public class MSWordUtiliy
    {

        private static void DeleteOldFiles(string dirPath)
        {


            DirectoryInfo dinfFull = new DirectoryInfo(dirPath);
            if (dinfFull.Parent != null)
            {
                DirectoryInfo dinf = new DirectoryInfo(dinfFull.Parent.FullName);
                foreach (FileInfo f in dinf.GetFiles())
                {
                    if (f.CreationTime.AddDays(3) <= DateTime.Now)
                    {
                        if ((f.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                            f.Attributes = FileAttributes.Normal;
                        f.Delete();
                    }
                }
            }


        }

    }
}


