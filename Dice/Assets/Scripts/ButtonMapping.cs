using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Button.Utils
{
    public class ButtonMapping
    {
        public static bool GetButton(EControllerType controllerType, EButtonActions buttonAction)
        {
            switch (controllerType)
            {
                case EControllerType.Controller:
                    switch (buttonAction)
                    {
                        case EButtonActions.RightAttack:
                            return Input.GetAxis("RTrigger") > 0;
                        case EButtonActions.LeftAttack:
                            return Input.GetAxis("LTrigger") > 0;
                        case EButtonActions.Interact:
                            return Input.GetKeyDown(KeyCode.JoystickButton0);
                        case EButtonActions.LeftEquipt:
                            return Input.GetAxis("HorizontalDpad") < 0;
                        case EButtonActions.RightEquipt:
                            return Input.GetAxis("HorizontalDpad") > 0;

                    }
                    break;
                case EControllerType.Computer:
                    switch (buttonAction)
                    {
                        case EButtonActions.RightAttack:
                            return Input.GetMouseButton(1);
                        case EButtonActions.LeftAttack:
                            return Input.GetMouseButton(0);
                        case EButtonActions.Interact:
                            return Input.GetKey(KeyCode.Space);
                        case EButtonActions.LeftEquipt:
                            return Input.GetAxis("PickUpKeyBoard") > 0;
                        case EButtonActions.RightEquipt:
                            return Input.GetAxis("PickUpKeyBoard") < 0;


                    }
                    break;
            }


            return false;
        }

        public static float GetStick(EControllerType controllerType, EStickMovement stickMovement, Vector3 playerPosition)
        {
            Vector3 object_pos;
            Vector3 mouse_pos;
            switch (controllerType)
            {
                case EControllerType.Controller:
                    switch (stickMovement)
                    {

                        case EStickMovement.VerticalMovement:
                            return Input.GetAxis("LVertical");
                        case EStickMovement.HorizontalMovement:
                            return Input.GetAxis("LHorizontal");
                        case EStickMovement.VerticalFacing:


                            return Input.GetAxis("RVertical");
                        case EStickMovement.HorizontalFacing:
                            return Input.GetAxis("RHorizontal");
                    }
                    break;
                case EControllerType.Computer:
                    switch (stickMovement)
                    {

                        case EStickMovement.VerticalMovement:
                            return Input.GetAxis("Vertical");
                        case EStickMovement.HorizontalMovement:
                            return Input.GetAxis("Horizontal");
                        case EStickMovement.VerticalFacing:

                            mouse_pos = Input.mousePosition;
                            //mouse_pos.z = 5.23; //The distance between the camera and object
                            object_pos = Camera.main.WorldToScreenPoint(playerPosition);
                            //object_pos = new Vector3(320.5f, 160.2f, 21.0f);

                            mouse_pos.x = mouse_pos.x - object_pos.x;
                            mouse_pos.y = mouse_pos.y - object_pos.y;
                            return mouse_pos.y;
                        case EStickMovement.HorizontalFacing:
                            mouse_pos = Input.mousePosition;
                            //mouse_pos.z = 5.23; //The distance between the camera and object
                            object_pos = Camera.main.WorldToScreenPoint(playerPosition);
                            //object_pos = new Vector3(320.5f, 160.2f, 21.0f);
                            mouse_pos.x = mouse_pos.x - object_pos.x;
                            mouse_pos.y = mouse_pos.y - object_pos.y;
                            return mouse_pos.x;
                    }
                    break;

            }
            return 0;
        }

    }

}
        public enum EControllerType
        {
            Controller,
            Computer,
        }

        public enum EButtonActions
        {
            RightAttack,
            LeftAttack,
            Interact,
            LeftEquipt,
            RightEquipt,        
            Teleport,
        }

        public enum EStickMovement
        {         
            VerticalMovement,
            HorizontalMovement,
            VerticalFacing,
            HorizontalFacing,
        }