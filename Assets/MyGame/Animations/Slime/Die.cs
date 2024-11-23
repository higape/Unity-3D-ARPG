using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Animations.Slime
{
    public class Die : StateMachineBehaviour
    {
        private float timeCount;

        public override void OnStateEnter(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex
        )
        {
            //设置销毁对象的时间点为动作结束后0.2秒
            timeCount = stateInfo.length + 0.2f;
        }

        public override void OnStateUpdate(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex
        )
        {
            if (timeCount > 0)
            {
                timeCount -= Time.deltaTime;
            }
            else
            {
                //销毁对象
                Destroy(animator.gameObject);
            }
        }
    }
}
