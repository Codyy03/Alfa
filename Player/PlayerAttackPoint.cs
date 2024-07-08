using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackPoint : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] AudioClip enemyHitSound;
    public LayerMask enemyLayer;
    public float radius = 1f;

    ShootingPoints points;
    AudioManager audioManager;
    MeleWeapon meleWeapon;
    SetFire setFire;
    private void Awake()
    {
        meleWeapon = GetComponentInParent<MeleWeapon>();
        audioManager = FindAnyObjectByType<AudioManager>();
        points = FindAnyObjectByType<ShootingPoints>();
        setFire = GetComponent<SetFire>();
    }
    void Update()
    {
        AddDamage();
    }
    void AddDamage()
    {
        // jezeli w tablicy colliderow znajduje sie zombie zadaj obrazenia
        Collider[] hit = Physics.OverlapSphere(transform.position, radius, enemyLayer);
        ZombieHealth zombieHealth;
        if (hit.Length > 0)
        {
            zombieHealth = hit[0].GetComponent<ZombieHealth>();
            audioManager.PlayClip(enemyHitSound);
            points.CreateBloodPoint(transform.position);
            zombieHealth.ChangeHealth(-damage,false);

            // jezeli obiekt ma mozliowsc podpalenia zombie, sprawdz czy zombie moze zostac podpalony
            if(setFire!=null && setFire.CanBurn())
            {
                setFire.CreateFire(hit[0].transform,new Vector3(0, 0.987f,0), hit[0].GetComponent<AudioSource>());
                zombieHealth.AddFireDamage();

            }
            meleWeapon.DisableDamage();
        }
    }

}
