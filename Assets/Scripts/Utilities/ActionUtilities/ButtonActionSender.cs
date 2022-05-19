using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

public class ButtonActionSender : MonoBehaviour
{
    public static Action TapToStartButtonClicked;
    public static Action RestartButtonClicked;
    public static Action NextLevelButtonClicked;
    public static Action CloseButtonClicked;
    public static Action ShopButtonClicked;
    public static Action ResumeButtonCLicked;
    public static Action PauseButtonClicked;
    public static Action ResumeButtonClicked;
    public static Action SoundButtonClicked;

    [SerializeField, EnumPagingAttribute] private ButtonClickActionTypes actionType = ButtonClickActionTypes.Close;
    private Coroutine delayedActionInvokeCoroutine;

    public void InvokeAction()
    {
        switch (actionType)
        {
            case ButtonClickActionTypes.Restart:
                RestartButtonClicked?.Invoke();
                break;

            case ButtonClickActionTypes.NextLevel:
                NextLevelButtonClicked?.Invoke();
                break;

            case ButtonClickActionTypes.Close:
                CloseButtonClicked?.Invoke();
                break;

            case ButtonClickActionTypes.TapToStart:
                TapToStartButtonClicked?.Invoke();
                break;

            case ButtonClickActionTypes.Skip:
                ResumeButtonCLicked?.Invoke();
                break;

            case ButtonClickActionTypes.Pause:
                PauseButtonClicked?.Invoke();
                break;

            case ButtonClickActionTypes.Resume:
                ResumeButtonClicked?.Invoke();
                break;

            case ButtonClickActionTypes.Sound:
                SoundButtonClicked?.Invoke();
                break;

            default:
                Debug.LogError($"System Action hasn't been declared to this type of action: {actionType}");
                break;
        }
    }

    private IEnumerator DelayedInvokeRoutine(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        delayedActionInvokeCoroutine = null;
        InvokeAction();
    }

    public void InvokeActionDelayed(float delay)
    {
        if (delayedActionInvokeCoroutine == null)
            delayedActionInvokeCoroutine = StartCoroutine(DelayedInvokeRoutine(delay));
    }
}

public enum ButtonClickActionTypes
{
    TapToStart,
    Restart,
    NextLevel,
    Close,
    Shop,
    Skip,
    Pause,
    Resume,
    Sound
}
