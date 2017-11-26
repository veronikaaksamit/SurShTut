using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    private UnityEngine.AI.NavMeshAgent nav;


    void Awake ()
    {
        
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        if (!player)
        {
            Debug.Log("Make sure your player is tagged!!");
        }
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


    void Update ()
    {
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination (player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
