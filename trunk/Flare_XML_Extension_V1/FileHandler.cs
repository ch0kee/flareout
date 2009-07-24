using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FlareOut
{
    static class FileFunctions
    {
        public static string GetNextFileName( string filename )
        {
            string filepath = Path.GetDirectoryName(filename);

            string ext = Path.GetExtension(filename);
            string withoutext = Path.GetFileNameWithoutExtension(filename);

            filename = withoutext + ext;

            int i = 0;
            while (ContainsFile(filepath, filename))
            {
                filename = withoutext + "_" + i.ToString() + ext;
                ++i;
            }
            return filepath+@"\"+filename;
        }

        private static bool ContainsFile(string path, string file)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string f in files)
                if (Path.GetFileName(f) == Path.GetFileName(file))
                    return true;
            return false;
        }
    }
    class BackupMaker
    {
        public BackupMaker( string filename, bool createbackup )
        {
            m_OriginalFile = filename;
            if (createbackup)
            {
                MainForm.Output = "Biztonsági másolat errõl : " + filename;
                CreateBackup(m_OriginalFile);
            }
        }
        private string m_OriginalFile = "";

        public static implicit operator string( BackupMaker b )
        {
            return b.m_OriginalFile;
        }
        public static void CreateBackup(string filename)
        {
            string backupfile = FileFunctions.GetNextFileName(filename + ".fobckp");
            File.Copy(filename, backupfile);
            MainForm.Output = "Biztonsági másolat létrehozva ide : " + backupfile;
        }

    }
}
