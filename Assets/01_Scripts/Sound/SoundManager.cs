using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



namespace Sound.System
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer Master_Mixer; // VolumeMixer 전체
        [SerializeField] private Slider BGM_slider;
        [SerializeField] private Slider SFX_slider;
        [SerializeField] private Slider Master_Slider; // MasterSlider로 보임

        private void Start()
        {
            BGM_slider.onValueChanged.AddListener(SetBGMVolume);
            SFX_slider.onValueChanged.AddListener(SetSFXVolume);
            Master_Slider.onValueChanged.AddListener(SetMasterVolume);

            BGM_slider.value = 1f;
            SFX_slider.value = 1f;
            Master_Slider.value = 1f;
        }

        public void SetBGMVolume(float value)
        {
            Master_Mixer.SetFloat("BGM", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        }

        public void SetSFXVolume(float value)
        {
            Master_Mixer.SetFloat("SFX", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        }

        public void SetMasterVolume(float value)
        {
            Master_Mixer.SetFloat("Master", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        }
    }
}

