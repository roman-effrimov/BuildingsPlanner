using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerExitEventInvoker : MonoBehaviour
{
    [SerializeField] private bool useLayermask = false;
    [SerializeField, ShowIf("useLayermask")] private LayerMask layermask;
    [SerializeField] private bool useTagFilter;
    [SerializeField, ShowIf("useTagFilter")] private List<string> tagFilter;

    [SerializeField] private UnityEvent eventToInvoke;

    private void OnTriggerExit(Collider other)
    {
        if (useLayermask && layermask != (layermask | (1 << other.gameObject.layer)))
            return;

        if (useTagFilter && !tagFilter.Contains(other.tag))
            return;

        eventToInvoke?.Invoke();
    }
}
