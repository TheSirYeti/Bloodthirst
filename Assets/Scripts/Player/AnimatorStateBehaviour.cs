using Player.Behaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStateBehaviour : MonoBehaviour
{

    public BasicAttacks attacking;
    public int attackTurn;

    public void setAttackingTurn()
    {
        attacking.attackTurn = attackTurn;
    }
}
