using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    private PlayerController playerController;
    private EnemyController enemyController;
    
    void Start()
    {
       playerController = FindObjectOfType<PlayerController>();
        enemyController = transform.root.GetComponent<EnemyController>();
       
    }

    // Update is called once per frame
   
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.tag == "Player" && enemyController.health>0 && enemyController.isAttacking == true)
        {
            
            playerController.PlayerDamage(5);
        }
      
    }
}
