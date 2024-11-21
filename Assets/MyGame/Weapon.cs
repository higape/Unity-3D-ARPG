using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private new Collider collider;

        [SerializeField]
        private GameObject effectPrefab;

        [SerializeField]
        private float effectTime = 1;

        public Action<Collider> TriggerEnterCallback { get; set; }

        private void Awake()
        {
            collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            TriggerEnterCallback?.Invoke(other);
            if (effectPrefab != null)
                StartCoroutine(ProcessVisualEffect(other.bounds.center));
        }

        public void SetColliderEnabled(bool enabled)
        {
            collider.enabled = enabled;
        }

        private IEnumerator ProcessVisualEffect(Vector3 position)
        {
            GameObject obj = Instantiate(effectPrefab, position, Quaternion.identity);
            yield return new WaitForSeconds(effectTime);
            Destroy(obj);
        }
    }
}
