using System;
using System.Collections.Generic;
using UnityEngine;

namespace BreakTheCycle.CyberTaxi
{
    [Serializable]
    public class SectionColliderData
    {
        public bool enabled;
        public Transform section;
        public int count;
        public ColliderOrientation orientation;
        public float size;
        public float offsetToCenter;
        public float offsetToSides;
        public RecyclableGameObjectPool _collidersPool;
    }
}
