using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningFloor : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        SetValues();
        BasicSummon(posistion, rot, tag, SpellObject, ESpellEnum.LightningFloor);
    }

    public LightningFloor()
    {
        duration = 10;
        castingColour = Color.blue;
        element = EElementalyType.Electricity;
        castingSound = ESoundClipEnum.LighntingSFX;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.NoSound;
        SpellName = "SparkieBoi";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "LightningFloor", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "LightningUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }
}
