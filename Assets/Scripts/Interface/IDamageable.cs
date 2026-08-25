using UnityEngine;
using System;

public interface IDamageable
{

    event Action<IDamageable> OnDeath;
    void ChangeHealth(float amount);

    void Die();

}
