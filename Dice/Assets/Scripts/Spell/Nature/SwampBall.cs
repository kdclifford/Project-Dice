using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampBall : SpellBase
{
    public float projectileSpeed = 700;

    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        BasicProjectile(posistion, rot, tag, projectileSpeed, ESpellEnum.SwampBall);
    }

    public SwampBall()
    {
        unlockTier = 0;
        damage = 10;
        manaCost = 5;
        duration = 3;
        castingColour = Color.green;
        element = EElementalyType.Nature;
        castingSound = ESoundClipEnum.GrassOrbSFX;
        spellType = ESpellType.Projectile;
        SpellName = "ShrekFart";
        deathSound = ESoundClipEnum.GrassCrumpleSFX;

        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "SwampBall", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "NatureUI");
        destroyInstantiante = Resources.Load(PathDeath + "GrassDestroy", typeof(GameObject)) as GameObject;
    }
}
