using UnityEngine;

public class RedShotHitArea : MonoBehaviour 
{
	[SerializeField] private float explosionPower = default;

    private void Update() 
	{
        RaycastHit[] enemies =  Physics.SphereCastAll(transform.position, explosionPower, Vector2.up,0,LayerMask.GetMask("Enemy"));

		foreach (RaycastHit enemy in enemies)
		{
            enemy.transform.SendMessage("ChangeHealth", - UpdateData.In.Updates["DAMAGE"].GetData(0) * 2, SendMessageOptions.DontRequireReceiver);
        }
	}
}

