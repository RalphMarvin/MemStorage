# MemStorage
MemStorage provides your application with storage where your can store temporary data and permanent data.
Its like your browser storage system. MemStorage provides you with both local storage and
session storage systems which can serve your applications purposes.

MemStorage storages session storage has a limit of 10MB of storage size and the local storage has a limit
of 50MB storage size.

Version: 0.0.1.0
MemStorage is currently in its Beta stage and still requires some testing.

Contributing
MemStorage is an open source project and is freely available to all.

License

Usage:
Both local storage and session storage provides static methods. But first it also come with an init method
which should be called first before calling any other MemStorage method. Init method takes your applications 
name as argument.
Example:
---------
using System;
using MemStorage;

class MyApp
{
    static void Main(string[] args)
    {
        LocalStorage.Init("MiniApp");
        LocalStorage.SetItem("codeID", "244CD2432G24");
        Console.WriteLine(LocalStorage.GetItem("codeID"));
        
        Console.ReadLine();
    }
}

NOTE: The Init method is important as this is what identifies your application as being legit to use MemStorage.
Currently there isnt any way of automatically clearing session data. Hence the application programmer should always
use the Clear() method to clear session data.

