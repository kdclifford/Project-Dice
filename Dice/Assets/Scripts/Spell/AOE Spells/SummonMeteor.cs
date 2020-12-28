using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonMeteor : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        SetValues();
        //Summon A Pumkin 
        BasicSummon(posistion, rot, tag, SpellObject, ESpellEnum.SummonMeteor);
    }
    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "SummonMeteor", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "RockUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }

    public SummonMeteor()
    {
        duration = 10;
        castingColour = Color.red;
        element = EElementalyType.Rock;
        castingSound = ESoundClipEnum.Bubble;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.Bubble;
        SpellName = "DinoKiller";
    }
}
