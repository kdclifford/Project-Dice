using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FireBall : SpellBase
{
    public float projectileSpeed = 700;
    public override void CastSpell(Vector3 posistion, Quaternion playerRot, string tag = "Equipped")
    {
        ProjectileFire(posistion, playerRot, tag, projectileSpeed);

        //soundManager.Play(castingSound, bullet);

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

    public override void SetValues()
    {
        SpellObject = Resources.Load("Player/FireBall", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>("UIIcons/Spells/FireUI");
    }


    // //Default time
    //public FireBall() : this(3)
    // {       
    // }

}
