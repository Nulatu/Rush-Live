using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class GameAnims : MonoBehaviour
{
    public static void moveLabelAnimation(GameObject prefab, GameObject parent, GameObject gemBar, Vector3 startPos, Vector3 endPos, float time, Action callback = null)
    {
        GameObject go = GameObject.Instantiate(prefab, startPos, Quaternion.identity, parent.transform);
        go.transform.localScale = Vector3.zero;
        go.transform.localPosition = startPos;
        go.SetActive(true);
        Sequence seq = DOTween.Sequence();
        seq.Append(go.transform.DOScale(Vector3.one, 0.1f));
        seq.Append(go.transform.DOLocalMove(endPos, time));
        seq.Append(go.transform.DOScale(Vector3.zero, 0.1f));
        seq.Append(gemBar.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.25f, 15, 1));
        seq.AppendCallback(() => {
            GameObject.Destroy(go);
            if(callback != null)
            {
                callback();
            }
        });
    }

    public static void lerpTextLabel(Text textLabel, double oldValue, double newValue, float lerpValue, Action callback = null)
    {
        float lerp = 0f;
        float lerpEndVal = lerpValue;
        Sequence seq = DOTween.Sequence();

        while (lerp < lerpEndVal)
        {
            lerp += Time.deltaTime;
            double val = GameAnims.lerp(oldValue, newValue, lerp);
            seq.AppendInterval(Time.deltaTime);
            seq.AppendCallback(() => textLabel.text = "" + NumberSystem.Output(val));
        }
        seq.AppendInterval(Time.deltaTime);
        seq.AppendCallback(() =>
        {
            textLabel.text = "" + NumberSystem.Output(newValue);
            if (callback != null)
            {
                callback();
            }
        });
    }


    public static void lerpFillImageLabel(Image imageLabel, float oldValue, float newValue, float lerpValue, Action callback = null)
    {
        float lerp = 0f;
        float lerpEndVal = lerpValue;
        Sequence seq = DOTween.Sequence();

        while (lerp < lerpEndVal)
        {
            lerp += Time.deltaTime;
            float val = GameAnims.lerp(oldValue, newValue, lerp);
            seq.AppendInterval(Time.deltaTime);
            seq.AppendCallback(() => imageLabel.fillAmount = val);
        }
        seq.AppendInterval(Time.deltaTime);
        seq.AppendCallback(() =>
        {
            imageLabel.fillAmount = newValue;
            if (callback != null)
            {
                callback();
            }
        });
    }


    /////////////////////////////////////
    /////////private functions///////////
    /////////////////////////////////////

    static double lerp(double a, double b, float f)
    {
        return a + f * (b - a);
    }

    static float lerp(float a, float b, float f)
    {
        return a + f * (b - a);
    }
}
