using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
	[SerializeField] private AudioMixer _audioMixer;
	[Header("Музыка")]
	[SerializeField] private AudioSource _musicSource;
	[SerializeField] private AudioClip _menuMusic;
	[SerializeField] private AudioClip _gameMusic;
	[Header("Звуки")]
	[SerializeField] private AudioClip _clickSound;

	private const string masterMixerGroup = "Master";
	private const string soundMixerGroup = "Sound";
	private const string musicMixerGroup = "Music";


	public static Audio Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}

		DontDestroyOnLoad(this.gameObject);
	}

	public void PlayClickSound()
	{
		PlaySound(_clickSound);
	}

	public void PlaySound(AudioClip audioClip, Transform parent = null, bool randomPitch = false)
	{
		var audioSource = CreateAudioSource(parent != null);

		audioSource.transform.SetParent(parent, false);

		if (randomPitch)
		{
			audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
		}
		audioSource.PlayOneShot(audioClip);
		Destroy(audioSource.gameObject, audioClip.length);
	}

	private AudioSource CreateAudioSource(bool isSpatial)
	{
		var audioSourceObject = new GameObject("AudioSource");
		var audioSource = audioSourceObject.AddComponent<AudioSource>();
		audioSource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups(soundMixerGroup).First();
		audioSource.playOnAwake = false;

		if (isSpatial)
		{
			audioSource.spatialBlend = 1f;
			audioSource.maxDistance = 20f;
			audioSource.rolloffMode = AudioRolloffMode.Custom;
		}

		return audioSource;
	}

	public void PlayMenuMusic()
	{
		PlayMusic(_menuMusic);
	}

	public void PlayGameMusic()
	{
		PlayMusic(_gameMusic);
	}

	public void PlayMusic(AudioClip music)
	{
		if (_musicSource.clip != music)
		{
			_musicSource.clip = music;
			_musicSource.Play();
		}
	}

	public void SetSoundMute(bool isMute)
	{
		_audioMixer.SetFloat(soundMixerGroup, isMute ? -80f : 0f);
	}

	public void SetMusicMute(bool isMute)
	{
		_audioMixer.SetFloat(musicMixerGroup, isMute ? -80f : 0f);
	}
}
