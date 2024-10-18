using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    public Action<float> OnHealthUpdated;
    public Action OnDeath;

    public bool _isDead { get; private set; }
    private float _health;

    // Start is called before the first frame update
    void Start()
    {
        _health = _maxHealth;
        OnHealthUpdated(_maxHealth);
    }
    
    public void DeductHealth(float value)
    {
        if (_isDead) return;

        _health -= value;

        if(_health <= 0)
        {
            _isDead = true;
            OnDeath();
            _health = 0;
        }

        OnHealthUpdated(_health);
    }
}
