using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] BoxCollider headCollider;
    int health;

    AnimatorController animatorController;
    private void Awake()
    {
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

    public void ChangeHealth(int value)
    {
      
        health = Mathf.Clamp(health+value, 0 ,maxHealth);

        

        if (health == 0)
            IsDead();
        
    }

    public void IsDead()
    {
    
            
        headCollider.enabled = false;
        GetComponent<ZombieController>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        
        animatorController.ChangeAnimationState(animatorController.deadAnimations[Random.Range(0, animatorController.deadAnimations.Count)]);
        GetComponent<AudioSource>().enabled = false;

       
    }

    public void RemoveCollider()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;

        
    }
}
