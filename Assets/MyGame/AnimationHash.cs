using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public static class AnimationHash
    {
        public static int IsWalking = Animator.StringToHash("IsWalking");
        public static int IsRunning = Animator.StringToHash("IsRunning");
        public static int IsDizzy = Animator.StringToHash("IsDizzy");
        public static int IsDead = Animator.StringToHash("IsDead");
        public static int IsAttacking = Animator.StringToHash("IsAttacking");
        public static int IsAttackContinue = Animator.StringToHash("IsAttackContinue");
        public static int IsDefending = Animator.StringToHash("IsDefending");
        public static int HitTrigger = Animator.StringToHash("HitTrigger");
    }
}
