using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticEnemy : Enemy
{
    public override void takeDamage(int amount)
    {
        hp--;
        //throw new System.NotImplementedException();
    }
}
