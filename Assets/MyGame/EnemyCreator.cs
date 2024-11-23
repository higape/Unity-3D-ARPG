using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyGame
{
    public class EnemyCreator : MonoBehaviour
    {
        public int maxCount = 10;
        public float intervalTime = 5f;
        public float maxDistance = 5f;
        public GameObject enemyPrefab;
        public GameObject effectPrefab;
        public float effectTime = 1f;

        private List<GameObject> GameObjects = new List<GameObject>();

        void Start()
        {
            StartCoroutine(ProcessCreate());
        }

        private IEnumerator ProcessCreate()
        {
            for (; ; )
            {
                //刷新数量统计
                int i = 0;
                while (i < GameObjects.Count)
                {
                    if (GameObjects[i] == null)
                        GameObjects.RemoveAt(i);
                    else
                        i++;
                }

                if (GameObjects.Count < maxCount)
                {
                    Vector3 randomPosition =
                        new Vector3(Random.value - 0.5f, 0, Random.value - 0.5f) * maxDistance;
                    float randomAngle = Random.Range(-180f, 180f);
                    Quaternion randomRotation = Quaternion.Euler(0, randomAngle, 0);
                    GameObject createdObject = Instantiate(
                        enemyPrefab,
                        randomPosition,
                        randomRotation
                    );
                    GameObjects.Add(createdObject);
                    if (effectPrefab != null)
                    {
                        GameObject effect = Instantiate(effectPrefab, createdObject.transform);
                        StartCoroutine(ProcessEffect(effect));
                    }
                }
                yield return new WaitForSeconds(intervalTime);
            }
        }

        private IEnumerator ProcessEffect(GameObject effect)
        {
            yield return new WaitForSeconds(effectTime);
            Destroy(effect);
        }

        // private void OnDrawGizmos()
        // {
        //     if (UnityEditor.Selection.gameObjects.Contains(gameObject))
        //     {
        //         Gizmos.color = new Color(1, 0, 0, 0.5f);
        //         Gizmos.DrawSphere(transform.position, maxDistance);
        //     }
        // }
    }
}
