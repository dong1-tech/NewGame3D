using UnityEngine;

public class ItemDropMessageUI : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
        Invoke("Hide", 5);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        Hide();
    }
}
