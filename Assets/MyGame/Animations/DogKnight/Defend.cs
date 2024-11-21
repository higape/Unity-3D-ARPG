using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Animations.DogKnight
{
    public class Defend : StateMachineBehaviour
    {
        private Player player;

        public override void OnStateEnter(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex
        )
        {
            player = animator.gameObject.GetComponentInParent<Player>();
        }

        public override void OnStateUpdate(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex
        )
        {
            player.UpdateDefend();
        }

        public override void OnStateExit(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex
        )
        {
            animator.SetBool(AnimationHash.IsDefending, false);
        }
    }
}
