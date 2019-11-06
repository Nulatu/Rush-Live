using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class RoundsSystem : MonoBehaviour 
{
    public static RoundsSystem In;

    [HideInInspector] public List<Round> Rounds;
	
	public int CurrentRound = 0;

    [SerializeField] private Image imageRound = default;
    [SerializeField] private Text textRound = default;
    [SerializeField] private Spawner Spawner = default;
    [SerializeField] private AudioSource soundLevelUp = default;

    private int killCount; // How many enemies have been killed in this round
    private int amountRounds = 100;
    private Color basicColorTextRound;

    public void ChangeKillCount(bool boss,int changeValue) // -> Enemy - Die()
    {
        if (changeValue == 0) // If the enemy approached the dense and self-destructed, then he does not count towards the progress of the round
        {
            if (boss) // This is when the boss self-destructs about the player. He should spawn again in the round. Round failed
                Rounds[CurrentRound].HaveBoss = true;
            ++Spawner.EnemyCount;
        }

        killCount += changeValue;

        if (CurrentRound == 0)
        {
            if (killCount == 6)
                Spawner.SpawnDelay = 0.1f; //  There is no point in changing here Rounds[0].spawnDelay = 0.1f; // the system at this point does not call the round update function(reassignment Spawner.SpawnDelay = Rounds[CurrentRound].spawnDelay;)
            if (killCount == 8)
                Tutorial.In.ActiveFirstStepTutorual();
        }

        if (CurrentRound == 1 && killCount == 8)
        {
            Tutorial.In.ActiveSecondStepTutorual();
        }

        if (killCount >= Rounds[CurrentRound].EnemyCount)
        {
            if (CurrentRound < Rounds.Count - 1)
            {
                RoundUp(1);
            }
        }

        imageRound.fillAmount = killCount * 1.0f / Rounds[CurrentRound].EnemyCount * 1.0f;
    }

    public void DownKillCount() // -> Enemy - OnTriggerEnter()
    {
        if (killCount - 3 < 0)
        {        
            Spawner.EnemyCount += killCount;
            killCount = 0;
        }
        else
        {
            killCount -= 3;
            Spawner.EnemyCount += 3;
        }
        imageRound.DOColor(Color.red,0.5f).OnComplete(() => imageRound.color = basicColorTextRound);
        imageRound.transform.DOPunchScale(Vector3.one*0.2f, 0.3f,vibrato:0).OnComplete(() => imageRound.transform.localScale = Vector3.one);
        imageRound.fillAmount = killCount * 1.0f / Rounds[CurrentRound].EnemyCount * 1.0f;
    }

    public void RoundUp(int amountRound) // -> TestRoundsButton - OnPointerClick() 
    {
        CurrentRound += amountRound;
        killCount = 0;
        UpdateRound();
        soundLevelUp.Play();
    }

    private void Awake()
    {
        basicColorTextRound = textRound.color;

        Rounds = new List<Round>(amountRounds);
        for (int i = 0 ; i < amountRounds; i++)
        {
            Rounds.Add(new Round());
            Rounds[i].HaveBoss = true;
            Rounds[i].BossDelay = 1;
            Rounds[i].EnemyCount = 5 * (i + 3);
            Rounds[i].EnemySpeed = 1;
            Rounds[i].SpawnDelay = 0.5f;
        }

        Rounds[0].EnemyCount = 20; // print(Rounds[1].enemyCount); // current = 20 ; print(Rounds[2].enemyCount); // current = 25
        Rounds[0].SpawnDelay = 0.8f;
        Rounds[1].SpawnDelay = 0.2f;
    }

    private void Start()
    {
        UpdateRound();
    }

    private void UpdateRound()
	{
        if (Rounds.Count == 0) return;

        textRound.text = (CurrentRound + 1).ToString();
        imageRound.fillAmount = 0;
        Spawner.EnemyCount = Rounds[CurrentRound].EnemyCount;
        Spawner.SpawnDelay = Rounds[CurrentRound].SpawnDelay;
	}     
}
