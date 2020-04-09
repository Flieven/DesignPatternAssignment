using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, iDamageable
{
    [SerializeField] private int currentHealth = 0;

    public event Action<int> OnPlayerHealthChanged;

    public void setupPlayer()
    {
        Health = currentHealth;
    }

    public int Health
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            OnPlayerHealthChanged?.Invoke(currentHealth);
            //if (currentHealth != value)
            //{
            //    currentHealth = value;
            //    OnPlayerHealthChanged?.Invoke(currentHealth);
            //}
        }
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage;
        if(Health <= 0) 
        { 
            Debug.Log("Player Killed!");
            transform.parent.GetComponent<WorldManager>().GameOver();
        }
    }

    [ContextMenu("Increase Health")]
    public void Increase() { Health++; }

    [ContextMenu("Decrease Health")]
    public void Decrease() { TakeDamage(10); }
}
