// Copyright 2020 Red Helium Games
// Author: Denis Brilev (Nickname: Soulook) 

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace RedHeliumGames.IO
{
    public class BinarySerializer : Serializer
    {
        protected BinaryFormatter binaryFormatter;

        public BinarySerializer(string path, FileMode fileMode = FileMode.OpenOrCreate) : base(path, fileMode)
        {
             binaryFormatter = new BinaryFormatter();
        }

        public BinarySerializer(string path, Encoding encoding, FileMode fileMode = FileMode.OpenOrCreate) 
        : base(path, encoding, fileMode)
        {
            binaryFormatter = new BinaryFormatter();
        }

        public override T Deserialize<T>()
        {
            using (FileStream fileStream = new FileStream(path, fileMode))
            {
                return (T)binaryFormatter.Deserialize(fileStream);
            }
        }

        public override void Serialize<T>(T data)
        {
            using (FileStream fileStream = new FileStream(path, fileMode))
            {
                binaryFormatter.Serialize(fileStream, data);
            }
        }
    }
}