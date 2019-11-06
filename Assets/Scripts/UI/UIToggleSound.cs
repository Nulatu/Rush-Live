using UnityEngine;
using UnityEngine.UI;

public class UIToggleSound : MonoBehaviour
{
	[SerializeField] private AudioSource music = default; 
    [SerializeField] private AudioSource sounds = default;
    [SerializeField] private Image musicButton = default;
    [SerializeField] private Image soundsButton = default;

    private int musicOnOff;
    private int soundOnOff;

    public void ChangeMusic() // -> OnClick = PausePanel -> ButtonMusic
    {
        PlayerPrefs.SetInt("Music", PlayerPrefs.GetInt("Music") == 0 ? 1 : 0);
        musicOnOff = PlayerPrefs.GetInt("Music");
        music.volume = 1 - musicOnOff;

        ChangeUIToggleSound(music);
    }

    public void ChangeSound() // -> OnClick = PausePanel -> ButtonSound
    {
        PlayerPrefs.SetInt("Sound", PlayerPrefs.GetInt("Sound") == 0 ? 1 : 0);
        soundOnOff = PlayerPrefs.GetInt("Sound");
        sounds.volume = 1 - soundOnOff;

        ChangeUIToggleSound(sounds);
    }

    private void Awake()
	{
        musicOnOff = PlayerPrefs.GetInt("Music"); // 0 - ONN = default; 1 - OFF
        soundOnOff = PlayerPrefs.GetInt("Sound"); // 0 - ONN = default; 1 - OFF

        if (musicOnOff == 1)
        {
            music.volume = 1 - musicOnOff;
            ChangeUIToggleSound(music);
        }
        if (soundOnOff == 1)
        {
            sounds.volume = 1 - soundOnOff;
            ChangeUIToggleSound(sounds);
        }
	}

    private void ChangeUIToggleSound(AudioSource soundObject)
	{
        Image changingImage = soundObject == music ? musicButton : soundsButton;

        Color newColor = changingImage.material.color;

		if (soundObject.volume == 1)
			newColor.a = 1;
		else
			newColor.a = 0.5f;

        changingImage.color = newColor;
	}
}
