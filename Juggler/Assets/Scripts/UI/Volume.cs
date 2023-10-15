using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private GameObject volumeIcon;
    [SerializeField] private GameObject muteIcon;

    private const float minVolumeVal = 0.0001f;

    public void SetVolume(float volume)
    {
        mixer.SetFloat("Volume", Mathf.Log10 (volume) * 20);

        SetMute(volume == minVolumeVal);
    }

    private void SetMute(bool mute)
    {
        muteIcon.SetActive(mute);
        volumeIcon.SetActive(!mute);
    }
}
