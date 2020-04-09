using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponBehaviour))]
public class TowerBehaviour : MonoBehaviour
{
    [SerializeField] private float overlapRadius = 1.0f;
    [SerializeField] private float firingTime = 1;
    private WeaponBehaviour weapon;
    [SerializeField] private GameObject TowerHead = null;

    private Transform cachedTransform = null;

    private void Awake()
    {
        cachedTransform = this.transform;
        weapon = transform.GetComponent<WeaponBehaviour>();
        InvokeRepeating(nameof(CheckForFire), 0, firingTime);
    }

    private void CheckForFire()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, overlapRadius);
        if (hitColliders.Length > 0)
        {
            foreach(Collider col in hitColliders)
            {
                if (col.gameObject.layer.Equals(9))
                {
                    Vector3 target = (col.transform.position - transform.position).normalized;
                    TowerHead.transform.rotation = Quaternion.LookRotation(target, transform.up);
                    weapon.Fire();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
     Gizmos.color = Color.red;
     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
     Gizmos.DrawWireSphere(transform.position, overlapRadius);
    }
}
