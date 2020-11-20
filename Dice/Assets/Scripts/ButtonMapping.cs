using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Button.Utils
{
    public class ButtonMapping
    {
        public static dynamic GetButton(EControllerType controllerType, EButtonActions buttonAction)
        {
            Vector3 object_pos;
            Vector3 mouse_pos;
            switch (controllerType)
            {
                case EControllerType.Controller:
                    switch (buttonAction)
                    {
                        case EButtonActions.RightAttack:
                            return "RTrigger";
                        case EButtonActions.LeftAttack:
                            return "LTrigger";
                        case EButtonActions.Interact:
                            return KeyCode.JoystickButton0;
                        case EButtonActions.LeftEquipt:
                            return "HorizontalDpad";
                        case EButtonActions.RightEquipt:
                            return "HorizontalDpad";
                        case EButtonActions.VerticalMovement:
                            return "LVertical";
                        case EButtonActions.HorizontalMovement:
                            return "LHorizontal";
                        case EButtonActions.VerticalFacing:
                            return Input.GetAxis("RVertical");
                        case EButtonActions.HorizontalFacing:
                            return Input.GetAxis("RHorizontal");
                    }
                    break;
                case EControllerType.Computer:
                    switch (buttonAction)
                    {
                        case EButtonActions.RightAttack:
                            return "Mouse Fire 1";
                        case EButtonActions.LeftAttack:
                            return "Mouse Fire 2";
                        case EButtonActions.Interact:
                            return KeyCode.Space;
                        case EButtonActions.LeftEquipt:
                            return "PickUpKeyBoard";
                        case EButtonActions.RightEquipt:
                            return "PickUpKeyBoard";
                        case EButtonActions.VerticalMovement:
                            return "Vertical";
                        case EButtonActions.HorizontalMovement:
                            return "Horizontal";
                        case EButtonActions.VerticalFacing:
                            
                            mouse_pos = Input.mousePosition;
                            //mouse_pos.z = 5.23; //The distance between the camera and object
                            object_pos = Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("Player").transform.position);
                            //object_pos = new Vector3(320.5f, 160.2f, 21.0f);

                            mouse_pos.x = mouse_pos.x - object_pos.x;
                            mouse_pos.y = mouse_pos.y - object_pos.y;
                            return mouse_pos.y;
                        case EButtonActions.HorizontalFacing:                            
                            mouse_pos = Input.mousePosition;
                            //mouse_pos.z = 5.23; //The distance between the camera and object
                            object_pos = Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("Player").transform.position);
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
    VerticalMovement,
    HorizontalMovement,
    VerticalFacing,
    HorizontalFacing,
    Teleport,
}