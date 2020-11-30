using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellBase 
{
    public abstract void CastSpell();
    public float durition;
    public EElementalyType element;
    public Color colour;
    public ESpellType spellType;
    public Sound castingSound;
    public Sprite UILogo;

    //Data Structure for UI Needs to be defineded
    //Maybe another Abstract Function called "GetUIData" in base class that can be called by children to feedback Data



}
