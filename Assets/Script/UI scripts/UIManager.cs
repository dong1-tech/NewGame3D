
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private EnemyHealthBarInstantiate newSpawner;

    [SerializeField]
    private HealthBarUI healthBarUI;

    [SerializeField]
    private GameObject InventoryScene;
    [SerializeField]
    private GameObject PauseGameScene;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void OnEnable()
    {
        GameManager.OnStateChange += OnEnableScene;
    } 

    public void OnDisable()
    {
        GameManager.OnStateChange -= OnEnableScene;
    }

    private void OnEnableScene(GameState state)
    {
        InventoryScene.SetActive(state == GameState.OpenInventory);
        PauseGameScene.SetActive(state == GameState.Pause);
    }

    public void OnClickIventoryIcon()
    {
        GameManager.Instance.UpdateState(GameState.OpenInventory);
    }

    public void OnClickPauseIcon()
    {
        GameManager.Instance.UpdateState(GameState.Pause);
    }

    public void OnClickContinueIcon()
    {
        GameManager.Instance.UpdateState(GameState.GameRunning);
        Time.timeScale = 1.0f;
    }

    public void OnClickCloseIcon()
    {
        GameManager.Instance.UpdateState(GameState.GameRunning);
    }

    public void OnClickSaveIcon()
    {
        GameManager.Instance.SaveGame();
    }

    public void OnClickHomeIcon()
    {
        GameManager.Instance.SaveGame();
        SceneManager.LoadScene(0);
    }

    public EnemyHealthBarUI CreateHealthBar(Transform followPos, Camera cam)
    {
        return newSpawner.InstantiateEnemyHealthBar(followPos, cam);
    }

    public void UpdateHealthBar(float healthPercent)
    {
        healthBarUI.OnValueChange(healthPercent);
    }
}
