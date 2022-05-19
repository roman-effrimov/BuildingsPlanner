using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invisibles
{
    public class MeshColliderDrawer : ColliderDrawer
    {
        private MeshCollider meshCollider;

        public override Mesh Mesh
        {
            get
            {
                mesh = meshCollider.sharedMesh;
                return mesh;
            }
            set
            {
                mesh = value;
            }
        }

        public MeshColliderDrawer(Transform transform, Collider collider): base(transform)
        {
            meshCollider = collider as MeshCollider;
            mesh = Mesh;
        }

        public override bool Compare(Collider collider)
        {
            MeshCollider mesh = collider as MeshCollider;
            return mesh != null && mesh == meshCollider;
        }

        public override void Draw()
        {
            base.Draw();
            if (meshCollider.convex)
            {
                AssignConvexHullWarningMessage();
            }
            else
            {
                message.ClearWarning();
            }
        }

        private void AssignConvexHullWarningMessage()
        {
            message.SetWarningMessage("Drawing original mesh instead of convex hull!");
        }
    }
}