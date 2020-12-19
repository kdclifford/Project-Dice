using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningCone : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, string tag = "Equipped")
    {
        PlayerAOE(posistion, tag, GameObject.FindGameObjectWithTag("Player"), ESpellEnum.LightningCone);
    }

    public LightningCone()
    {
        duration = 1;
        castingColour = Color.white;
        element = EElementalyType.Electricity;
        castingSound = ESoundClipEnum.Electric;
        spellType = ESpellType.Projectile;
        deathSound = ESoundClipEnum.PuttingOutFireBall;
        SpellName = "LekkyCone";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "SparkCone", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "LightningUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }
}
