using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
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

    [SerializeField] Transform _shieldIcon;


    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        shieldSystem = GetComponent<ShieldSystem>();
    }

    void Update()
    {
        ShowSieldIconCheck();
    }

    void ShowSieldIconCheck()
    {
        if (shieldSystem.ShieldValue < 1)
            _shieldIcon.gameObject.SetActive(false);
        else _shieldIcon.gameObject.SetActive(true);
    }


    public HealthSystem GetHealthSystem() => healthSystem;
    public ShieldSystem GetShieldSystem() => shieldSystem;




}
