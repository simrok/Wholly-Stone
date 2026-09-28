using UnityEngine;

public interface IDamageable
{
    public bool TakeDamage(DamageMessage damageMessage, int amount);
}
