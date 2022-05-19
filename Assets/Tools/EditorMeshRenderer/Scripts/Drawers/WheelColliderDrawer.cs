using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invisibles
{
    public class WheelColliderDrawer : ColliderDrawer
    {
        private WheelCollider wheelCollider;

        public override Mesh Mesh
        {
            get
            {
                if (mesh == null)
                {
#if UNITY_EDITOR
                    mesh = UnityEditor.AssetDatabase.LoadAssetAtPath<Mesh>("Assets/InvisiblesDebugger/Resources/Models/Wheel.obj");
#else
                    mesh = Resources.GetBuiltinResource<Mesh>("New-Sphere.fbx");
#endif
                }
                    return mesh;
            }
            set
            {
                mesh = value;
            }
        }

        protected override Vector3 Position => base.Position + transform.localToWorldMatrix.MultiplyVector(wheelCollider.center + Vector3.down*wheelCollider.suspensionDistance/2); //new Vector3(wheelCollider.center.x * base.Scale.x, wheelCollider.center.y * base.Scale.y, wheelCollider.center.z * base.Scale.z);
        protected override Vector3 Scale
        {
            get
            {
                float sideScale = Mathf.Max(Mathf.Abs(base.Scale.x), Mathf.Abs(base.Scale.z), Mathf.Abs(base.Scale.y));
                return Vector3.one * sideScale * wheelCollider.radius * 2;
            }
        }

        public WheelColliderDrawer(Transform transform, Collider collider): base(transform)
        {
            wheelCollider = collider as WheelCollider;
            mesh = Mesh;
        }

        public override bool Compare(Collider collider)
        {
            WheelCollider wheel = collider as WheelCollider;
            return wheel != null && wheel == wheelCollider;
        }
    }
}