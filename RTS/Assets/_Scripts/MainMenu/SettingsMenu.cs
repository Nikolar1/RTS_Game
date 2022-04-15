using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace NR.RTS.MainMenu
{
    public class SettingsMenu : MonoBehaviour
    {
        public AudioMixer mainMixer;
        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public void SetVolume(float volume)
        {
            mainMixer.SetFloat("mainVolume", volume);
        }

        public void SetMusicVolume(float volume)
        {
            mainMixer.SetFloat("musicVolume", volume);
        }

        public void SetVoiceVolume(float volume)
        {
            mainMixer.SetFloat("voiceVolume", volume);
        }

        public void SetEffectVolume(float volume)
        {
            mainMixer.SetFloat("effectsVolume", volume);
        }

        readonly List<int> widths = new List<int>() {720,1280,1366,1600,1920,2560,3840};
        readonly List<int> heights = new List<int>() {480,720,768,900,1080,1440,2160};

        public void SetResolution(int index)
        {
            bool fullscreen = Screen.fullScreen;
            int width = widths[index];
            int height = heights[index];
            Screen.SetResolution(width, height, fullscreen);
        }
    }
}
