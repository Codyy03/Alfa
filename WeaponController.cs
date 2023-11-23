using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    
    public enum GunType
    {
        handgun,
        shotgun
    }
   
    [SerializeField] float fireRate = 15f;
    [SerializeField] int damage;
    [SerializeField] float maxDistanceShoot;
    [SerializeField] string[] animationsNames;
    [SerializeField] GunType gunType;
    [SerializeField] AudioClip fireSound;

    float nextTimeToFire = 0;
    Camera cam;
    ShootingPoints shootingPoint;
    Ammunition ammunition;
    AnimatorController controller,playerAnimatorController;
    WeaponsManager weaponsManager;
    AudioManager audioManager;
    private void Awake()
    {
        playerAnimatorController = GameObject.FindGameObjectWithTag("Player").GetComponent<AnimatorController>();
        weaponsManager = FindAnyObjectByType<WeaponsManager>();
        cam = Camera.main;
        shootingPoint = FindAnyObjectByType<ShootingPoints>();
        ammunition = FindAnyObjectByType<Ammunition>();
        controller = GetComponent<AnimatorController>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (weaponsManager.isReolading)
            return;

        RaycastHit hit;
   
       
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time>=nextTimeToFire)
        {

            //|| 
            if (weaponsManager.CurrentWeaponSlot().weapon.name=="shotgun" && ammunition.GetshotgunAmmo() == 0)
            {
                ammunition.ShotgunShoot(-1);
                return;
            }
                

            if (weaponsManager.CurrentWeaponSlot().weapon.name == "handgun" && ammunition.GethandgunAmmo() == 0)
            {
               ammunition.HandgunShoot(-1);
                return;
            }
              


            nextTimeToFire = Time.time + 1f / fireRate;
         
            
            if (gunType == GunType.handgun)
            {
                playerAnimatorController.ChangeAnimationState("Handgun Shoot");

                ammunition.HandgunShoot(-1);
            }
           
            if (gunType == GunType.shotgun)
            {
                playerAnimatorController.ChangeAnimationState("Shotgun_Shoot");
                ammunition.ShotgunShoot(-1);
            }

            audioManager.PlayClip(fireSound);


            controller.ChangeAnimationState(animationsNames[0]);
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxDistanceShoot))
            {

              

                switch (hit.transform.tag)
                {
                    case "Enemy": shootingPoint.CreatePoint(new Vector3(hit.point.x, hit.point.y, hit.point.z), 0 ,hit); hit.transform.GetComponent<ZombieHealth>().ChangeHealth(-damage); break;
                    case "Metal": shootingPoint.CreatePoint(new Vector3(hit.point.x, hit.point.y, hit.point.z), 2, hit); break;
                    case "Ground": shootingPoint.CreatePoint(new Vector3(hit.point.x, hit.point.y, hit.point.z), 3, hit); break;


                }

               
                
                if (hit.collider.CompareTag("Head"))
                {
                    shootingPoint.CreatePoint(new Vector3(hit.point.x, hit.point.y, hit.point.z), 4, hit);
                    hit.collider.gameObject.SetActive(false);
                    hit.transform.parent.GetComponent<ZombieHealth>().IsDead();
                }
            
                   

            }
        }
        if ((weaponsManager.CurrentWeaponSlot().weapon.name == "handgun" && ammunition.GethandgunAmmo() == ammunition.maxHandgunAmmo || ammunition.handgunStorageAmmunition==0) || (weaponsManager.CurrentWeaponSlot().weapon.name == "shotgun" && ammunition.GetshotgunAmmo() == ammunition.maxShotgunAmmo || ammunition.shotgunStorageAmmunition == 0))
            return;

            if (Input.GetKeyDown(KeyCode.R) && !weaponsManager.isReolading)
            controller.ChangeAnimationState(animationsNames[1]);
    }

   
}
