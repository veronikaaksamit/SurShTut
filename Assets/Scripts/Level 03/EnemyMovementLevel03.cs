using System;
using UnityEngine;
using System.Collections;

public class EnemyMovementLevel03 : MonoBehaviour
{
    private Transform player;
    PlayerHealth playerHealth;
    PlayerGhostForm playerGhostForm;
    EnemyHealth enemyHealth;
    private Animator anim;
    private UnityEngine.AI.NavMeshAgent nav;
    public Boolean IsMoving = true;
    public bool isStunned = false;


    void Awake ()
    {
        
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        if (!player)
        {
            Debug.Log("Make sure your player is tagged!!");
        }
        playerHealth = player.GetComponent <PlayerHealth> ();
        playerGhostForm = player.GetComponent<PlayerGhostForm>();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        anim = GetComponent<Animator>();
    }


    void Update ()
    {
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0 && IsMoving && !playerGhostForm.isActive && !isStunned)
        {
            if (!nav.isActiveAndEnabled)
            {
                nav.enabled = true;
            }
            nav.SetDestination (player.position);
            anim.SetBool("IsStunned", false);
            //Debug.Log("Is moving");
        }
        else if (playerGhostForm.isActive || isStunned)
        {
            anim.SetBool("IsStunned", true);
            gameObject.isStatic = true;
            nav.enabled = false;
        }
        else
        {
            anim.SetBool("IsStunned", false);
            //Debug.Log("Is NOT moving");
            gameObject.isStatic = true;
            nav.enabled = false;
        }
    }
}
