# Data Serialization
 Binary, XML and JSON (Only for Unity) Serialization

## Usage
 0. using RedHeliumGames.IO namespace
 1. Create an instance of the FileManager class.
    1. Call Save<T>(T data) function for save your serialization object
    2. Call T Load<T>() function for load your serialization object from file

## Example

```C#
//This is example serialization structure
[Serializable]
public struct TestStruct
{
    public int a;
    public string b;

    public TestStruct(int a, string b)
    {
        this.a = a;
        this.b = b;
    }
}

private void Start()
{
   // Create FileManager
   FileManager fileManager = new FileManager(Path.Combine(Application.dataPath, "test123.bin"), 
   FileManager.SerializerType.Binary);
   
   //Initialize testing serialization object
   test = new TestStruct() { a = 25, b = "Hello" };
  
   //Save our structure
   fileManager.Save(test);

   // Output loaded data from file
   print(fileManager.Load<TestStruct>().a);
}

```
