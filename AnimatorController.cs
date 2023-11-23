using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
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
    void Start()
    {
        
    }

    // Update is called once per f
    void Update()
    {

    }

    public void ChangeAnimationState(string animation)
    {
        if (currentAnimation == animation || animation=="")
            return;

        animator.Play(animation);
        currentAnimation = animation;
    }

   
}
