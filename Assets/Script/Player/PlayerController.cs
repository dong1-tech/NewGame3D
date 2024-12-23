using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private LayerMask groundLayer;
    private Transform distanceToGround;
    private int numOfAttack = 1;
    private float sprintTimer;
    private bool isSprint;

    private GameInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction defendAction;
    private InputAction attackAction;
    private InputAction rotationAction;
    [SerializeField]
    private float rotationSpeed;

    private Animator animator;
    private Rigidbody rigi;

    private Vector2 direction;
    [SerializeField] private float playerSpeed;

    [SerializeField] 
    private BoxCollider playerShieldCollider;
    [SerializeField]
    private BoxCollider playerSwordCollider;

    private StateMachine playerStateMachine;

    private void Awake()
    {
        playerStateMachine = new StateMachine();
        animator = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody>();
        CreateState();
    }

    // Start is called before the first frame update

    private void OnEnable()
    {
        playerInput = new GameInput();
        moveAction = playerInput.Player.Move;
        moveAction.Enable();
        jumpAction = playerInput.Player.Jump;
        jumpAction.Enable();
        attackAction = playerInput.Player.Attack;
        attackAction.Enable();
        defendAction = playerInput.Player.Defend;
        defendAction.Enable();
        rotationAction = playerInput.Player.Rotation;
        rotationAction.Enable();
        rotationAction.performed += OnRotation;
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        attackAction.Disable();
        defendAction.Disable();
        rotationAction.performed -= OnRotation;
        rotationAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsInventoryOpen() && !GameManager.Instance.IsPause())
        {
            //float mouseX = Input.GetAxis("Mouse X");
            //transform.Rotate(Vector3.up * mouseX);
            playerStateMachine.Update();
            OnMove();
        }
        else
        {  
            playerStateMachine.TransitionTo("idle");
        }
    }
    private void CreateState()
    {
        var idleState = playerStateMachine.CreateState("idle");
        idleState.onEnter = delegate
        {
            int idleAnimationNum = Random.Range(1, 3);
            if (idleAnimationNum == 1)
            {
                animator.CrossFade("Idle_Battle_SwordAndShield", 0);
            }
            else
            {
                animator.CrossFade("Idle_Normal_SwordAndShield", 0);
            }
        };
        idleState.onFrame = delegate
        {
            StateChange();
        };
        var moveLeftState = playerStateMachine.CreateState("mLeft");
        moveLeftState.onEnter = delegate
        {
            animator.CrossFade("MoveLFT_Battle_RM_SwordAndShield", 0);
        };
        moveLeftState.onFrame = delegate
        {
            StateChange();
        };
        var moveRightState = playerStateMachine.CreateState("mRight");
        moveRightState.onEnter = delegate
        {
            animator.CrossFade("MoveRGT_Battle_RM_SwordAndShield", 0);
        };
        moveRightState.onFrame = delegate
        {
            StateChange();
        };
        var moveForwardState = playerStateMachine.CreateState("mForward");
        moveForwardState.onEnter = delegate
        {
            animator.CrossFade("MoveFWD_Normal_RM_SwordAndShield", 0);
            sprintTimer = Time.time;
        };
        moveForwardState.onFrame = delegate
        {
            if(sprintTimer + 2 <= Time.time)
            {
                isSprint = true;
            }
            StateChange();
        };
        var moveBackwardState = playerStateMachine.CreateState("mBackward");
        moveBackwardState.onEnter = delegate
        {
            animator.CrossFade("MoveBWD_Battle_RM_SwordAndShield", 0); 
        };
        moveBackwardState.onFrame = delegate
        {
            StateChange();
        };
        var attackState = playerStateMachine.CreateState("attack");
        attackState.onEnter = delegate
        {
            playerSwordCollider.enabled = true;
            if(numOfAttack > 3) { numOfAttack = 1; }
            switch (numOfAttack)
            {
                case 1:
                    animator.CrossFade("Attack01_SwordAndShiled", 0);
                    break;
                case 2:
                    animator.CrossFade("Attack02_SwordAndShiled", 0);
                    break;
                case 3:
                    animator.CrossFade("Attack03_SwordAndShiled", 0);
                    break;
            }
            numOfAttack++;
        };
        attackState.onFrame = delegate
        {
            StateChange();
        };
        attackState.onExit = delegate
        {
            playerSwordCollider.enabled = false;
        };
        var specialAttackState = playerStateMachine.CreateState("spAttack");
        specialAttackState.onEnter = delegate
        {
            animator.CrossFade("Attack04_SwordAndShiled", 0);
        };
        specialAttackState.onFrame = delegate
        {
            StateChange();
        };
        var defendState = playerStateMachine.CreateState("defend");
        defendState.onEnter = delegate
        {
            animator.CrossFade("Defend_SwordAndShield", 0);
            playerShieldCollider.enabled = true;
        };
        defendState.onFrame = delegate
        {
            StateChange();
        };
        var defendHitState = playerStateMachine.CreateState("defendHit");
        defendHitState.onEnter = delegate
        {
            animator.CrossFade("DefendHit_SwordAndShield", 0);
            playerStateMachine.currentState.isCompleted = false;
        };
        defendHitState.onFrame = delegate
        {
            if (playerStateMachine.currentState.isCompleted)
            {
                playerStateMachine.TransitionTo("idle");
            }
        };
        var dieState = playerStateMachine.CreateState("die");
        dieState.onEnter = delegate
        {
            animator.CrossFade("Die01_SwordAndShield", 0);
        };
        dieState.onFrame = delegate
        {
            StateChange();
        };
        var dieStayState = playerStateMachine.CreateState("dieStay");
        dieStayState.onEnter = delegate
        {
            animator.CrossFade("Die01_Stay_SwordAndShield", 0);
        };
        dieStayState.onFrame = delegate
        {
            StateChange();
        };
        var dizzyState = playerStateMachine.CreateState("dizzy");
        dizzyState.onEnter = delegate
        {
            animator.CrossFade("Dizzy_SwordAndShield", 0);
        };
        dizzyState.onFrame = delegate
        {
            StateChange();
        };
        var jumpState = playerStateMachine.CreateState("jump");
        jumpState.onEnter = delegate
        {
            animator.CrossFade("JumpFull_Normal_RM_SwordAndShield", 0);
        };
        jumpState.onFrame = delegate
        {
            StateChange();
        };
        var getHitState = playerStateMachine.CreateState("hit");
        getHitState.onEnter = delegate
        {
            animator.CrossFade("GetHit01_SwordAndShield", 0);
            playerStateMachine.currentState.isCompleted = false;
        };
        getHitState.onFrame = delegate
        {
            if (playerStateMachine.currentState.isCompleted)
            {
                playerStateMachine.TransitionTo("idle");
            }
        };
        var sprintState = playerStateMachine.CreateState("sprint");
        sprintState.onEnter = delegate
        {
            animator.CrossFade("SprintFWD_Battle_RM_SwordAndShield", 0);
            playerSpeed = playerSpeed * 2;
        };
        sprintState.onFrame = delegate
        {
            if(!(direction.y > 0))
            {
                isSprint = false;
            }
            StateChange();
        };
        sprintState.onExit = delegate
        {
            playerSpeed = playerSpeed / 2;
        };
        var levelUpState = playerStateMachine.CreateState("levelUp");
        levelUpState.onEnter = delegate
        {
            animator.CrossFade("LevelUp_Battle_SwordAndShield", 0);
        };
        levelUpState.onFrame = delegate
        {
            StateChange();
        };
        var victoryState = playerStateMachine.CreateState("victory");
        victoryState.onEnter = delegate
        {
            animator.CrossFade("Victory_Battle_SwordAndShield", 0);
        };
        victoryState.onFrame = delegate
        {
            StateChange();
        };
    }
    private void StateChange()
    {
        if (defendAction.WasReleasedThisFrame())
        {
            playerStateMachine.currentState.isCompleted = true;
            playerShieldCollider.enabled = false;
        }
        if (!playerStateMachine.currentState.isCompleted)
        {
            return;
        }
        if(jumpAction.WasPressedThisFrame())
        {
            playerStateMachine.TransitionTo("jump");
            return;
        }
        if (defendAction.inProgress)
        {
            playerStateMachine.TransitionTo("defend");
            return;
        }
        if (attackAction.WasPressedThisFrame())
        {
            playerStateMachine.TransitionTo("attack");
            return;
        }
        if (isSprint)
        {
            playerStateMachine.TransitionTo("sprint");
            return;
        }
        if (direction == Vector2.zero)
        {
            playerStateMachine.TransitionTo("idle");
            return;
        }

        if (direction.y > 0)
        {
            playerStateMachine.TransitionTo("mForward");
            return;
        }

        if (direction.y < 0)
        {
            playerStateMachine.TransitionTo("mBackward");
            return;
        }

        if (direction.x > 0)
        {
            playerStateMachine.TransitionTo("mRight");
            return;
        }

        if (direction.x < 0)
        {
            playerStateMachine.TransitionTo("mLeft");
            return;
        }
    }
    private void OnMove()
    {
        var state = playerStateMachine.currentState;
        if (state == null) return;
        if (!state.isCompleted && state.name != "jump")
        {
            return;
        }
        direction = moveAction.ReadValue<Vector2>();
        Vector3 velocity = transform.TransformVector(new Vector3(direction.x, 0, direction.y)) * playerSpeed * Time.fixedDeltaTime;
        rigi.velocity = new Vector3(velocity.x, rigi.velocity.y, velocity.z);
    }

    private void OnStateStart()
    {
        playerStateMachine.currentState.isCompleted = false;   
    }

    private void OnStateEnd()
    { 
        playerStateMachine.currentState.isCompleted = true;   
    }

    private void OnJump()
    {
        transform.position += new Vector3(0, 0.5f, 0);
    }

    public void HandlerOnHit()
    {
        playerStateMachine.TransitionTo("hit");
    }

    public void HandlerOnDefendSucess()
    {
        playerShieldCollider.enabled = false;
        playerStateMachine.currentState.isCompleted = true;
        playerStateMachine.TransitionTo("defendHit");
    }

    private void OnRotation(InputAction.CallbackContext context)
    {
        if (!GameManager.Instance.IsInventoryOpen() && !GameManager.Instance.IsPause())
        {
            float value = context.ReadValue<Vector2>().x;
            transform.rotation = Quaternion.Euler(0, value * rotationSpeed + transform.rotation.eulerAngles.y, 0);
        }
    }
}
