using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponBehaviour))]
public class TowerBehaviour : MonoBehaviour
{
    [SerializeField] private int firingTime = 1;
    private WeaponBehaviour weapon;

    private void Awake()
    {
        weapon = transform.GetComponent<WeaponBehaviour>();
        InvokeRepeating(nameof(Fire), 0, firingTime);
    }

    private void Fire()
    {
        weapon.Fire();
    }
}
