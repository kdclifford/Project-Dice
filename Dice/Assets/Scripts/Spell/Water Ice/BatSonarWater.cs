using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSonarWater : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        PlayerAOE(posistion, tag, agentRef, ESpellEnum.BatSonarWater);
    }

    public BatSonarWater()
    {
        duration = 2;
        castingColour = Color.blue;
        element = EElementalyType.Water;
        castingSound = ESoundClipEnum.SonarSpell;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.CHANGEME;
        SpellName = "EEEEK BLUB";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "BatSonarWater", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "WaterUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }
}
