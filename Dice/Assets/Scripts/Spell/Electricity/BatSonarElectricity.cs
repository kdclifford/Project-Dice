using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSonarElectricity : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        PlayerAOE(posistion, tag, agentRef, ESpellEnum.BatSonarElectricity);
    }

    public BatSonarElectricity()
    {
        duration = 2;
        castingColour = Color.white;
        element = EElementalyType.Electricity;
        castingSound = ESoundClipEnum.SonarSpell;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.NoSound;
        SpellName = "EEEEK ZZZT";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "BatSonarElectricity", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "LightningUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }
}
