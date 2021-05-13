using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : SpellBase
{
    public float projectileSpeed = 100;

    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        Quaternion agentRot = Quaternion.identity;
        agentRot.eulerAngles = new Vector3(0, rot, 90);
        GameObject effect = MonoBehaviour.Instantiate(SpellObject, posistion, agentRot, agentRef.transform) as GameObject;
        effect.AddComponent<SpellIndex>().spellIndex = ESpellEnum.Flamethrower;
        effect.tag = tag;


    }

    public Flamethrower()
    {
        duration = 1;
        unlockTier = 4;
        damage = 20;
        manaCost = 25;
        castingColour = Color.red;
        element = EElementalyType.Fire;
        castingSound = ESoundClipEnum.FlamingConeSFX;
        spellType = ESpellType.Projectile;
        SpellName = "Thrower";
        coolDown = 6;
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        UILogo = Resources.Load<Sprite>(PathUI + "FireUI");
        SpellObject = Resources.Load(PathProjectile + "Flamethrower", typeof(GameObject)) as GameObject;
        destroyInstantiante = Resources.Load(PathDeath + "GroundExplosion", typeof(GameObject)) as GameObject;
    }

}
