using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCone : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        PlayerAOE(posistion, tag, agentRef, ESpellEnum.LightningCone);
    }

    public LightningCone()
    {
        duration = 1;
        castingColour = Color.yellow;
        element = EElementalyType.Electricity;
        castingSound = ESoundClipEnum.Electric;
        spellType = ESpellType.Projectile;
        deathSound = ESoundClipEnum.PuttingOutFireBall;
        SpellName = "LekkyCone";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "SparkCone", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "LightningUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }
}
