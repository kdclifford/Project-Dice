using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBall : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        PlayerAOE(posistion, tag, agentRef, ESpellEnum.LightningBall);
    }

    public LightningBall()
    {
        duration = 1;
        damage = 12;
        castingColour = Color.white;
        element = EElementalyType.Electricity;
        castingSound = ESoundClipEnum.Electric;
        spellType = ESpellType.Projectile;
        deathSound = ESoundClipEnum.PuttingOutFireBall;
        SpellName = "POWWAAR";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "SparkCircle", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "LightningUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }
}
