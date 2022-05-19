using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invisibles
{
    [AddComponentMenu("Invisibles/Editor Collider Renderer")]
    public class EditorColliderRenderer : EditorRenderer
    {
        [HideInInspector] new public Collider collider;

        protected override void OnValidate()
        {
            Validate();
        }

        public override void Validate()
        {
            if (collider == null && TryGetComponent(out Collider newcollider))
            {
                collider = newcollider;
                drawer = null;
            }

            ColliderDrawer colliderDrawer = drawer as ColliderDrawer;
            if (drawer == null || colliderDrawer == null || !colliderDrawer.Compare(collider))
            {
                //Debug.Log("created");
                CreateColliderDrawer();
                AssignMesh();
            }

            if (mesh != drawer.Mesh)
            {
                mesh = drawer.Mesh;
            }
        }

        private void CreateColliderDrawer()
        {
            if (collider is BoxCollider)
            {
                drawer = new BoxColliderDrawer(transform, collider);
            }
            else if (collider is CapsuleCollider)
            {
                drawer = new CapsuleColliderDrawer(transform, collider);
            }
            else if (collider is SphereCollider)
            {
                drawer = new SphereColliderDrawer(transform, collider);
            }
            else if (collider is MeshCollider)
            {
                drawer = new MeshColliderDrawer(transform, collider);
            }
            else if (collider is WheelCollider)
            {
                drawer = new WheelColliderDrawer(transform, collider);
            }
        }

        private void AssignMesh()
        {
            if (mesh == null && drawer != null)
            {
                mesh = drawer.Mesh;
            }
        }
    }
}
