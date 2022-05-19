using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invisibles
{
    public class ColliderDrawer: Drawer
    {
        public ColliderDrawer(Transform transform): base(transform)
        {
        }

        public virtual bool Compare(Collider collider)
        {
            return true;
        }
    }
}