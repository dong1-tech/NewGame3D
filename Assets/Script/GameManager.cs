using UnityEngine;
using InventorySystem;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]

    private InventoryController inventoryController;

    [SerializeField]

    private ItemDropManager itemDropManager;

    [SerializeField]
    private Health playerHealth;

    [HideInInspector]
    public GameState currentState;
    public static Action<GameState> OnStateChange;

    private bool isInventoryOpen = false;
    private bool isPause = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        if (!GameStartController.isNewGame)
        {
            LoadGame();
        }
        currentState = GameState.GameRunning;
    }

    private void OnEnable()
    {
        playerHealth.OnHealthChange += UpdateHealthBar;
    }

    public void UpdateState(GameState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case GameState.GameRunning:
                OnGameRunning();
                break;
            case GameState.OpenInventory:
                OnOpenInventory();
                break;
            case GameState.Pause:
                OnPause();
                break;
            case GameState.End:
                OnEnd();
                break;     
        }
        OnStateChange.Invoke(currentState);
    }

    private void OnEnd()
    {
        
    }

    private void OnPause()
    {
        isPause = true;
        Time.timeScale = 0f;
    }

    private void OnOpenInventory()
    {
        isInventoryOpen = true;
    }

    private void OnGameRunning()
    {
        isPause = false;
        isInventoryOpen = false;
    }

    private void UpdateHealthBar(float healthPercent)

    {
        UIManager.Instance.UpdateHealthBar(healthPercent);
    }

    public void NotifyOnAttack(Collider other, float damage)
    {
        IDefendable defendObject = other.GetComponent<IDefendable>();
        if(defendObject != null )
        {
            defendObject.OnDefend();
            return;
        }
        IHitable hitObjcet = other.GetComponent<IHitable>();
        if (hitObjcet != null)
        {
            hitObjcet.OnHit(damage);
        }
    }

    public EnemyHealthBarUI CreateEnemyHealthBar(Transform followPos, Camera cam)
    {
        return UIManager.Instance.CreateHealthBar(followPos, cam);
    }

    public void NotifyOnDropItem(int enemyID)
    {
        itemDropManager.OnDropItem(enemyID);
    }

    public void LoadGame()
    {
        DataLoading dataLoading = new();
        dataLoading.LoadData();
    }

    public void SaveGame()
    {
        PlayerData.instance.SaveData();
    }

    public void NotifyOnUseItem(int itemID)
    {
        // to be continued
        Debug.Log("aaa");
    }

    public bool IsInventoryOpen() { return isInventoryOpen; }

    public bool IsPause() {  return isPause; }
}

public enum GameState
{
    GameRunning, OpenInventory, Pause, End
}
