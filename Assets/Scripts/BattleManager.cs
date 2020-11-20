using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance = null;

    public PlayerCharacter playerCharacter;

    public List<Character> characters = new List<Character>();
    private Character currentCharacter = null;

    public List<CubeCoordinates> hexes = new List<CubeCoordinates>();
    public List<CubeCoordinates> hexesToCheck = new List<CubeCoordinates>();
    public List<CubeCoordinates> checkedHexes = new List<CubeCoordinates>();

    public LayerMask walkableLayers;
    public LayerMask attackableLayers;

    public delegate void DisableHitChanceUIHandler();
    public DisableHitChanceUIHandler OnDisableHitChance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        FindWalkableSurfaces();
        currentCharacter = playerCharacter;
        StartRound();
    }

    private void StartRound()
    {
        currentCharacter.StartRound();
    }

    public void NextRound()
    {
        DisableHitChance();

        characters.Add(currentCharacter);

        currentCharacter = characters[0];
        characters.RemoveAt(0);
        StartRound();
    }

    public void EnableHitChance()
    {
        if(currentCharacter.IsPlayerCharacter())
        {
            foreach(Character character in characters)
            {
                character.EnableHitChanceUI(currentCharacter);
            }
        }
    }

    public void DisableHitChance()
    {
        OnDisableHitChance?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        foreach(CubeCoordinates hex in hexes)
        {
            Gizmos.DrawSphere(Hex.ToWorld(hex), 0.1f);
        }

        float resolution = 5f;
        foreach(Character character in characters)
        {

            Vector3 position = playerCharacter.transform.position;

            Collider collider = character.GetComponent<Collider>();
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
                        //Gizmos.color = Color.green;
                        //Gizmos.DrawRay(position, new Vector3(x, y, z) - position);
                        RaycastHit hitInfo;
                        if (Physics.Raycast(ray, out hitInfo))
                        {
                            Gizmos.color = Color.yellow;
                            Gizmos.DrawLine(position, hitInfo.point);
                            if (hitInfo.collider == collider)
                            {
                                visiblePoints += 1f;
                            }
                            else
                            {
                                Gizmos.color = Color.red;
                                Gizmos.DrawLine(hitInfo.point, new Vector3(x, y, z));
                            }
                        }
                    }
                }
            }
        }
    }

    public void FindWalkableSurfaces()
    {
        hexes.Clear();
        hexesToCheck.Clear();
        checkedHexes.Clear();

        hexesToCheck.Add(new CubeCoordinates());

        for(;  hexesToCheck.Count > 0 && checkedHexes.Count < 1000; )
        {
            CubeCoordinates hex = hexesToCheck[0];
            hexesToCheck.RemoveAt(0);

            Ray ray = new Ray(Hex.ToWorld(hex) + Vector3.up, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, walkableLayers))
            {
                hexes.Add(hex);
                CubeCoordinates[] neighbors = hex.Neighbors();
                foreach (CubeCoordinates neighbor in neighbors)
                {
                    if (!checkedHexes.Contains(neighbor) && !hexesToCheck.Contains(neighbor) && !hexes.Contains(neighbor))
                    {
                        hexesToCheck.Add(neighbor);
                    }
                }
            }

            checkedHexes.Add(hex);
        }
    }

    public bool IsEmpty(CubeCoordinates hex)
    {
        foreach(Character character in characters)
        {
            if(character.CubeCoordinates == hex)
            {
                return false;
            }
        }
        return true;
    }
}
