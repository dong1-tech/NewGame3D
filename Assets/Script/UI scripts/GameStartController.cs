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
        if(File.Exists(Application.dataPath + "/Data/Json/SaveData.json"))
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
        File.Create(Application.dataPath + "/Data/Json/SaveData.json");
        loadingScene.Show();
        loadingScene.LoadLevel(1);
    }

    public void OnClickContinueGame()
    {
        isNewGame = false;
        loadingScene.Show();
        loadingScene.LoadLevel(1);
    }
}
