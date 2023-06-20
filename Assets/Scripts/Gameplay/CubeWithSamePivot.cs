using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    [Serializable]
    public class CubeWithSamePivot
    {
        public List<CubeController> Objects;
        public Vector3 Pivot;
        public Vector3 Rotation;
        public CubeWithSamePivot()
        {
            Objects = new List<CubeController>();
        }
    }
}