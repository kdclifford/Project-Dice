using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusNature : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        PlayerAOE(posistion, tag, agentRef, ESpellEnum.CactusNature);
    }

    public CactusNature()
    {
        duration = 2;
        castingColour = Color.green;
        element = EElementalyType.Nature;
        castingSound = ESoundClipEnum.BoulderCrumpleSFX;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.BoulderCrumpleSFX;
        SpellName = "Cact Rock";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "CactusSpell", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "NatureUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }
}
