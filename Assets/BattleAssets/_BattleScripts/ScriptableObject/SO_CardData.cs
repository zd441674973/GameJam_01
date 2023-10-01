using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Creat New Card")]
public class SO_CardData : ScriptableObject
{
    enum CardType
    {
        ElectricalEnergy, MagicEnergy
    }


    [Header("Card Info")]
    [SerializeField] CardType _cardType;
    [SerializeField] string _cardName;
    [SerializeField] int _cardID;
}
