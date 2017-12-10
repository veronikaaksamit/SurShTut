using System.Collections.Generic;
using UnityEngine;

/**
 * To add new weapons, put them as children below this game object
 */
public class PlayerShooting2 : MonoBehaviour
{
    private List<PlayerWeapon> m_weapons;
    [SerializeField]
    private PlayerWeapon m_currentWeapon;

    void Awake()
    {
        InitializeWeapons();
    }


    void Update()
    {
        //TODO: swaping weapons
    }

    void LateUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            m_currentWeapon.TryShoot();
        }
    }

    private void InitializeWeapons()
    {
        m_weapons = new List<PlayerWeapon>(GetComponentsInChildren<PlayerWeapon>());
        if (m_weapons.Count == 0)
        {
            Debug.LogError("Player has no weapons");
        }

        if (m_currentWeapon == null)
        {
            Debug.LogWarning("No initial player weapon set, setting the first one");
            m_currentWeapon = m_weapons[0];
        }
    }
}
