using UnityEngine;

namespace Assets.Scripts.Gems
{
    public class SleepScript:MonoBehaviour
    {
        public int sleepSeconds;
        
        private GameObject player;
        private GameObject[] enemies;
        private int counter;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void OnTriggerEnter(Collider other)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (other.gameObject == player)
            {
                gameObject.SetActive(false);
                Invoke("SleepingEnemies", 1);
                Destroy(gameObject, 3*sleepSeconds);
                counter = sleepSeconds;
            }
        }

        void SleepingEnemies()
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (counter > 0)
            {
                counter--;
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].gameObject.GetComponent<EnemyMovement>().IsMoving = false;
                    enemies[i].gameObject.GetComponent<Animator>().SetBool("IsMoving", false);
                }
                Invoke("SleepingEnemies", 1);
                Debug.Log("ZZZ");
            }
            else
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].gameObject.GetComponent<EnemyMovement>().IsMoving = true;
                    enemies[i].gameObject.GetComponent<Animator>().SetBool("IsMoving", true);
                }
                Debug.Log("Awake");
            }
        }
    }
}
