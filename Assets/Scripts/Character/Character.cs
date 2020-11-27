using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Race
{
    EarthPony,
    Unicorn,
    Pegasus
}

public abstract class Character : MonoBehaviour, IPointerClickHandler
{
    public Animator animator;
    public Collider characterCollider;
    [Header("Periphery")]
    public Race race;
    [Header("S.P.E.C.I.A.L. Attributes")]
    public int Strength = 5;
    public int Perception = 5;
    public int Endurance = 5;
    public int Charisma = 5;
    public int Intelligence = 5;
    public int Agility = 5;
    public int Luck = 5;

    [Header("Secondary Attributes")]
    public float Health;
    private float startingHealth;
    public int ActionPoints;
    private int startingActionPoints;

    [Header("Skills")]
    public int Unarmed;
    public int Melee;
    public int Guns;

    [Header("Items")]
    public Inventory inventory;
    public WeaponType currentWeapon;
    public Weapon defaultWeapon;

    [Header("Object fields")]
    public HitChanceCanvasUI hitChanceCanvas;
    public float speed = 1f;
    private float elapsedTime = 0f;

    private List<CubeCoordinates> path;
    private CubeCoordinates startingPoint;
    private CubeCoordinates currentGoal;
    protected bool isMoving = false;
    protected bool hasTurn = false;

    protected virtual void Awake()
    {
        startingActionPoints = 5 + Agility / 2;

        startingHealth = 15f + (2f * (float)Endurance) + (float)Strength;
        Health = startingHealth;
    }

    protected virtual void Start()
    {
        BattleManager.instance.OnDisableHitChance += DisableHitChanceUI;

        currentWeapon = WeaponType.Primary;
    }

    protected virtual void OnDestroy()
    {
        BattleManager.instance.OnDisableHitChance -= DisableHitChanceUI;
    }

    public virtual void StartRound()
    {
        hasTurn = true;
        SetAP(startingActionPoints);

        BattleManager.instance.EnableHitChance();

        animator.SetBool("isWalking", isMoving);
    }

    public virtual void EndRound()
    {
        hasTurn = false;
        DefaultUI.instance.SetUIInteractable(false);

        animator.SetBool("isWalking", false);
    }

    public void EnableHitChanceUI(Character other)
    {
        float cover = Cover(other, this);

        //Determine hit chance
        float hitChance = other.HitChance(other.ActiveWeapon.skillType, cover);

        float distance = Hex.Distance(other.CubeCoordinates, CubeCoordinates);

        float rangedHitChance = Mathf.Lerp(hitChance, 0f, (distance - 1f) / other.ActiveWeapon.attacks[0].range);

        hitChanceCanvas.Enable(rangedHitChance);
    }

    private void DisableHitChanceUI()
    {
        hitChanceCanvas.Disable();
    }

    public virtual bool IsPlayerCharacter()
    {
        return false;
    }

    public CubeCoordinates CubeCoordinates
    {
        get
        {
            return Hex.FromWorld(transform.position);
        }
    }

    public void SetPath(CubeCoordinates[] path)
    {
        if(path != null && path.Length > 0)
        {
            this.path = new List<CubeCoordinates>(path);
            currentGoal = this.path[0];
            this.path.RemoveAt(0);
            SetNextGoal();
            BattleManager.instance.DisableHitChance();

            animator.SetBool("isWalking", true);
        }
    }

    private void SetNextGoal()
    {
        if(path.Count > 0 && ActionPoints > 0)
        {
            ConsumeAP(1);
            startingPoint = currentGoal;
            elapsedTime = 0;
            currentGoal = path[0];
            isMoving = true;
            path.RemoveAt(0);
            transform.LookAt(Hex.ToWorld(currentGoal) + new Vector3(0f, 0.5f, 0f));
        }
        else
        {
            isMoving = false;
            animator.SetBool("isWalking", false);

            BattleManager.instance.EnableHitChance();
        }
    }

    protected virtual void Update()
    {
        if(isMoving)
        {
            elapsedTime += Time.deltaTime * speed;
            if(elapsedTime > 1f)
            {
                SetNextGoal();
            }
        }
    }

    protected virtual void FixedUpdate()
    {
        if(isMoving)
        {
            transform.position = Vector3.Lerp(Hex.ToWorld(startingPoint), Hex.ToWorld(currentGoal), elapsedTime);
            transform.position += new Vector3(0f, 0.5f, 0f);
        }
    }

    public Weapon ActiveWeapon
    {
        get
        {
            Weapon weapon = null;
            switch(currentWeapon)
            {
                case WeaponType.Primary:
                    weapon = inventory.primaryWeapon;
                    break;

                case WeaponType.Secondary:
                    weapon = inventory.secondaryWeapon;
                    break;
            }
            if(weapon != null)
            {
                return weapon;
            }
            return defaultWeapon;
        }
    }

    private float ArmorClass
    {
        get
        {
            return (float)Agility + inventory.GetArmorClass();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <remarks>Switch from Character to IAttackable so you can destroy things</remarks>
    public void Attack(Character other)
    {
        transform.LookAt(other.transform);
        Attack defaultAttack = ActiveWeapon.attacks[0];

        float distance = Hex.Distance(CubeCoordinates, other.CubeCoordinates);

        if (ActionPoints >= defaultAttack.actionPointCost && distance <= defaultAttack.range)
        {
            //Check cover
            float cover = Cover(this, other);

            //Determine hit chance
            float hitChance = HitChance(ActiveWeapon.skillType, cover);

            float rangedHitChance = Mathf.Lerp(hitChance, 0f, (distance - 1f) / defaultAttack.range);

            float roll = Random.Range(0f, 100f);

            float requiredToHit = Mathf.Min((rangedHitChance - other.ArmorClass), 95f);
            if(roll <= requiredToHit)
            {
                float damage = Random.Range(ActiveWeapon.minDamage, ActiveWeapon.maxDamage);
                float bonusDamage = Mathf.Clamp((float)Strength - 5f, 0f, (float)Strength);
                damage += bonusDamage;
                other.Damage(damage);
            }
            else
            {
                other.Miss();
            }

            ConsumeAP(defaultAttack.actionPointCost);
        }
    }

    private float HitChance(SkillType skillType, float cover)
    {
        float baseHitChance = 0f;

        switch(skillType)
        {
            case SkillType.Unarmed:
                baseHitChance = 65f + (float)(Strength + Agility) / 2f + (float)Unarmed;
               break;
            case SkillType.Melee:
                baseHitChance = 55f + (float)(Strength + Agility) / 2f + (float)Melee;
                break;
            case SkillType.Ranged:
                baseHitChance = 35f + (float)Agility + (float)Guns;
                break;
        }

        baseHitChance += (float)Luck;

        float hitChance = Mathf.Lerp(baseHitChance, 0f, cover);

        return hitChance;
    }

    public void Damage(float damage)
    {
        float damageResisted = damage * (100f - Mathf.Min(inventory.GetDamageResistance(), 85f)) / 100f;
        float damageFinal = Mathf.Max(damageResisted - inventory.GetDamageThreshold(), damage * 0.2f);

        DamageTextManager.instance.Damage(transform.position, damage);
        Health -= damage;
        if(Health <= 0f)
        {
            BattleManager.instance.Kill(this);
        }
    }

    public void Miss()
    {
        DamageTextManager.instance.Damage(transform.position);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns>Percentage of cover between 0f and 1f</returns>
    public static float Cover(Character from, Character to, float resolution = 5f)
    {
        Vector3 position = from.transform.position;

        //Collider collider = to.GetComponent<Collider>();
        Collider collider = to.characterCollider;
        Vector3 min = collider.bounds.min;
        Vector3 max = collider.bounds.max;

        float xResolution = (max.x - min.x) / resolution;
        float yResolution = (max.y - min.y) / resolution;
        float zResolution = (max.z - min.z) / resolution;

        float testedPoints = 0f;
        float visiblePoints = 0f;

        for (float x = min.x + xResolution / 2f; x <= max.x; x += xResolution)
        {
            for (float y = min.y + yResolution / 2f; y <= max.y; y += yResolution)
            {
                for (float z = min.z + zResolution / 2f; z <= max.z; z += zResolution)
                {
                    testedPoints += 1f;
                    Ray ray = new Ray(position, new Vector3(x, y, z) - position);
                    RaycastHit hitInfo;
                    if(Physics.Raycast(ray, out hitInfo))
                    {
                        if(hitInfo.collider == collider)
                        {
                            visiblePoints += 1f;
                        }
                    }
                }
            }
        }

        return 1 - (visiblePoints / testedPoints);
    }

    protected virtual void ConsumeAP(int value)
    {
        ActionPoints -= value;
    }

    protected virtual void SetAP(int value)
    {
        ActionPoints = value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BattleManager.instance.ClickedCharacter(this);
    }

    /// <summary>
    /// Sets a path to target without checking if target can be reached
    /// </summary>
    /// <param name="target"></param>
    public virtual void SetTarget(CubeCoordinates target)
    {
        CubeCoordinates[] path = AStar.FindWay(CubeCoordinates, target);
        SetPath(path);
    }
}
