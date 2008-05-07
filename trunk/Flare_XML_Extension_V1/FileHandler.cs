using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FlareOut
{
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
            string filepath = Path.GetDirectoryName(filename);
            string backupfile = filename + ".fobckp";
            if (ContainsFile(filepath, backupfile))
            {
                int i = 0;
                string new_bckp = "";
                do
                {
                    ++i;
                    new_bckp = backupfile + "_" + i.ToString();
                } while (ContainsFile(filepath, new_bckp));
                backupfile = new_bckp;
            }
            File.Copy(filename, backupfile);
            MainForm.Output = "Biztonsági másolat létrehozva ide : " + backupfile;
        }
        //
        private static bool ContainsFile(string path, string file)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string f in files)
                if (f == file)
                    return true;
            return false;
        }
    }
}
