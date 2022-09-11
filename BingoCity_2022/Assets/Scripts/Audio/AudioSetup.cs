using System;
using UnityEngine;

namespace BingoCity
{
    public class AudioSetup :MonoBehaviour
    {
        [SerializeField] private AudioSource mainSource;

        private void Start()
        {
            SoundUtils.SetAudioSource(mainSource);

            foreach (var audioTrack in GameConfigs.GameConfigData.AudioTracks)
            {
                SoundUtils.AddTrack(audioTrack.audioName,audioTrack.trackFile);
            }
        }
    }

    [SerializeField]
    public class AudioTracks
    {
        public string trackname;
        public AudioClip trackFile;
    }
}