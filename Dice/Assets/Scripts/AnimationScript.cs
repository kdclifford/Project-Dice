using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AnimationFunctions.Utils
{
    public class AnimationScript
    {
      // public bool rightFire = false;
        public static void Idle4(Animator animator)
        {
            animator.SetInteger("Animation", 0);
        }

        public static void RightAttack(Animator animator)
        {
            animator.SetInteger("Animation", 1);
        }

        public static void LeftAttack(Animator animator)
        {
            animator.SetInteger("Animation", 2);
        }

        public static void WalkFoward(Animator animator)
        {
            animator.SetInteger("Animation", 3);
        }
    }
}