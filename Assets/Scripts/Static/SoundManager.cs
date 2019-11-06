using System;
using UnityEngine;

public enum Sounds
{
    Upgrade,
    GetCoinsReward
}
// Many links to other scripts. Called from everywhere the public functions of this script
public class SoundManager : MonoBehaviour
{
    public static SoundManager In;

    public AudioClip Upgrade, GetCoinsReward;

    private AudioSource audioSource;

    public void PlaySound(Sounds s) // Sounds that do not overlap each other. Do not mix. Do not play at the same time.
    {
        switch (s)
        {
            case Sounds.Upgrade: audioSource.PlayOneShot(Upgrade); break;
            case Sounds.GetCoinsReward: audioSource.PlayOneShot(GetCoinsReward); break;
        }
    }

    public void PlaySound(string sound) // Sounds that do not overlap each other. Do not mix. Do not play at the same time.
    {
        PlaySound((Sounds)Enum.Parse(typeof(Sounds), sound, true));
    }

    public void PlaySound(AudioSource getNewAudioSource) // Sounds that overlap each other. Are mixed. Played at the same time.
    {
        if (PlayerPrefs.GetInt("Sound") == 1) return;
        getNewAudioSource.Play();
    }

    public void PlayOneShotSound(AudioSource getNewAudioSource, AudioClip getAudoiClip) // Sounds that overlap each other. Are mixed. Played at the same time.
    {
        if (PlayerPrefs.GetInt("Sound") == 1) return;
        getNewAudioSource.PlayOneShot(getAudoiClip);
    }

    private void Awake()
    {
        In = this;
        audioSource = GetComponent<AudioSource>();
    }
}
