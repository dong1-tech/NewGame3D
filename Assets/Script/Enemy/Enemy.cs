using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDeadable
{
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float walkSpeed;

    [SerializeField] protected Transform playerPos;

    [SerializeField] protected float guardDistance;
    [SerializeField] protected float chasingDistance;
    [SerializeField] protected float attack2Distance;

    private bool isCreatedHealthBar;

    [SerializeField] protected float damage;

    [SerializeField]
    private Transform followPos;
    [SerializeField]
    private Camera cam;
    private Health enemyHealth;
    private EnemyHealthBarUI healthBar;

    protected StateMachine enemyStateMachine;

    private NavMeshAgent enemyAgent;

    protected Vector3 returnPosition;

    protected Animator animator;

    //State
    protected StateMachine.State idleState;
    protected StateMachine.State attack2State;
    protected StateMachine.State dieState;
    protected StateMachine.State getHitState;
    protected StateMachine.State runState;
    protected StateMachine.State walkState;
    protected StateMachine.State guardState;
    protected StateMachine.State dizzyState;


    private void Awake()
    {
        enemyHealth = GetComponentInChildren<Health>();
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyStateMachine = new StateMachine();
        animator = GetComponent<Animator>();
        isCreatedHealthBar = false;
        returnPosition = transform.position;
        enemyAgent.stoppingDistance = attack2Distance;
        CreatState();
    }

    private void OnEnable()
    {
        enemyHealth.OnHealthChange += UpdateHealthBar;
    }

    private void OnDisable()
    {
        enemyHealth.OnHealthChange -= UpdateHealthBar;
    }

    private void OnStateEnter()
    {
        enemyStateMachine.currentState.isCompleted = false;
    }

    private void OnStateDone()
    {
        enemyStateMachine.currentState.isCompleted = true;
    }
    
    protected virtual void CreatState()
    {
        idleState = enemyStateMachine.CreateState("idle");
        idleState.onEnter = delegate
        {
            animator.Play("IdleNormal");
        };
        idleState.onFrame = delegate
        {
            StateChange();
        };
        guardState = enemyStateMachine.CreateState("guard");
        guardState.onEnter = delegate
        {
            animator.Play("SenseSomethingRPT");
        };
        guardState.onFrame = delegate
        {
            transform.right = -playerPos.right;
            StateChange();
        };
        attack2State = enemyStateMachine.CreateState("attack2");
        attack2State.onEnter = delegate
        {
            animator.Play("Attack02");
            enemyStateMachine.currentState.isCompleted = false;
        };
        attack2State.onFrame = delegate
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerPos.position - transform.position), Time.deltaTime);
            StateChange();
        };
        dieState = enemyStateMachine.CreateState("die");
        dieState.onEnter = delegate
        {
            animator.Play("Die");
        };
        getHitState = enemyStateMachine.CreateState("getHit");
        getHitState.onEnter = delegate
        {
            animator.Play("GetHit");
            enemyStateMachine.currentState.isCompleted = false;
        };
        getHitState.onFrame = delegate
        {
            if(enemyStateMachine.currentState.isCompleted)
            {
                enemyStateMachine.TransitionTo(runState);
            }
        };
        dizzyState = enemyStateMachine.CreateState("dizzy");
        dizzyState.onEnter = delegate
        {
            animator.Play("Dizzy");
        };
        dizzyState.onFrame = delegate
        {
            StateChange();
        };
        runState = enemyStateMachine.CreateState("run");
        runState.onEnter = delegate
        {
            if (!isCreatedHealthBar)
            {
                CreateHealthBar();
                isCreatedHealthBar = true;
            }
            animator.Play("RunFWD");
            enemyAgent.speed = chaseSpeed;
        };
        runState.onFrame = delegate
        {
            enemyAgent.destination = playerPos.position;
            StateChange();
        };
        walkState = enemyStateMachine.CreateState("walk");
        walkState.onEnter = delegate
        {
            if (isCreatedHealthBar)
            {
                DestroyHealthBar();
                isCreatedHealthBar = false;
            }
            animator.Play("WalkFWD");
            enemyAgent.speed = walkSpeed;
        };
        walkState.onFrame = delegate
        {
            enemyAgent.destination = returnPosition;
            StateChange();
        };
    }

    protected virtual void StateChange()
    {
        float distance = Vector3.Distance(transform.position, playerPos.position);
        if (!enemyStateMachine.currentState.isCompleted)
        {
            return;
        }
        if(distance < attack2Distance)
        {
            enemyStateMachine.TransitionTo(attack2State);
            return;
        }
        if(distance < chasingDistance)
        {
            enemyStateMachine.TransitionTo(runState);
            return;
        }
        if(distance < guardDistance)
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
    private void Update()
    {
        enemyStateMachine.Update();
    }
    private void OnAnimationCompleted()
    {
        enemyStateMachine.currentState.isCompleted = true;
    }

    public void HandlerOnHit()
    {
        enemyStateMachine.TransitionTo(getHitState);
    }
    private void CreateHealthBar()
    {
        healthBar = GameManager.Instance.CreateEnemyHealthBar(followPos, cam);
    }
    private void UpdateHealthBar(float healthPercent)
    {
        healthBar.UpdateHealthBar(healthPercent);
    }
    private void DestroyHealthBar()
    {
        Destroy(healthBar.gameObject);
    }

    public virtual void OnDead()
    {
        enemyStateMachine.TransitionTo(dieState);
        Invoke("DestroyObject", 2);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
        DestroyHealthBar();
    }
    
    public void SetUp(Transform playerPos, Camera cam)
    {
        this.playerPos = playerPos;
        this.cam = cam;   
    }
}


