using UnityEngine;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    public static Tutorial In;

    [SerializeField] private GameObject buttonEnergy = default;
    [SerializeField] private GameObject joystick = default;
    [SerializeField] private GameObject fonJoystick = default;
    [SerializeField] private GameObject bottomUpdates = default;
    [SerializeField] private GameObject pointingHand = default;
    [SerializeField] private GameObject upRightPointingHand = default;
    [SerializeField] private ParticleSystem particleEnergyJoystick = default;
    [SerializeField] private bool finishedTutorial = default;

   // джойстик работает только в канвасе оверлай. Частицы поверх фона джойстика возможны только если фон джойстика перенести в канвас камера т.е. разделить уи джайстика на две части и каждую часть в свой канвас.
   public void ActiveFirstStepTutorual() // -> RoundSystem - ChangeKillCount()
    {
        if (finishedTutorial) return;

        GameManager.In.PlayerObject.GetEnergyPoint().transform.DOScale(Vector3.one, 0.05f); // !!!баг в логике!!!  я думаю это лучше активировать через EnergyPointSystem
        Time.timeScale = 0.025f; // в нуле не работает анимация пальца туториала // скорость аниматора = 30
        buttonEnergy.SetActive(true);
        pointingHand.SetActive(true);
    }

    public void FinishFirstStepTutorual() // -> EnergyButton - OnPointerClick()
    {
        if (finishedTutorial) return;

        Time.timeScale = 1;
        pointingHand.SetActive(false);
    }

    public void ActiveSecondStepTutorual() // -> RoundSystem - ChangeKillCount()
    {
        if (finishedTutorial) return;

        Time.timeScale = 0.025f; // в нуле не работает анимация пальца туториала // скорость аниматора = 30
        joystick.SetActive(true);
        fonJoystick.SetActive(true);
        particleEnergyJoystick.Play();
        upRightPointingHand.SetActive(true);
    }

    public void FinishSecondStepTutorual() // -> Joystick - OnDrag()
    {
        if (finishedTutorial) return;

        Time.timeScale = 1;
        EnergyPointSystem.In.AnimationAppearanceEnergyPoints();
        upRightPointingHand.SetActive(false);
        float aspectRatio = (float)Screen.height / Screen.width;
        if (aspectRatio >= 2) // Проблемы с перспективой - на больших экранах 2.1,2.0 - точки энергии слишком близко к краям экрана - нижние ближе чем верхние из-за угловой камеры 
            Camera.main.fieldOfView = 33;
        Camera.main.transform.DOMove(new Vector3(0, 40, -39.5f), 1).OnComplete(() => { bottomUpdates.SetActive(true); finishedTutorial = true; });
    }

    private void Start()
    {
        In = this;

        if (RoundsSystem.In.CurrentRound > 0) // не включаем туториал когда игрок прошёл первый раунд. (Потом когда туториал увеличиться, надо поменять это условие)
            finishedTutorial = true;

        if (finishedTutorial)
        {
            buttonEnergy.SetActive(true);
            joystick.SetActive(true);
            fonJoystick.SetActive(true);
            EnergyPointSystem.In.AnimationAppearanceEnergyPoints();

            float aspectRatio = (float)Screen.height / Screen.width;
            if (aspectRatio >= 2) // Проблемы с перспективой - на больших экранах 2.1,2.0 - точки энергии слишком близко к краям экрана - нижние ближе чем верхние из-за угловой камеры 
                Camera.main.fieldOfView = 33;
            Camera.main.transform.DOMove(new Vector3(0, 40, -39.5f), 1).OnComplete(() => bottomUpdates.SetActive(true));
        }

    }
}
