using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonPumpkin : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        SetValues();
        BasicSummon(posistion, rot, tag, SpellObject, ESpellEnum.SummonPumpkin);
    }
    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "PumkinSpell", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "NatureUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }

    public SummonPumpkin()
    {
        duration = 5;
        damage = 30;
        unlockTier = 5;
        castingColour = Color.red;
        element = EElementalyType.Nature;
        castingSound = ESoundClipEnum.NoSound;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.NoSound;
        SpellName = "PumpKing";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }
}
