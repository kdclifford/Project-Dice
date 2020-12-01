using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellBase : MonoBehaviour
{
    public GameObject SpellObject;
    public abstract void CastSpell(Vector3 posistion, Quaternion playerRot);
    public float durition;
    public EElementalyType element;
    public Color castingColour;
    public ESpellType spellType;
    public ESoundClipEnum castingSound;
    public Sprite UILogo;

    //Data Structure for UI Needs to be defineded
    //Maybe another Abstract Function called "GetUIData" in base class that can be called by children to feedback Data



}
