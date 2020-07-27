// Copyright 2020 Red Helium Games
// Author: Denis Brilev (Nickname: Soulook) 

using System.IO;
using UnityEngine;

namespace RedHeliumGames.IO
{

    public class UnityJSONSerializer : Serializer
    {

        public string Data { get; protected set; }

        public UnityJSONSerializer(string path, FileMode fileMode = FileMode.OpenOrCreate) : base(path) { }

        public override void Serialize<T>(T data)
        {
            using (FileStream fileStream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    Data = JsonUtility.ToJson(data);
                    writer.Write(Data);
                }
            }
        }

        public override T Deserialize<T>()
        {
            using (StreamReader reader = new StreamReader(path))
            {
                try
                {
                    return JsonUtility.FromJson<T>(reader.ReadToEnd());
                }
                catch (IOException e) { throw new IOException(e.Message); }
            }
        }

        public virtual void Overwrite<T>(T data) => JsonUtility.FromJsonOverwrite(Data, data);

    }
}
