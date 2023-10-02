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

    
    [SerializeField] bool _isEnemyTurn;



    void Start()
    {

    }
    void Update()
    {
        if (!_isEnemyTurn) return;

    }





}
