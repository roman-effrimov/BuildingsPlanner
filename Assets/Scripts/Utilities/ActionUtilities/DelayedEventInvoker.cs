using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SniperMechanics
{
    public class DelayedEventInvoker: MonoBehaviour
    {
        [ToggleGroup("usingSubscription", ToggleGroupTitle = "Subscription", CollapseOthersOnExpand = true)]
        [SerializeField] private bool usingSubscription = true;
        [ToggleGroup("usingSubscription")]
        [SerializeField, EnumToggleButtons, HideLabel] private EventSubscriptionType eventSubscriptionType = EventSubscriptionType.Custom;
        [ToggleGroup("usingSubscription")]
        [SerializeField, EnumPagingAttribute, ShowIf("usingButtonActionSubsctiption")] private ButtonClickActionTypes buttonEventSubscription = ButtonClickActionTypes.Close;
        [ToggleGroup("usingSubscription")]
        [SerializeField, EnumPagingAttribute, ShowIf("usingCustomActionSubscription")] private CustomActionTypes customEventSubscription = CustomActionTypes.WinAction;

        [ToggleGroup("usingDelay", ToggleGroupTitle = "Delay", CollapseOthersOnExpand = true)]
        [SerializeField] private bool usingDelay = true;
        [ToggleGroup("usingDelay"), ShowIf("usingDelay")]
        [SerializeField, EnumToggleButtons, HideLabel] private DelayType delayType = DelayType.GameTime;
        [ToggleGroup("usingDelay"), ShowIf("usingDelay")]
        [SerializeField, Min(0)] private float delay = 1.5f;

        [SerializeField] private UnityEvent delayedUnityEvent;

        private Coroutine delayedInvokeCoroutine;

        private bool usingButtonActionSubsctiption = false;
        private bool usingCustomActionSubscription = false;

        private void OnValidate()
        {
            if (usingSubscription)
            {
                if (eventSubscriptionType == EventSubscriptionType.Button)
                {
                    usingButtonActionSubsctiption = true;
                    usingCustomActionSubscription = false;
                }
                else if (eventSubscriptionType == EventSubscriptionType.Custom)
                {
                    usingButtonActionSubsctiption = false;
                    usingCustomActionSubscription = true;
                }
            }
            else
            {
                usingButtonActionSubsctiption = false;
                usingCustomActionSubscription = false;
            }
        }

        private void Start()
        {
            if (eventSubscriptionType == EventSubscriptionType.Start)
                InvokeEvent();
        }

        private void OnEnable()
        {
            if (eventSubscriptionType == EventSubscriptionType.Button)
            {
                ButtonEventSubscription();
            }
            else if (eventSubscriptionType == EventSubscriptionType.Custom)
            {
                CustomEventsSubscription();
            }
        }

        private void OnDisable()
        {
            ButtonEventsUnsubscription();
            CustomEventsUnsubscription();

            if (delayedInvokeCoroutine != null)
                StopCoroutine(delayedInvokeCoroutine);
        }



        public void InvokeEvent()
        {
            if (usingDelay)
            {
                if (delayedInvokeCoroutine != null)
                    StopCoroutine(delayedInvokeCoroutine);

                delayedInvokeCoroutine = StartCoroutine(EventInvokeRoutine());
            }
            else
                delayedUnityEvent.Invoke();
        }

        public void StopEventRoutine()
        {
            if (delayedInvokeCoroutine != null)
                StopCoroutine(delayedInvokeCoroutine);
        }

        private IEnumerator EventInvokeRoutine()
        {
            if (delayType == DelayType.Realtime)
                yield return new WaitForSecondsRealtime(delay);
            else if (delayType == DelayType.GameTime)
                yield return new WaitForSeconds(delay);

            delayedInvokeCoroutine = null;
            delayedUnityEvent.Invoke();
        }

        private void ButtonEventSubscription()
        {
            switch (buttonEventSubscription)
            {
                case ButtonClickActionTypes.Close:
                    ButtonActionSender.CloseButtonClicked += InvokeEvent;
                    break;

                case ButtonClickActionTypes.NextLevel:
                    ButtonActionSender.NextLevelButtonClicked += InvokeEvent;
                    break;

                case ButtonClickActionTypes.Restart:
                    ButtonActionSender.RestartButtonClicked += InvokeEvent;
                    break;

                case ButtonClickActionTypes.TapToStart:
                    ButtonActionSender.TapToStartButtonClicked += InvokeEvent;
                    break;

                case ButtonClickActionTypes.Shop:
                    ButtonActionSender.ShopButtonClicked += InvokeEvent;
                    break;

                case ButtonClickActionTypes.Skip:
                    ButtonActionSender.ResumeButtonCLicked += InvokeEvent;
                    break;

                default:
                    Debug.LogError($"Event hasn't been defined for thg next button subscription type: {buttonEventSubscription}");
                    break;
            }
        }

        private void ButtonEventsUnsubscription()
        {
            ButtonActionSender.CloseButtonClicked -= InvokeEvent;
            ButtonActionSender.NextLevelButtonClicked -= InvokeEvent;
            ButtonActionSender.RestartButtonClicked -= InvokeEvent;
            ButtonActionSender.TapToStartButtonClicked -= InvokeEvent;
            ButtonActionSender.ShopButtonClicked -= InvokeEvent;
            ButtonActionSender.ResumeButtonCLicked -= InvokeEvent;
        }


        private void CustomEventsSubscription()
        {
            switch (customEventSubscription)
            {

                default:
                    Debug.LogError($"Event hasn't been defined for thg next custom subscription type: {customEventSubscription} on gameobject: {gameObject.name}");
                    break;
            }
        }

        private void CustomEventsUnsubscription()
        {

        }
    }

    public enum DelayType
    {
        Realtime,
        GameTime
    }

    public enum EventSubscriptionType
    {
        Button,
        Custom,
        Start
    }

    public enum CustomActionTypes
    {
        WinAction,
        LoseAction,
    }
}