using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Image playerHealth;
    [SerializeField] GameObject lowHealthNotification;
    [SerializeField] float maxHealth;

    AnimatorController animatorController;

    bool isLowHP=false;
    float health;
    // Start is called before the first frame update
    private void Awake()
    {
        animatorController = lowHealthNotification.GetComponent<AnimatorController>();
    }
    void Start()
    {
        health = maxHealth;
      
    }

    // Update is called once per frame
    void Update()
    {
       
        if (health < 20)
        {
            isLowHP = true;
            lowHealthNotification.SetActive(true);
            animatorController.ChangeAnimationState("Low_HP");
            RegenerateHealthTo20(health);
        }
        if(isLowHP && health>=20)
        {
            animatorController.ChangeAnimationState("Low_HP reverse");
            isLowHP = false;
        }

    }

    public void ChangeHealth(float value)
    {
        health = Mathf.Clamp(health+value, 0, maxHealth);
        playerHealth.fillAmount = health / maxHealth;
    }    

    void RegenerateHealthTo20(float currentHealth)
    {
        float health = 0;
        
        health += Time.deltaTime*2;
      
         

        if (currentHealth <= 20)
           ChangeHealth(health);
       

    }
}
