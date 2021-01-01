using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDamage : MonoBehaviour
{
    private Collider SpellCollider;

    [SerializeField] private float Duration;
    [SerializeField] private float tickTimeMax;
    private float CurrTickTime;

    // Start is called before the first frame update
    void Start()
    {
        SpellCollider = gameObject.GetComponent<Collider>();
        SpellCollider.enabled = false;
        CurrTickTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        CurrTickTime -= Time.deltaTime;
        Duration -= Time.deltaTime;

        if (CurrTickTime <= 0.0f)
        {
            StartCoroutine(CollisionTimer());
            CurrTickTime = tickTimeMax;
        }

        if(Duration <= 0.0f)
        {
            Object.Destroy(gameObject);
        }
    }

    IEnumerator CollisionTimer()
    {
        SpellCollider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        SpellCollider.enabled = false;
    }
}
