using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances occured");
            Destroy(Instance);
        }
        Instance = this;
    }

    HealthSystem healthSystem;
    ShieldSystem shieldSystem;

    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        shieldSystem = GetComponent<ShieldSystem>();
    }


    public HealthSystem GetHealthSystem() => healthSystem;
    public ShieldSystem GetShieldSystem() => shieldSystem;
}
