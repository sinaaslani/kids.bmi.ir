using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Kids.Utility.Zip
{

    public static class ZipUtil
    {
        public static void ZipFile(IEnumerable<string> InputFilePath, string outputPathAndFile, string password)
        {
            string outPath = outputPathAndFile;
            ZipOutputStream oZipStream = new ZipOutputStream(File.Create(outPath));

            foreach (string FilePath in InputFilePath)
            {
                int TrimLength = 0;
                if (Path.GetDirectoryName(FilePath) != null)
                {
                    TrimLength = (Path.GetDirectoryName(FilePath)).Length;
                    TrimLength += 1; //remove '\'
                }


                if (!string.IsNullOrEmpty(password))
                    oZipStream.Password = password;
                oZipStream.SetLevel(9); // maximum compression

                ZipFile(TrimLength, oZipStream, FilePath);
            }
            

            oZipStream.Finish();
            oZipStream.Close();
        }

        public static void ZipFiles(string InputFolderPath, string outputPathAndFile, string password)
        {
            string outPath = outputPathAndFile;
            ZipOutputStream oZipStream = new ZipOutputStream(File.Create(outPath));


            int TrimLength = 0;
            if (Directory.GetParent(InputFolderPath) != null)
            {
                TrimLength = (Directory.GetParent(InputFolderPath)).ToString().Length;
                TrimLength += 1; //remove '\'
            }


            string DirPath = outPath.Remove(outPath.LastIndexOf(@"\"));
            if (!Directory.Exists(DirPath))
                Directory.CreateDirectory(DirPath);


            DirectoryInfo dinf = new DirectoryInfo(InputFolderPath);

            if (dinf.Exists)
            {

                if (!string.IsNullOrEmpty(password))
                    oZipStream.Password = password;
                oZipStream.SetLevel(9); // maximum compression


                ArrayList ar = GenerateFileList(InputFolderPath); // generate file list

                foreach (object t in ar)
                {
                    string Fil = t.ToString();
                    ZipFile(TrimLength, oZipStream, Fil);
                }
            }
            else
            {
                FileInfo finf = new FileInfo(InputFolderPath);
                if (!string.IsNullOrEmpty(password))
                    oZipStream.Password = password;
                oZipStream.SetLevel(9); // maximum compression
                ZipFile(TrimLength, oZipStream, finf.FullName);
            }

            oZipStream.Finish();
            oZipStream.Close();
        }

        public static void ZipFiles(BackgroundWorker worker, DoWorkEventArgs e, string inputFolderPath, string outputPathAndFile, string password)
        {
            int TrimLength = 0;
            if (Directory.GetParent(inputFolderPath) != null)
            {
                TrimLength = (Directory.GetParent(inputFolderPath)).ToString().Length;
                // find number of chars to remove     // from orginal file path
                TrimLength += 1; //remove '\'
            }
            string outPath = outputPathAndFile;

            string DirPath = outPath.Remove(outPath.LastIndexOf(@"\"));
            if (!Directory.Exists(DirPath))
                Directory.CreateDirectory(DirPath);

            if (worker != null)
                worker.ReportProgress(1, 3);

            ZipOutputStream oZipStream = new ZipOutputStream(File.Create(outPath));

            DirectoryInfo dinf = new DirectoryInfo(inputFolderPath);

            if (dinf.Exists)
            {

                if (!string.IsNullOrEmpty(password))
                    oZipStream.Password = password;
                oZipStream.SetLevel(9); // maximum compression

                if (worker != null)
                    worker.ReportProgress(3, 4);

                if (worker != null && worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                ArrayList ar = GenerateFileList(inputFolderPath); // generate file list
                if (worker != null && worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                for (int i = 0; i < ar.Count; i++) // for each file, generate a zipentry
                {
                    string Fil = ar[i].ToString();

                    if (worker != null)
                    {
                        worker.ReportProgress(Convert.ToInt32((i * 100) / (double)ar.Count), 5);
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                    }
                    ZipFile(TrimLength, oZipStream, Fil);

                }
            }
            else
            {

                FileInfo finf = new FileInfo(inputFolderPath);
                if (!string.IsNullOrEmpty(password))
                    oZipStream.Password = password;
                oZipStream.SetLevel(9); // maximum compression
                ZipFile(TrimLength, oZipStream, finf.FullName);
            }

            oZipStream.Finish();
            oZipStream.Close();
        }
        #region ZipOld

        #endregion

        private static void ZipFile(int TrimLength, ZipOutputStream oZipStream, string Fil)
        {
            FileStream ostream;
            byte[] obuffer;
            ZipEntry oZipEntry = new ZipEntry(Fil.Remove(0, TrimLength));
            oZipStream.PutNextEntry(oZipEntry);

            if (!Fil.EndsWith(@"/")) // if a file ends with '/' its a directory
            {
                ostream = File.OpenRead(Fil);
                obuffer = new byte[ostream.Length];
                ostream.Read(obuffer, 0, obuffer.Length);
                oZipStream.Write(obuffer, 0, obuffer.Length);
            }
        }

        private static ArrayList GenerateFileList(string Dir)
        {
            ArrayList fils = new ArrayList();
            bool Empty = true;

            string[] Files = Directory.GetFiles(Dir);

            foreach (string file in Files)
            {
                fils.Add(file);
                Empty = false;
            }

            if (Empty)
            {
                if (Directory.GetDirectories(Dir).Length == 0)
                // if directory is completely empty, add it
                {
                    fils.Add(Dir + @"/");
                }
            }

            foreach (string dirs in Directory.GetDirectories(Dir)) // recursive
            {
                foreach (object obj in GenerateFileList(dirs))
                {
                    fils.Add(obj);
                }
            }
            return fils; // return file list
        }

        public static void UnZipFiles(string zipPathAndFile, string outputFolder, string password, bool deleteZipFile)
        {
            ZipInputStream s = new ZipInputStream(File.OpenRead(zipPathAndFile));
            if (!string.IsNullOrEmpty(password))
                s.Password = password;
            ZipEntry theEntry;

            while ((theEntry = s.GetNextEntry()) != null)
            {
                string directoryName = outputFolder;
                string fileName = Path.GetFileName(theEntry.Name);
                // create directory 
                if (directoryName != "")
                    Directory.CreateDirectory(directoryName);

                if (fileName != String.Empty)
                {
                    #region Unzip File
                    //if (theEntry.Name.IndexOf(".ini") < 0)
                    // {
                    string fullPath = string.Format("{0}\\{1}", directoryName, theEntry.Name.TrimEnd(Convert.ToChar(@"/")));
                    fullPath = fullPath.Replace("\\ ", "\\");
                    string fullDirPath = Path.GetDirectoryName(fullPath);

                    if (!Directory.Exists(fullDirPath))
                        Directory.CreateDirectory(fullDirPath);

                    FileStream streamWriter = File.Create(fullPath);

                    byte[] data = new byte[2048];
                    while (true)
                    {
                        int size = s.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }
                    streamWriter.Close();
                    //}
                    #endregion
                }
                else
                {
                    #region Unzip Folder
                    //string folderName = Path.GetDirectoryName(theEntry.Name);
                    string fullPath = string.Format("{0}\\{1}", directoryName, theEntry.Name.TrimEnd(Convert.ToChar(@"/")));
                    fullPath = fullPath.Replace("\\ ", "\\");
                    //string fullDirPath = Path.GetDirectoryName(fullPath);
                    Directory.CreateDirectory(fullPath);

                    #endregion
                }
            }
            s.Close();
            if (deleteZipFile)
                File.Delete(zipPathAndFile);
        }
       
    }

}
