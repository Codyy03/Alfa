using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponsManager : MonoBehaviour
{

    [Serializable]
    public class WeaponsSlot 
    {
        public string[] animations;
        public GameObject weapon;
        public bool shootingWeapon;
        public AudioClip attackSound;
        public AudioClip reload;

        public void ActivateWeapon(AnimatorController animatorController)
        {
            weapon.SetActive(true);
          
            animatorController.ChangeAnimationState(animations[0]);
        }

        public void PlayAttack(AnimatorController animatorController, AudioManager audioManager,Ammunition ammunition)
        {          
          
               animatorController.ChangeAnimationState(animations[UnityEngine.Random.Range(1,3)]);
               
                audioManager.PlayClip(attackSound);

        }
        public void Reload(AnimatorController animatorController, AudioManager audioManager, Ammunition ammunition)
        {
            
            if ((weapon.gameObject.name == "handgun" && ammunition.GethandgunAmmo() != ammunition.maxHandgunAmmo && ammunition.handgunStorageAmmunition!=0) || (weapon.gameObject.name == "shotgun" && ammunition.GetshotgunAmmo() != ammunition.maxShotgunAmmo && ammunition.shotgunStorageAmmunition!=0))
            {
                animatorController.ChangeAnimationState(animations[2]);

                audioManager.PlayClip(reload);
            }
               
        }
    }
    [SerializeField] GameObject attackPoint;
    public bool isReolading = false;
    public List<WeaponsSlot> weaponsSlots;

    AnimatorController animatorController;
    AudioManager audioManager;
    Ammunition ammunition;
    KeyCode[] weaponsNumbersp = new KeyCode[]
     {
           KeyCode.Alpha1,
           KeyCode.Alpha2,
           KeyCode.Alpha3,
           KeyCode.Alpha4,
           KeyCode.Alpha5,
           KeyCode.Alpha6,
     };


    WeaponsSlot currentWeapon;
    
    private void Awake()
    {
        ammunition = FindAnyObjectByType<Ammunition>();
        animatorController = GetComponent<AnimatorController>();
        audioManager = FindAnyObjectByType<AudioManager>();
        currentWeapon = weaponsSlots[0];
    }
   

    // Update is called once per frame
    void Update()
    {
        if (isReolading)
            return;

        for (int i = 0; i < weaponsSlots.Count; i++)
        {
            if (Input.GetKeyDown(weaponsNumbersp[i]))
            {
                DisableWeapons();
                weaponsSlots[i].ActivateWeapon(animatorController);
                currentWeapon = weaponsSlots[i];
            }


        }

        switch(currentWeapon.weapon.name)
        {
            case "handgun": ammunition.ammunitionUIHandgun.SetCurrentAmmunitionAmount(ammunition.GethandgunAmmo(), ammunition.maxHandgunAmmo); ammunition.ammunitionUIHandgun.ammunitionSprite.SetActive(true); ammunition.ammunitionUIShotgun.ammunitionSprite.SetActive(false); break;
            case "shotgun": ammunition.ammunitionUIShotgun.SetCurrentAmmunitionAmount(ammunition.GetshotgunAmmo(), ammunition.maxShotgunAmmo); ammunition.ammunitionUIHandgun.ammunitionSprite.SetActive(false); ammunition.ammunitionUIShotgun.ammunitionSprite.SetActive(true); break;
        }
        if (Input.GetKeyDown(KeyCode.R) && currentWeapon.shootingWeapon)
            currentWeapon.Reload(animatorController, audioManager,ammunition);

        if (Input.GetKeyDown(KeyCode.Mouse0) && !currentWeapon.shootingWeapon)
            currentWeapon.PlayAttack(animatorController, audioManager,ammunition);
        

       
    }

    void DisableWeapons()
    {
        foreach(WeaponsSlot weapon in weaponsSlots)  
            weapon.weapon.SetActive(false);


        ammunition.ammunitionUIHandgun.ammunitionSprite.SetActive(false);
        ammunition.ammunitionUIShotgun.ammunitionSprite.SetActive(false);
    }
    public WeaponsSlot CurrentWeaponSlot()
    { return currentWeapon; }


    public void StartReolading()
    {
        isReolading = true;
    }
    public void StopReloadingHandgun()
    {
        isReolading = false;

        int ammunitionToReload = ammunition.ReturnCurrentAmmountToReload(ammunition.GethandgunAmmo(), ammunition.maxHandgunAmmo, ammunition.handgunStorageAmmunition);
        ammunition.SetHandgunAmmo(ammunitionToReload);

        ammunition.handgunStorageAmmunition -= ammunitionToReload;
        ammunition.ammunitionUIHandgun.SetCurrentAmmunitionAmount(ammunition.GethandgunAmmo(),ammunition.maxHandgunAmmo);
        ammunition.ammunitionUIHandgun.SetStorageAmount(ammunition.handgunStorageAmmunition);
    } 
    public void StopReloadingShootgun()
    {
        isReolading = false;

        int ammunitionToReload = ammunition.ReturnCurrentAmmountToReload(ammunition.GetshotgunAmmo(), ammunition.maxShotgunAmmo, ammunition.shotgunStorageAmmunition);
        //   Debug.Log(ammunition.ReturnCurrentAmmountToReload(ammunition.GetshotgunAmmo(), ammunition.maxShotgunAmmo, ammunition.shotgunStorageAmmunition));

        ammunition.SetShotgunAmmo(ammunitionToReload);
      
        ammunition.shotgunStorageAmmunition -= ammunitionToReload;

        ammunition.ammunitionUIShotgun.SetCurrentAmmunitionAmount(ammunition.GetshotgunAmmo(), ammunition.maxShotgunAmmo);
        ammunition.ammunitionUIShotgun.SetStorageAmount(ammunition.shotgunStorageAmmunition);
   
    }
    public void EnableAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    public void DisableAttackPoint()
    {
        attackPoint.SetActive(false);
    }
}
