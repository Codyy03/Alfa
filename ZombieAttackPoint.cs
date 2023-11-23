using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackPoint : MonoBehaviour
{
    [SerializeField] int damage = 1;

    public LayerMask playerLayer;
    public float radius=1f;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AddDamage();
    }
    void AddDamage()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, radius, playerLayer);
        if(hit.Length>0)
        {
           
            hit[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);

            gameObject.SetActive(false);
        }
    }

   
}
