using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour
{
    public float health = 100;
    public int attackPower = 10;
    private Animation animation;
    public int speed=5;
    private NavMeshAgent agent;
    public GameObject player;
    public LayerMask groundLayer ;
    public LayerMask playerLayer;
    public Slider enemyHealthBar;

    public bool olumBool = false;
    
    //Patrolliing
    public Vector3 walkPoint;
    public float walkPointRange;
    public bool walkPointSet;

    // 
    public float sightRange,attackRange;
    public bool enemySightRange,enemyAttackRange;

    // Attack

    public float attackDelay;
    public bool isAttacking; 


    
    void Start()
    {
        animation = GetComponent<Animation>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        enemySightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        enemyAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if(!enemySightRange && !enemyAttackRange && olumBool == false)
        {
            Patroling();


        }
        else if(enemySightRange && !enemyAttackRange && olumBool == false)
        {
            DetectPlayer();

        }

        else if(enemySightRange && enemyAttackRange && olumBool == false)
        {
            AttackPlayer();
        }
    }

    public void Patroling()
    {
        animation.Play("Walk");
        if(walkPointSet == false && olumBool == false)
        {
            float randomZpos = Random.Range(-walkPointRange,walkPointRange);
            float randomXPos = Random.Range(-walkPointRange, walkPointRange);
            walkPoint = new Vector3(transform.position.x+randomXPos,0,transform.position.z+randomZpos);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer) && olumBool == false)
            {
                walkPointSet = true;
                
            }
           
        }
        if (walkPointSet == true && olumBool == false)
        {

            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f && olumBool == false)
        {
            walkPointSet = false;
        }

    }


    public void DetectPlayer()
    {
        if (olumBool == false)
        {
            animation.Play("Run");
            agent.SetDestination(player.transform.position);
            transform.LookAt(player.transform.position);
        }
    }

    public void AttackPlayer()
    {
       if(player.GetComponent<PlayerController>().health > 0) { 
        agent.SetDestination(player.transform.position);
        transform.LookAt(player.transform.position);

            if (isAttacking == false && olumBool == false)
            {
                animation.Play("Attack1");
                // Atak Türü
                isAttacking = true;
                Invoke("ResetAttack", attackDelay);
            }
        }

    }

    public void ResetAttack() {
        isAttacking = false;

    }


    public void EnemyDamage(float damage)
    {
        health -= damage;
        enemyHealthBar.value = health;
      

        if (health <= 0 && olumBool == false)
        {
            
            animation.Play("Death" , PlayMode.StopAll);
            olumBool = true;
            enemyHealthBar.value =0;
           
            
        }
    }


}
