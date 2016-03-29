using System;
using System.IO;

namespace MemStorage
{
    /// <summary>
    /// MemStorage's engine class.
    /// </summary>
    class StorageFileHandler : IStorageFileOperations
    {
        /// <summary>
        /// Contains the name of the application using the library.
        /// </summary>
        private static string appName;
        
        /// <summary>
        /// Represents the default directory where all storage files and directories are stored.
        /// </summary>
        private string DEFAULT_PATH = Path.GetTempPath() + Path.DirectorySeparatorChar + "MemStorage";

        /// <summary>
        /// The file extension for all storage files.
        /// </summary>
        private string MEMSTORAGE_EXTENSION = ".mst";

        /// <summary>
        /// Gets and sets the name of the application using this library.
        /// </summary>
        public static string AppName
        {
            set { appName = value; }
            get { return appName; }
        }
        
        /// <summary>
        /// Returns the full path to the local storage directory
        /// </summary>
        public string LocalStoragePath
        {
            get { return DEFAULT_PATH + Path.DirectorySeparatorChar + "LocalStorage" + Path.DirectorySeparatorChar + AppName; }
        }
        
        /// <summary>
        /// Returns the full path to the session storage directory
        /// </summary>
        public string SessionStoragePath
        {
            get { return DEFAULT_PATH + Path.DirectorySeparatorChar + "SessionStorage" + Path.DirectorySeparatorChar + AppName; }
        }
        
        /// <summary>
        /// Returns the number of files stored in the storage path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public int Count(string path, bool localStorage)
        {
            path = HandleStoragePaths(localStorage);

            string[] files = Directory.GetFiles(path);

            return files.Length;
        }

        /// <summary>
        /// Creates the main directory to hold all storage data.
        /// </summary>
        public void CreateMainPath()
        {
            string localPath = LocalStoragePath;
            string sessionPath = SessionStoragePath;
            
            //only check for either of the directory's existence,
            //the assumption here is that if one exists the other exists.
            if (!DirectoryExists(localPath, true) || !DirectoryExists(sessionPath, false))
            {
                try
                {
                    Directory.CreateDirectory(localPath);
                    Directory.CreateDirectory(sessionPath);
                }
                catch (IOException err)
                {
                    throw new ApplicationException(err.Message);
                }
            }
        }

        /// <summary>
        /// Deletes all storage files belonging to the application uisng this library.
        /// </summary>
        /// <param name="path"></param>
        public void DeleteAllFiles(string path, bool localStorage)
        {
            path = HandleStoragePaths(localStorage);
            string[] files = Directory.GetFiles(path);
            
            foreach(string file in files)
            {
                if (FileExists(file))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (IOException err)
                    {
                        throw new ApplicationException(err.Message);
                    }
                }
                else
                {
                    throw new ApplicationException(string.Format("The key {0} doesnt exists", file));
                }
            }
        }

        /// <summary>
        /// Permanently deletes a storage file.
        /// </summary>
        /// <param name="fileName"></param>
        public void DeleteFile(string fileName, bool localStorage)
        {
            fileName = HandleStoragePaths(localStorage) + Path.DirectorySeparatorChar + fileName + MEMSTORAGE_EXTENSION;

            if (FileExists(fileName))
            {
                try
                {
                    File.Delete(fileName);
                }
                catch (IOException err)
                {
                    throw new ApplicationException(err.Message);
                }
            }
            else
            {
                throw new ApplicationException(string.Format("The key {0} doesnt exists", fileName));
            }
        }

        /// <summary>
        /// Determines whether a directory exists.
        /// </summary>
        /// <param name="dirName"></param>
        /// <returns></returns>
        public bool DirectoryExists(string dirName, bool localStorage)
        {
            dirName = HandleStoragePaths(localStorage);
            if (Directory.Exists(dirName))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Determines whether a storage file exists.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool FileExists(string fileName)
        {
            if (File.Exists(fileName))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns the appropriate path depending on the parameter passed.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="localStorage"></param>
        public string HandleStoragePaths(bool localStorage)
        {
            switch(localStorage)
            {
                case true:
                    return LocalStoragePath;
                default:
                    return SessionStoragePath;
            }
        }

        /// <summary>
        /// Checks whether a storage path is empty.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool IsEmpty(string path, bool localStorage)
        {
            path = HandleStoragePaths(localStorage);
            string[] files = Directory.GetFiles(path);

            if (files.Length == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Overwrites an existing storage file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public void OverWriteFile(string fileName, string content, bool localStorage)
        {
            fileName = HandleStoragePaths(localStorage) + Path.DirectorySeparatorChar + fileName + MEMSTORAGE_EXTENSION;
            if (FileExists(fileName))
            {
                try
                {
                    File.AppendAllText(fileName, content);
                }
                catch (IOException err)
                {
                    throw new ApplicationException(err.Message);
                }
            }
            else
            {
                throw new ApplicationException(string.Format("The key {0} doesnt exists", fileName));
            }
        }

        /// <summary>
        /// Reads and returns the content of a storage file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string ReadFile(string fileName, bool localStorage)
        {
            fileName = HandleStoragePaths(localStorage) + Path.DirectorySeparatorChar + fileName + MEMSTORAGE_EXTENSION;
            if (FileExists(fileName))
            {
                string content = File.ReadAllText(fileName);
                return content;
            }
            else
            {
                throw new ApplicationException(string.Format("The key {0} doesnt exists", fileName));
            }
        }

        /// <summary>
        /// Writes to a storage file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public void WriteToFile(string fileName, string content, bool localStorage)
        {
            fileName = HandleStoragePaths(localStorage) + Path.DirectorySeparatorChar + fileName + MEMSTORAGE_EXTENSION;
            if (!FileExists(fileName))
            {
                try
                {
                    File.WriteAllText(fileName, content);
                }
                catch (IOException err)
                {
                    throw new ApplicationException(err.Message);
                }
            }
            else
            {
                throw new ApplicationException(string.Format("The key {0} doesnt exists", fileName));
            }
        }
    }
}
