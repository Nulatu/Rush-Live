using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int EnemyCount = 0; // How many enemies are left in this round
    public float SpawnDelay = 1; // "How may seconds to wait between enemy spawns"

    [SerializeField] private Enemy[] enemies = default;
    [SerializeField] private Enemy[] bosses = default;

    private Vector2 spawnDistance = new Vector2(5, 10); // "The distance at which this object is spawned relative to the spawnAroundObject"
    private float time = 0;
    private int spawnAngleX = 0;
    private int spawnAngleY = 360;

    private void Update()
    {
        if (EnemyCount > 1) // spawn all in the current round means enemyCount = 0
        {
            if (time > 0)
                time -= Time.deltaTime;
            else
            {
                time = SpawnDelay;
                Spawn();
                EnemyCount--;
            }
        }
        else if (EnemyCount == 1 && RoundsSystem.In.Rounds[RoundsSystem.In.CurrentRound].HaveBoss)
        {
            RoundsSystem.In.Rounds[RoundsSystem.In.CurrentRound].HaveBoss = false;
            StartCoroutine("SpawnBoss");
        }
    }

    private void Spawn()
    {
        int spawnIndex = Mathf.FloorToInt(Random.Range(0, enemies.Length));
        Transform newSpawn = Instantiate(enemies[spawnIndex].transform) as Transform;
        if (RoundsSystem.In.CurrentRound < 2) // For the first step of the tutorial, the spawn should be closer to the lower left point of the energy = new Vector3(-5, 0, -5);
        {
            newSpawn.position = new Vector3(-5, 0, -5);
            spawnDistance = new Vector2(5, 5);
            if (RoundsSystem.In.CurrentRound == 1)
            { // Enemy spawn under northeast player jerk
                spawnAngleX = 35;
                spawnAngleY = 60;
            }
        }
        else
        {
            spawnAngleX = 0;
            spawnAngleY = 360;
            newSpawn.position = Vector3.zero;
            spawnDistance = new Vector2(5, 10);
        }

        newSpawn.eulerAngles = Vector3.up * Random.Range(spawnAngleX, spawnAngleY); // Rotate the object randomly, and then move it forward to a random distance from the spawn point
        newSpawn.Translate(Vector3.forward * Random.Range(spawnDistance.x, spawnDistance.y), Space.Self);       
        newSpawn.eulerAngles += Vector3.up * 180; // otherwise Enemy will spawn with his back to the player

        if (RoundsSystem.In.CurrentRound > 1) // the first two rounds is a tutorial (0 и 1)
        {
            if ((Random.Range(0,4)) == 0) // to spawn less often increase the number
                newSpawn.GetComponent<Enemy>().SetType(EnemyType.EnergyEater);

            float distPlayterEnemy = Vector3.Distance(newSpawn.position, GameManager.In.PlayerObject.transform.position);

            if ((Random.Range(0, 4)) == 0 && distPlayterEnemy > 8f) // to spawn less often increase the number
                newSpawn.GetComponent<Enemy>().SetType(EnemyType.Rush);
        }

        newSpawn.SendMessage("SetSpeed", RoundsSystem.In.Rounds[RoundsSystem.In.CurrentRound].EnemySpeed);
    }

    private IEnumerator SpawnBoss()
    {
        Transform newBoss = Instantiate(bosses[0].transform) as Transform;
        newBoss.gameObject.SetActive(false);

        yield return new WaitForSeconds(RoundsSystem.In.Rounds[RoundsSystem.In.CurrentRound].BossDelay);

        newBoss.gameObject.SetActive(true);

        if (RoundsSystem.In.CurrentRound < 2) // For the first step of the tutorial, the spawn should be closer to the lower left point of the energy = new Vector3(-5, 0, -5);
        {
            newBoss.position = new Vector3(-5, 0, -5);
            if(RoundsSystem.In.CurrentRound == 1)
                newBoss.position = Vector3.zero;
            spawnDistance = new Vector2(5, 5);
        }
        else
        {
            newBoss.position = Vector3.zero;
            spawnDistance = new Vector2(5, 10);
        }

        newBoss.eulerAngles = Vector3.up * Random.Range(0, 360);  // Rotate the object randomly, and then move it forward to a random distance from the spawn point
        newBoss.Translate(Vector3.forward * Random.Range(spawnDistance.x, spawnDistance.y), Space.Self);
        newBoss.eulerAngles += Vector3.up * 180;  // Then rotate it back to face the spawn point
        newBoss.SendMessage("SetSpeed", RoundsSystem.In.Rounds[RoundsSystem.In.CurrentRound].EnemySpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, spawnDistance.x);
        Gizmos.DrawWireSphere(Vector3.zero, spawnDistance.y);
    }
}
