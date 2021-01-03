using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAOEFire : SpellBase
{
    //Cast Spell call instaiate your object in here the 
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        Quaternion agentRot = Quaternion.identity;
        agentRot.eulerAngles = new Vector3(0, rot, 90);
        GameObject effect = MonoBehaviour.Instantiate(SpellObject, posistion, agentRot, agentRef.transform) as GameObject;
        effect.AddComponent<SpellIndex>().spellIndex = ESpellEnum.Flamethrower;
        effect.tag = tag;

    }

    public ProjectileAOEFire()
    {
        duration = 1;
        castingColour = Color.red;
        element = EElementalyType.Fire;
        castingSound = ESoundClipEnum.FireBall;
        spellType = ESpellType.Projectile;
        SpellName = "FireThrow";
        coolDown = 3;
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        UILogo = Resources.Load<Sprite>(PathUI + "FireUI");
        SpellObject = Resources.Load(PathProjectile + "GroundExplosion", typeof(GameObject)) as GameObject;
    }
}
