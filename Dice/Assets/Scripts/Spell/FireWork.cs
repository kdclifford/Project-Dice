using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWork : SpellBase
{

    public float projectileSpeed = 100;
    public GameObject particlePrefab;
    public override void CastSpell(Vector3 posistion, float rot, string tag = "Equipped")
    {
       GameObject effect = MonoBehaviour.Instantiate(particlePrefab, posistion, Quaternion.identity) as GameObject;
        MonoBehaviour.Destroy(effect, 1.5f);
        //Spell Code would go in here then when controller has correct input Cast Spell
        for (int i = 0; i < 10; i++)
        {

            Quaternion agentRot = Quaternion.identity;
            agentRot.eulerAngles = new Vector3(0, rot + (36 * i), 90);
            GameObject bullet = MonoBehaviour.Instantiate(SpellObject, posistion, agentRot) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * projectileSpeed);
            bullet.tag = tag;
            bullet.GetComponent<ProjectileController>().setTimer(duration);
            bullet.AddComponent<SpellIndex>().spellIndex = ESpellEnum.FireWork;
            SoundManager.instance.Play(castingSound, bullet);
        }

    }

    public FireWork()
    {
        duration = 1;
        castingColour = Color.red;
        element = EElementalyType.Fire;
        castingSound = ESoundClipEnum.FireBall;
        spellType = ESpellType.Projectile;
        //  UILogo = (Sprite)Resources.Load("UI Icons/Spells/FireUI");
    }

    public override void SetValues()
    {
        SpellObject = Resources.Load("Player/FireBall", typeof(GameObject)) as GameObject;
        particlePrefab = Resources.Load("Player/GroundExplosion", typeof(GameObject)) as GameObject;
        UILogo = Resources.Load<Sprite>("UIIcons/Spells/FireUI");
        destroyInstantiante = Resources.Load("Player/GroundExplosion", typeof(GameObject)) as GameObject;
    }


}
