using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyGame
{
    public class Character : MonoBehaviour
    {
        public new string name;

        [SerializeField]
        private int life = 1;
        public int attack = 1;
        public int defence;
        public float moveSpeed = 2;
        public UnityEvent<Character> anyChanged;

        public int Life
        {
            get => life;
            set
            {
                life = Mathf.Clamp(value, 0, MaxLife);
                anyChanged.Invoke(this);
            }
        }
        public int MaxLife { get; set; }
        public bool IsAlive => Life > 0;
        public float TurnSpeed => moveSpeed * 90;

        private void Awake()
        {
            if (Life <= 0)
            {
                Debug.LogWarning(gameObject.name + "的生命值设置不正确。");
                Life = 1;
            }
            MaxLife = Life;
            anyChanged.Invoke(this);
        }
    }
}
