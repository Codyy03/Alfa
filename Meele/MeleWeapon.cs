using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleWeapon : MonoBehaviour
{
    [SerializeField] GameObject attackPoint;
    [SerializeField] string[] attacksAnimations;
    [SerializeField] AudioClip weaponSound;
    public bool isAttacking;
    AudioManager audioManager;
    AnimatorController animatorController;

    private void Awake()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        animatorController = GetComponent<AnimatorController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animatorController.ChangeAnimationState(attacksAnimations[Random.Range(0, attacksAnimations.Length)]);
                audioManager.PlayClip(weaponSound);
            }
        }
    }


    void IsAttacking()
    {
        isAttacking = true;
    }
    void IsNotAttacking()
    {
        isAttacking = false;
    }
    public void DisableDamage()
    {
        attackPoint.SetActive(false);
    }
    void EnableDamage()
    {
        attackPoint.SetActive(true);
    }
}
