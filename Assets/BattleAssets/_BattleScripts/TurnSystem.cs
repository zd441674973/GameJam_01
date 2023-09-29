using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances occured");
            Destroy(Instance);
        }
        Instance = this;
    }

    [SerializeField] int _turnIndex;
    [SerializeField] bool _isPlayerTurn;

    void Start()
    {
        _turnIndex = 0;
        _isPlayerTurn = true;
    }

    public void NextTurn()
    {
        _turnIndex++;
        _isPlayerTurn = !_isPlayerTurn;

        Debug.Log(_turnIndex);



        //OnTurnChanged?.Invoke();
    }

    public int GetTurnIndex() => _turnIndex;
    public bool IsPlayerTurn() => _isPlayerTurn;



}
