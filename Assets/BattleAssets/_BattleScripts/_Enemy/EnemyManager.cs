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
    bool _isOwnShield;








    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        shieldSystem = GetComponent<ShieldSystem>();
    }
    void Update()
    {
        
    }


    public HealthSystem GetHealthSystem() => healthSystem;
    public ShieldSystem GetShieldSystem() => shieldSystem;





    void HideShieldIcon(bool condition)
    {
        if (condition) _shieldIcon.gameObject.SetActive(true);
        else _shieldIcon.gameObject.SetActive(false);
    }




}
