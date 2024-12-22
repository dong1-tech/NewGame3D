public interface IHitable
{
    void OnHit(float damageRecieve);
}

public interface IDeadable
{
    void OnDead();  
}

public interface IAttackable
{
    void OnAttack();
}

public interface IDefendable
{
    void OnDefend();
}

public interface IHealth
{
    public void TakeDamage(float damage);
}