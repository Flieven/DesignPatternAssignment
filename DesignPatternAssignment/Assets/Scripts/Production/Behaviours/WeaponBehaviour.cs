using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab = null;

    Tools.ObjectPool bulletPool;
    [SerializeField] private Transform spawnPoint = null;

    private void Awake()
    {
        bulletPool = new Tools.ObjectPool(1, bulletPrefab, 1, this.transform);
    }

    public void Fire()
    {
        GameObject spawnedBullet = bulletPool.Rent(false);
        spawnedBullet.GetComponent<BulletBehaviour>().Reset();
        spawnedBullet.transform.position = spawnPoint.position;
        spawnedBullet.SetActive(true);
        spawnedBullet.GetComponent<BulletBehaviour>().Push(transform.forward);
    }
}
