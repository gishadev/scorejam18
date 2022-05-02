using System;
using System.Collections;
using UnityEngine;

namespace Gisha.Effects.Audio
{
    public class AudioManager : ImportTarget
    {
        #region Singleton
        public static AudioManager Instance { private set; get; }
        #endregion

        public AudioData[] musicCollection = default;
        public AudioData[] sfxCollection = default;
        public float fadeTransitionSpeed = default;

        AudioData _currentMusic;
        AudioData _previousMusic;

        public float MusicVolume
        {
            get => _musicVolume;
            set { _musicVolume = Mathf.Clamp01(value); }
        }
        float _musicVolume = 1f;

        public float SfxVolume
        {
            get => _sfxVolume;
            set { _sfxVolume = Mathf.Clamp01(value); }
        }
        float _sfxVolume = 1f;

        public bool IsMusicMuted => MusicVolume == 0;
        public bool IsSfxMuted => SfxVolume == 0;


        void Awake()
        {
            CreateInstance();

            SetUpAudioArray(musicCollection);
            SetUpAudioArray(sfxCollection);
        }

        private void CreateInstance()
        {
            DontDestroyOnLoad(gameObject);

            if (Instance == null)
                Instance = this;
            else
            {
                if (Instance != this)
                    Destroy(gameObject);
            }
        }

        private void SetUpAudioArray(AudioData[] _array)
        {
            for (int i = 0; i < _array.Length; i++)
            {
                GameObject child = new GameObject(_array[i].Name);
                child.transform.SetParent(transform);

                AudioSource audioSource = child.AddComponent<AudioSource>();

                _array[i].GameObject = child;
                _array[i].AudioSource = audioSource;

                _array[i].AudioSource.clip = _array[i].AudioClip;
                _array[i].AudioSource.volume = _array[i].Volume;
                _array[i].AudioSource.pitch = _array[i].Pitch;
                _array[i].AudioSource.loop = _array[i].IsLooping;
            }
        }

        #region Play Music
        public void PlayMusic(string _name)
        {
            Debug.Log("Playing Music");
            AudioData data = Array.Find(musicCollection, bgm => bgm.Name == _name);
            if (data == null)
            {
                Debug.LogError("There is no music with name " + _name);
                return;
            }
            else
            {
                _previousMusic = _currentMusic;
                _currentMusic = data;

                PlayNextMusicTrack();
            }
        }

        public void PlayMusic(int index)
        {
            if (index < 0 || index > musicCollection.Length - 1)
            {
                Debug.LogError("There is no music with index " + index);
                return;
            }
            AudioData data = musicCollection[index];

            _previousMusic = _currentMusic;
            _currentMusic = data;

            PlayNextMusicTrack();
        }

        private void PlayNextMusicTrack()
        {
            _currentMusic.AudioSource.Play();

            if (!_currentMusic.IsFade && _previousMusic != null) _previousMusic.AudioSource.Stop();
            if (_currentMusic.IsFade)
            {
                StartCoroutine(FadeIn(_currentMusic));
                if (_previousMusic != null) StartCoroutine(FadeOut(_previousMusic));
            }
        }
        #endregion

        #region Play SFX
        public void PlaySFX(string _name)
        {
            AudioData data = Array.Find(sfxCollection, sfx => sfx.Name == _name);
            if (data == null)
            {
                Debug.LogError("There is no sfx with name " + _name);
                return;
            }
            else
            {
                data.AudioSource.Play();
            }
        }

        public void PlaySFX(int index)
        {
            if (index < 0 || index > sfxCollection.Length - 1)
            {
                Debug.LogError("There is no sfx with index " + index);
                return;
            }

            AudioData data = sfxCollection[index];
            data.AudioSource.Play();
        }
        #endregion

        #region Fade Transition
        private IEnumerator FadeIn(AudioData _audioData)
        {
            _audioData.AudioSource.volume = 0;
            float volume = _audioData.AudioSource.volume;

            while (_audioData.AudioSource.volume < _audioData.Volume)
            {
                volume += fadeTransitionSpeed * Time.deltaTime;
                _audioData.AudioSource.volume = volume;
                yield return new WaitForSeconds(0.1f);
            }
        }

        private IEnumerator FadeOut(AudioData _audioData)
        {
            float volume = _audioData.AudioSource.volume;

            while (_audioData.AudioSource.volume > 0)
            {
                volume -= fadeTransitionSpeed * Time.deltaTime;
                _audioData.AudioSource.volume = volume;
                yield return new WaitForSeconds(0.1f);
            }
            if (_audioData.AudioSource.volume == 0)
            {
                _audioData.AudioSource.Stop();
                _audioData.AudioSource.volume = _audioData.Volume;
            }
        }
        #endregion

        #region Volume
        public void SetMusicVolume(float volume)
        {
            MusicVolume = volume;

            for (int i = 0; i < musicCollection.Length; i++)
                musicCollection[i].AudioSource.volume = MusicVolume;
        }

        public void SetSFXVolume(float volume)
        {
            SfxVolume = volume;

            for (int i = 0; i < sfxCollection.Length; i++)
                sfxCollection[i].AudioSource.volume = volume;
        }
        #endregion

        #region ImportTarget
        public override void Import(string _collection, ResourceData[] _resources)
        {
            AudioData[] newCollection = new AudioData[_resources.Length];

            for (int i = 0; i < _resources.Length; i++)
            {
                AudioData data = new AudioData(
                    _resources[i].name,
                    _resources[i].o as AudioClip,
                    1f,
                    1f);

                newCollection[i] = data;
            }

            GetType().GetField(_collection).SetValue(this, newCollection);
        }
        #endregion
    }
}