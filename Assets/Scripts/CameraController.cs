using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null) return;
        
        Vector3 desiredPosition = new Vector3(target.transform.position.x, target.transform.position.y, -1);;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        // transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -1);
    }
    
    public void SwitchControl(GameObject newTarget)
    {
        if (target != null)
        {
            // Disable controls for the current target object
            PlayerController currentController = target.GetComponent<PlayerController>();
            if (currentController != null)
            {
                currentController.isControllable = false;
            }
        }
        // Enable controls for the new target object
        PlayerController newController = newTarget.GetComponent<PlayerController>();
        if (newController != null)
        {
            newController.isControllable = true;
        }

        // Switch camera target
        target = newTarget;
    }
}
