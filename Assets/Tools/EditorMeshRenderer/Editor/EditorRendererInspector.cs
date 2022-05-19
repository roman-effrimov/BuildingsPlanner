using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Invisibles
{
    [CustomEditor(typeof(EditorRenderer))]
    [CanEditMultipleObjects]
    public class EditorRendererInspector : Editor
    {
        private EditorRenderer editorRenderer;
        protected bool needValidation;
        protected SerializedProperty indexProperty;
        private SerializedProperty meshProperty;

        void OnEnable()
        {
            indexProperty = serializedObject.FindProperty("typeIndex");
            meshProperty = serializedObject.FindProperty("mesh");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            editorRenderer = (EditorRenderer)target;
            
            CreateMeshField(editorRenderer);
            CreateInvisiblesTypeField(editorRenderer);
            CreateDrawerMessage(editorRenderer);

            ApplyChanges();
        }

        protected MessageType ConvertDrawerMessageType(DrawerMessage.MessageType typeToConvert)
        {
            switch (typeToConvert)
            {
                case DrawerMessage.MessageType.error: return MessageType.Error;
                case DrawerMessage.MessageType.warning: return MessageType.Warning;
                default: return MessageType.None;
            }
        }

        protected void CreateDrawerMessage(EditorRenderer invisibles)
        {
            if (invisibles.DrawerMessage != null && invisibles.DrawerMessage.type != DrawerMessage.MessageType.none)
            {
                EditorGUILayout.HelpBox(invisibles.DrawerMessage.text, ConvertDrawerMessageType(invisibles.DrawerMessage.type));
            }
        }
        
        protected void CreateInvisiblesTypeField(EditorRenderer invisibles)
        {
            int newIndex = EditorPropertyUtility.PopupProperty(indexProperty, "Type", invisibles.typeIndex, EditorRendererSettings.Settings.invisibleTypes.ToArray());
            if (newIndex != invisibles.typeIndex)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    var inv = (EditorRenderer)targets[i];
                    ChangePrefabValue((EditorRenderer renderer) => { renderer.typeIndex = newIndex; }, inv);
                }
            }
        }

        protected void CreateMeshField(EditorRenderer invisibles)
        {
            Mesh mesh = EditorPropertyUtility.ObjectPropertyField(meshProperty, "Mesh", invisibles.mesh, typeof(Mesh)) as Mesh;
            if (mesh != invisibles.mesh)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    var inv = (EditorRenderer)targets[i];
                    ChangePrefabValue((EditorRenderer renderer) => { renderer.mesh = mesh; }, inv);
                }

                needValidation = true;
            }
        }

        protected void ApplyChanges()
        {
            if (needValidation)
            {
                ValidateAll();
                if (!EditorApplication.isPlaying)
                {
                    EditorUtility.SetDirty(editorRenderer);
                    PrefabUtility.RecordPrefabInstancePropertyModifications(this);
                    UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(editorRenderer.gameObject.scene);
                }
                needValidation = false;
            }
        }

        protected void ValidateAll()
        {
            for (int i = 0; i < targets.Length; i++)
            {
                var inv = (EditorRenderer)targets[i];
                inv.Validate();
            }
        }

        protected void ChangePrefabValue<T>(System.Action<T> changeAction, T target) where T : Component
        {
            if (!EditorApplication.isPlaying)
            {
                Undo.RecordObject(target, "INVinspectorChanges");
            }
            changeAction?.Invoke(target);
            if (!EditorApplication.isPlaying)
            {
                EditorUtility.SetDirty(target);
                PrefabUtility.RecordPrefabInstancePropertyModifications(this);
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(target.gameObject.scene);
            }
        }
    }
}