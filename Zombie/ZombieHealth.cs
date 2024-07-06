using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] BoxCollider headCollider;
    [SerializeField] AudioClip bodyHit;
    public bool isOnFire;


    int health;
    AnimatorController animatorController;
    ZombieController zombieController;
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        zombieController = GetComponent<ZombieController>();
        animatorController = GetComponent<AnimatorController>();    
        health = maxHealth;
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
            audioSource.PlayOneShot(bodyHit);

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

    public void AddFireDamage()
    {
        if (isOnFire)
            return;

            isOnFire = true;
            StartCoroutine(AddFireDamageEveryOneSecond());
    }

    IEnumerator AddFireDamageEveryOneSecond()
    {
        if (health == 0 )
            yield break;

        for (int i = 0; i < 5; i++)
        {
            if (health == 0)
            {
                Destroy(GetComponentInChildren<ParticleSystem>().gameObject);
                isOnFire = false;
                yield break;
            }
           ChangeHealth(-10, false); // obrazenia od ognia
           
            yield return new WaitForSeconds(1f);
        }
        isOnFire = false;
    }



}
