using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Invisibles
{
    public class EditorPropertyUtility : Editor
    {
        public static Object ObjectPropertyField(SerializedProperty property, string label, Object obj, System.Type objType)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            EditorGUI.BeginProperty(rect, null, property);
            Object resultObject = EditorGUI.ObjectField(rect, label, obj, objType);
            EditorGUI.EndProperty();
            return resultObject;
        }

        public static int PopupProperty(SerializedProperty property, string label, int selectedIndex, string[] displayedOptions)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            EditorGUI.BeginProperty(rect, null, property);
            int resultIndex = EditorGUI.Popup(rect, label, selectedIndex, displayedOptions);
            EditorGUI.EndProperty();
            return resultIndex;
        }
    }
}
