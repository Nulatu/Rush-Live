using UnityEngine;
// Rotate an object the other way relative to the parent(currentAngleCutomer).
// So that the child(transform) does not turn towards the parent, but always remains in his starting position
public class FixedAngle : MonoBehaviour
{
    [SerializeField] private Transform currentAngleCutomer = default;

    private void Update()
    {
		transform.rotation = Quaternion.Euler(new Vector3(transform.forward.x,-currentAngleCutomer.forward.y, transform.forward.z));  // not allowed = -currentAngleCutomer.forward // it is necessary that the angle along the X axis at 45 does not change
    }
}
