using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform playerRoot, lookRoot;
    [SerializeField] bool invert;

    [SerializeField] bool can_Unlock = true;

    [SerializeField] float sensevity = 10;
    [SerializeField] int smoothSteps;
    [SerializeField] float smoothWeight = 0.4f;
    [SerializeField] float rollAngel = 10f;
    [SerializeField] float rollSpeed = 3f;
    [SerializeField] Vector2 defaultLookLimits = new Vector2(-70f, 80f);

    Vector2 lookAngles;
    Vector2 currentMouseLook;
    Vector2 smoothMove;

    float currentRollAngle;
    int lastLookFrame;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        LockAndUnlockCursor();
        if (Cursor.lockState == CursorLockMode.Locked)
            LookAround();
    }
    

    void LockAndUnlockCursor()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }
        }
    }

    void LookAround()
    {
        currentMouseLook = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        lookAngles.x += currentMouseLook.x * sensevity * ( invert? 1f: -1f);
        lookAngles.y += currentMouseLook.y * sensevity;

        lookAngles.x = Mathf.Clamp(lookAngles.x, defaultLookLimits.x, defaultLookLimits.y);

        currentRollAngle = Mathf.Lerp(currentRollAngle, Input.GetAxisRaw("Mouse X") * rollAngel, Time.deltaTime * rollSpeed);
        lookRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, currentRollAngle);
        playerRoot.localRotation = Quaternion.Euler(0f, lookAngles.y, 0f);
    
    }

}
