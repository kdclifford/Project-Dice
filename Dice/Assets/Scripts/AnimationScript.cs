﻿using System.Collections;
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


        public static void SpiderAttack(Animator animator)
        {
            animator.SetInteger("Attack", 1);
        }


        public static Vector2 CurrentDirection(Vector2 input, GameObject agent)
        {
            var a = Vector3.SignedAngle(new Vector3(input.x, 0, input.y), agent.transform.forward, agent.transform.up);

            // Normalize the angle
            if (a < 0)
            {
                a *= -1;
            }
            else
            {
                a = 360 - a;
            }

            // Take into consideration the angle of the camera
            //a += Camera.main.transform.eulerAngles.y;

            var aRad = Mathf.Deg2Rad * a; // degrees to radians
                                          //Debug.Log(leftStickInputAxis);
                                          // If there is some form of input, calculate the new axis relative to the rotation of the model
            if (input.x != 0 || input.y != 0)
            {
                input = new Vector2(Mathf.Sin(aRad), Mathf.Cos(aRad));
            }


            if (input.x < -1)
            {
                input.x = -1;
            }
            else if (input.x > 1.6f)
            {
                input.x = 1.6f;
            }


            if (input.y < -1)
            {
                input.y = -1;
            }
            else if (input.y > 1.6f)
            {
                input.y = 1.6f;
            }

            return input;
        }


    }
}