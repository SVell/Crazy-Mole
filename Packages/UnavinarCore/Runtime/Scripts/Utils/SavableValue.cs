using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Unavinar.HCUnavinarCore
{
    [Serializable]
    public sealed class SavableValue<T>
    {
        public event Action OnChanged = () => { };
        
        private readonly string playerPrefsPath;
        private T value;


        public T Value
        {
            get => value;
            set
            {
                PrevValue = this.value;
                this.value = value;
                SaveToPrefs();
                OnChanged.Invoke();
            }
        }

        public T PrevValue { get; private set; }

        public SavableValue(string playerPrefsPath, T defaultValue = default(T))
        {
            if (string.IsNullOrEmpty(playerPrefsPath))
            {
                throw new Exception("Empty playerPrefsPath in savableValue");
            }

            this.playerPrefsPath = playerPrefsPath;

            value = defaultValue;
            PrevValue = defaultValue;

            LoadFromPrefs();
        }

        private void LoadFromPrefs()
        {
            if (!PlayerPrefs.HasKey(playerPrefsPath))
            {
                SaveToPrefs();
                return;
            }

            var stringToDeserialize = PlayerPrefs.GetString(playerPrefsPath, "");

            var bytes = Convert.FromBase64String(stringToDeserialize);
            var memoryStream = new MemoryStream(bytes);
            var bf = new BinaryFormatter();

            value = (T)bf.Deserialize(memoryStream);
        }

        private void SaveToPrefs()
        {
            var memoryStream = new MemoryStream();
            var bf = new BinaryFormatter();
            bf.Serialize(memoryStream, value);
            var stringToSave = Convert.ToBase64String(memoryStream.ToArray());

            PlayerPrefs.SetString(playerPrefsPath, stringToSave);
        }
    }
}