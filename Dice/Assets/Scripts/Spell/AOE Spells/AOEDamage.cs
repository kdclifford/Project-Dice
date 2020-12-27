using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDamage : MonoBehaviour
{
    private Collider SpellCollider;

    [SerializeField] private float tickTimeMax;
    private float CurrTickTime;

    // Start is called before the first frame update
    void Start()
    {
        CurrTickTime = tickTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        CurrTickTime -= Time.deltaTime;
    }
}
