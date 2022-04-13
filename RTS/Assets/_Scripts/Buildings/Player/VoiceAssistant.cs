using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NR.RTS.Player
{
    public class VoiceAssistant : MonoBehaviour
    {
        public enum ClipPurpose
        {
            ConstructionInterupted,
            ConstructionComplete
        };
        public static VoiceAssistant instance;
        public AudioSource voiceAssistantSource;
        public ClipPurpose[] clipPurposes;
        public AudioClip[] voiceAudioClips;
        public List<AudioClip> clipQueue;

        private void Awake()
        {
            instance = this;
        }
        private void Update()
        {
            if (!voiceAssistantSource.isPlaying && clipQueue.Count > 0)
            {
                PlayVoice();
            }
        }

        private void PlayVoice()
        {
            voiceAssistantSource.clip = clipQueue[0];
            voiceAssistantSource.Play();
            clipQueue.Remove(clipQueue[0]);
        }

        public void PlayConstructionInterupted()
        {
            int i = 0;
            foreach (ClipPurpose purpose in clipPurposes)
            {
                
                if (purpose == ClipPurpose.ConstructionInterupted)
                {
                    clipQueue.Add(voiceAudioClips[i]);
                    break;
                }
                i++;
            }
        }
        public void PlayConstructionComplete()
        {
            int i = 0;
            foreach (ClipPurpose purpose in clipPurposes)
            {

                if (purpose == ClipPurpose.ConstructionComplete)
                {
                    clipQueue.Add(voiceAudioClips[i]);
                    break;
                }
                i++;
            }
        }
    }
}