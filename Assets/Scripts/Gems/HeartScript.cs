using UnityEngine;

public class HeartScript : MonoBehaviour {
    

    private GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth.currentHealth <= 90)
            {
                playerHealth.currentHealth += 30;
            }
        } 

        Destroy(gameObject);
    }
    
}
