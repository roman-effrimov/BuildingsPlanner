using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invisibles
{
    public class Drawer
    {
        protected DrawerMessage message = new DrawerMessage();
        protected Transform transform;
        protected Mesh mesh;

        public virtual DrawerMessage Message => message;
        public virtual Mesh Mesh {
            get { return mesh; }
            set { mesh = value; }
        }

        protected virtual Vector3 Position => transform.position;
        protected virtual Quaternion Rotation => transform.rotation;
        protected virtual Vector3 Scale => transform.lossyScale;

        public Drawer(Transform transform)
        {
            this.transform = transform;
        }

        public virtual void Draw()
        {
            if (Mesh != null)
            {
                Gizmos.DrawMesh(Mesh, Position, Rotation, Scale);
                message.ClearError();
            }
            else
            {
                AssignMeshErrorMessage();
            }
        }

        protected void AssignMeshErrorMessage()
        {
            message.SetErrorMessage("Can not find mesh. Assign it manually");
        }
    }
}