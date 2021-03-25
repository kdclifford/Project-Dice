using AnimationFunctions.Utils;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Button.Utils;
using UnityEngine;

namespace PlayerCollisionCheck.Utils
{
    public class CollisionCheck
    {
        // Update is called once per frame
        public static Vector2 RayCastCollisions(Vector2 velocity, Vector3 rayOffset, float rayDist, LayerMask layerMask, float collisionForce, Transform playerPos)
        {
        Vector3 upRight = Vector3.forward + Vector3.right;
        Vector3 upLeft = Vector3.forward + Vector3.left;
        Vector3 downRight = Vector3.back + Vector3.right;
        Vector3 downLeft = Vector3.back + Vector3.left;

        bool clearVelocity = false;
        Vector3 oldpos;
            oldpos = playerPos.position;

            if(CheckRay(rayOffset, rayDist, layerMask, playerPos, Vector3.left, collisionForce, downLeft, upLeft))
            {
                clearVelocity = true;
            }

            if (CheckRay(rayOffset, rayDist, layerMask, playerPos, Vector3.right, collisionForce, upRight, downRight))
            {
                clearVelocity = true;
            }

            if (CheckRay(rayOffset, rayDist, layerMask, playerPos, Vector3.forward, collisionForce, upLeft, upRight))
            {
                clearVelocity = true;
            }

            if (CheckRay(rayOffset, rayDist, layerMask, playerPos, Vector3.back, collisionForce, downRight, downLeft))
            {
                clearVelocity = true;
            }

            
            


            if (!clearVelocity)
            {
                playerPos.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }

            DrawRay(rayOffset, rayDist, layerMask, playerPos, ref velocity.y, Vector3.forward);
            DrawRay(rayOffset, rayDist, layerMask, playerPos, ref velocity.y, Vector3.back);
            DrawRay(rayOffset, rayDist, layerMask, playerPos, ref velocity.x, Vector3.left);
            DrawRay(rayOffset, rayDist, layerMask, playerPos, ref velocity.x, Vector3.right);

            return velocity;
        }



        static bool DrawDiagonals(Vector3 rayOffset, float rayDist, LayerMask layerMask, Transform playerPos, Vector3 direction)
        {
            if (Physics.Raycast(playerPos.position + rayOffset, direction, rayDist * 0.9f, ~layerMask))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void DrawRay(Vector3 rayOffset, float rayDist, LayerMask layerMask, Transform playerPos, ref float velocity, Vector3 direction)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerPos.position + rayOffset, direction, out hit, rayDist, ~layerMask))
            {
                float tempVelocity = velocity;

                if(tempVelocity > 0)
                {
                    tempVelocity = 1;
                }

                if(tempVelocity < 0)
                {
                    tempVelocity = -1;
                }

                if (tempVelocity -(direction.x + direction.z ) == 0)
                {
                    velocity = 0;
                }
            }
            else
            {

            }
        }


        static bool CheckRay(Vector3 rayOffset, float rayDist, LayerMask layerMask, Transform playerPos, Vector3 direction, float collisionForce, Vector3 checkDir1, Vector3 checkDir2)
        {
            RaycastHit hit;

            if (DrawDiagonals(rayOffset, rayDist, layerMask, playerPos, checkDir1) && DrawDiagonals(rayOffset, rayDist, layerMask, playerPos, checkDir2))
            {
                if (Physics.Raycast(playerPos.position + rayOffset, direction, out hit, rayDist, ~layerMask))
                {
                    //Vector3 newPos = new Vector3(transform.position.x - (rayDist - hit.distance), transform.position.y, transform.position.z);
                    //transform.position = newPos;

                    playerPos.GetComponent<Rigidbody>().AddForce(-direction * collisionForce);
                    return true;
                }
            }
            return false;
        }



    }
}