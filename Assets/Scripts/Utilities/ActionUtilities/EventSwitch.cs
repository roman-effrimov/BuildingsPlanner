using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSwitch : MonoBehaviour
{
    [SerializeField] private UnityEvent activationEvent;
    [SerializeField] private UnityEvent disactivationEvent;
    [SerializeField] private bool ignoreRepeats;
    [SerializeField] private SwitchStates switchState = SwitchStates.Initial;

    public void Activate()
    {
        if (ignoreRepeats && switchState == SwitchStates.Active)
            return;

        switchState = SwitchStates.Active;
        activationEvent?.Invoke();
    }

    public void Disactivate()
    {
        if (ignoreRepeats && switchState == SwitchStates.Disabled)
            return;

        switchState = SwitchStates.Disabled;
        disactivationEvent?.Invoke();
    }

    public void Switch()
    {
        if (switchState == SwitchStates.Active)
            Disactivate();
        else if (switchState == SwitchStates.Disabled)
            Activate();
    }
}
