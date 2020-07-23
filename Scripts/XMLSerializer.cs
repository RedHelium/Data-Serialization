// Copyright 2020 Red Helium Games
// Author: Denis Brilev (Nickname: Soulook) 

using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace RedHeliumGames.IO
{
    public class XMLSerializer : Serializer
    {
        private XmlSerializer serializer;

        public XMLSerializer(string path, FileMode fileMode = FileMode.OpenOrCreate) : base(path, fileMode)
        {
        }

        public XMLSerializer(string path, Encoding encoding, FileMode fileMode = FileMode.OpenOrCreate) 
        : base(path, encoding, fileMode)
        {
        }

        public override T Deserialize<T>()
        {
            using (FileStream fileStream = new FileStream(path, fileMode))
            {
                return (T)serializer.Deserialize(fileStream);
            }
        }

        public override void Serialize<T>(T data)
        {
            using (FileStream fileStream = new FileStream(path, fileMode))
            {
                serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(fileStream, data);
            }
        }
    }
}