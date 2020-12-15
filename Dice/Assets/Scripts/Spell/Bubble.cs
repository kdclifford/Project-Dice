using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : SpellBase
{
    public float projectileSpeed = 700;

    public override void CastSpell(Vector3 posistion, float rot, string tag = "Equipped")
    {
        BasicProjectile(posistion, rot, tag, projectileSpeed, ESpellEnum.Bubble);
        //soundManager.Play(castingSound, bullet);
    }

    public Bubble()
    {
        duration = 3;
        castingColour = Color.blue;
        element = EElementalyType.Water;
        castingSound = ESoundClipEnum.Bubble;
        spellType = ESpellType.Projectile;      
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "Bubble", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "WaterUI");
        destroyInstantiante = Resources.Load( PathDeath +  "BubbleDestroy", typeof(GameObject)) as GameObject;
    }

}
