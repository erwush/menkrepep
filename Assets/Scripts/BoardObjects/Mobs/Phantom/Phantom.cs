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
        base.Awake();
        skills.Add(new NormalAttack(this));
        skills.Add(new Haunt(this));
        skills.Add(new SupersonicFlight(this));
        skills.Add(new Phantasm(this));

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
            if (Mathf.Abs(amount) > maxHp)
            {
                //?0.25 means it is 75% damage reduction so * 0.25
                //?ternary operator, if phantasm, 0.75, if not then 0.5
                Mathf.Ceil(amount *= isPhantasm ? 0.25f : 0.5f);
                if (!isPhantasm)
                {
                    MembraneSkin membraneSkin = new MembraneSkin();
                    if (!statusEffects.Contains(membraneSkin)) ApplyEffect(membraneSkin, this);
                    else membraneSkin.ResetEffect();
                }
            }

            //? new supaya instancenya gk sama di semua char jdi durasinya terpisah dll


        }
        base.ChangeHealth(amount);

    }
    public override void OnTurnStart()
    {
        if (doNothing)
        {
            owner.ChangeStar(1);
            if(isPhantasm) owner.ChangeUltStar(1);
        }
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
