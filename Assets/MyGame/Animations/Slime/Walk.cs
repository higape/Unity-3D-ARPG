using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Animations.Slime
{
    public class Walk : StateMachineBehaviour
    {
        private MyGame.Slime slime;
        private float timeCount;
        private Quaternion targetRotation;

        public override void OnStateEnter(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex
        )
        {
            slime = animator.GetComponent<MyGame.Slime>();
            if (slime.MoveTarget == null)
                timeCount = slime.intervalTime * Random.Range(0.9f, 1.1f);
            else
                timeCount = 0;
            //随机走动
            float angle = Random.Range(45f, 120f) * Mathf.Sign(Random.value - 0.5f);
            Vector3 e = slime.transform.rotation.eulerAngles;
            e.y += angle;
            targetRotation = Quaternion.Euler(e.x, e.y, e.z);
        }

        public override void OnStateUpdate(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex
        )
        {
            if (slime.MoveTarget == null)
            {
                //随机移动
                if (timeCount > 0)
                {
                    timeCount -= Time.deltaTime;
                    //更新旋转
                    if (slime.RotateTo(targetRotation))
                    {
                        //更新移动
                        slime.Walk(slime.transform.forward);
                    }
                }
                else if (!animator.IsInTransition(0))
                {
                    animator.SetTrigger("Idle");
                }
            }
            else if (slime.AttackTarget != null)
            {
                //攻击玩家
                if (!animator.IsInTransition(0))
                {
                    animator.SetTrigger("Attack01");
                    timeCount = slime.intervalTime;
                }
            }
            else
            {
                //走向玩家
                Vector3 moveDirection =
                    slime.MoveTarget.transform.position - slime.transform.position;
                moveDirection.Normalize();
                if (slime.RotateTo(Quaternion.LookRotation(moveDirection)))
                {
                    if (!animator.IsInTransition(0))
                    {
                        slime.Walk(slime.transform.forward);
                    }
                }
            }
        }
    }
}
