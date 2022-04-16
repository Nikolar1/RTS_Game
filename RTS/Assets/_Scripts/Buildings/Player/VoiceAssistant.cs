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
            ConstructionComplete,
            TrainingComplete,
            TraniningCanceled,
            UnitLost,
            UnderAttack,
            BuildingDestroyed,
            ResourceDepleated
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

        private void AddToClipQueue(AudioClip clip)
        {
            if (clipQueue.Count<3)
            {
                clipQueue.Add(clip);
            }
        }

        public void PlayClip(ClipPurpose desiredPurpose)
        {
            int i = 0;
            foreach (ClipPurpose purpose in clipPurposes)
            {

                if (purpose == desiredPurpose)
                {
                    AddToClipQueue(voiceAudioClips[i]);
                    break;
                }
                i++;
            }
        }

        public void PlayConstructionInterupted()
        {
            PlayClip(ClipPurpose.ConstructionInterupted);
        }
        public void PlayConstructionComplete()
        {
            PlayClip(ClipPurpose.ConstructionComplete);
        }
        public void PlayTrainingComplete()
        {
            PlayClip(ClipPurpose.TrainingComplete);
        }
        public void PlayTrainingCanceled()
        {
            PlayClip(ClipPurpose.TraniningCanceled);
        }
        public void PlayUnitLost()
        {
            PlayClip(ClipPurpose.UnitLost);
        }
        public void PlayUnderAttack()
        {
            PlayClip(ClipPurpose.UnderAttack);
        }
        public void PlayBuildingDestroyed()
        {
            PlayClip(ClipPurpose.BuildingDestroyed);
        }
        public void PlayResourceDepleated()
        {
            PlayClip(ClipPurpose.ResourceDepleated);
        }
    }
}