using UnityEngine;

namespace Assets.Scripts.Gems
{
    public class SleepScript:MonoBehaviour
    {
        private GameObject player;
        private GameObject[] enemies;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == player)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].gameObject.GetComponent<EnemyMovement>().IsMoving = false;
                }
            }

            Destroy(gameObject);
            
        }
    }
}
