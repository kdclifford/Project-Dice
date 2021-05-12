using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSwamp : SpellBase
{
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        SetValues();
        //Summon A Pumkin 
        BasicSummon(posistion, rot, tag, SpellObject, ESpellEnum.SummonSwamp);
    }
    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "Swamp", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "NatureUI");
        destroyInstantiante = Resources.Load(PathDeath + "EmptyDestroy", typeof(GameObject)) as GameObject;
    }

    public SummonSwamp()
    {
        duration = 10;
        castingColour = Color.green;
        element = EElementalyType.Nature;
        castingSound = ESoundClipEnum.GrassCrumpleSFX;
        spellType = ESpellType.AOE;
        deathSound = ESoundClipEnum.NoSound;
        SpellName = "ShreksVoid";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }
}
