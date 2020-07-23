// Copyright 2020 Red Helium Games
// Author: Denis Brilev (Nickname: Soulook) 

using System;
using System.IO;
using System.Text;

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
        protected Encoding encoding;
        protected FileMode fileMode;

        public Encoding SetEncoding(Encoding encoding) => this.encoding = encoding;

        public Serializer(string path, Encoding encoding, FileMode fileMode = FileMode.OpenOrCreate)
        {
            this.path = path;
            this.encoding = encoding;
            this.fileMode = fileMode;
        }

        public Serializer(string path, FileMode fileMode = FileMode.OpenOrCreate)
        {
            this.path = path;
            this.fileMode = fileMode;
            encoding = Encoding.UTF8;
        }

        public virtual T Deserialize<T>() => throw new NullReferenceException("Missing object");

        public virtual void Serialize<T>(T data) { }
    }

   
    public sealed class FileManager 
    {

        public enum SerializerType : sbyte { JSON, XML, Binary }

        private readonly string filePath;
        private readonly Serializer serializator;

        private Encoding encoding;

        /// <summary>
        /// Creates file manager specified type
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        public FileManager(string path, SerializerType type)
        {
            filePath = path;
            encoding = Encoding.UTF8;

            switch (type)
            {
                case SerializerType.JSON: serializator = new JSONUnitySerializer(path); break;
                case SerializerType.XML: serializator = new XMLSerializer(path);  break;
                case SerializerType.Binary: serializator = new BinarySerializer(path); break;
            }
        }

        /// <summary>
        /// Creates file manager specified type
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <param name="encoding"></param>
        public FileManager(string path, SerializerType type, Encoding encoding)
        {
            filePath = path;
            this.encoding = encoding;

            switch (type) 
            {   case SerializerType.JSON: serializator = new JSONUnitySerializer(path, encoding); break;
                case SerializerType.XML: serializator = new XMLSerializer(path, encoding); break;
                case SerializerType.Binary: serializator = new BinarySerializer(path, encoding); break;
            }
        }

        public Encoding SetEncoding(Encoding encoding)
        {
            serializator.SetEncoding(encoding);
            return this.encoding = encoding;
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
