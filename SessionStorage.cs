using System;
using System.IO;

namespace MemStorage
{
    /// <summary>
    /// Contains methods whcih helps an application stores data temporarily for later use.
    /// </summary>
    public static class SessionStorage
    {
        /// <summary>
        /// Represents the maximum memory the session storage can have, i.e, 10MB in bytes.
        /// </summary>
        private static long MAX_MEMORY = 10485760;

        /// <summary>
        /// Creates the neccessary memstorage containers for your application.
        /// </summary>
        public static void Init(string app)
        {
            StorageFileHandler StorageFile = new StorageFileHandler();
            StorageFileHandler.AppName = app;
            StorageFile.CreateMainPath();
        }

        /// <summary>
        /// Determines whether the session storage is full.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool IsMemoryFull(string path)
        {
            long totalSize = 0;
            string[] files = Directory.GetFiles(path);

            foreach(string file in files)
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
            if (StorageFile.IsEmpty(app, StorageFileHandler.StorageType.SESSION))
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
            return StorageFile.Count(app, StorageFileHandler.StorageType.SESSION);
        }

        /// <summary>
        /// Stores data in the session storage system for temporary use.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetItem(string key, string value)
        {
            StorageFileHandler StorageFile = new StorageFileHandler();
            if (IsMemoryFull(StorageFile.SessionStoragePath))
            {
                throw new ApplicationException("Exceeds session storage memory limit");
            }
            else
            {
                StorageFile.WriteToFile(key, value, StorageFileHandler.StorageType.SESSION);
            }
        }

        /// <summary>
        /// Returns data stored in the session storage system associated with the key passed.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetItem(string key)
        {
            StorageFileHandler StorageFile = new StorageFileHandler();
            string value = StorageFile.ReadFile(key, StorageFileHandler.StorageType.SESSION);

            return value;
        }

        /// <summary>
        /// Removes an item stored in the session storage system created by your application.
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveItem(string key)
        {
            StorageFileHandler StorageFile = new StorageFileHandler();
            StorageFile.DeleteFile(key, StorageFileHandler.StorageType.SESSION);
        }

        /// <summary>
        /// Removes everything from the session storage system created by your application.
        /// </summary>
        public static void Clear()
        {
            StorageFileHandler StorageFile = new StorageFileHandler();
            string appStoragePath = StorageFile.SessionStoragePath;

            StorageFile.DeleteAllFiles(appStoragePath, StorageFileHandler.StorageType.SESSION);
        }
    }
}
