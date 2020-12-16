using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAOEFire : SpellBase
{
    public float projectileSpeed = 300;

    //Cast Spell call instaiate your object in here the 
    public override void CastSpell(Vector3 posistion, float rot, string tag = "Equipped")
    {
        BasicProjectile(posistion, rot, tag, projectileSpeed, ESpellEnum.ProjectileAOEFire);

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
        UILogo = Resources.Load<Sprite>(PathUI + "FireUI");
        SpellObject = Resources.Load(PathProjectile + "FireBallAOE", typeof(GameObject)) as GameObject;
        destroyInstantiante = Resources.Load(PathDeath + "GroundExplosion", typeof(GameObject)) as GameObject;
    }
}
