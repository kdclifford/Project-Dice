using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FireBall : SpellBase
{
    public float projectileSpeed = 700;
    public override void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped")
    {
        BasicProjectile(posistion, rot, tag, projectileSpeed, ESpellEnum.FireBall);

        //soundManager.Play(castingSound, bullet);

    }

    public FireBall()
    {
        range = 12;
        damage = 10;
        unlockTier = 0;
        manaCost = 5;
        duration = 3;
        castingColour = Color.red;
        element = EElementalyType.Fire;
        castingSound = ESoundClipEnum.FireBall;
        spellType = ESpellType.Projectile;
        deathSound = ESoundClipEnum.PuttingOutFireBall;
        SpellName = "PewPewFire";
        PathProjectile = PathProjectile + element.ToString() + "/";
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load(PathProjectile + "FireBall", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>(PathUI + "FireUI");
        destroyInstantiante = Resources.Load(PathDeath + "FireballDestroy 1", typeof(GameObject)) as GameObject;
    }

}
