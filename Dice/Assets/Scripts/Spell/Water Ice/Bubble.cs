using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : SpellBase
{
    public float projectileSpeed = 700;

    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        BasicProjectile(posistion, rot, tag, projectileSpeed, ESpellEnum.Bubble);
        //soundManager.Play(castingSound, bullet);
    }

    public Bubble()
    {
        range = 10;
        unlockTier = 0;
        damage = 10;

        duration = 3;
        castingColour = Color.blue;
        element = EElementalyType.Water;
        castingSound = ESoundClipEnum.Bubble;
        spellType = ESpellType.Projectile;
        SpellName = "BlubBlub";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "Bubble", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "WaterUI");
        destroyInstantiante = Resources.Load( PathDeath +  "BubbleDestroy", typeof(GameObject)) as GameObject;
    }

}
