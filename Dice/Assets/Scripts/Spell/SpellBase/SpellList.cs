using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class SpellList : MonoBehaviour
{
    public static SpellList instance;

    // [HideInInspector]
    public SpellBase[] spells;
    //public List<SpellBase> spells;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }

    private void OnEnable()
    {
        InitializeSpells();
    }

    //Find all Spells derived from SpellBase
    public void InitializeSpells()
    {

        //foreach (Type type in AppDomain.CurrentDomain.GetAssemblies()
        //                       .SelectMany(assembly => assembly.GetTypes())
        //                       .Where(type => type.IsSubclassOf(typeof(SpellBase))))
        //{
        //    spells.Add((SpellBase)Activator.CreateInstance(type));
        //}

        spells = new SpellBase[(int)ESpellEnum.Size]
        {
            new BatSonarElectricity(),
       new      LightningBall(),
       new LightningCone(),
       new LightningFloor(),
       new Spark(),
       new SummonLightning(),
       new BatSonarFire(),
       new CactusFire(),
       new FireBall(),
       new FireCone (),
       new FireLine(),
       new FireWork(),
       new Flamethrower(),
       new ProjectileAOEFire(),
       new BatSonarNature(),
       new Boulder(),
       new CactusNature(),
       new GrassBall(),
       new SummonMeteor(),
       new SummonSwamp(),
       new SwampBall(),
       new BatSonar(),
       new Kunai(),
       new MagicArrow(),
       new StarterKnife(),
       new SwordProjectile(),
          new SummonPumpkin(),
       new BatSonarWater(),
       new Bubble(),
       new CactusWater(),
       new IceLine(),
       new IceSpike(),
       new SummonRain(),
       new SummonSnow(),

        };

        foreach (SpellBase spell in spells)
        {
            spell.SetValues();
        }
    }
}
