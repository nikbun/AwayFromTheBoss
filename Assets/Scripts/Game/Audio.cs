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

	public void Start()
	{
		CreateAudioSource();
	}

	public void PlayClickSound()
	{
		PlaySound(_clickSound);
	}

	public void PlaySound(AudioClip audioClip, Transform parent = null, bool randomPitch = false)
	{
		var soundSource = CreateAudioSource(parent);
		if (randomPitch)
		{
			soundSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
		}
		soundSource.PlayOneShot(audioClip);
		Destroy(soundSource.gameObject, audioClip.length);
	}

	private AudioSource CreateAudioSource(Transform parent = null)
	{
		var isGlobal = parent == null;
		if (isGlobal)
		{
			parent = this.transform;
		}

		var audioSourceObject = Instantiate(new GameObject("AudioSource"), parent);
		var audioSource = audioSourceObject.AddComponent<AudioSource>();
		audioSource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups(soundMixerGroup).First();
		audioSource.playOnAwake = false;

		if (!isGlobal)
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
