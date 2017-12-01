using UnityEngine;
using UnityEngine.UI;

public class HeartScript : MonoBehaviour {
    

    private GameObject player;
    private GameObject healthSlider;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthSlider = GameObject.FindGameObjectWithTag("HealthSlider");
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth.currentHealth <= 90)
            {
                playerHealth.currentHealth = 100;
                healthSlider.GetComponent<Slider>().value = playerHealth.currentHealth;
            }
        } 

        Destroy(gameObject);
    }
    
}
