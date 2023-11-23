using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class Ammunition : MonoBehaviour
{

    [Serializable]
    public class AmmunitionUI
    {
        public GameObject ammunitionSprite;
        public TextMeshProUGUI currentAmount,storage;


        public void SetCurrentAmmunitionAmount(int currentAmmo, int maxAmmo)
        {
            currentAmount.text = currentAmmo + "/" + maxAmmo;
        }
        public void SetStorageAmount(int storageAmmunition)
        {
            storage.text = storageAmmunition.ToString();
        }


    }
    public int maxHandgunAmmo, maxShotgunAmmo, handgunStorageAmmunition, shotgunStorageAmmunition;
    public AmmunitionUI ammunitionUIHandgun,ammunitionUIShotgun;

    [SerializeField] AnimatorController handgunAnimator,shotgunAnimator, handsAnimator;


   

    int shotgunAmmo, handgunAmmo;
  
    AudioManager audioManager;
    WeaponsManager weaponsManager;
    private void Awake()
    {
        ammunitionUIHandgun.SetStorageAmount(handgunStorageAmmunition);
        ammunitionUIShotgun.SetStorageAmount(shotgunStorageAmmunition);
        audioManager = FindAnyObjectByType<AudioManager>();
      
        weaponsManager = FindAnyObjectByType<WeaponsManager>();
        shotgunAmmo = maxShotgunAmmo;
        handgunAmmo = maxHandgunAmmo;
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HandgunShoot(int ammo)
    {
        if (weaponsManager.isReolading)
            return;
        if (handgunAmmo == 0)
        {
          
            weaponsManager.CurrentWeaponSlot().Reload(handsAnimator, audioManager,this);
            return;
        }
        handgunAmmo = Mathf.Clamp(handgunAmmo+ammo, 0, maxHandgunAmmo);
        ammunitionUIHandgun.SetCurrentAmmunitionAmount(handgunAmmo,maxHandgunAmmo);
         

     
    } 
    
    public void ShotgunShoot(int ammo)
    {
        if (weaponsManager.isReolading)
            return;

        if (shotgunAmmo == 0)
        {
     
            weaponsManager.CurrentWeaponSlot().Reload(handsAnimator, audioManager,this);
            return;

        }
          
        shotgunAmmo = Mathf.Clamp(shotgunAmmo + ammo, 0, maxShotgunAmmo);
        ammunitionUIShotgun.SetCurrentAmmunitionAmount(shotgunAmmo, maxShotgunAmmo);

    }
    public int ReturnCurrentAmmountToReload(int currentAmmo, int maxAmmo, int ammo)
    {
        int value = 0;
       if(currentAmmo==0 && ammo >= maxAmmo && currentAmmo!=maxAmmo)
            value = maxAmmo;
        

        if (ammo!=0 && currentAmmo != maxAmmo )
        {
            value = maxAmmo-currentAmmo;
            if(value>ammo)
                value = ammo;
        }
        

        return value;
    }
    public int GethandgunAmmo()
    { return handgunAmmo;  }

    public int GetshotgunAmmo()
    { return shotgunAmmo; }

    public void SetHandgunAmmo(int ammo)
    {

        handgunAmmo = Mathf.Clamp(handgunAmmo + ammo, 0, maxHandgunAmmo);
    }
    public void SetShotgunAmmo(int ammo)
    {

        shotgunAmmo = Mathf.Clamp(shotgunAmmo + ammo, 0, maxShotgunAmmo);
    }
}
