using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShootingPoints : MonoBehaviour
{
    [SerializeField] GameObject storageAmmo;
    [SerializeField] List<GameObject> bloodPoints;
    [SerializeField] List<GameObject> bulletPoints;
    [SerializeField] GameObject headExplode;
    [SerializeField] GameObject metalEffect, groundEffect;
    
    public void CreatePoint(Vector3 postion, int variant,RaycastHit target)
    {
        
        switch (variant)
        {
            case 0: Instantiate(bloodPoints[Random.Range(0, bloodPoints.Count)], postion, Quaternion.LookRotation(target.normal)); break;
            case 1: Instantiate(bulletPoints[Random.Range(0, bulletPoints.Count)], postion, Quaternion.LookRotation(target.normal)); break;
            case 2: Instantiate(metalEffect, postion, Quaternion.LookRotation(target.normal)); break;
            case 3: Instantiate(groundEffect, postion, Quaternion.LookRotation(target.normal)); break;
            case 4: Instantiate(headExplode, postion, Quaternion.LookRotation(target.normal)); break;

        }

    }
    public void CreateBloodPoint(Vector3 postion)
    {
         Instantiate(bloodPoints[Random.Range(0, bloodPoints.Count)], postion, transform.rotation); 
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Tab))
            storageAmmo.SetActive(true);
        else if(storageAmmo.activeInHierarchy && Input.GetKeyUp(KeyCode.Tab))
            storageAmmo.SetActive(false);
    }
}
