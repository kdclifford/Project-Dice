﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellBase
{
    public GameObject SpellObject;
    public abstract void CastSpell(Vector3 posistion, float rot, string tag = "Equipped");
    public abstract void SetValues();
    public float duration;
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
public string PathProjectile { get { return pathProjectile;} }
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

    public void death(Vector3 ProjectilePosition, GameObject currentProjectile, Quaternion ProjectileRotation)
    {
        GameObject deathObject = MonoBehaviour.Instantiate(destroyInstantiante, ProjectilePosition, ProjectileRotation) as GameObject;
        SoundManager.instance.PlayOnceAtPoint(deathSound, deathObject);
       // MonoBehaviour.Destroy(currentProjectile);
    }
}
