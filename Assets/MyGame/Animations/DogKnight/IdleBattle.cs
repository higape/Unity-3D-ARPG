using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Animations.DogKnight
{
    public class IdleBattle : StateMachineBehaviour
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
            player.UpdateMove();
            player.UpdateAttack();
            player.UpdateDefend();
        }
    }
}
