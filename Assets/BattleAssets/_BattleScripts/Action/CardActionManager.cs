using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardActionManager : MonoBehaviour
{
    public static CardActionManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances occured");
            Destroy(Instance);
        }
        Instance = this;
    }

    [SerializeField] bool _cardIsPlayed;

    public bool CardIsPlayed { get { return _cardIsPlayed; } set { _cardIsPlayed = value; } }


    void Update()
    {
        if (!_cardIsPlayed) return;

        Debug.Log("Card is playing");
        _cardIsPlayed = false;

    }






}
