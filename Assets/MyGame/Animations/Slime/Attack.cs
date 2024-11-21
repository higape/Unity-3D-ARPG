using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Animations.Slime
{
    public abstract class AttackBase : StateMachineBehaviour
    {
        protected abstract float StartTime { get; }
        protected abstract float EndTime { get; }

        protected MyGame.Slime slime;
        protected float timeCount;

        public override void OnStateEnter(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex
        )
        {
            slime = animator.GetComponent<MyGame.Slime>();
            timeCount = 0;
        }

        public override void OnStateUpdate(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex
        )
        {
            timeCount += Time.deltaTime;
            if (timeCount > EndTime)
            {
                if (!animator.IsInTransition(0))
                {
                    slime.weapon1.SetColliderEnabled(false);
                }
            }
            else if (timeCount > StartTime)
            {
                slime.weapon1.SetColliderEnabled(true);
            }
        }
    }
}
