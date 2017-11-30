using System.Collections.Generic;
using UnityEngine;

public class SpeedScript : MonoBehaviour {
    

    private GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.speed = playerMovement.speed + 10;
        } 

        Destroy(gameObject);

        //new WaitForSeconds(20);
    }

    /*IEnumerator<> waiter()
    {
    }*/

}
