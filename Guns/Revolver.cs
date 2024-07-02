using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Revolver : Gun
{
    
    Ammunition ammunition;
    private void Awake()
    {
        ammunition = FindFirstObjectByType<Ammunition>();
        SetValues();
        DisplayCurrentWeaponAmmunition();
    }
    private void OnEnable()
    {
        DisplayCurrentWeaponAmmunition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (currentAmmoInMagazine == 0 && ammunition.revolverAmmunition > 0)
            {
                PlayReloadAnimation();
                return;
            }
            if (ammunition.revolverAmmunition == 0 && currentAmmoInMagazine==0)
                return;

            Shoot();
            DisplayCurrentWeaponAmmunition();
        }

        Aim();

        if (currentAmmoInMagazine == maxAmmoInMagazine)
            return;
        if (Input.GetKeyDown(KeyCode.R) && !isShooting && !isReloading && currentAmmoInMagazine < maxAmmoInMagazine && ammunition.revolverAmmunition > 0)
        {
            PlayReloadAnimation();
        }
    }


    public override void Reload()
    {
        int ammo = maxAmmoInMagazine - currentAmmoInMagazine;

        if(ammo<=ammunition.revolverAmmunition)
        {
            ammunition.revolverAmmunition -= ammo;
            currentAmmoInMagazine += ammo;
        }
        else
        {
            currentAmmoInMagazine += ammunition.revolverAmmunition;
            ammunition.revolverAmmunition = 0;
        }
        isReloading = false;
        DisplayCurrentWeaponAmmunition();
    }
    public override void DisplayCurrentWeaponAmmunition()
    {
        ammunition.DisplayAmmunition(currentAmmoInMagazine, ammunition.revolverAmmunition);
    }
    public void StartReloading()
    {
        PlayReloadSound();
        ChangePointerSize(1f);
        isReloading = true; 
    }
 
}
