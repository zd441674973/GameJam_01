using UnityEngine;

public class D17 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        int damage = Random.Range(_card.HealthToOpponentMax, _card.HealthToOpponentMin);

        if (_isDarkEnergyFull && !_cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageSelf(damage);
            Debug.Log("D17_DamageSelf_DF: " + damage);
        }
        else if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageOpponent(damage * _brightBonus);
        }
        else
        {
            CardActionManager.Instance.DamageOpponent(damage);
            Debug.Log("D17_DamageOpponent_R: " + damage);
        }
    }
}
