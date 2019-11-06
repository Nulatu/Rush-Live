using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 10;
    [SerializeField] protected Transform hitEffect = default; // "The effect that is created at the location of this object when it is destroyed"
    [SerializeField] private float damageMultiplier = 1;

    protected double shotDamage;
    protected Transform thisTransform;
   
    private void Start()
    {
        thisTransform = transform;
        shotDamage = UpdateData.In.Updates["DAMAGE"].GetData(0);
    }

    private void Update()
    {
        thisTransform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>()) // What did we hit this enemy? which has the script Enemy
        {
            other.SendMessage("ChangeHealth", -shotDamage * damageMultiplier, SendMessageOptions.DontRequireReceiver);
            Transform hitEffectObject = Instantiate(hitEffect, thisTransform.position, Quaternion.identity); 
            SoundManager.In.PlaySound(hitEffectObject.GetComponent<AudioSource>());
            Destroy(gameObject);
        }
    }
}
