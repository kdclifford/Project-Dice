using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public SpellBase[] spellList = new SpellBase[1];

    private void Start()
    {
        spellList[1] = new FireBall();
    }


}
