using UnityEngine;

namespace Assets.Scripts.Gems
{
    public class SpeedScript : MonoBehaviour {
    
        private GameObject player;
        private float previousPlayerSpeed;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == player)
            {
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                if (playerMovement.speed < 30 && playerMovement.speed > 0)
                {
                    Debug.Log("Setting previous speed");
                    previousPlayerSpeed = playerMovement.speed;
                }
                playerMovement.speed = 30;

                gameObject.SetActive(false);
                Invoke("SlowDownPlayer", 10);
                Destroy(gameObject, 10);

            
            } 
        }

        void SlowDownPlayer()
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.speed = previousPlayerSpeed;
            Debug.Log("Slowing down the player to " + previousPlayerSpeed);
        }


    }
}
