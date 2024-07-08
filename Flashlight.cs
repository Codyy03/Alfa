using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] GameObject spotLight;
    [SerializeField] AudioClip flashlightSound;
    bool flashlightIsTurnOn = true;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !flashlightIsTurnOn)
        {
            spotLight.SetActive(true);
            flashlightIsTurnOn = true;
            audioManager.PlayClip(flashlightSound);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && flashlightIsTurnOn)
        {
            spotLight.SetActive(false);
            flashlightIsTurnOn = false;
            audioManager.PlayClip(flashlightSound);
        }

    }
}
