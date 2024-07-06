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
 
        public bool isUnlock,isMeele;
        public GameObject weapon;

        
        public void ActivateWeapon()
        {
            animatorController = weapon.GetComponent<AnimatorController>();
        }
        public void EneableWeapon()
        {
            weapon.SetActive(true);
        }
        public bool GetIsMeele()
        {
            return isMeele;
        
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
                if (!currentWeapon.isMeele)
                {
                    currentWeapon.weapon.GetComponent<Gun>().isReloading = false;
                    currentWeapon.weapon.GetComponent<Gun>().isShooting = false;
                }
                else if(currentWeapon.isMeele && currentWeapon.weapon.GetComponent<MeleWeapon>()!=null) 
                    currentWeapon.weapon.GetComponent<MeleWeapon>().isAttacking = false;

                currentWeapon.animatorController.ChangeAnimationState("Get");
                AnimatorController.isGettingWeapon = true;
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
        {
            weapon.weapon.SetActive(false);
            if (weapon.weapon.GetComponent<Lighter>() != null)
                weapon.weapon.GetComponent<Lighter>().DisableLighterLight();
        }
        
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
        if (currentWeapon.isMeele)
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
        if (currentWeapon.isMeele)
            return false;

        if (currentWeapon.weapon.GetComponent<Gun>().isReloading)
            return true;

        return false;

    }

    public bool IsAttacking()
    {
        if (currentWeapon.weapon.GetComponent<MeleWeapon>() == null)
        { 
            return false;
        
        }
        if (currentWeapon.GetIsMeele())
        {
           return currentWeapon.weapon.GetComponent<MeleWeapon>().isAttacking;
        
        }

        return false;
    
    }
  
  
}
