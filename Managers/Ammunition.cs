using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class Ammunition : MonoBehaviour
{
    public int revolverAmmunition;
    public int rifleAmmunition;
    public TextMeshProUGUI currentAmmoText,allAmmoText;


    public void DisplayAmmunition(int currentAmmo,int weaponAmmunition)
    {
        allAmmoText.text = weaponAmmunition.ToString();
        currentAmmoText.text = currentAmmo.ToString();
    }
   


}
