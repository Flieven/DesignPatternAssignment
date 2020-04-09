using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float minSpeed = 0;
    [SerializeField] private float maxSpeed = 0;
    [SerializeField] private int Damage;

    public void Push(Vector3 dir)
    {
        transform.GetComponent<Rigidbody>().AddForce(dir * Random.Range(minSpeed, maxSpeed));
        Invoke(nameof(Sleep), 1.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponentInParent<iDamageable>() is iDamageable) 
        { 
            collision.gameObject.GetComponentInParent<UnitBehaviour>().TakeDamage(Damage); 
            Sleep(); 
        }
        else { Sleep(); }
    }

    public void Reset() { transform.GetComponent<Rigidbody>().velocity = Vector3.zero; }
    private void Sleep() { gameObject.SetActive(false); }
    private void OnDisable()
    {
        CancelInvoke(nameof(Sleep));
    }
}
