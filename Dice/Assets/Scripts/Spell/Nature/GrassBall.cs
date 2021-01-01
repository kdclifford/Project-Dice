using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBall : SpellBase
{
    public float projectileSpeed = 500;

    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        BasicProjectile(posistion, rot, tag, projectileSpeed, ESpellEnum.GrassBall);
    }

    public GrassBall()
    {
        duration = 3;
        castingColour = Color.green;
        element = EElementalyType.Nature;
        castingSound = ESoundClipEnum.Electric;
        spellType = ESpellType.Projectile;
        SpellName = "GrassBall";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "GrassBall", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "NatureUI");
        destroyInstantiante = Resources.Load(PathDeath + "GrassDestroy", typeof(GameObject)) as GameObject;
    }

}
