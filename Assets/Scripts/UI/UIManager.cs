using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour, InputMaster.IUIActions
{
    public static UIManager instance = null;
    private InputMaster controls;
    public DefaultUI defaultUI;
    public InventoryUI inventoryUI;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        controls = new InputMaster();
        controls.UI.SetCallbacks(this);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void OnToggleInventory(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.performed && BattleManager.instance.IsPlayerTurn())
        {
            ToggleInventoryUI();
        }
    }

    public void ToggleInventoryUI()
    {
        inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
    }

    public void UpdateInventoryUI(Inventory inventory)
    {
        inventoryUI.UpdateUI(inventory);
        defaultUI.UpdateWeaponsDisplay(inventory.primaryWeapon, inventory.secondaryWeapon);
    }
}
