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

    private void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
        points = FindAnyObjectByType<ShootingPoints>();
    }
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
        Collider[] hit = Physics.OverlapSphere(transform.position, radius, enemyLayer);
        if (hit.Length > 0)
        {
            audioManager.PlayClip(enemyHitSound);
            points.CreateBloodPoint(transform.position);
            hit[0].GetComponent<ZombieHealth>().ChangeHealth(-damage);

            gameObject.SetActive(false);
        }
    }

}
