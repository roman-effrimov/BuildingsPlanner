using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Invisibles
{
    [CustomEditor(typeof(EditorColliderRenderer))]
    [CanEditMultipleObjects]
    public class EditorColliderRendererInspector : EditorRendererInspector
    {
        private EditorColliderRenderer editorColliderRenderer;
        protected SerializedProperty colliderProperty;

        void OnEnable()
        {
            indexProperty = serializedObject.FindProperty("typeIndex");
            colliderProperty = serializedObject.FindProperty("collider");
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            editorColliderRenderer = (EditorColliderRenderer)target;

            CreateColliderField(editorColliderRenderer);
            CreateInvisiblesTypeField(editorColliderRenderer);
            CreateDrawerMessage(editorColliderRenderer);

            ApplyChanges();
        }

        protected void CreateColliderField(EditorColliderRenderer invisibles)
        {
            Collider collider = EditorPropertyUtility.ObjectPropertyField(colliderProperty,"Collider", invisibles.collider, typeof(Collider)) as Collider;
            if (collider != invisibles.collider)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    var inv = (EditorColliderRenderer)targets[i];
                    ChangePrefabValue((EditorColliderRenderer renderer) => { renderer.collider = collider; }, inv);
                }

                needValidation = true;
            }
        }
    }
}