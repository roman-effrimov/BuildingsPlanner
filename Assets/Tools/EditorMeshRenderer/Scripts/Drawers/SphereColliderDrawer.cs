using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invisibles
{
    public class SphereColliderDrawer : ColliderDrawer
    {
        private SphereCollider sphereCollider;

        public override Mesh Mesh
        {
            get
            {
                if (mesh == null)
                {
                    mesh = Resources.GetBuiltinResource<Mesh>("New-Sphere.fbx");
                }
                return mesh;
            }
            set
            {
                mesh = value;
            }
        }

        protected override Vector3 Position => base.Position + transform.localToWorldMatrix.MultiplyVector(sphereCollider.center); //new Vector3(sphereCollider.center.x*base.Scale.x,sphereCollider.center.y*base.Scale.y, sphereCollider.center.z * base.Scale.z);
        protected override Vector3 Scale {
            get
            {
                float sideScale = Mathf.Max(Mathf.Abs( base.Scale.x), Mathf.Abs(base.Scale.z), Mathf.Abs(base.Scale.y));
                return Vector3.one * sideScale * sphereCollider.radius * 2;
                //return base.Scale;
            }
        } 

        public SphereColliderDrawer(Transform transform,Collider collider): base(transform)
        {
            sphereCollider = collider as SphereCollider;
            mesh = Mesh;
        }

        public override bool Compare(Collider collider)
        {
            SphereCollider sphere = collider as SphereCollider;
            return sphere != null && sphere== sphereCollider;
        }
    }
}