using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "EditorRendererSettings", menuName = "ScriptableObjects/EditorRendererSettings")]
public class EditorRendererSettings : ScriptableObject
{
    private const string SETTINGS_PATH = "Tools/EditorMeshRenderer/Resources";
    private const string SETTINGS_FILENAME = "EditorRendererSettings.asset";

    public Color defaultColor = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.5f);
    public List<string> invisibleTypes = new List<string>();
    public List<bool> permissions = new List<bool>();
    public List<Color> colors = new List<Color>();
    [SerializeField] private static EditorRendererSettings _settings;

    public static EditorRendererSettings Settings
    {
        get
        {
            if (_settings == null)
            {
#if UNITY_EDITOR
                InitSettings();
                _settings.OnValidate();
#endif
            }
            return _settings;
        }
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        if (invisibleTypes.Count <= 0)
        {
            invisibleTypes.Add("Default");
            permissions.Add(true);
            colors.Add(defaultColor);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
    }

    private static void InitSettings()
    {
        _settings = AssetDatabase.LoadAssetAtPath<EditorRendererSettings>("Assets/" + SETTINGS_PATH + "/" + SETTINGS_FILENAME);

        if (_settings == null)
        {
            //If the settings asset doesn't exist, then create it. We require a resources folder
            if (!Directory.Exists(Application.dataPath + "/" + SETTINGS_PATH))
            {
                Directory.CreateDirectory(Application.dataPath + "/" + SETTINGS_PATH);
            }

            const string path = "Assets/" + SETTINGS_PATH + "/" + SETTINGS_FILENAME;

            if (File.Exists(path))
            {
                AssetDatabase.LoadAssetAtPath<EditorRendererSettings>(path);
            }
            else
            {
                var asset = ScriptableObject.CreateInstance<EditorRendererSettings>();
                AssetDatabase.CreateAsset(asset, path);
                AssetDatabase.Refresh();

                AssetDatabase.SaveAssets();
                Debug.Log("Editor renderer settings file sasn't been found and has been created");
                //Selection.activeObject = asset;

                //save reference
                _settings = asset;
            }
        }
    }

#endif
}