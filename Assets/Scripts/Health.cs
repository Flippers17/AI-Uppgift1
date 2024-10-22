using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth = 3;
    [SerializeField]
    private int _health;

    [SerializeField]
    private float _invincibillityTime = 0.1f;
    private float _invincibleTimer = 0;

    [SerializeField]
    private bool _regenerate = false;
    [SerializeField, Tooltip("The time this entity has to go without getting hit before starting to regenerate")]
    private float _regenerationTime = 3f;
    private float _regenTimer = 0;

    public UnityEvent OnDie;

    public UnityEvent<int> OnHealthUpdate;

    private void OnEnable()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_invincibleTimer > 0)
            return;

        _health -= damage;
        OnHealthUpdate?.Invoke(_health);
        _invincibleTimer = _invincibillityTime;
        _regenTimer = _regenerationTime;

        if (_health <= 0)
            Die();
    }

    private void Update()
    {
        if(_invincibleTimer > 0)
        {
            _invincibleTimer -= Time.deltaTime;
        }

        HandleRegeneration();
    }

    public void SetInvincibility(float invincibility)
    {
        if (invincibility > _invincibleTimer)
        {
            _invincibleTimer = invincibility;
        }
    }

    public void Heal(int healing)
    {
        _health += healing;
        OnHealthUpdate?.Invoke(_health);
    }


    private void HandleRegeneration()
    {
        if (!_regenerate || _health >= _maxHealth)
            return;

        if (_regenTimer > 0)
        {
            _regenTimer -= Time.deltaTime;
        }
        if (_regenTimer <= 0)
        {
            _regenTimer = _regenerationTime;
            Heal(1);
        }
    }

    public void Die()
    {
        OnDie?.Invoke();
    }
}
