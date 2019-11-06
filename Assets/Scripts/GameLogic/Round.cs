public class Round
{
	public string RoundName = "ROUND 1"; // The name of the current round
    public int EnemyCount = 10; // The number of kills needed to win this round"
    public float EnemySpeed = 1; // The speed of the enemies in this round
    public float SpawnDelay = 1; // How quickly enemies are spawned in this round
    public bool HaveBoss;
	public float BossDelay = 2; // How many seconds to wait before spawning the boss. The delay count stars after the last enemy is spawned
}
