using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusFire : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        PlayerAOE(posistion, tag, agentRef, ESpellEnum.CactusFire);
    }

    public CactusFire()
    {
        duration = 2;
        castingColour = Color.red;
        element = EElementalyType.Fire;
        castingSound = ESoundClipEnum.FireBall;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.NoSound;
        SpellName = "Cact Fire";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "CactusFire", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "FireUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }
}
