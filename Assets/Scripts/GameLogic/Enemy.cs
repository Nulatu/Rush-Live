using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    None, // Boss and simple Enemy
    EnergyEater,
    Rush
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem lowHealthEffect = default; // The effect that appears on the enemy when it has low health, near death
    [SerializeField] private Transform deathEffect = default; // The effect that is created at the location of this enemy when it dies
    [SerializeField] private AnimationClip spawnAnimation = default;
    [SerializeField] private AnimationClip hurtAnimation = default;
    [SerializeField] private AnimationClip moveAnimation = default;
    [SerializeField] private AnimationClip attackAnimation = default;
    [SerializeField] private AudioClip soundHitTarget = default; // Various sounds that play when the enemy touches the target, or when it gets hurt
    [SerializeField] private AudioClip soundLand = default;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private Image hpBar = default;
    [SerializeField] private ParticleSystem particleGold = default;
    [SerializeField] private ParticleSystem particleEnemyType  = default;

    private double coins = 1;
    private double health = 1; // The enemy's health. If health reaches 0, this enemy dies
    private double healthMax;
    private float hurtTime = 1; 
    private Transform thisTransform;
    private bool isSpawning = true; // The enemy is still spawning, it won't move until it finises spawning
    private Animation enemyAnimation;
    private Transform target;
    private bool selfDestruction;
    private EnemyType type;
    private Vector3 rushTarget; // variable for EnemyType.Rush

    public void SetType(EnemyType getType) // -> Spawner - Spawn()
    {
        type = getType;
    }

    public void EnableGoldEnemy() // -> Player - GoldEnemies()
    {
        particleGold.Play();
    }

    public void SetSpeed(float valueSpeedCurrentRound) // -> Spawner - Spawn() and SpawnBoss()
    {
        moveSpeed *= valueSpeedCurrentRound;
    }

    public void PlaySoundLandInAnimation() // In the "spawnAnimation" animation, this function is called in the frame of contact with the ground (this is not the last frame of the animation)
    {
        SoundManager.In.PlayOneShotSound(GetComponent<AudioSource>(), soundLand);
        GetComponent<BoxCollider>().enabled = true;
    }

    private void Start()
    {
        if (tag == "Boss")
        {
            coins *= 10;
            health *= 10;
        }

        target = GameManager.In.PlayerObject.transform;
        enemyAnimation = GetComponent<Animation>();
        thisTransform = transform;
        healthMax = health + RoundsSystem.In.CurrentRound;
        health = healthMax;

        if(tag != "Boss")
            GetComponent<BoxCollider>().enabled = true; 

        StartCoroutine(PlayAnimation(spawnAnimation, moveAnimation));

        if (type == EnemyType.Rush)
        { ///////// Rush \\\\\\\\\
            rushTarget = GameManager.In.PlayerObject.transform.position;
            ParticleSystem.MainModule pm = particleEnemyType.main;
            pm.startColor = Color.red;
            moveSpeed *= 10;
            particleEnemyType.Play();
        } ///////// Rush \\\\\\\\\

        if (type == EnemyType.EnergyEater) 
        { ///////// EnergyEater \\\\\\\\\
            particleEnemyType.Play();
            List<float> distances = new List<float>();
            Collider[] energyPoints = Physics.OverlapSphere(thisTransform.position, 100, LayerMask.GetMask("Energy"));
            for (int i = 0; i < energyPoints.Length; i++)
            {
                distances.Add(Vector3.Distance(thisTransform.position, energyPoints[i].transform.position));
            }
            int indexEnergy = distances.FindIndex(x => x == distances.Min());
            target = energyPoints[indexEnergy].transform;
        } ///////// EnergyEater \\\\\\\\\
    }

    private void Update()
    {
        if (isSpawning == false)
        {
            if (type != EnemyType.Rush)
            {
                thisTransform.position = Vector3.MoveTowards(thisTransform.position, target.position, moveSpeed * Time.deltaTime * 2);
                if (target.position - thisTransform.position != Vector3.zero) // fix bug Look Vector3 zero
                    thisTransform.rotation = Quaternion.LookRotation(target.position - thisTransform.position);
                else
                {
                    if (type == EnemyType.EnergyEater)
                    { ///////// EnergyEater \\\\\\\\\
                        enabled = false; // In order to get into this if once in Update
                        enemyAnimation.Stop();
                        InvokeRepeating("EatEnergy", 1, 1);
                    } ///////// EnergyEater \\\\\\\\\
                }
            }
            else
            {
                thisTransform.position = Vector3.MoveTowards(thisTransform.position, rushTarget, moveSpeed * Time.deltaTime * 2);
                if (rushTarget - thisTransform.position != Vector3.zero) // fix bug Look Vector3 zero
                    thisTransform.rotation = Quaternion.LookRotation(rushTarget - thisTransform.position);
                else
                {
                    enabled = false; // In order to get into this if once in Update
                    Die(true);
                }
            }
        }
    }

    private void EatEnergy()
    {
        target.GetComponent<EnergyPoint>().DownEnergy();
    }

    private void OnTriggerEnter(Collider other) // When Enemy Encounters a Player
    {
        if (other.GetComponent<Player>())
        {
            if(type == EnemyType.Rush)
                RoundsSystem.In.DownKillCount();
            StartCoroutine(PlayAnimation(attackAnimation, moveAnimation));
            SoundManager.In.PlayOneShotSound(GetComponent<AudioSource>(), soundHitTarget);
            if (!other.GetComponent<Player>().ActivatedEnergy()) // When a player in energy moves, he should not kill Enemi with a delay and receive gray coins
                Invoke("DelayDie", 0.5f);
            else
                Die(false);
        }
    }

    private void DelayDie()
    {
        Die(true);
    }

    private IEnumerator PlayAnimation(AnimationClip firstAnimation, AnimationClip defaultAnimation)
    { // Default animation to be played after first animation is done
        enemyAnimation.Stop();
        enemyAnimation.Play(firstAnimation.name);
		
		yield return new WaitForSeconds(firstAnimation.length);

        // If the spawning animation finished, we are no longer spawning and can start moving
        if (isSpawning == true && firstAnimation == spawnAnimation)
        {
            isSpawning = false;
        }

        enemyAnimation.Stop();
        enemyAnimation.CrossFade(defaultAnimation.name);
    }

    private IEnumerator ChangeHealth(double changeValue)
    {
        health += changeValue;
        hpBar.fillAmount = (float) (health / healthMax);

        if (health <= 0.3f * healthMax || health == 1)
        {
            lowHealthEffect.Play();
        }
        else if (lowHealthEffect.isEmitting == true)
        {
            lowHealthEffect.Stop();
        }      

        if (health <= 0)
        { 
            Die(false);
        }

		if (changeValue < 0)
		{
            enemyAnimation.CrossFade(hurtAnimation.name);		

			yield return new WaitForSeconds(hurtTime);

            enemyAnimation.CrossFade(moveAnimation.name);		
		}
	}

    private void Die(bool selfDestruction)
	{
        Transform deathEffectObject = Instantiate(deathEffect, transform.position, Quaternion.identity);
        SoundManager.In.PlaySound(deathEffectObject.GetComponent<AudioSource>());

        if (selfDestruction)
        {
            GetCoins(Color.gray);
            bool deathBoos = tag == "Boss" ? true : false;
            RoundsSystem.In.ChangeKillCount(deathBoos,0);
        }
        else
        {
            GetCoins(new Color(1, 0.58f, 0, 1));
            RoundsSystem.In.ChangeKillCount(false,1);
        }

		Destroy(gameObject);
	}

    private void GetCoins(Color colorTextCoins) // -> Enemy - Die() // Enemy death UI coin animation
    {
        coins *= RoundsSystem.In.CurrentRound + 1;
        coins += UpdateData.In.Updates["PROFIT"].GetData(0);
        GameManager.In.OfflineEarningsOneSecond = coins; // for OfflineEarningsOneSecond
        if (particleGold.isPlaying)
            coins *= 2;
        if (colorTextCoins == Color.gray)
            coins /= 4;
        Transform newGetCoinsAnimation = Instantiate(GameManager.In.AnimationPrefabGetCoins, thisTransform.position, Quaternion.identity) as Transform;
        newGetCoinsAnimation.GetChild(0).GetComponent<Text>().color = colorTextCoins;
        newGetCoinsAnimation.GetChild(0).GetComponent<Text>().text = "+" + coins.ToString();
        if (particleGold.isPlaying)
            newGetCoinsAnimation.GetChild(0).GetComponent<Text>().text += "  X2";

        newGetCoinsAnimation.eulerAngles = Vector3.forward * Random.Range(-10, 10); // adding variety to the coin spawn text spawn
        Income.In.ChangeCoins(coins * (RoundsSystem.In.CurrentRound + 1));
    }
}
