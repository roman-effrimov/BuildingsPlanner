using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEventSequence : MonoBehaviour
{
    [SerializeField] private EventStartModes startMode = EventStartModes.Manual;
    [SerializeField] private bool loop = false;
    [Space]
    [SerializeField] private UnityEvent OnEnableEvent;
    [Space]
    [SerializeField] private UnityEvent[] eventSequence;
    [SerializeField] private float[] eventSequenceDelayes;
    [Space]
    [SerializeField] private UnityEvent OnDisableEvent;

    private int currentEventIndex;

    private void Awake()
    {
        if (startMode == EventStartModes.OnAwake)
            StartEventSequence();
    }

    private void Start()
    {
        if (startMode == EventStartModes.OnStart)
            StartEventSequence();
    }

    private void OnEnable()
    {
        OnEnableEvent?.Invoke();

        if (startMode == EventStartModes.OnEnable)
            StartEventSequence();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        OnDisableEvent?.Invoke();
    }

    public void StartEventSequence()
    {
        StopAllCoroutines();
        StartCoroutine(EventSequenceRoutine());
    }

    private IEnumerator EventSequenceRoutine()
    {
        currentEventIndex = 0;

        while (currentEventIndex < eventSequence.Length && currentEventIndex < eventSequenceDelayes.Length)
        {
            yield return new WaitForSeconds(eventSequenceDelayes[currentEventIndex]);
            eventSequence[currentEventIndex].Invoke();
            currentEventIndex++;
        }

        if (loop)
        {
            StartEventSequence();
        }
    }
}
