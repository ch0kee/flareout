using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FlareOut
{
    // xml fájl
    class FlareFile
    {
        public readonly string FileName;
        protected readonly string m_OutFileName;

        public FlareFile(string FullPath, string Outfilename)
        {
            FileName = System.IO.Path.GetFileName(FullPath);
            Path = FullPath;
            m_OutFileName = Outfilename;
        }
        public string Path { set { m_FullPath = value; MainForm.Output = FileName + " betöltve"; } get { return m_FullPath; } }
        public bool IsLoaded { get { return m_FullPath != null; } }
        protected string m_FullPath;
    };
}
