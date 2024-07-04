using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] BoxCollider headCollider;
    [SerializeField] AudioClip bodyHit;
    int health;

    AnimatorController animatorController;
    ZombieController zombieController;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        zombieController = GetComponent<ZombieController>();
        animatorController = GetComponent<AnimatorController>();    
        health = maxHealth;
    }
    void Start()
    {
        
    }

    // Update is called once per fr
    void Update()
    {
        
    }

    public void ChangeHealth(int value, bool gunDamage)
    {
      
        health = Mathf.Clamp(health+value, 0 ,maxHealth);
        zombieController.SetPlayerIsInZombieRange(true);

        if(gunDamage)
        audioManager.PlayClip(bodyHit);

        if (health == 0)
            IsDead();
        
    }

    public void IsDead()
    {
    
            
        headCollider.enabled = false;
        GetComponent<ZombieController>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        gameObject.layer = 0;
        gameObject.tag = "Untagged";
        
        animatorController.ChangeAnimationState(animatorController.deadAnimations[Random.Range(0, animatorController.deadAnimations.Count)]);
        GetComponent<AudioSource>().enabled = false;
        GetComponent<DestoryEffect>().enabled = true;

       
    }

    public void RemoveCollider()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;

        
    }
}
