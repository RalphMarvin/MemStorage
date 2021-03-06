﻿namespace MemStorage
{
    /// <summary>
    /// Contains methods to be implemented to handle storage files opertaions.
    /// </summary>
    interface IStorageFileOperations
    {
        /// <summary>
        /// Creates the main directory to keep all other applications storage files inside.
        /// </summary>
        void CreateMainPath();

        /// <summary>
        /// Write to a storage file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        void WriteToFile(string fileName, string content, StorageFileHandler.StorageType storageType);

        /// <summary>
        /// Overwrites a storage file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        void OverWriteFile(string fileName, string content, StorageFileHandler.StorageType storageType);
        
        /// <summary>
        /// Checks whether a storage area is empty or not
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool IsEmpty(string path, StorageFileHandler.StorageType storageType);

        /// <summary>
        /// Returns the number of items stored in the storage file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        int Count(string path, StorageFileHandler.StorageType storageType);

        /// <summary>
        /// Deletes all storage file belonging to a specific application.
        /// </summary>
        /// <param name="path"></param>
        void DeleteAllFiles(string path, StorageFileHandler.StorageType storageType);

        /// <summary>
        /// Checks whether a storage file exists.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        bool FileExists(string fileName);

        /// <summary>
        /// Checks whether a storage directory exists.
        /// </summary>
        /// <param name="dirName"></param>
        /// <returns></returns>
        bool DirectoryExists(string dirName, StorageFileHandler.StorageType storageType);

        /// <summary>
        /// Deletes a storage file
        /// </summary>
        /// <param name="fileName"></param>
        void DeleteFile(string fileName, StorageFileHandler.StorageType storageType);

        /// <summary>
        /// Reads and returns the content of a storage file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string ReadFile(string fileName, StorageFileHandler.StorageType storageType);
    }
}
