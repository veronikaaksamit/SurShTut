using System;
using UnityEngine;

namespace Assets.Scripts.Gems
{
    public class SpeedScript : MonoBehaviour {
    
        private GameObject _player;
        private float _previousPlayerSpeed = 10;
        private int slowerSpeed = 10;

        void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _player)
            {
                PlayerMovement playerMovement = _player.GetComponent<PlayerMovement>();
                if (playerMovement.speed < 30 && Math.Abs(playerMovement.speed - slowerSpeed) > 0)
                {
                    _previousPlayerSpeed = playerMovement.speed;
                    //Debug.Log("Setting previous speed to" + previousPlayerSpeed);
                }
                playerMovement.speed = 30;

                gameObject.SetActive(false);
                Invoke("SlowDownPlayer", slowerSpeed);
                Destroy(gameObject, 10);

            
            } 
        }

        void SlowDownPlayer()
        {
            PlayerMovement playerMovement = _player.GetComponent<PlayerMovement>();
            playerMovement.speed = _previousPlayerSpeed;
            //Debug.Log("Slowing down the player to " + previousPlayerSpeed);
        }


    }
}
