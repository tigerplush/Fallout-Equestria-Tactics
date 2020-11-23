using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public bool canMove = false;


    protected override void Awake()
    {
        base.Awake();
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
    }

    public override void EndRound()
    {
        base.EndRound();
        canMove = false;
    }

    protected override void Update()
    {
        base.Update();

        if(canMove)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, BattleManager.instance.walkableLayers))
                {
                    CubeCoordinates target = Hex.FromWorld(hit.point);
                    if (BattleManager.instance.IsEmpty(target) && Hex.Distance(CubeCoordinates, target) <= ActionPoints)
                    {
                        CubeCoordinates[] path = AStar.FindWay(CubeCoordinates, target);
                        SetPath(path);
                    }
                }
            }
        }
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
}
