using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invisibles
{
    public class BoxColliderDrawer : ColliderDrawer
    {
        private BoxCollider boxCollider;

        public override Mesh Mesh {
            get
            {
                if (mesh == null)
                {
                    mesh = Resources.GetBuiltinResource<Mesh>("Cube.fbx");
                }
                return mesh;
            }
            set
            {
                mesh = value;
            }
        }

        protected override Vector3 Position => base.Position + transform.localToWorldMatrix.MultiplyVector(boxCollider.center); //new Vector3(boxCollider.center.x * base.Scale.x, boxCollider.center.y * base.Scale.y, boxCollider.center.z * base.Scale.z));
        protected override Vector3 Scale => new Vector3(base.Scale.x * boxCollider.size.x, base.Scale.y * boxCollider.size.y, base.Scale.z * boxCollider.size.z);

        public BoxColliderDrawer(Transform transform, Collider collider): base(transform)
        {
            boxCollider = collider as BoxCollider;
            mesh = Mesh;
        }

        public override bool Compare(Collider collider)
        {
            BoxCollider box = collider as BoxCollider;
            return box != null && box == boxCollider;
        }
    }
}