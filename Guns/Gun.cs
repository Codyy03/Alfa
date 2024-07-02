using Knife.RealBlood.SimpleController;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Burst.CompilerServices;
using UnityEngine;



public class Gun : MonoBehaviour
{
    [SerializeField] float fireRate = 15f;
    [SerializeField] int damage;
    [SerializeField] float maxDistanceShoot;
    [SerializeField] AudioClip fireSound,headExplode;
    [SerializeField] AudioClip reloadSound;
    [SerializeField] string[] animationsNames;
    [SerializeField] RectTransform pointer;
    [SerializeField] GameObject scope;
    [SerializeField] Camera scopeCam,fpCamera;
    public int maxAmmoInMagazine;
    public int currentAmmoInMagazine;
    public bool isShooting;
    public bool isReloading;
    public bool hasScope;
    float nextTimeToFire = 0;
    string currentAnimation;
    Camera cam;
    ShootingPoints shootingPoint;
    AudioManager audioManager;
    AnimatorController controller;
    PlayerController playerController;

    private void Awake()
    {
        
    }

    private void Update()
    {
        
        
    }
    public void SetValues() {
        playerController = Object.FindFirstObjectByType<PlayerController>();
        audioManager = FindAnyObjectByType<AudioManager>();
        cam = Camera.main;
        controller = GetComponent<AnimatorController>();
        shootingPoint = FindAnyObjectByType<ShootingPoints>();
    }

    public virtual void Reload(){
       

    }
    public virtual void DisplayCurrentWeaponAmmunition()
    {


    }
    public void PlayReloadAnimation()
    {     
        controller.ChangeAnimationState(animationsNames[3]);
    }
    public void PlayReloadSound()
    {
        audioManager.PlayClip(reloadSound);
    }
    public void ChangePointerSize(float size)
    {
        pointer.localScale = new Vector3(size, size, size);
    }
    public void StopShooting(string animation)
    {
        controller.ChangeAnimationState(animation);
        isShooting = false;
    }
    public void Shoot()
    {
        if (isReloading || isShooting)
            return;


        if (Time.time >= nextTimeToFire && playerController.GetSpeed() != 4f)
        {
            RaycastHit hit;
            isShooting = true;
            nextTimeToFire = Time.time + 1f / fireRate;
            playerController.SetColliderRadius(10.05697f);
            StartCoroutine(PlayerShootSound());
            audioManager.PlayClip(fireSound);

            currentAmmoInMagazine--;
            controller.ChangeAnimationState(currentAnimation);

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxDistanceShoot))
            {

                switch (hit.transform.tag)
                {
                    case "Enemy": shootingPoint.CreatePoint(new Vector3(hit.point.x, hit.point.y, hit.point.z), 0, hit); hit.transform.GetComponent<ZombieHealth>().ChangeHealth(-damage); break;
                    case "Metal": shootingPoint.CreatePoint(new Vector3(hit.point.x, hit.point.y, hit.point.z), 2, hit); break;
                    case "Ground": shootingPoint.CreatePoint(new Vector3(hit.point.x, hit.point.y, hit.point.z), 3, hit); break;
                }
                if (hit.collider.CompareTag("Head"))
                {
                    shootingPoint.CreatePoint(new Vector3(hit.point.x, hit.point.y, hit.point.z), 4, hit);
                    hit.collider.gameObject.SetActive(false);
                    hit.transform.parent.GetComponent<ZombieHealth>().IsDead();
                    audioManager.PlayClip(headExplode);
                }
            }
        }

    }

    public void Aim()
    {
        if (isReloading)
            return;

        if (Input.GetKey(KeyCode.Mouse1) && playerController.GetSpeed() != 4f)
        {
            if (!hasScope)
            {
                ChangePointerSize(1.5f);
                currentAnimation = animationsNames[2];
            }
            else {
                cam.gameObject.SetActive(false);
                scope.SetActive(true);
                scopeCam.gameObject.SetActive(true);
                fpCamera.gameObject.SetActive(false);
            } 

           
        }
        else
        {
            ChangePointerSize(1f);
            currentAnimation = animationsNames[0];

            if (hasScope)
            {
                cam.gameObject.SetActive(true);
                scope.SetActive(false);
                scopeCam.gameObject.SetActive(false);
                fpCamera.gameObject.SetActive(true);
            }
                
        }
    }

    IEnumerator PlayerShootSound()
    {
        yield return new WaitForSeconds(1f);
        if (playerController.GetIsCrouch())
            playerController.SetColliderRadius(3.38f);
        else playerController.SetColliderRadius(6.65f);

    }
  
    


}
