using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSonarNature : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        PlayerAOE(posistion, tag, agentRef, ESpellEnum.BatSonarNature);
    }

    public BatSonarNature()
    {
        duration = 2;
        castingColour = Color.green;
        element = EElementalyType.Nature;
        castingSound = ESoundClipEnum.SonarSpell;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.CHANGEME;
        SpellName = "EEEEK SPTT";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "BatSonarNature", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "NatureUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }
}
