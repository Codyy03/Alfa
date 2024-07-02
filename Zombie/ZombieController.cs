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
    [SerializeField] float distanceToStopWalikngToPlayer;
    [SerializeField] AudioClip zombieIdle, zombieAttack;
    Vector3 relativePos;
    float distanceToPlayer;
    bool playerIsInZombieRange;
    NavMeshAgent agent;
    AnimatorController animatorController;
    AudioSource audioSource;
 
    // Start is called before the first frame upda
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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


        if (!playerIsInZombieRange)
        {
            animatorController.ChangeAnimationState("Z_Idle");
            if (audioSource.clip != zombieIdle)
            {
                audioSource.clip = zombieIdle;
                audioSource.Play();
            }
            return;
        
        }
        if (distanceToPlayer > 2f && distanceToPlayer < distanceToStopWalikngToPlayer && playerIsInZombieRange)
        {
            agent.SetDestination(player.transform.position);
            animatorController.ChangeAnimationState(animatorController.walkAnimation);
            if (audioSource.clip != zombieAttack)
            {
                audioSource.clip = zombieAttack;
                audioSource.Play();
            }
             
        }

        if (distanceToPlayer >= distanceToStopWalikngToPlayer)
        {
            playerIsInZombieRange = false;
        }
       // else audioSource.Pause();

        if (distanceToPlayer <= 2f)
            animatorController.ChangeAnimationState(animatorController.AttackAnimation[Random.Range(0,1)]);


        relativePos = player.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos);
        

        Quaternion current = transform.localRotation;
        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * rotationSpeed);


    }

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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerIsInZombieRange = true;
    }

    public void SetPlayerIsInZombieRange(bool playerIsInZombieRange)
    { 
        this.playerIsInZombieRange = playerIsInZombieRange;
    }

}
