using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : SpellBase
{
    public float projectileSpeed = 500;

    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        BasicProjectile(posistion, rot, tag, projectileSpeed, ESpellEnum.Boulder);
    }

    public Boulder()
    {
        range = 1;
        duration = 3;
        damage = 15;
        unlockTier = 2;
        castingColour = Color.green;
        element = EElementalyType.Nature;
        castingSound = ESoundClipEnum.Electric;
        spellType = ESpellType.Projectile;
        SpellName = "BBBoulder";
        PathProjectile = PathProjectile + element.ToString() + "/";        
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "BoulderBoom", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "RockUI");
        destroyInstantiante = Resources.Load(PathDeath + "RockDestroy", typeof(GameObject)) as GameObject;
    }

}
