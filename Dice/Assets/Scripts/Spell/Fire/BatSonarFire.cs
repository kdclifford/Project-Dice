using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSonarFire : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        PlayerAOE(posistion, tag, agentRef, ESpellEnum.BatSonarFire);
    }

    public BatSonarFire()
    {
        duration = 2;
        castingColour = Color.red;
        element = EElementalyType.Fire;
        castingSound = ESoundClipEnum.CHANGEME;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.CHANGEME;
        SpellName = "EEEEK TSHH";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "BatSonarFire", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "FireUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }
}
