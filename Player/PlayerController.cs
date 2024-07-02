using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed, changingPositionToCrouching;
    [SerializeField] float gravity = -9.81f;

    private float walkVolumeMin = 0.2f, walkVolumeMax = 0.6f;
    private float sprintVolume = 1f;
    private float sprintStepDistance = 0.25f;
    private float walkStepDistance = 0.4f;
    private float verticalVelocity;
    private float transportY,heightToTransportUp,centerUp;
    private Vector3 movement;
    private Vector3 transportPosition;
    private bool isCrouch, isTransporting, isTransportingUp;
    private PlayerFootsteps playerFootsteps;
    private CharacterController characterController;
    private WeaponsManager weaponsManager;
    private SphereCollider sphereCollider;
    private void Awake()
    {
        playerFootsteps = GetComponent<PlayerFootsteps>();
        characterController = GetComponent<CharacterController>();
        weaponsManager = Object.FindFirstObjectByType<WeaponsManager>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Start is called before the first fr
    void Start()
    {

    }

 

    private void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movement = transform.TransformDirection(movement);
        

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {


            if (!isCrouch)
            {
                SetCharacterToMovementPosition(new Vector3(0, 0.28f, 0), 1.35f);
                isCrouch = true;
                transportY = -0.07500002f;
                
                SetColliderRadius(3.38f);
                isTransporting = true;
            }
            else
            {
                isCrouch = false;
                transportY = 0.423f;
                isTransportingUp = true;
                heightToTransportUp = 1.8f;

            }


        }

        if (isTransporting || isTransportingUp)
        {
            transportPosition = new Vector3(transform.position.x, transportY, transform.position.z);
    
            transform.position = Vector3.Lerp(transform.position, transportPosition, changingPositionToCrouching * Time.deltaTime);
           
            if(isTransportingUp)
            {

                characterController.height = Mathf.Lerp(characterController.height, heightToTransportUp, 3.8f * Time.deltaTime);
                characterController.center = new Vector3(0, Mathf.Lerp(characterController.center.y, 0, 4f * Time.deltaTime), 0);
            }


            if (Vector3.Distance(transform.position, transportPosition) < 0.01f)
            {
                transform.position = transportPosition;
                isTransporting = false;
                isTransportingUp = false;
            }
        }

     

        if (movement != Vector3.zero)
        {

            if (isCrouch)
                speed = 1f;
            else speed = (Input.GetKey(KeyCode.LeftShift) ? 4f : 2f);
            //grawitacja
            if (characterController.isGrounded)
            {
                verticalVelocity = -0.5f; // Lekka ujemna wartoœæ, aby trzymaæ postaæ przy ziemi
            }
            else
            {
                verticalVelocity += gravity * Time.deltaTime;
            }


         
            movement.y = verticalVelocity;

           
            characterController.Move(movement * speed * Time.deltaTime);
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                playerFootsteps.stepDistance = walkStepDistance;
                playerFootsteps.volumeMax = walkVolumeMax;
                playerFootsteps.volumeMin = walkVolumeMin;
                if (!weaponsManager.IsReloading())
                {
                    if (Input.GetKey(KeyCode.Mouse1))
                        weaponsManager.PlayAnimation("Walk_Aiming");
                    else
                        weaponsManager.PlayAnimation("Walk");
                }
                if(!isCrouch)
                    SetColliderRadius(6.65f); 
            }
            else if (Input.GetKey(KeyCode.LeftShift) && !weaponsManager.IsShooting() && !weaponsManager.IsReloading() && !isCrouch)
            {
                playerFootsteps.stepDistance = sprintStepDistance;
                playerFootsteps.volumeMin = sprintVolume;
                playerFootsteps.volumeMax = sprintVolume;
                weaponsManager.PlayAnimation("Run");
                SetColliderRadius(10.05697f);
            }
            return;
        }

        if (!weaponsManager.IsReloading())
        {
            if (Input.GetKey(KeyCode.Mouse1))
                weaponsManager.PlayAnimation("Idle_Aiming");
            else
                weaponsManager.PlayAnimation("Idle");
        }
    }

    void SetCharacterToMovementPosition(Vector3 center, float height)
    {
        characterController.center = center;
        characterController.height = height;
    

    }

    public Vector3 GetMovement()
    {
        return movement;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetColliderRadius(float radius)
    {
        sphereCollider.radius = radius;

    }
    public bool GetIsCrouch()
    {
        return isCrouch;
    
    }
   

}