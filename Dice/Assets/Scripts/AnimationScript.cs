using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AnimationFunctions.Utils
{
    public class AnimationScript
    {      
        public static void RightAttack(Animator animator)
        {
            animator.SetInteger("Attack", 1);
            //animator.SetBool("Shooting", true);
        }

        public static void LeftAttack(Animator animator)
        {
            animator.SetInteger("Attack", 2);
            //animator.SetBool("Shooting", true);
        }

        public static void DoubleAttack(Animator animator)
        {
            animator.SetInteger("Attack", 3);
            //animator.SetBool("Shooting", true);
        }
    }
}