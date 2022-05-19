using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invisibles
{
    [ExecuteInEditMode]
    [AddComponentMenu("Invisibles/Editor Renderer")]
    public class EditorRenderer : MonoBehaviour
    {
        [HideInInspector] public int typeIndex;
        [HideInInspector] public Mesh mesh;
        protected Drawer drawer;

        protected static EditorRendererSettings settings;

        protected virtual Vector3 Position => transform.position;
        protected virtual Quaternion Rotation => transform.rotation;
        protected virtual Vector3 Scale => transform.lossyScale;
        public DrawerMessage DrawerMessage
        {
            get
            {
                if (drawer != null)
                {
                    return drawer.Message;
                }
                return null;
            }
        }

        protected virtual void OnValidate()
        {
            Validate();
        }

        [ExecuteInEditMode]
        private void OnEnable()
        {
            Validate();
        }

        public virtual void Validate()
        {
            if (mesh == null && TryGetComponent(out MeshFilter meshFilter))
            {
                mesh = meshFilter.sharedMesh;
            }

            if (drawer == null) drawer = new Drawer(transform);

            if (drawer.Mesh != mesh)
            {
                drawer.Mesh = mesh;
            }
        }

        protected virtual void DrawMesh()
        {
            if (drawer != null)
            {
                drawer.Draw();
            }
        }

        protected bool PermisionsCheck()
        {
            if (settings == null) settings = EditorRendererSettings.Settings;
            if (settings.permissions.Count <= typeIndex) typeIndex = 0;
            return settings.permissions[typeIndex];
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            if (PermisionsCheck())
            {
                Gizmos.color = settings.colors[typeIndex];
                DrawMesh();
            }
        }
#endif
    }
}
