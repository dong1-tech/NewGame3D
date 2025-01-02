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

    public ItemTag itemTag;

    public ItemSize itemSize;
}

public enum ItemTier
{
    Normal = 60, Rare = 30, Epic = 5, Legendary = 1
}

public enum ItemTag
{
    Health, Mana
}

public enum ItemSize
{
    Small = 15, Medium = 30, Large = 100
}
