using System.Collections.Generic;
using UnityEngine;

namespace BingoCity
{
    public class SoundUtils
    {
        public static AudioSource audioSource;
        public static Dictionary<AudioTrackNames, AudioClip> tracks = new Dictionary<AudioTrackNames, AudioClip>();

        public static void SetAudioSource(AudioSource source)
        {
            audioSource = source;
        }

        public static void AddTrack(AudioTrackNames trackName, AudioClip trackFile)
        {
            if (!tracks.ContainsKey(trackName))
                tracks.Add(trackName, trackFile);
        }

        public static void PlaySoundOnce(AudioTrackNames trackName)
        {
            if (tracks.ContainsKey(trackName))
            {
                audioSource.clip = tracks[trackName];
                audioSource.PlayOneShot(audioSource.clip);
            }
            else
            {
                Debug.Log($"--trackName {trackName} not found");
            }
        }
    }
}