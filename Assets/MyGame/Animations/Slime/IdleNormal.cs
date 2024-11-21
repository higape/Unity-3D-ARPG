using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Animations.Slime
{
    public class IdleNormal : StateMachineBehaviour
    {
        private MyGame.Slime slime;
        private float timeCount;
        private bool noTarget;

        public override void OnStateEnter(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex
        )
        {
            slime = animator.GetComponent<MyGame.Slime>();
            timeCount = slime.intervalTime * Random.Range(0.9f, 1.1f);
            noTarget = slime.MoveTarget == null;
        }

        public override void OnStateUpdate(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex
        )
        {
            //发现目标
            if (noTarget && slime.MoveTarget != null)
            {
                if (!animator.IsInTransition(0))
                {
                    animator.SetTrigger("Walk");
                }
            }
            else if (timeCount > 0)
            {
                timeCount -= Time.deltaTime;
            }
            else if (!animator.IsInTransition(0))
            {
                animator.SetTrigger("Walk");
            }
        }
    }
}
