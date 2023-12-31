using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic : MonoBehaviour
{
    public GameObject damageCol;

    void DamageColOn()
    {
        damageCol.SetActive(true);
    }

    void DamageColOff()
    {
        damageCol.SetActive(false);
    }
}
