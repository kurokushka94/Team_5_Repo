using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeScript : MonoBehaviour
{
	public AudioMixer masterMixer;

	public void SetMasterVolume(float _volume)
	{
		masterMixer.SetFloat("MasterVolume", Mathf.Log(_volume) * 20);
		PlayerPrefs.SetFloat("MasterVolume", _volume);
		PlayerPrefs.Save();
	}

	public void SetMusicVolume(float _volume)
	{
		masterMixer.SetFloat("MusicVolume", Mathf.Log(_volume) * 20);
		PlayerPrefs.SetFloat("MusicVolume", _volume);
		PlayerPrefs.Save();
	}

	public void SetSFXVolume(float _volume)
	{
		masterMixer.SetFloat("SFXVolume", Mathf.Log(_volume) * 20);
		PlayerPrefs.SetFloat("SFXVolume", _volume);
		PlayerPrefs.Save();
	}
}
