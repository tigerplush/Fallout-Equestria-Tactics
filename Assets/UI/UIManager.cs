using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour, InputMaster.IPlayerActions
{
    private InputMaster controls;
    public InventoryUI inventoryUI;

    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void OnMoveCamera(InputAction.CallbackContext callbackContext)
    {
        Vector2 delta = callbackContext.ReadValue<Vector2>();
        Debug.Log(delta);
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
}
