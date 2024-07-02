using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWeapon : MonoBehaviour
{
     [SerializeField] float distanceToEneableTakeWeapon;
     [SerializeField] int weaponSlot;
     [SerializeField] GameObject notificationCanvas;

     Transform player;
     WeaponsManager weaponsManager;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        weaponsManager = FindAnyObjectByType<WeaponsManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= distanceToEneableTakeWeapon)
        {
            notificationCanvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                weaponsManager.weaponsSlots[weaponSlot].isUnlock = true;
                weaponsManager.DisableWeapons();
              //  weaponsManager.weaponsSlots[weaponSlot].ActivateWeapon(weaponsManager.GetComponent<AnimatorController>());
                weaponsManager.SetCurrentWeapon(weaponsManager.weaponsSlots[weaponSlot]);
                Destroy(gameObject);
            }
        }
        else notificationCanvas.SetActive(false);
    }

  
}
