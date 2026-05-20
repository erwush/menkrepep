using System.Collections.Generic;
using UnityEngine;

public class Phantom : BoardMob
{

    public int costCounter;
    public bool doNothing;
    public bool isPhantasm;
    //**skill data index0 = haunt, 1 = supersonic flight, 2 = ultimate: Phantasm

    public override void Awake()
    {
        skills.Add(new Haunt(this));
        skills.Add(new SupersonicFlight(this));
        skills.Add(new Phantasm(this));
        base.Awake();



    }

    public override void Start()
    {
        base.Start();
        costCounter = -1;
    }




    public override void ChangeHealth(float amount)
    {
        if (amount < 0)
        {
            //?ternary operator, if phantasm, 0.75, if not then 0.5
            if (Mathf.Abs(amount) > maxHp)
            {
                Mathf.Ceil(amount *= isPhantasm ? 0.75f : 0.5f);
                MembraneSkin membraneSkin = new MembraneSkin();
                if (!statusEffects.Contains(membraneSkin)) statusEffects.Add(membraneSkin);
                else membraneSkin.ResetEffect();
                if (!isPhantasm)
                {
                    bonusAtk -= 4;
                }
            }

            //? new supaya instancenya gk sama di semua char jdi durasinya terpisah dll


        }
        base.ChangeHealth(amount);

    }
    public override void OnTurnStart()
    {
        if (doNothing) owner.ChangeStar(1);
        costCounter = owner.star;
        base.OnTurnStart();

    }

    public override void OnTurnEnd()
    {
        if (costCounter == owner.star) doNothing = true;
        else doNothing = false;
        base.OnTurnEnd();
    }
}
