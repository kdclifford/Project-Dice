using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IceSpike : SpellBase
{
    public float projectileSpeed = 400;
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        BasicProjectile(posistion, rot, tag, projectileSpeed, ESpellEnum.IceSpike);
    }

    public IceSpike()
    {
        range = 14;
        duration = 3;
        castingColour = Color.white;
        element = EElementalyType.Water;
        castingSound = ESoundClipEnum.IceDeathSoundSFX;
        spellType = ESpellType.Projectile;
        deathSound = ESoundClipEnum.IceDeathSoundSFX;
        SpellName = "IceOwchie";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "Spike", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "IceUI");
        destroyInstantiante = Resources.Load(PathDeath + "IceDestroy", typeof(GameObject)) as GameObject;
    }
}
