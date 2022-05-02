using UnityEngine;

namespace Gisha.Effects.Audio
{
    [System.Serializable]
    public class AudioData
    {
        [Header("General")]
        [SerializeField] private string name = default;
        [SerializeField] private AudioClip audioClip = default;

        [Header("Settings")]
        [SerializeField] private bool isLooping = default;
        [SerializeField] private bool isFade = default;

        [Range(0f, 1f)]
        [SerializeField] private float volume = default;
        [Range(0.3f, 3f)]
        [SerializeField] private float pitch = default;

        public AudioData(string name, AudioClip audioClip, float volume, float pitch, bool isLooping = false, bool isFade = false)
        {
            this.name = name;
            this.audioClip = audioClip;
            this.volume = volume;
            this.pitch = pitch;
            this.isLooping = isLooping;
            this.isFade = isFade;
        }

        public GameObject GameObject { get; set; }
        public AudioSource AudioSource { get; set; }

        public string Name => name;
        public AudioClip AudioClip => audioClip;
        public bool IsLooping => isLooping;
        public bool IsFade => isFade;
        public float Volume => volume;
        public float Pitch => pitch;
    }
}