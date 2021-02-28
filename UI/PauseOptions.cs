using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Space_Adventures.Saving;


namespace Space_Adventures.UI
{
    public class PauseOptions : MonoBehaviour, ISaveable
    {
        [SerializeField] private AudioMixer audiomixer = null;
        [SerializeField] private GameObject musicTick = null;
        [SerializeField] private GameObject gameSoundTick = null;
        [SerializeField] private Slider musicSlider = null;
        [SerializeField] private Slider gameSoundSlider = null;

        private static float[] volumeSettings = { 0f, 0f }; // 0) Music Volume 1) Game Volume

        public void SetMusicVolume(float volume)
        {
            volumeSettings[0] = volume;
            SetMusicVolumeSettings(volume);
        }

        public void SetGameVolume(float volume)
        {
            volumeSettings[1] = volume;
            SetGameVolumeSettings(volume);
        }

        private void SetMusicVolumeSettings(float volume)
        {
            audiomixer.SetFloat("Options Music Volume", volume);
            musicSlider.value = volume;

            if (volume == -80) { musicTick.gameObject.SetActive(false); }
            else { musicTick.gameObject.SetActive(true); }
        }

        private void SetGameVolumeSettings(float volume)
        {
            gameSoundSlider.value = volume;
            audiomixer.SetFloat("Options Game Volume", volume);

            if (volume == -80) { gameSoundTick.gameObject.SetActive(false); }
            else { gameSoundTick.gameObject.SetActive(true); }
        }

        public void SaveOptions()
        {
            FindObjectOfType<SavingWrapper>().Save();
        }

        public object CaptureState()
        {
            return volumeSettings;
        }

        public void RestoreState(object state)
        {
            if (state == null) return;

            volumeSettings = (float[])state;
            SetMusicVolumeSettings(volumeSettings[0]);
            SetGameVolumeSettings(volumeSettings[1]);
        }
    }
}
