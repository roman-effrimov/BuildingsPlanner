using Sirenix.OdinInspector;
using UnityEngine;

public class BehaviourSingleton<T> : MonoBehaviour where T : Component
{
    [FoldoutGroup("Singleton Settings")]
    [SerializeField] private bool dontDestroyOnLoad = false;

    [Header("Dublicate Deleting Mode")]
    [EnumToggleButtons, HideLabel, FoldoutGroup("Singleton Settings")]
    [SerializeField] private DublicateDeletingMode dublicateDeletingMode = DublicateDeletingMode.DestroyComponent;

    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance != null)
                {
                    Debug.Log("Instance of " + typeof(T) + " has been called before its initialazing in Awake() and has been found with " +
                    "FindObjectOfType() method. It may cause performance drop. Try to change the script order to avoid it");
                }
                else
                {
                    Debug.LogError("Instance of " + typeof(T) + " BehaviourSingleton hasn't been found. Make sure that there's one object of " +
                    typeof(T) + " on the scene before trying to access it");
                }
            }
            return _instance;
        }
    }

    public static bool IsInitialized { get { return _instance != null; } }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;

            if (dontDestroyOnLoad)
            {
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            if (_instance != this)
            {
                if (dublicateDeletingMode == DublicateDeletingMode.DestroyComponent)
                {
                    Debug.Log($"Destroying a component \"{typeof(T)}\" on \"{gameObject.name}\" as second instance of {typeof(T)} BehaviourSingleton!");
                    Destroy(this);
                }
                else if (dublicateDeletingMode == DublicateDeletingMode.DestroyGameObject)
                {
                    Debug.Log($"Destroying a \"{gameObject.name}\" gameObject as second instance of {typeof(T)} BehaviourSingleton!");
                    Destroy(gameObject);
                }
            }
        }
    }

    private enum DublicateDeletingMode
    {
        DestroyComponent,
        DestroyGameObject
    }
}