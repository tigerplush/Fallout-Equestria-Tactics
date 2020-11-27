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
        DefaultUI.instance.SetUIInteractable(true);
        controls.Enable();
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
        if(currentWeapon == inventory.primaryWeapon)
        {
            currentWeapon = inventory.secondaryWeapon;
        }
        else
        {
            currentWeapon = inventory.primaryWeapon;
        }

        BattleManager.instance.EnableHitChance();
    }

    public void OnSwitchWeapons(InputAction.CallbackContext context)
    {

    }
}
