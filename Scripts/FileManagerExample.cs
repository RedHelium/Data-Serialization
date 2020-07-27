// Copyright 2020 Red Helium Games
// Author: Denis Brilev (Nickname: Soulook) 

using System;
using System.IO;
using UnityEngine;

namespace RedHeliumGames.IO
{

    /// <summary>
    /// Example class for Unity
    /// </summary>
    [AddComponentMenu("RedHeliumGames/IO/Example File Manager")]
    public sealed class FileManagerExample : MonoBehaviour
    {
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

        private FileManager fileManager;
        private TestStruct test;

        private void Start()
        {
            test = new TestStruct() { a = 25, b = "Hello" };


            fileManager = new FileManager(Path.Combine(Application.dataPath, "Resources", "Files", "test123.json"), 
            FileManager.SerializerType.UnityJSON);

            fileManager.Save(test);

            print(fileManager.Load<TestStruct>().a);
 

        }
    }
}
