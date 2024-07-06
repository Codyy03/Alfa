using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float scrollSensitivity = 1f;
    [SerializeField] float bobbingSpeed = 0.18f;
    [SerializeField] float bobbingAmount = 0.05f;
    private float defaultPosY = 0;
    private float timer = 0;
    float viewLimit;


    PlayerController playerController;

    private void Awake()
    {
        playerController =FindFirstObjectByType<PlayerController>();
    }
    private void Start()
    {
        defaultPosY = cam.transform.localPosition.y;
    }
    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            viewLimit = cam.fieldOfView - scroll * scrollSensitivity;
            viewLimit = Mathf.Clamp(viewLimit, 20f, 75f);
            cam.fieldOfView = viewLimit;

        }

        // Efekt ruchu kamery
        if (cam.isActiveAndEnabled)
        {
            ChangeMovingCameraStatsToRightLevel();
            timer += Time.deltaTime * bobbingSpeed;
            float waveslice = Mathf.Sin(timer);
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, defaultPosY + waveslice * bobbingAmount, cam.transform.localPosition.z);
        }

    }

    void ChangeMovingCameraStatsToRightLevel()
    {
        if (playerController.GetMovement() != Vector3.zero)
        {
            ChangeMovingCameraStats(1.5f, 0.3f);
        }
        else
        {
            if (playerController.GetIsCrouch())
                ChangeMovingCameraStats(0f, 0f);
            else ChangeMovingCameraStats(0.9f, 0.15f);
        }
    }

    void ChangeMovingCameraStats(float speed, float amount)
    {
        bobbingSpeed = speed;
        bobbingAmount = amount;


    }
    
}
