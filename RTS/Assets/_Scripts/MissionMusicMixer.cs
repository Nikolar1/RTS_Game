using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS
{
    public class MissionMusicMixer : MonoBehaviour
    {
        public static MissionMusicMixer instance;
        public AudioSource musicSource;
        public AudioClip[] peacefullAudioClips;
        public AudioClip[] battleAudioClips;
        public bool isPeacefull = true;
        public float peacefullStateCooldown = 20;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            PlayRandomMusic();
        }

        private void Update()
        {
            if (!musicSource.isPlaying)
            {
                PlayRandomMusic();
            }
            if (!isPeacefull)
            {
                peacefullStateCooldown -= Time.deltaTime;
            }
            if (peacefullStateCooldown <= 0)
            {
                SetPeacefull(true);
                peacefullStateCooldown = 20;
            }
        }

        private void PlayRandomMusic()
        {
            if (isPeacefull)
            {
                musicSource.clip = peacefullAudioClips[Random.Range(0, peacefullAudioClips.Length)];
            }
            else
            {
                musicSource.clip = battleAudioClips[Random.Range(0, battleAudioClips.Length)];
            }
            musicSource.Play();
        }

        public void SetPeacefull(bool isPeacefull)
        {
            if (this.isPeacefull != isPeacefull)
            {
                this.isPeacefull = isPeacefull;
                PlayRandomMusic();
            }
            else if (!isPeacefull)
            {
                peacefullStateCooldown = 20;
            }

        }
    }
}
