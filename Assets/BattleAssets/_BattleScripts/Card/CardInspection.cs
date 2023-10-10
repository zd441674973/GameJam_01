using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardInspection : MonoBehaviour
{
    public static CardInspection Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances occured");
            Destroy(Instance);
        }
        Instance = this;
    }



    [SerializeField] Image _cardImage;
    [SerializeField] TextMeshProUGUI _cardName;
    [SerializeField] TextMeshProUGUI _cardDescription;

    public void SetCardImage(Sprite image) => _cardImage.sprite = image;
    public void SetCardName(string name) => _cardName.text = name;
    public void SetCardDescription(string cardDes) => _cardDescription.text = cardDes;














}
