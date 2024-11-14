using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiAttack : MonoBehaviour
{
    private PlayerController playerController;


    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy") && Input.GetMouseButton(1) && other.GetComponent<EnemyController>().olumBool==false && playerController.playerUltiSlider.value>=100 ) 
        {
            other.GetComponent<Animation>().Play("Death", PlayMode.StopAll);
            other.GetComponent<EnemyController>().health = 0;
            other.GetComponent<EnemyController>().olumBool = true;

        }

    }
}
