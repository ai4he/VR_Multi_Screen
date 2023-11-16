using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;




public class RaycastFromController : MonoBehaviour

{
    public XRController rightHandController;
    public LayerMask screenLayer; // Layer of the screen

    void Update()
    {
        if (CheckIfRightControllerPointing())
        {
            if (rightHandController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool rightTriggerPressed))
            {
                if (rightTriggerPressed)
                {
                    Debug.Log("Right controller's trigger is pressed while pointing at the target GameObject!");
                }
            }
        }
    }

    private bool CheckIfRightControllerPointing()
    {
        return Physics.Raycast(rightHandController.transform.position, rightHandController.transform.forward, out RaycastHit hit, Mathf.Infinity, screenLayer) && hit.transform == transform;
    }
}
