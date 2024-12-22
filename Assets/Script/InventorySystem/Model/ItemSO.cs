using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    [field: SerializeField]
    public bool isStackable;

    public int itemID;

    [field: SerializeField]
    public int maxStackSize = 1;

    [field: SerializeField]
    public string itemName;

    [field: SerializeField, TextArea]
    public string ItemDescription { get; set; }

    [field: SerializeField]
    public Sprite ItemImage { get; set; }

    [field: SerializeField]
    public ItemTier itemTier;
}
