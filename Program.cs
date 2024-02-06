using K4os.Compression.LZ4.Streams;
using Newtonsoft.Json;
using ProtoBuf;
using ProtobufInitialTest;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

[Serializable]
[ProtoContract]
public class MyItem
{
    [ProtoMember(1)]
    public string Name { get; set; }
    [ProtoMember(2)]
    public double Value { get; set; }
}
[Serializable]
[ProtoContract]
public class MyObject
{
    [ProtoMember(1)]
    public List<MyItem> Items { get; set; } = new List<MyItem>();
}

class Program
{
    static void Main(string[] args)
    {
        MyObject obj = new MyObject();
        for (int i = 0; i < 100000; i++)
            obj.Items.Add(new MyItem { Name = "Item " + i, Value = i });

        // Binary formatter
        Console.WriteLine("Write to Binary...");
        const string binaryFilePath = "Result.bin";
        using (new Measure())
        {
            AppContext.SetSwitch("System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization", true);
            using (FileStream fs = new(binaryFilePath, FileMode.Create))
            {
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                BinaryFormatter bf = new();
#pragma warning restore SYSLIB0011 // Type or member is obsolete
                bf.Serialize(fs, obj);
            }
            AppContext.SetSwitch("System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization", false);
        }
        Console.WriteLine($"File size: {GetFileSize(binaryFilePath) / 1024 / 1024:F2}Mb");
        Console.WriteLine("File to Binary finished.");
        Console.WriteLine();

        // JSON
        Console.WriteLine("Write to JSON...");
        const string jsonFile = "Result.json";
        using (new Measure())
        {
            string json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(jsonFile, json);
        }
        Console.WriteLine($"File size: {GetFileSize(jsonFile) / 1024 / 1024:F2}Mb");
        Console.WriteLine("File to JSON finished.");
        Console.WriteLine();

        // Protobuf
        Console.WriteLine("Write to Protobuf...");
        const string protoBufFile = "Result.proto";
        using (new Measure())
        {
            using var file = File.Create(protoBufFile);
            Serializer.Serialize(file, obj);
        }
        Console.WriteLine($"File size: {GetFileSize(protoBufFile) / 1024 / 1024:F2} Mb");
        Console.WriteLine("File to Protobuf finished.");
        Console.WriteLine();

        // Custom serialization
        Console.WriteLine("Write to Custom...");
        const string customFile = "Result.custom";
        using (new Measure())
        {
            WriteToCustomFile(customFile, obj);
        }
        Console.WriteLine($"File size: {GetFileSize(customFile) / 1024:F2} Kb");
        Console.WriteLine("File to Custom finished.");
        Console.WriteLine();
    }

    #region Routines
    public static long GetFileSize(string filePath)
    {
        // Get file size in bytes
        FileInfo info = new(filePath);
        return info.Length;
    }
    public static void WriteToCustomFile(string filePath, MyObject obj)
    {
        using LZ4EncoderStream stream = LZ4Stream.Encode(File.Create(filePath));
        using BinaryWriter writer = new(stream, Encoding.UTF8, false);
        WriteToStream(writer, obj);

        static void WriteToStream(BinaryWriter writer, MyObject obj)
        {
            // Items
            writer.Write(obj.Items.Count);
            foreach (var item in obj.Items)
                WriteItem(writer, item);
        }

        static void WriteItem(BinaryWriter writer, MyItem item)
        {
            writer.Write(item.Name);
            writer.Write(item.Value);
        }
    }
    #endregion
}