using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] AudioClip[] footsteps;

    [HideInInspector] public float volumeMin, volumeMax;
    [HideInInspector] public float stepDistance;

   
    float accumulatedDistance;

    AudioSource audioSource;
    CharacterController characterController;
    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
       
    }

    // Update is called once per
    void Update()
    {
        CheckToPlayFootstepSound();

    }

    void CheckToPlayFootstepSound()
    {
        if (playerController.GetMovement()!=Vector3.zero)
        {

            accumulatedDistance += Time.deltaTime;
            if (accumulatedDistance > stepDistance)
            {
                audioSource.volume = Random.Range(volumeMin, volumeMax);
                audioSource.clip = footsteps[Random.Range(0, footsteps.Length)];
                audioSource.Play();
                accumulatedDistance = 0f;
            }
        }
        else accumulatedDistance = 0f;
    }
}
