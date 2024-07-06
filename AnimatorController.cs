using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public static bool isGettingWeapon;
    public string idleAnimation;
    public string walkAnimation;
    public string hitAnimation;
    public List<string> deadAnimations;
    public string[] AttackAnimation;

    string currentAnimation;

    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

   public Animator GetAnimator() { 
        return animator;
    
    }
    public void ChangeAnimationState(string animation)
    {
        if (currentAnimation == animation || string.IsNullOrEmpty(animation))
            return;

        animator.Play(animation);
        currentAnimation = animation;
    }

    void DisableGettingWeapon()
    { isGettingWeapon = false; }
    
}
