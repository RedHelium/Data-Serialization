// Copyright 2020 Red Helium Games
// Author: Denis Brilev (Nickname: Soulook) 

using System;

namespace RedHeliumGames.IO
{
    public interface ISerializer
    {
        void Serialize<T>(T data);
        T Deserialize<T>();
    }

    public abstract class Serializer : ISerializer
    {
        protected string path;

        public Serializer(string path)
        {
            this.path = path;
        }

        public virtual T Deserialize<T>() => throw new NullReferenceException("Missing object");
        public virtual void Serialize<T>(T data) { }
    }

   
    public sealed class FileManager 
    {

        public enum SerializerType : sbyte { UnityJSON, XML, Binary }

        private readonly Serializer serializator;


        /// <summary>
        /// Creates file manager specified type
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        public FileManager(string path, SerializerType type)
        {

            switch (type)
            {
                case SerializerType.UnityJSON: serializator = new UnityJSONSerializer(path); break;
                case SerializerType.XML: serializator = new XMLSerializer(path);  break;
                case SerializerType.Binary: serializator = new BinarySerializer(path); break;
            }
        }

        /// <summary>
        /// Serialize data object and save it in file
        /// </summary>
        /// <typeparam name="T">serializable data object</typeparam>
        /// <param name="data"></param>
        public void Save<T>(T data) => serializator.Serialize(data);

        /// <summary>
        /// Deserialize object from file and return it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Load<T>() => serializator.Deserialize<T>();

    }
}
