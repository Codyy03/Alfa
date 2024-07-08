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
    // ustawia wartosci tak¿dej broni na starcie
    public void SetValues() {
        playerController = Object.FindFirstObjectByType<PlayerController>();
        audioManager = FindAnyObjectByType<AudioManager>();
        cam = Camera.main;
        controller = GetComponent<AnimatorController>();
        shootingPoint = FindAnyObjectByType<ShootingPoints>();
    }

    // funckja odpowiadaj¹ca za prze³adowanie
    public virtual void Reload(){
        isShooting = false;

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
        // jezeli gracz prze³adowuje lub strzela nie pozwól strzeliæ ponownie
        if (isReloading || isShooting)
            return;
        
        // sprawdza mozliwosc oddania strzalu
        if (Time.time >= nextTimeToFire && playerController.GetSpeed() != 4f)
        {
            RaycastHit hit;
            isShooting = true;
            nextTimeToFire = Time.time + 1f / fireRate;
            playerController.SetColliderRadius(25f);
            StartCoroutine(PlayerShootSound());
            audioManager.PlayClip(fireSound);

            currentAmmoInMagazine--;
            controller.ChangeAnimationState(currentAnimation);

            // jezeli pocisk trafi³ w jakiœ obiekt
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxDistanceShoot))
            {
                // swtórz poprawny efekt strza³u w zale¿noœci od powierzchni w która trafi³ pocisk
                switch (hit.transform.tag)
                {
                    case "Enemy": shootingPoint.CreatePoint(new Vector3(hit.point.x, hit.point.y, hit.point.z), 0, hit); hit.transform.GetComponent<ZombieHealth>().ChangeHealth(-damage,true); break;
                    case "Metal": shootingPoint.CreatePoint(new Vector3(hit.point.x, hit.point.y, hit.point.z), 2, hit); break;
                    case "Ground": shootingPoint.CreatePoint(new Vector3(hit.point.x, hit.point.y, hit.point.z), 3, hit); break;
                }
                // po trafieniu w g³owe przeciwnika: zabij go i zniszcz g³owe
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
        // jezeli prze³adowuje nie pozwól celowaæ
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

    // po up³ywie jednej sekudy zmniejsz obszar w którym zombie mo¿e zobaczyæ gracza
    IEnumerator PlayerShootSound()
    {
        yield return new WaitForSeconds(1f);
        if (playerController.GetIsCrouch())
            playerController.SetColliderRadius(3.38f);
        else playerController.SetColliderRadius(6.65f);

    }
  
    


}
