using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerGhostForm : MonoBehaviour
{
    public Slider GhostSlider;
    public GameObject PlayerCharacter;

    public bool isActive;

    public float chargeTime = 30f;
    public float activeTime = 5f;
    public float stunDuration = 10f;

    private CapsuleCollider capsuleCollider;

    private float drainGhost;
    private bool isReady;
    private GameObject character;
    private PlayerShooting shooting;


    void Awake ()
    {
        isActive = false;
        isReady = true;
        drainGhost = GhostSlider.maxValue / activeTime;
        capsuleCollider = GetComponent<CapsuleCollider>();
        shooting = GetComponentInChildren<PlayerShooting>();
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            GhostSlider.value -= Time.deltaTime * drainGhost;
        }
        else
        {
            GhostSlider.value += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isReady)
        {
            character = Instantiate(PlayerCharacter, transform.position, transform.rotation);
            isReady = false;
            isActive = true;
            capsuleCollider.isTrigger = true;
            shooting.enabled = false;
            Invoke("Deactivate", activeTime);
            Invoke("Ready", chargeTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && isActive)
        {
            other.GetComponent<EnemyMovementLevel03>().isStunned = true;
        }
    }

    private void Deactivate()
    {
        shooting.enabled = true;
        isActive = false;
        capsuleCollider.isTrigger = false;
        transform.position = character.transform.position;
        transform.rotation = character.transform.rotation;
        GameObject.Destroy(character);
        character = null;
        Invoke("StunEnds", stunDuration);
    }

    private void StunEnds()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<EnemyMovementLevel03>().isStunned = false;
        }
    }

    private void Ready()
    {
        isReady = true;
    }
}
