using UnityEditor;

public class TemplatesApplication
{
    private const string pathToBehaviourScriptTemplate = "Assets/Scripts/ScriptTemplates/BehaviourScriptTemplate.cs.txt";
    private const string pathToScriptableObjectScriptTemplate = "Assets/Scripts/ScriptTemplates/ScriptableObject.cs.txt";
    private const string pathToSingletonTemplate = "Assets/Scripts/ScriptTemplates/BehaviourSingletonTemplate.cs.txt";
    private const string pathToClassTemplate = "Assets/Scripts/ScriptTemplates/NonBehaviourClassTemplate.cs.txt";
    private const string pathToEnumTemplate = "Assets/Scripts/ScriptTemplates/EnumTemplate.cs.txt";
    private const string pathToStructTemplate = "Assets/Scripts/ScriptTemplates/StructTemplate.cs.txt";

    [MenuItem(itemName: "Assets/Create/C# Behaviour Script", isValidateFunction: false, priority: 50)]
    public static void CreateBehaviourScriptFromTemplate()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(pathToBehaviourScriptTemplate, "NewBehaviourScript.cs");
    }

    [MenuItem(itemName: "Assets/Create/C# Behaviour Singleton", isValidateFunction: false, priority: 50)]
    public static void CreateSingletonFromTemplate()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(pathToSingletonTemplate, "NewBehaviourSingleton.cs");
    }

    [MenuItem(itemName: "Assets/Create/C# Scriptable Object Script", isValidateFunction: false, priority: 50)]
    public static void CreateScriptableObjectFromTemplate()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(pathToScriptableObjectScriptTemplate, "NewScriptableObject.cs");
    }

    [MenuItem(itemName: "Assets/Create/Utilities/C# Class Script", isValidateFunction: false, priority: 51)]
    public static void CreateClassFromTemplate()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(pathToClassTemplate, "NewClass.cs");
    }

    [MenuItem(itemName: "Assets/Create/Utilities/C# Enum", isValidateFunction: false, priority: 51)]
    public static void CreateEnumFromTemplate()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(pathToEnumTemplate, "NewEnum.cs");
    }

    [MenuItem(itemName: "Assets/Create/Utilities/C# Struct", isValidateFunction: false, priority: 51)]
    public static void CreateStructFromTemplate()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(pathToStructTemplate, "NewStruct.cs");
    }
}
