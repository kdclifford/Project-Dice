using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellBase
{
    public GameObject SpellObject;
    public float coolDown = 1;
    public abstract void CastSpell(Vector3 posistion, float rot, GameObject agentRef, string tag = "Equipped");
    public abstract void SetValues();
    public float duration;
    public string SpellName;
    public EElementalyType element;
    public Color castingColour;
    public ESpellType spellType;
    public ESoundClipEnum castingSound;
    public ESoundClipEnum deathSound;
    public Sprite UILogo;
    public GameObject destroyInstantiante;
    private string pathProjectile = "Spells/";
    private string pathDeath = "SpellDestruction/";
    private string pathUI = "UIIcons/Spells/";
    public float damage = 6;

    public string PathProjectile { get { return pathProjectile; } set { pathProjectile = value; } }
    public string PathDeath { get { return pathDeath; } }
    public string PathUI { get { return pathUI; } }

    //Data Structure for UI Needs to be defineded
    //Maybe another Abstract Function called "GetUIData" in base class that can be called by children to feedback Data

    //Basic projectile funcion if making new projectile maybe dont use this
    public void BasicProjectile(Vector3 posistion, float rot, string tag, float projectileSpeed, ESpellEnum spell)
    {
        Quaternion agentRot = Quaternion.identity;


        agentRot.eulerAngles = new Vector3(0, rot, 90);
        //Spell Code would go in here then when controller has correct input Cast Spell
        GameObject bullet = MonoBehaviour.Instantiate(SpellObject, posistion, agentRot) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * projectileSpeed);
        bullet.tag = tag;
        bullet.GetComponent<ProjectileController>().setTimer(duration);
        bullet.AddComponent<SpellIndex>().spellIndex = spell;
        SoundManager.instance.Play(castingSound, bullet);
    }



    public void BasicSummon(Vector3 posistion, float rot, string tag, GameObject summonedSystem, ESpellEnum spell)
    {
        Quaternion agentRot = Quaternion.identity;

        agentRot.eulerAngles = new Vector3(0, rot, 0);
        //Spell Code would go in here then when controller has correct input Cast Spell

        //ChangePosition based on roation and add 5 in local y

        GameObject bullet = MonoBehaviour.Instantiate(SpellObject, posistion, agentRot) as GameObject;
        bullet.tag = tag;
        bullet.AddComponent<SpellIndex>().spellIndex = spell;
        SoundManager.instance.Play(castingSound, bullet);
    }

    public void PlayerAOE(Vector3 posistion, string tag, GameObject player, ESpellEnum spell)
    {
        GameObject bullet = MonoBehaviour.Instantiate(SpellObject, posistion, player.transform.rotation) as GameObject;
        bullet.transform.SetParent(player.transform);
        bullet.tag = tag;
        bullet.AddComponent<SpellIndex>().spellIndex = spell;
        bullet.GetComponent<ProjectileController>().setTimer(duration);
        SoundManager.instance.Play(castingSound, bullet);
    }


    public void death(Vector3 ProjectilePosition, GameObject currentProjectile, Quaternion ProjectileRotation)
    {
        if (destroyInstantiante != null)
        {
            GameObject deathObject = MonoBehaviour.Instantiate(destroyInstantiante, ProjectilePosition, ProjectileRotation) as GameObject;
            SoundManager.instance.PlayOnceAtPoint(deathSound, deathObject);
        }
        // MonoBehaviour.Destroy(currentProjectile);
    }
}
