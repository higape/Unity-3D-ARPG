using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Animations.DogKnight
{
    public class Attack01 : StateMachineBehaviour
    {
        private Player player;

        public override void OnStateEnter(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex
        )
        {
            player = animator.gameObject.GetComponentInParent<Player>();
            animator.SetBool(AnimationHash.IsAttackContinue, false);
        }

        public override void OnStateUpdate(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex
        )
        {
            player.UpdateAttack();
        }

        public override void OnStateExit(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex
        )
        {
            animator.SetBool(AnimationHash.IsAttacking, false);
        }
    }
}
