using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
    [SerializeField] private float fireRate = 1;   // "The fire rate of the player's weapon. How often it shoots"
    [SerializeField] private AnimationClip spawnAnimation = default;
    [SerializeField] private AnimationClip idleAnimation = default;
    [SerializeField] private Shot shotObject = default; 
    [SerializeField] private Shot redShotObject = default;
    [SerializeField] private Transform shotSource = default; // "The source from which shots are fired"
    [SerializeField] private Transform shootEffect = default;  // "The muzzle effect when shooting"
    [SerializeField] private EnergyPoint currentEnergyPoint = default; // EnergyPointSystem - EnergyPoints[0] = basic start point = zero 
    [SerializeField] private ParticleSystem particleEnergy = default;
    [SerializeField] private ParticleSystem particleEnergyWave = default;
    [SerializeField] private ParticleSystem particleEnergyButton = default;
    [SerializeField] private ParticleSystem particleEnergyJoystick = default;
    [SerializeField] private Color activeEnergyButton = default;
    [SerializeField] private Color inActiveEnergyButton = default;
    [SerializeField] private Image imageEnergyButton = default;
    [SerializeField] private BoxCollider boxCollider = default;

    private int speed = 10;
    private int amountRedShot;
    private float time;
    private Transform targetEnemy;
    private Transform myTransform;
    private Animation playerAnimation;
    private bool moving;
    private bool activatedEnergy;

    public EnergyPoint GetEnergyPoint() // -> EnergyButton - OnPointerClick() 
    {
        return currentEnergyPoint;
    }

    public void UpAmountRedShot() // -> UIUpdateBehavior - ChangeUpdate() 
    {
        ++amountRedShot;
    }

    public bool ActivatedEnergy() // -> Enemy - OnTriggerEnter()
    {
        return particleEnergy.isPlaying;
    }

    public void GoldEnemies()  // -> UIUpdateBehavior - ChangeUpdate() 
    {
        RaycastHit[] enemies = Physics.SphereCastAll(Vector3.zero, 100, Vector2.up,0, LayerMask.GetMask("Enemy"));

        foreach (RaycastHit enemy in enemies)
        {
            enemy.transform.GetComponent<Enemy>().EnableGoldEnemy();
        }
    }

    public void EnableEnergy() // -> EnergyPoint - Activated() 
    {
        fireRate = 0.2f;
        speed = 40; // speed*= 4;
        particleEnergy.Play();
        particleEnergyButton.Play();
        particleEnergyJoystick.Play();
        imageEnergyButton.color = activeEnergyButton;
        particleEnergyWave.Stop(); // fix - so that after turning on the energy (particleEnergy), the particleEnergyWave particle system is not played simultaneously (children automatically lose particles without PlayOnAwake)
    }

    public void DisableEnergy()  // -> EnergyPoint - Deactivated() 
    {
        fireRate = 1f;
        speed = 10; 
        particleEnergy.Stop();
        particleEnergyButton.Clear(true); // Without this function, not all particles will stop.
        particleEnergyButton.Stop();
        particleEnergyJoystick.Stop();
        particleEnergyJoystick.Clear(true); // Without this function, not all particles will stop.
        imageEnergyButton.color = inActiveEnergyButton;
    }

    public void MoveEnergyPoint(EnergyPoint energyPoint , float distance) // -> MoveButton - OnPointerClick() 
    {
        if (moving) return; // previous movement did not end
        moving = true;

        if (particleEnergy.isPlaying) // it only needs to be done when we move in activated energy mode
        {
            currentEnergyPoint.Deactivated(); // from the point at which they started to move
            energyPoint.TryActivate(); // to the point where we are moving

            ParticleSystem.MainModule pm = particleEnergy.main;
            pm.startSize = 2f;
            ParticleSystem.EmissionModule pe = particleEnergy.emission;
            pe.rateOverTime = 240;
            boxCollider.size = Vector3.one * 2; // under the jerk a larger area of Enemy's lesion
        }
        currentEnergyPoint = energyPoint;
        transform.DOMove(energyPoint.transform.position, distance / speed).SetEase(Ease.Linear).OnComplete
        (() => {
            moving = false;
            if (particleEnergy.isPlaying)
            {
                particleEnergyWave.Play();
                HitEnergyWave();

                ParticleSystem.MainModule pm = particleEnergy.main;
                pm.startSize = 1;
                ParticleSystem.EmissionModule pe = particleEnergy.emission;
                pe.rateOverTime = 60;
                boxCollider.size = Vector3.one;
            }

            targetEnemy = null; // In order for the shooting sight not to go astray by Enemy, after movement
        } );
    }

    private void HitEnergyWave()
    {
        RaycastHit[] enemies = Physics.SphereCastAll(transform.position, 3.12f, Vector2.up, 0, LayerMask.GetMask("Enemy"));

        foreach (RaycastHit enemy in enemies)
        {
            enemy.transform.SendMessage("ChangeHealth", - UpdateData.In.Updates["DAMAGE"].GetData(0) * 2, SendMessageOptions.DontRequireReceiver);
        }
    }

	private void Awake() 
	{
        playerAnimation = GetComponent<Animation>();
        myTransform = transform;
        playerAnimation.AddClip( spawnAnimation, spawnAnimation.name);
        playerAnimation.AddClip( idleAnimation, idleAnimation.name);
    }

    private void Update()
    {
        if (!moving) // The player does not shoot while moving
        { 
            time += Time.deltaTime;
            if (time > fireRate)
            {
                if(targetEnemy) // So that the player does not shoot into the void when there are no enemies
                    Shoot();
                time = 0;
            }
        }

      
        List<float> distances = new List<float>();
        Collider[] enemies = Physics.OverlapSphere(myTransform.position, 20, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < enemies.Length; i++)
        {
            distances.Add(Vector3.Distance(myTransform.position, enemies[i].transform.position));
        }
        int index = distances.FindIndex(x => x == distances.Min());
        if (index != -1)
        {
            targetEnemy = enemies[index].transform;
            myTransform.LookAt(targetEnemy);
        }
        

        if (!playerAnimation.isPlaying)
            playerAnimation.Play(idleAnimation.name); 
    }
   
    private void Shoot()
	{
        Shot newShot;
        if (amountRedShot == 0)
            newShot = Instantiate(shotObject) as Shot;
        else
        {
            --amountRedShot;
            newShot = Instantiate(redShotObject) as Shot;
        }

        newShot.transform.position = shotSource.position;
        newShot.transform.rotation = transform.rotation;
        Transform shootEffectObject = Instantiate(shootEffect, newShot.transform.position, newShot.transform.rotation);     
        SoundManager.In.PlaySound(shootEffectObject.GetComponent<AudioSource>());
    }
}

