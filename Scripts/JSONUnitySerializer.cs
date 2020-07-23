// Copyright 2020 Red Helium Games
// Author: Denis Brilev (Nickname: Soulook) 

using System.IO;
using System.Text;
using UnityEngine;

namespace RedHeliumGames.IO
{

    public class JSONUnitySerializer : Serializer
    {

        public string Data { get; protected set; }

        public JSONUnitySerializer(string path, Encoding encoding, FileMode fileMode = FileMode.OpenOrCreate) 
        : base(path, encoding, fileMode)
        {
        }

        public JSONUnitySerializer(string path, FileMode fileMode = FileMode.OpenOrCreate) : base(path, fileMode)
        {
        }

        public override void Serialize<T>(T data)
        {
            using (FileStream fileStream = File.Open(path, fileMode, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fileStream, encoding))
                {
                    Data = JsonUtility.ToJson(data);
                    writer.Write(Data);
                }
            }
        }

        public override T Deserialize<T>()
        {
            using (StreamReader reader = new StreamReader(path, encoding))
            {
                try
                {
                    return JsonUtility.FromJson<T>(reader.ReadToEnd());
                }
                catch (IOException e) { throw new IOException(e.Message); }
            }
        }
    }
}
