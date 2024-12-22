using UnityEngine;

public class PlayerShield : MonoBehaviour, IDefendable
{
    [SerializeField] private PlayerController controller;
    private BoxCollider2D box;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }
    public void OnDefend()
    {
        controller.HandlerOnDefendSucess();
    }
}
