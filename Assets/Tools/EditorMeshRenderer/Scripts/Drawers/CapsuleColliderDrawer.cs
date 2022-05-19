using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invisibles
{
    public class CapsuleColliderDrawer : ColliderDrawer
    {
        private CapsuleCollider capsuleCollider;
        private Mesh[] meshes;

        public override Mesh Mesh
        {
            get
            {
                if (mesh == null)
                {
#if UNITY_EDITOR
                    mesh = UnityEditor.AssetDatabase.LoadAssetAtPath<Mesh>("Assets/InvisiblesDebugger/Resources/Models/SideCylinder.obj");
#else
                    mesh = Resources.GetBuiltinResource<Mesh>("New-Capsule.fbx");
#endif
                }
                return mesh;
            }
            set
            {
                mesh = value;
            }
        }

        protected override Vector3 Position => base.Position + transform.localToWorldMatrix.MultiplyVector(capsuleCollider.center); //new Vector3(capsuleCollider.center.x * base.Scale.x, capsuleCollider.center.y * base.Scale.y, capsuleCollider.center.z * base.Scale.z);
        protected override Vector3 Scale
        {
            get
            {
                float sideScale = Mathf.Max(Mathf.Abs(base.Scale.x),Mathf.Abs( base.Scale.z));
                float upScale = Mathf.Abs(base.Scale.y) * capsuleCollider.height - sideScale * 2 * capsuleCollider.radius;
                return new Vector3(sideScale * 2 * capsuleCollider.radius, upScale > 0? upScale:0, sideScale * 2 * capsuleCollider.radius);
            }
        }

        public CapsuleColliderDrawer(Transform transform, Collider collider): base(transform)
        {
            capsuleCollider = collider as CapsuleCollider;
            mesh = Mesh;
            meshes = GetMeshes();
        }

        public override bool Compare(Collider collider)
        {
            CapsuleCollider capsule = collider as CapsuleCollider;
            return capsule != null && capsule == capsuleCollider;
        }

        public override void Draw()
        {
            if (meshes != null)
            {
                Gizmos.DrawMesh(meshes[0], Position, Rotation, Scale);
#if UNITY_EDITOR
                Gizmos.DrawMesh(meshes[1], Position + transform.up *  Mathf.Abs(Scale.y) / 2, Rotation, Vector3.one * Scale.x);
                Gizmos.DrawMesh(meshes[2], Position - transform.up * Mathf.Abs(Scale.y) / 2, Rotation, Vector3.one * Scale.x);
#endif
                message.ClearError();
            }
            else
            {
                AssignMeshErrorMessage();
            }
        }

        public Mesh[] GetMeshes()
        {
#if UNITY_EDITOR
            Mesh[] meshes = new Mesh[3];
            meshes[1] = UnityEditor.AssetDatabase.LoadAssetAtPath<Mesh>("Assets/InvisiblesDebugger/Resources/Models/HalfSphereUp.obj");
            meshes[2] = UnityEditor.AssetDatabase.LoadAssetAtPath<Mesh>("Assets/InvisiblesDebugger/Resources/Models/HalfSphereDown.obj");
#else
            Mesh[] meshes = new Mesh[1];
#endif
            meshes[0] = Mesh;
            return meshes;
        }
    }
}