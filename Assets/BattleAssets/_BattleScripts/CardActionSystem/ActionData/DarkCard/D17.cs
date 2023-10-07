using UnityEngine;

public class D17 : CardAction
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TakeAction()
    {
        int damage = Random.Range(card.HealthToOpponentMin, card.HealthToOpponentMax);
        CardActionManager.Instance.DealDamage(damage);

        Debug.Log("D1_Played; " + "Damage: " + damage);
    }
}
