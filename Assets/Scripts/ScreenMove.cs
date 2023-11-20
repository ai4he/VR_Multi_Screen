// using UnityEngine;
// using UnityEngine.XR.Interaction.Toolkit;
// using UnityEngine.XR;

// public class ScreenMove : MonoBehaviour
// {
//     public XRController rightHandController;
//     public LayerMask screenLayer; // Layer of the screen
//     public float moveSpeed = 10f; // Adjust to set the speed of the screen movement

//     void Update()
//     {
//         // Check if the right controller is pointing at the screen
//         if (CheckIfRightControllerPointing())
//         {
//             // Detect grip button pressed on the right controller
//             if (rightHandController.inputDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool rightGripPressed))
//             {
//                 if (rightGripPressed)
//                 {
//                     Debug.Log("Right controller's grip is pressed while pointing at the target GameObject!");

//                     // Check if the device has a velocity feature
//                     if(rightHandController.inputDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity))
//                     {
//                         // Move the screen towards the controller at the speed of the controller's velocity times a move speed factor
//                         transform.position += velocity * moveSpeed;
//                     }
//                 }
//             }
//         }
//     }

//     private bool CheckIfRightControllerPointing()
//     {
//         return Physics.Raycast(rightHandController.transform.position, rightHandController.transform.forward, out RaycastHit hit, Mathf.Infinity, screenLayer) && hit.transform == transform;
//     }
// }











// THis code is working fine



// using UnityEngine;
// using UnityEngine.XR.Interaction.Toolkit;
// using UnityEngine.XR;

// public class ScreenMove : MonoBehaviour
// {
//     public XRController rightHandController;
//     public XRController leftHandController;
//     public LayerMask screenLayer; // Layer of the screen
//     public float moveSpeed = 10f; // Adjust to set the speed of the screen movement

//     void Update()
//     {
//         // Right Controller
//         if (CheckIfControllerPointing(rightHandController))
//         {
//             // Detect grip button pressed on the right controller
//             if (rightHandController.inputDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool rightGripPressed))
//             {
//                 if (rightGripPressed)
//                 {
//                     Debug.Log("Right controller's grip is pressed while pointing at the target GameObject!");

//                     // Check if the device has a velocity feature
//                     if(rightHandController.inputDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity))
//                     {
//                         // Move the screen towards the controller at the speed of the controller's velocity times a move speed factor
//                         transform.position += velocity * moveSpeed;
//                     }
//                 }
//             }
//         }

//         // Left Controller
//         if (CheckIfControllerPointing(leftHandController))
//         {
//             // Detect grip button pressed on the left controller
//             if (leftHandController.inputDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool leftGripPressed))
//             {
//                 if (leftGripPressed)
//                 {
//                     Debug.Log("Left controller's grip is pressed while pointing at the target GameObject!");

//                     // Check if the device has a velocity feature
//                     if(leftHandController.inputDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity))
//                     {
//                         // Move the screen towards the controller at the speed of the controller's velocity times a move speed factor
//                         transform.position += velocity * moveSpeed;
//                     }
//                 }
//             }
//         }
//     }

//     private bool CheckIfControllerPointing(XRController controller)
//     {
//         return Physics.Raycast(controller.transform.position, controller.transform.forward, out RaycastHit hit, Mathf.Infinity, screenLayer) && hit.transform == transform;
//     }
// }


///////////////////////////////////////

// using UnityEngine;
// using UnityEngine.XR;
// using UnityEngine.XR.Interaction.Toolkit;

// public class ScreenMove : MonoBehaviour
// {
//     public XRController rightHandController;
//     public XRController leftHandController;
//     public LayerMask screenLayer; // Layer of the screen
//     public float moveSpeed = 10f; // Adjust to set the speed of the screen movement

//     private Vector3 lastRightPosition;
//     private Vector3 lastLeftPosition;

//     void Update()
//     {
//         // Right Controller
//         if (CheckIfControllerPointing(rightHandController))
//         {
//             HandleGripAndMovement(rightHandController);
//         }

//         // Left Controller
//         if (CheckIfControllerPointing(leftHandController))
//         {
//             HandleGripAndMovement(leftHandController);
//         }

//         // Rotate screen based on the controllers movement in opposite directions
//         if (CheckIfControllerPointing(rightHandController) && CheckIfControllerPointing(leftHandController))
//         {
//             Vector3 rightDiff = rightHandController.transform.position - lastRightPosition;
//             Vector3 leftDiff = leftHandController.transform.position - lastLeftPosition;

//             float angle = Vector3.SignedAngle(rightDiff, -leftDiff, Vector3.up);
//             transform.Rotate(0, angle, 0);
//         }

//         lastRightPosition = rightHandController.transform.position;
//         lastLeftPosition = leftHandController.transform.position;
//     }

//     private void HandleGripAndMovement(XRController controller)
//     {
//         // Detect grip button pressed on the controller
//         if (controller.inputDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripPressed))
//         {
//             if (gripPressed)
//             {
//                 Debug.Log("Controller's grip is pressed while pointing at the target GameObject!");

//                 // Check if the device has a velocity feature
//                 if (controller.inputDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity))
//                 {
//                     // Move the screen towards the controller at the speed of the controller's velocity times a move speed factor
//                     transform.position += velocity * moveSpeed;
//                 }
//             }
//         }
//     }

//     private bool CheckIfControllerPointing(XRController controller)
//     {
//         return Physics.Raycast(controller.transform.position, controller.transform.forward, out RaycastHit hit, Mathf.Infinity, screenLayer) && hit.transform == transform;
//     }
// }


using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ScreenMove : MonoBehaviour
{
    public XRController rightHandController;
    public XRController leftHandController;
    public LayerMask screenLayer; // Layer of the screen
    public float moveSpeed = 0.07f; // Adjust to set the speed of the screen movement
    //public float rotationSpeed = 0.01f; // Adjust to set the speed of the screen rotation

    private Vector3 lastRightPosition;
    private Vector3 lastLeftPosition;

    void Update()
    {
        // Right Controller
        if (CheckIfControllerPointing(rightHandController))
        {
            HandleGripAndMovement(rightHandController);
        }

        // Left Controller
        if (CheckIfControllerPointing(leftHandController))
        {
            HandleGripAndMovement(leftHandController);
        }
        // Rotate();
        // Rotate screen based on the controllers movement in opposite directions
        // if (IsGripPressed(rightHandController) && IsGripPressed(leftHandController) && CheckIfControllerPointing(rightHandController) && CheckIfControllerPointing(leftHandController))
        // {
        //     Vector3 rightDiff = rightHandController.transform.position - lastRightPosition;
        //     Vector3 leftDiff = leftHandController.transform.position - lastLeftPosition;

        //     float angle = Vector3.SignedAngle(rightDiff, -leftDiff, Vector3.up);
        //     transform.Rotate(0, angle * rotationSpeed, 0);
        // }

        // lastRightPosition = rightHandController.transform.position;
        // lastLeftPosition = leftHandController.transform.position;
    }

    private void HandleGripAndMovement(XRController controller)
    {
        // Detect grip button pressed on the controller
        if (IsGripPressed(controller))
        {
            Debug.Log("Controller's grip is pressed while pointing at the target GameObject!");

            // Check if the device has a velocity feature
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity))
            {
                // Move the screen towards the controller at the speed of the controller's velocity times a move speed factor
                transform.position += velocity * moveSpeed;
            }
        }
    }

    private bool CheckIfControllerPointing(XRController controller)
    {
        return Physics.Raycast(controller.transform.position, controller.transform.forward, out RaycastHit hit, Mathf.Infinity, screenLayer) && hit.transform == transform;
    }

    private bool IsGripPressed(XRController controller)
    {
        return controller.inputDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripPressed) && gripPressed;
    }




    // void Rotate()
    // {
    //     // Check if left controller is pointing
    //     if (CheckifLeftControllersPointing())
    //     {
    //         if (leftHandController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 leftThumbstick) && leftThumbstick.x != 0)
    //         {
    //             // Rotate based on left thumbstick horizontal movement
    //             transform.Rotate(Vector3.up, leftThumbstick.x * rotationSpeed, Space.Self);
                
    //         }
    //     }

    //     // Check if right controller is pointing
    //     if (CheckifRightControllersPointing())
    //     {
    //         if (rightHandController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightThumbstick) && rightThumbstick.x != 0)
    //         {
    //             // Rotate based on right thumbstick horizontal movement
    //             transform.Rotate(Vector3.up, rightThumbstick.x * rotationSpeed, Space.Self);
                
    //         }
    //     }
    // }

    // private bool CheckifLeftControllersPointing(){

    //     bool isLeftPointing = Physics.Raycast(leftHandController.transform.position, leftHandController.transform.forward, out RaycastHit leftHit, Mathf.Infinity, screenLayer);
        
    //     return isLeftPointing && leftHit.transform == transform;

    // }


    // private bool CheckifRightControllersPointing(){


    //     bool isRightPointing = Physics.Raycast(rightHandController.transform.position, rightHandController.transform.forward, out RaycastHit rightHit, Mathf.Infinity, screenLayer);
    //     return isRightPointing && rightHit.transform == transform;
    // }

    
}





