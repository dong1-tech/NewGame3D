using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class TurtleShellController : Enemy
{
    [SerializeField] private Transform attackPos;
    private StateMachine.State defendState;
    [SerializeField] private float defendDistance;
    public static int enemyID = 1001;
    private EnemyDefend turtleDefend;
    private EnemyBody turtleBody;

    private float distance = 0.7f;

    private void Start()
    {
        turtleDefend = GetComponentInChildren<EnemyDefend>();
        turtleBody = GetComponentInChildren<EnemyBody>();
        turtleDefend.gameObject.SetActive(false);
        turtleBody.gameObject.SetActive(true);
    }

    private void Attack()
    {
        RaycastHit hit;

        if (Physics.Raycast(attackPos.position, transform.forward, out hit, distance))
        {
            GameManager.Instance.NotifyOnAttack(hit.collider, damage);
            return;
        }

        if (Physics.Raycast(new Vector3(attackPos.position.x, attackPos.position.y,
            attackPos.position.z - 0.2f), transform.forward, out hit, distance))
        {
            GameManager.Instance.NotifyOnAttack(hit.collider, damage);
            return;
        }

        if (Physics.Raycast(new Vector3(attackPos.position.x, attackPos.position.y,
            attackPos.position.z + 0.2f), transform.forward, out hit, distance))
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

    protected override void CreatState()
    {
        base.CreatState();
        defendState = enemyStateMachine.CreateState("defend");
        defendState.onEnter = delegate
        {
            animator.Play("Defend");
            turtleBody.gameObject.SetActive(false);
            turtleDefend.gameObject.SetActive(true);
        };
        defendState.onFrame = delegate
        {
            StateChange();
        };
        defendState.onExit = delegate
        {
            turtleBody.gameObject.SetActive(true);
            turtleDefend.gameObject.SetActive(false);
        };
    }

    protected override void StateChange()
    {
        float distance = Vector3.Distance(transform.position, playerPos.position);
        if (!enemyStateMachine.currentState.isCompleted)
        {
            return;
        }
        if(distance < defendDistance)
        {
            enemyStateMachine.TransitionTo(defendState);
            return;
        }
        if (distance < attack2Distance)
        {
            enemyStateMachine.TransitionTo(attack2State);
            return;
        }
        if (distance < chasingDistance)
        {
            enemyStateMachine.TransitionTo(runState);
            return;
        }
        if (distance < guardDistance)
        {
            enemyStateMachine.TransitionTo(guardState);
            return;
        }
        if (distance > guardDistance && transform.position.x == returnPosition.x && transform.position.z == returnPosition.z)
        {
            enemyStateMachine.TransitionTo(idleState);
            return;
        }
        if (transform.position != returnPosition)
        {
            enemyStateMachine.TransitionTo(walkState);
        }
    }
}
