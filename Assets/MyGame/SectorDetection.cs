using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class SectorDetection : MonoBehaviour
    {
        public Transform centerPoint;
        public float radius = 1;
        public float startAngle = 0;
        public float endAngle = 180;
        public LayerMask layerMask;
        private Collider[] colliderCache = new Collider[3];
        private List<Collider> results = new List<Collider>();

        public List<Collider> GetCollider()
        {
            int count = Physics.OverlapSphereNonAlloc(
                centerPoint.position,
                radius,
                colliderCache,
                layerMask
            );
            results.Clear();
            for (int i = 0; i < count; i++)
            {
                Collider collider = colliderCache[i];
                Vector3 direction = collider.transform.position - centerPoint.position;
                float angle = Vector3.Angle(centerPoint.forward, direction);
                if (angle >= startAngle && angle <= endAngle)
                {
                    results.Add(collider);
                }
            }
            return results;
        }
    }
}
