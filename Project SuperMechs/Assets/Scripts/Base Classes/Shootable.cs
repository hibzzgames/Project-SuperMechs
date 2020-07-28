using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    /// <summary>
    /// Virtual function, handle on being shot at
    /// </summary>
    /// <param name="damageValue"> Damage value to apply </param>
    /// <param name="ProjectileDirection"> The direction of the projectile </param>
    public virtual void OnShotAt(float damageValue, Vector3 ProjectileDirection) {	}
}
