using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    public ParticleSystem attackParticle;
    private PlayerController playerController;



    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            if(Input.GetMouseButtonDown(0)) { 
            collision.GetComponent<EnemyController>().EnemyDamage(25);
            ParticleSystem particle =  Instantiate(attackParticle, collision.transform);
            Destroy(particle,0.5f);

                if (playerController.playerUltiSlider.value<=100) 
                {
                    playerController.playerUltiSlider.value += 6;

                }

            }
        }

    }
}
