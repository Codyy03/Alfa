using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;

    private float walkVolumeMin = 0.2f, walkVolumeMax = 0.6f;
    private float spritVolume = 1f;
    private float sprintStepDistance = 0.25f;
    private float walkStepDistance = 0.4f;
    Vector3 movement;

    PlayerFootsteps playerFootsteps;
    CharacterController characterController;
 
    private void Awake()
    {
        playerFootsteps = GetComponent<PlayerFootsteps>();
        characterController = GetComponent<CharacterController>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void FixedUpdate()
    {
        
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        movement = transform.TransformDirection(movement);

        speed = (Input.GetKey(KeyCode.LeftShift) ? speed = 4f : speed = 2f);

        if (movement != Vector3.zero)
        {
            characterController.Move(movement  * speed * Time.fixedDeltaTime);


            if(!Input.GetKey(KeyCode.LeftShift)) {
                playerFootsteps.stepDistance = walkStepDistance;
                playerFootsteps.volumeMax = walkVolumeMax;
                playerFootsteps.volumeMin = walkVolumeMin;
            } else if(Input.GetKey(KeyCode.LeftShift))
            {
                playerFootsteps.stepDistance = sprintStepDistance;
                playerFootsteps.volumeMin = spritVolume;
                playerFootsteps.volumeMax = spritVolume;
            }
            



        }


        
    }

    public Vector3 GetMovement()
    { return movement; }
    // Update is called once per frame
    void Update()
    {
       

    }

   
}
