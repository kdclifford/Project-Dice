using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAOEFire : SpellBase
{
    public float projectileSpeed = 300;
    public override void CastSpell(Vector3 posistion, float rot, string tag = "Equipped")
    {
        ProjectileFire(posistion, rot, tag, projectileSpeed, ESpellEnum.FireBall);

        //soundManager.Play(castingSound, bullet);

    }

    public ProjectileAOEFire()
    {
        duration = 1;
        castingColour = Color.red;
        element = EElementalyType.Fire;
        castingSound = ESoundClipEnum.FireBall;
        spellType = ESpellType.Projectile;
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load("Player/FireBallAOE", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>("UIIcons/Spells/FireUI");
        destroyInstantiante = Resources.Load("Player/FireBall", typeof(GameObject)) as GameObject;
    }
}
