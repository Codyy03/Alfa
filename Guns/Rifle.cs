using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Rifle : Gun
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
            if (currentAmmoInMagazine == 0 && ammunition.rifleAmmunition > 0)
            {
                PlayReloadAnimation();
                return;
            }
            if (ammunition.rifleAmmunition == 0 && currentAmmoInMagazine == 0)
                return;

            Shoot();
            DisplayCurrentWeaponAmmunition();
        }

        Aim();

        if (currentAmmoInMagazine == maxAmmoInMagazine)
            return;
        if (Input.GetKeyDown(KeyCode.R) && !isShooting && !isReloading && currentAmmoInMagazine < maxAmmoInMagazine && ammunition.rifleAmmunition > 0)
        {
            PlayReloadAnimation();
        }
    }


    public override void Reload()
    {
        int ammo = maxAmmoInMagazine - currentAmmoInMagazine;

        if (ammo <= ammunition.rifleAmmunition)
        {
            ammunition.rifleAmmunition -= ammo;
            currentAmmoInMagazine += ammo;
        }
        else
        {
            currentAmmoInMagazine += ammunition.rifleAmmunition;
            ammunition.rifleAmmunition = 0;
        }
        isReloading = false;
        DisplayCurrentWeaponAmmunition();
    }
    public override void DisplayCurrentWeaponAmmunition()
    {
        ammunition.DisplayAmmunition(currentAmmoInMagazine, ammunition.rifleAmmunition);
    }
    public void StartReloading()
    {
        PlayReloadSound();
        ChangePointerSize(1f);
        isReloading = true;
    }
}
