using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Managers
{
    public class LevelManager:MonoBehaviour
    {

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
                SceneManager.LoadScene("Level 01", LoadSceneMode.Single);

            if (Input.GetKeyDown(KeyCode.F2))
                SceneManager.LoadScene("Level 02", LoadSceneMode.Single);

            if (Input.GetKeyDown(KeyCode.F3))
                SceneManager.LoadScene("Level 03", LoadSceneMode.Single);

            if (Input.GetKeyDown(KeyCode.F4))
                SceneManager.LoadScene("Level 04", LoadSceneMode.Single);
        }
        
    }
}
