using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager Instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;


	void Awake()
	{
		Instance = this;

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}
	public void AfterPlay(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
		if (GUIManager.Instance.clickCnt == 1)
            Invoke(nameof(SecondClick), s.source.clip.length + 1f);
		if (GUIManager.Instance.clickCnt == 2)
			Invoke(nameof(ShowEndCard), s.source.clip.length + 1f);

	}

    //2 sounds require Ver
    //void SecondClick()
    //   {

    //   }



    //SuperShortVer
    void ShowEndCard()
    {
        GUIManager.Instance.ShowEndCard();
    }
	void SecondClick()
    {
        GUIManager.Instance.SecondClick();
    }
    public void Stop(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.Stop();
	}
	public void StopAll()
	{
		for (int i =0; i<sounds.Length; i++)
        {
			Sound s = sounds[i];
			if (s == null)
			{
				Debug.LogWarning("Sound: " + s.name + " not found!");
				return;
			}
			s.source.Stop();
		}
	}

}
