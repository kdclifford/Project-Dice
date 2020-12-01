using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FireBall : SpellBase
{
    public override void CastSpell()
    {
        //Spell Code would go in here then when controller has correct input Cast Spell
        //GameObject bullet = Instantiate(projectileRight, RightFirePos, playerRot) as GameObject;
        //bullet.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
    }

    public FireBall(float mDuration = 3)
    {
        durition = mDuration;
        castingColour = Color.red;
        element = EElementalyType.Fire;
        castingSound = ESoundClipEnum.FireBall;
        spellType = ESpellType.Projectile;
      //  UILogo = (Sprite)Resources.Load("UI Icons/Spells/FireUI");
    }

   // //Default time
   //public FireBall() : this(3)
   // {       
   // }

}
