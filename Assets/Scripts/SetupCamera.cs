using UnityEngine;
// You need to set the camera rotation on the player in the 3D playing field.
// The fact is that initially in the Unity editor you need a camera in zero rotation angles.In order to make it convenient to modify the canvas UI objects with the camera type.
public class SetupCamera : MonoBehaviour
{
    private void Awake()
    {
        transform.position = new Vector3(-5, 30, -35.2f); // Camera for the first level tutorial
        transform.rotation = Quaternion.Euler(45,0,0);
    }
}
