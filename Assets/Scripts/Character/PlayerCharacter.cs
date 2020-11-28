using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : Character, InputMaster.IPlayerActions
{
    public bool canMove = false;

    private InputMaster controls;

    protected override void Awake()
    {
        base.Awake();
        controls = new InputMaster();
        controls.Player.SwitchWeapons.performed += context => SwitchWeapons();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override void StartRound()
    {
        base.StartRound();
        canMove = true;
        controls.Enable();

        DefaultUI.instance.SetUIInteractable(true);
    }

    public override void EndRound()
    {
        base.EndRound();
        canMove = false;
        controls.Disable();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override bool IsPlayerCharacter()
    {
        return true;
    }

    protected override void ConsumeAP(int value)
    {
        base.ConsumeAP(value);
        DefaultUI.instance.SetActionPoints(ActionPoints);
    }

    protected override void SetAP(int value)
    {
        base.SetAP(value);
        DefaultUI.instance.SetActionPoints(value);
    }

    public override void SetTarget(CubeCoordinates target)
    {
        if(canMove && Hex.Distance(CubeCoordinates, target) <= ActionPoints && target != CubeCoordinates)
        {
            base.SetTarget(target);
        }
    }

    public void SwitchWeapons()
    {
        switch(currentWeapon)
        {
            case WeaponType.Primary:
                currentWeapon = WeaponType.Secondary;
                break;
            case WeaponType.Secondary:
                currentWeapon = WeaponType.Primary;
                break;
        }

        BattleManager.instance.EnableHitChance();
        DefaultUI.instance.SetWeaponsDisplay(currentWeapon);
    }

    public void OnSwitchWeapons(InputAction.CallbackContext context)
    {

    }

    protected override void OnEquipmentChange()
    {
        base.OnEquipmentChange();
        BattleManager.instance.EnableHitChance();
        UIManager.instance.UpdateInventoryUI(inventory);
    }
}
