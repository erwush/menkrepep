using UnityEngine;

public class Phantom : BoardMob
{

    public int costCounter, turnCounter;
    public bool doNothing, isSelfDebuffed;

    public override void ChangeHealth(float amount)
    {
        if (amount < 0)
        {
            if (Mathf.Abs(amount) > maxHp) Mathf.Ceil(amount *= 0.5f);
            turnCounter = 4;
            bonusAtk -= 4;
        }
        base.ChangeHealth(amount);

    }
    public override void OnTurnStart()
    {
        if (isSelfDebuffed && turnCounter > 0) turnCounter--;
        
        if (doNothing) owner.ChangeStar(1);
        costCounter = owner.star;
    }

    public override void OnTurnEnd()
    {
        if (costCounter == owner.star) doNothing = true;
    }
}
