using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WindDirection
{
    North,       
    NorthEast,   
    East,        
    SouthEast,   
    South,       
    SouthWest,   
    West,        
    NorthWest    
}
public class WindSystem : MonoBehaviour
{
    public static WindSystem Instance { get; private set; }

    // Wind properties
    [SerializeField] private WindDirection windDirectionEnum = WindDirection.East; // Default wind direction
    [SerializeField] private float windStrength = 5f; // Default wind strength

    private Vector3 windDirection;

    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist this object between scene loads
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates if they exist
        }

        // Initialize wind direction based on enum
        SetWindDirectionFromEnum();
    }

    private void SetWindDirectionFromEnum()
    {
        switch (windDirectionEnum)
        {
            case WindDirection.North:
                windDirection = Vector3.forward;
                break;
            case WindDirection.NorthEast:
                windDirection = (Vector3.forward + Vector3.right).normalized;
                break;
            case WindDirection.East:
                windDirection = Vector3.right;
                break;
            case WindDirection.SouthEast:
                windDirection = (Vector3.back + Vector3.right).normalized;
                break;
            case WindDirection.South:
                windDirection = Vector3.back;
                break;
            case WindDirection.SouthWest:
                windDirection = (Vector3.back + Vector3.left).normalized;
                break;
            case WindDirection.West:
                windDirection = Vector3.left;
                break;
            case WindDirection.NorthWest:
                windDirection = (Vector3.forward + Vector3.left).normalized;
                break;
        }
    }

    public Vector3 GetWindDirection()
    {
        return windDirection;
    }

    public float GetWindStrength()
    {
        return windStrength;
    }

    public void SetWindDirection(WindDirection newDirection)
    {
        windDirectionEnum = newDirection;
        SetWindDirectionFromEnum();
    }

    public void SetWindStrength(float newStrength)
    {
        windStrength = newStrength;
    }
}
