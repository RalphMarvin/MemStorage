using System;
using System.IO;

namespace MemStorage
{
    /// <summary>
    /// Contains methods to enable applications creates a local storage for use.
    /// </summary>
    public class LocalStorage
    {
        /// <summary>
        /// Represents the maximum memory the session storage can have, i.e, 50MB in bytes.
        /// </summary>
        private static long MAX_MEMORY = 52428800;

        /// <summary>
        /// Creates the neccessary memstorage container for your application.
        /// </summary>
        public static void Init(string app)
        {
            StorageFileHandler StorageFile = new StorageFileHandler();
            StorageFileHandler.AppName = app;
            StorageFile.CreateMainPath();
        }

        /// <summary>
        /// Determines whether the storage directory is full.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool IsMemoryFull(string path)
        {
            long totalSize = 0;
            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                FileInfo Info = new FileInfo(file);
                totalSize += Info.Length;
            }
            if (totalSize.Equals(MAX_MEMORY) || totalSize > MAX_MEMORY)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks whether the storage area is empty.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsEmpty(string app)
        {
            StorageFileHandler StorageFile = new StorageFileHandler();
            if (StorageFile.IsEmpty(app, true))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns the number of items stored by your application.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static int Count(string app)
        {
            StorageFileHandler StorageFile = new StorageFileHandler();
            return StorageFile.Count(app, true);
        }

        /// <summary>
        /// Stores data stored in the local storage system.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetItem(string key, string value)
        {
            StorageFileHandler StorageFile = new StorageFileHandler();
            if (IsMemoryFull(StorageFile.LocalStoragePath))
            {
                throw new ApplicationException("Exceeds local storage memory limit");
            }
            else
            {
                StorageFile.WriteToFile(key, value, true);
            }
        }
        
        /// <summary>
        /// Returns data stored in the local storage system associated with the key passed.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetItem(string key)
        {
            StorageFileHandler StorageFile = new StorageFileHandler();
            string value = StorageFile.ReadFile(key, true);

            return value; 
        }
        
        /// <summary>
        /// Removes an item stored in the local storage system stored by your application.
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveItem(string key)
        {
            StorageFileHandler StorageFile = new StorageFileHandler();
            StorageFile.DeleteFile(key, true);
        }
        
        /// <summary>
        /// Removes everything from the local storage system stored by your application.
        /// </summary>
        public static void Clear()
        {
            StorageFileHandler StorageFile = new StorageFileHandler();
            string appStoragePath = StorageFile.LocalStoragePath;

            StorageFile.DeleteAllFiles(appStoragePath, true);
        }
    }
}
