using System.IO;
using UnityEngine;

public class GameStartController : MonoBehaviour
{
    public static bool isNewGame;

    [SerializeField]
    private GameObject continueButton;

    [SerializeField]
    private LoadingScene loadingScene;

    private void Awake()
    {
        if (File.Exists(Application.persistentDataPath + "SaveData.json"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    public void OnClickNewGame()
    {
        isNewGame = true;
        File.Create(Application.persistentDataPath + "SaveData.json");
        loadingScene.Show();
        loadingScene.LoadLevel(1);
    }

    public void OnClickContinueGame()
    {
        isNewGame = false;
        loadingScene.Show();
        loadingScene.LoadLevel(1);
    }

    public void OnClickExitGame()
    {
        Application.Quit();
    }
}
