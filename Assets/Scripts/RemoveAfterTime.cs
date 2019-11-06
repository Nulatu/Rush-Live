using UnityEngine;
using System.Collections;

public class RemoveAfterTime : MonoBehaviour 
{
	[SerializeField] private float removeAfterTime = 10; // How many seconds to wait before removing this object

    private void Start() 
	{
		StartCoroutine(RemoveAfterTime_(removeAfterTime));
	}

    private IEnumerator RemoveAfterTime_( float delay )
	{
		yield return new WaitForSeconds(delay);
		Destroy( gameObject);
	}
}

