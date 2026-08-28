using UnityEngine;

public interface IPlayable
{
    Player owner { get; set; }
    void ApplyEffect(BoardObject target);

    //*ON-(CONDITION) EFFECT
    void OnSelfTurnEnd();

    void OnSelfTurnStart();

    void OnActionDone();

    void OnAnyTurnStart();

    void OnAnyTurnEnd();
}
