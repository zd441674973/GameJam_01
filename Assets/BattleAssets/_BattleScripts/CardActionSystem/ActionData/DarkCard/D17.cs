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
        }
        else if (_isBrightEnergyFull && _cardData.IsBrightCard)
        {
            CardActionManager.Instance.DamageOpponent(damage * _brightBonus);
        }
        else
        {
            CardActionManager.Instance.DamageOpponent(damage);
        }



        // CardActionManager.Instance.DamageOpponent(damage);

        //Debug.Log("D1_Played; " + "Damage: " + damage);
    }
}
