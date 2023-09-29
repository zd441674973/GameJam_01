using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChangeTurn : MonoBehaviour
{
    Button button;
    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        button.onClick.AddListener
        (
            () =>
            {
                TurnSystem.Instance.NextTurn();


            }
        );
    }


}
