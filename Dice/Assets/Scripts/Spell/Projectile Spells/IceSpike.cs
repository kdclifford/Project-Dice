using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IceSpike : SpellBase
{
    public float projectileSpeed = 400;
    public override void CastSpell(Vector3 posistion, float rot, string tag = "Equipped")
    {
        BasicProjectile(posistion, rot, tag, projectileSpeed, ESpellEnum.IceSpike);
    }

    public IceSpike()
    {
        duration = 3;
        castingColour = Color.white;
        element = EElementalyType.Water;
        castingSound = ESoundClipEnum.Bubble;
        spellType = ESpellType.Projectile;
        deathSound = ESoundClipEnum.PuttingOutFireBall;
        SpellName = "IceOwchie";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "Spike", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "IceUI");
        destroyInstantiante = Resources.Load(PathDeath + "IceDestroy", typeof(GameObject)) as GameObject;
    }
}
