using UnityEngine;

public class SpriteLookAtCamera : MonoBehaviour 
{
    private void Start() // the script is attached to the shot sprite and the sprite of the crown(prefab Enemy)
    { // You cannot change the angle in Awake. SetupCamera does not have time to turn
        transform.LookAt(Camera.main.transform.position);
	}
}
