using UnityEngine;

public class SlimeController : Enemy
{
    [SerializeField] private Transform attackPos;

    public static int enemyID = 1000;

    private void Attack()
    {
        RaycastHit hit;

        if (Physics.Raycast(attackPos.position, transform.forward, out hit, 0.6f))
        {
            GameManager.Instance.NotifyOnAttack(hit.collider, damage);
            return;
        }

        if (Physics.Raycast(new Vector3(attackPos.position.x, attackPos.position.y,
            attackPos.position.z - 0.2f), transform.forward, out hit, 0.6f))
        {
            GameManager.Instance.NotifyOnAttack(hit.collider, damage);
            return;
        }

        if (Physics.Raycast(new Vector3(attackPos.position.x, attackPos.position.y,
            attackPos.position.z + 0.2f), transform.forward, out hit, 0.6f))
        {
            GameManager.Instance.NotifyOnAttack(hit.collider, damage);
            return;
        }
    }

    public override void OnDead()
    {
        base.OnDead();
        GameManager.Instance.NotifyOnDropItem(enemyID);
    }
}
