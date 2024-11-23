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
                weapon1.TriggerEnterCallback = (hit) =>
                    hit.Hit(new AttackData() { strength = attack });
            if (weapon2 != null)
                weapon2.TriggerEnterCallback = (hit) =>
                    hit.Hit(new AttackData() { strength = attack });
        }

        void FixedUpdate()
        {
            //坠落到地面以下
            if (transform.position.y < -10)
            {
                Destroy(gameObject);
                return;
            }

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
    }
}
