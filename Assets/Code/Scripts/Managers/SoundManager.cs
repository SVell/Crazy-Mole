using System.Collections.Generic;
using Unavinar.HCUnavinarCore;
using UnityEngine;

namespace Unavinar.Managers
{
    public class SoundManager : Singleton<SoundManager>
    {
        private SavableValue<bool> isSoundEnable;
        private SavableValue<bool> isMusicEnable;

        private readonly Dictionary<int, AudioSourceData> sourceMedia = new();

        private int audioCodeIndex;

        public bool SoundEnable
        {
            get => isSoundEnable.Value;
            set
            {
                if (isSoundEnable.Value != value)
                {
                    foreach (var key in sourceMedia.Keys)
                    {
                        var sourceData = sourceMedia[key];
                        if (!sourceData.IsMusic)
                        {
                            sourceData.Source.volume = value ? sourceData.RequestedVolume : 0;
                        }
                    }

                    isSoundEnable.Value = value;
                }
            }
        }

        public bool MusicEnable
        {
            get => isMusicEnable.Value;
            set
            {
                if (isMusicEnable.Value != value)
                {
                    foreach (var key in sourceMedia.Keys)
                    {
                        var sourceData = sourceMedia[key];
                        if (sourceData.IsMusic)
                        {
                            sourceData.Source.volume = value ? sourceData.RequestedVolume : 0;
                        }
                    }

                    isMusicEnable.Value = value;
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
            
            isSoundEnable = new SavableValue<bool>("Sound", true);
            isMusicEnable = new SavableValue<bool>("Music", true);
        }

        public int PlayMusic(AudioClip clip, float volume)
        {
            volume = Mathf.Clamp01(volume);

            ScanForEndedSounds();
            audioCodeIndex++;

            var childSource = new GameObject
            {
                name = clip.name
            };

            childSource.transform.SetParent(transform);

            var source = childSource.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = true;
            source.priority = 0;

            var data = new AudioSourceData
            {
                Source = source,
                AudioCode = audioCodeIndex,
                RequestedVolume = volume,
                IsMusic = true,
                IsOnPause = false
            };

            source.volume = MusicEnable ? volume : 0;
            source.Play();
            sourceMedia.Add(data.AudioCode, data);

            return audioCodeIndex;
        }

        public int PlaySound(AudioClip clip, float volume)
        { 
            volume = Mathf.Clamp01(volume);

            ScanForEndedSounds();
            audioCodeIndex++;

            var childSource = new GameObject
            {
                name = clip.name
            };

            childSource.transform.SetParent(transform);

            var source = childSource.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = false;
            source.priority = 0;

            var data = new AudioSourceData
            {
                Source = source,
                AudioCode = audioCodeIndex,
                RequestedVolume = volume,
                IsMusic = false,
                IsOnPause = false
            };

            source.volume = MusicEnable ? volume : 0;
            source.Play();
            sourceMedia.Add(data.AudioCode, data);

            return audioCodeIndex;
        }

        public void StopPlayingClip(int audioCode)
        {
            if (!sourceMedia.ContainsKey(audioCode)) return;

            var s = sourceMedia[audioCode];
            sourceMedia.Remove(audioCode);
            s.Source.Stop();
            Destroy(s.Source.gameObject);
        }

        public void PausePlayingClip(int audioCode)
        {
            if (!sourceMedia.ContainsKey(audioCode)) return;

            var s = sourceMedia[audioCode];
            sourceMedia.Remove(audioCode);
            s.Source.Pause();
            s.IsOnPause = true;
        }

        public void UnpausePlayingClip(int audioCode)
        {
            if (!sourceMedia.ContainsKey(audioCode)) return;

            var s = sourceMedia[audioCode];
            sourceMedia.Remove(audioCode);
            s.Source.UnPause();
            s.IsOnPause = false;
        }

        public bool IsClipPlaying(int audioCode)
        {
            if (!sourceMedia.ContainsKey(audioCode)) return false;

            var s = sourceMedia[audioCode];
            return s.Source.isPlaying;
        }

        private void ScanForEndedSounds()
        {
            var toDelete = new Dictionary<int, AudioSourceData>();

            foreach (var k in sourceMedia.Keys)
            {
                var source = sourceMedia[k];

                if (!source.IsOnPause && !source.Source.isPlaying) toDelete.Add(k, source);
            }

            foreach (var k in toDelete.Keys)
            {
                var source = toDelete[k];
                sourceMedia.Remove(k);

                Destroy(source.Source.gameObject);
            }
        }

        private class AudioSourceData
        {
            public AudioSource Source;
            public int AudioCode;
            public float RequestedVolume;
            public bool IsMusic;
            public bool IsOnPause;
        }
    }
}
