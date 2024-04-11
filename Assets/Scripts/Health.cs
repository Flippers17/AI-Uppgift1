using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth = 3;
    private int _health;

    [SerializeField]
    private float _invincibillityTime = 0.1f;
    private float _invincibleTimer = 0;


    public UnityEvent OnDie;

    public UnityEvent<int> OnHealthUpdate;

    public void TakeDamage(int damage)
    {
        if (_invincibleTimer > 0)
            return;

        _health -= damage;
        OnHealthUpdate?.Invoke(_health);
        _invincibleTimer = _invincibillityTime;

        Debug.Log("Took damage");
        if (_health <= 0)
            Die();
    }

    private void Update()
    {
        if(_invincibleTimer > 0)
        {
            _invincibleTimer -= Time.deltaTime;
        }
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


    public void Die()
    {
        OnDie?.Invoke();
        Debug.Log("Died");
    }
}
