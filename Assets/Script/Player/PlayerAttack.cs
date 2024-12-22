using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float playerDamage;

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.NotifyOnAttack(other, playerDamage);
    }
}
