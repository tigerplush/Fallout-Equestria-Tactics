using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public RectTransform content;
    public GameObject itemUiPrefab;

    private List<ItemUI> itemUiPool = new List<ItemUI>();

    private void OnEnable()
    {
        Character currentCharacter = BattleManager.instance.CurrentCharacter;

        Inventory inventory = currentCharacter.inventory;

        int numberOfItems = inventory.inventory.Count;

        //Create missing number of prefabs
        for (int i = itemUiPool.Count; i < numberOfItems; i++)
        {
            GameObject itemUiObject = Instantiate<GameObject>(itemUiPrefab, content);
            ItemUI itemUi = itemUiObject.GetComponent<ItemUI>();
            itemUi.Init(this);
            itemUiPool.Add(itemUi);
        }

        //enable only as much itemUis as items exist
        for(int i = 0; i < itemUiPool.Count; i++)
        {
            itemUiPool[i].gameObject.SetActive(i < numberOfItems);
            itemUiPool[i].Set(inventory.inventory[i]);
        }
    }

    public void Hover(Item item)
    {
        Debug.Log(item.name);
    }
}
