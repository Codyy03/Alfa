using Knife.RealBlood.SimpleController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsManager : MonoBehaviour
{

    [Serializable]
    public class WeaponsSlot 
    {
        public AnimatorController animatorController;
 
        public bool isUnlock;
        public GameObject weapon;

        
        public void ActivateWeapon()
        {
            animatorController = weapon.GetComponent<AnimatorController>();
        }
        public void EneableWeapon()
        {
            weapon.SetActive(true);
        }

        public bool IsMeele()
        {
            if (weapon.GetComponent<Gun>() == null)
                return true;

            return false;
        
        }
       
    }

    
    public List<WeaponsSlot> weaponsSlots;

   

    KeyCode[] weaponsNumbersp = new KeyCode[]
     {
           KeyCode.Alpha1,
           KeyCode.Alpha2,
           KeyCode.Alpha3,
           KeyCode.Alpha4,
           KeyCode.Alpha5,
           KeyCode.Alpha6,
     };


    Ammunition ammunition;
    WeaponsSlot currentWeapon;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        ammunition = FindFirstObjectByType<Ammunition>();
        currentWeapon = weaponsSlots[0];
        for (int i = 0; i < weaponsSlots.Count; i++)
            weaponsSlots[i].ActivateWeapon();
    }
   

    // Update is called once per frame
    void Update()
    {
       

        for (int i = 0; i < weaponsSlots.Count; i++)
        {
            if (Input.GetKeyDown(weaponsNumbersp[i]) && weaponsSlots[i].isUnlock)
            {
                audioManager.StopSound();
                currentWeapon.animatorController.GetAnimator().Play("Idle", -1, 0f); // Zak³adam, ¿e "Idle" to twoja domyœlna animacja
                currentWeapon.animatorController.GetAnimator().Update(0f);
                if (!currentWeapon.IsMeele())
                { 
                    currentWeapon.weapon.GetComponent<Gun>().isReloading = false;
                    currentWeapon.weapon.GetComponent<Gun>().isShooting = false;
                }
               
                DisableWeapons();
                currentWeapon = weaponsSlots[i];
                currentWeapon.EneableWeapon();
               
                return;
            }
     

        }
    }

    public void DisableWeapons()
    {
        foreach(WeaponsSlot weapon in weaponsSlots)  
            weapon.weapon.SetActive(false);
        
    }
    public void PlayAnimation(string animation)
    {
        if(!IsShooting())
        currentWeapon.animatorController.ChangeAnimationState(animation);

    }
   
    public WeaponsSlot CurrentWeaponSlot()
    { return currentWeapon; }

    public void SetCurrentWeapon(WeaponsSlot currentSlot)
    {
       
        currentWeapon = currentSlot;
    }
   
    public bool IsShooting()
    {
        if (currentWeapon.IsMeele())
            return false;


        if (currentWeapon.weapon.GetComponent<Gun>().isShooting)
            return true;

            return false;

    }
    public void SetIsShooting(bool isShooting)
    {
        currentWeapon.weapon.GetComponent<Gun>().isShooting = isShooting;
    }
    public bool IsReloading()
    {
        if (currentWeapon.IsMeele())
            return false;

        if (currentWeapon.weapon.GetComponent<Gun>().isReloading)
            return true;

        return false;

    }
    //public void StopReloadingHandgun()
    //{
    //    isReolading = false;

    //    int ammunitionToReload = ammunition.ReturnCurrentAmmountToReload(ammunition.GethandgunAmmo(), ammunition.maxHandgunAmmo, ammunition.handgunStorageAmmunition);
    //    ammunition.SetHandgunAmmo(ammunitionToReload);

    //    ammunition.handgunStorageAmmunition -= ammunitionToReload;
    //    ammunition.ammunitionUIHandgun.SetCurrentAmmunitionAmount(ammunition.GethandgunAmmo(),ammunition.maxHandgunAmmo);
    //    ammunition.ammunitionUIHandgun.SetStorageAmount(ammunition.handgunStorageAmmunition);
    //} 
  
  
}
