using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Designed to be used both as base class and as interface for player's weapons
 */
public abstract class PlayerWeapon : MonoBehaviour
{
    public abstract bool TryShoot();

    public virtual string GetName()
    {
        return name;
    }
}
