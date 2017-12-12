using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * To add new weapons, put them as children below this game object
 */
public class PlayerShooting2 : MonoBehaviour
{
    private List<PlayerWeapon> m_weapons;
    [SerializeField] private PlayerWeapon m_currentWeapon;
    [SerializeField] private Text m_text;
    private string m_weaponTextPrefix = "Weapon:  ";
    private static string WEAPON_BUTTON_PREFIX = "Weapon";

    void Awake()
    {
        InitializeWeapons();
        UpdateText();
    }

    void Update()
    {
        //TODO: swaping weapons
        HandleWeaponSwapping();
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

    private void UpdateText()
    {
        m_text.text = m_weaponTextPrefix + m_currentWeapon.name;
    }

    private void HandleWeaponSwapping()
    {
        int weaponCount = m_weapons.Count;

        for (int i = 0; i < weaponCount; ++i)
        {
            string weaponButtonName = WEAPON_BUTTON_PREFIX + (i + 1);

            if (Input.GetButtonDown(weaponButtonName))
            {
                m_currentWeapon = m_weapons[i];
                UpdateText();
            }
        }
    }
}
