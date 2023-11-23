using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class ZombieController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject attackPoint;
    [SerializeField] float speed,rotationSpeed;

    Vector3 relativePos;
    float distanceToPlayer;
    NavMeshAgent agent;
    AnimatorController animatorController;
    AudioSource audioSource;
 
    // Start is called before the first frame upda
    private void Awake()
    {
        audioSource = FindAnyObjectByType<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        animatorController = GetComponent<AnimatorController>();
    


    }
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (!agent.enabled)
            return;

        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > 2f)
        {
            agent.SetDestination(player.transform.position);
            animatorController.ChangeAnimationState(animatorController.walkAnimation);

       

        }
        else audioSource.Pause();

        if (distanceToPlayer <= 2f)
            animatorController.ChangeAnimationState(animatorController.AttackAnimation[Random.Range(0,1)]);


        relativePos = player.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos);
        

        Quaternion current = transform.localRotation;
        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * rotationSpeed);


    }
    // Update is called once per f
    void Update()
    {
       

    }
    void DisableDamage()
    {
        attackPoint.SetActive(false);
    }
    void EnableDamage()
    {
        attackPoint.SetActive(true);
    }





}
