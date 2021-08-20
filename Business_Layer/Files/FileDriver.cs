using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Files
{
    class FileDriver
    {
        #region Contstructores
        public FileDriver()
        {
        }
        #endregion Constructores

        #region Propiedades
        public string Path 
        { 
            get=>_path;
            private set
            {
                if (value != Path) Path = value;

            }
        }
        public string FileName { get; private set; }
        public string BackupPath { get; private set; }
        #endregion Propiedades

        #region Campos
        private Stream fileStream;
        private BinaryFormatter binaryFormatter = new BinaryFormatter();
        private string _path;
        #endregion Campos

        #region Metodos
        private void SetFileStram()
        {
            if(Path == null || FileName == null)
            {
                return;
            }
            fileStream = new FileStream(Path + FileName, FileMode.OpenOrCreate);
        }
        #endregion Metodos
    }
}
