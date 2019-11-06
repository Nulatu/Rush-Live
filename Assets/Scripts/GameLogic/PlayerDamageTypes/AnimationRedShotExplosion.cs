using UnityEngine;
using DG.Tweening;

public class AnimationRedShotExplosion : MonoBehaviour
{
    private void Awake()
    {
        transform.DOScale(1f, 0.3f);
    }
}
