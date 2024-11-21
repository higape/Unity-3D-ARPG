using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class Slime : Character, IHit
    {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private new Rigidbody rigidbody;

        public float intervalTime = 3;
        public float attackDistance = 1.4f;
        public SectorDetection detection;
        public Weapon weapon1;
        public Weapon weapon2;
        public Player MoveTarget { get; private set; }
        public Player AttackTarget { get; private set; }

        void Start()
        {
            if (weapon1 != null)
                weapon1.TriggerEnterCallback = ProcessAttack1;
            if (weapon2 != null)
                weapon2.TriggerEnterCallback = ProcessAttack2;
        }

        void Update()
        {
            //如果视野内有玩家
            //如果玩家在攻击范围（距离），攻击
            //否则靠近玩家
            //否则休闲-随机移动
        }

        void FixedUpdate()
        {
            MoveTarget = null;
            AttackTarget = null;
            foreach (var c in detection.GetCollider())
            {
                if (c.TryGetComponent<Player>(out var player))
                {
                    MoveTarget = player;
                    if (
                        Vector3.Distance(MoveTarget.transform.position, transform.position)
                        < attackDistance
                    )
                    {
                        AttackTarget = player;
                    }
                    return;
                }
            }
        }

        public bool RotateTo(Quaternion target)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                target,
                TurnSpeed * Time.deltaTime
            );
            return transform.rotation == target;
        }

        public void Walk(Vector3 direction)
        {
            rigidbody.velocity = new Vector3(direction.x, rigidbody.velocity.y, direction.z);
        }

        public void Hit(AttackData data)
        {
            if (IsAlive)
            {
                Life -= Mathf.Max(data.strength - defence, 0);
                if (IsAlive)
                {
                    animator.SetTrigger(AnimationHash.HitTrigger);
                }
                else
                {
                    animator.SetBool(AnimationHash.IsDead, true);
                }
            }
        }

        private void ProcessAttack1(Collider other)
        {
            if (other.TryGetComponent<Player>(out var player))
            {
                player.Hit(new AttackData() { strength = attack });
            }
        }

        private void ProcessAttack2(Collider other)
        {
            if (other.TryGetComponent<Player>(out var player))
            {
                player.Hit(new AttackData() { strength = attack });
            }
        }
    }
}
