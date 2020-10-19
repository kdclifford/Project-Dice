using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AnimationFunctions.Utils
{
    public class AnimationScript
    {
        // public bool rightFire = false;
        public static void Idle(Animator animator)
        {            
                int idle = Random.Range(0, 4);
            
                switch (idle)
                {
                    case 0:
                        animator.SetInteger("Animation", 0);
                        break;
                    case 1:
                        animator.SetInteger("Animation", 7);
                        break;
                    case 2:
                        animator.SetInteger("Animation", 8);
                        break;
                    case 3:
                        animator.SetInteger("Animation", 9);
                        break;
                }            
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

        public static void WalkBack(Animator animator)
        {
           // Debug.Log("Walking Back");
            animator.SetInteger("Animation", 4);
        }
        public static void WalkLeft(Animator animator)
        {
            //Debug.Log("Walking Left");
            animator.SetInteger("Animation", 5);
        }

        public static void WalkRight(Animator animator)
        {
            //Debug.Log("Walking Right");
            animator.SetInteger("Animation", 6);
        }

    }
}