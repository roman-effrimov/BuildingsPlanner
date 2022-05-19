using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

public class ActionInvokerWindow : OdinEditorWindow
{
    [MenuItem("Window/Action Invoker")]
    private static void OpenWindow()
    {
        GetWindow<ActionInvokerWindow>().Show();
    }

    [Button("Win Action")]
    private void WinAction()
    {
        //GameManager.WinAction?.Invoke();
    }

    [Button("Lose Action")]
    private void LoseAction()
    {
        //GameManager.LoseAction?.Invoke();
    }
}
