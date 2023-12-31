using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOff : MonoBehaviour
{
    public GameObject fireDashLeft;
    public GameObject fireDashRight;
    public GameObject fireDJumpLeft;
    public GameObject fireDJumpRight;
    public GameObject fireAttackLeft;
    public GameObject fireAttackRight;
    public GameObject fireStompLeft;
    public GameObject fireStompRight;
    public GameObject fireJumpLeft;
    public GameObject fireJumpRight;
    public GameObject fireFlyAwayLeft;
    public GameObject fireFlyAwayRight;
    public GameObject dashEffect;
    public GameObject dashStartL;
    public GameObject dashStartR;
    public GameObject hitCol;
    public GameObject shadow;
    public GameObject startShadow;
    public bool startEnded = false;

    void DashFireOff()
    {
        fireDashLeft.SetActive(false);
        fireDashRight.SetActive(false);
    }

    void AttackFireOff()
    {
        fireAttackLeft.GetComponent<ParticleSystem>().Stop();
        fireAttackRight.GetComponent<ParticleSystem>().Stop();

        hitCol.SetActive(false);
    }

    void DJumpFireOff()
    {
        fireDJumpLeft.SetActive(false);
        fireDJumpRight.SetActive(false);
    }

    void FlyAwayStart()
    {
        fireAttackLeft.GetComponent<ParticleSystem>().Play();
        fireAttackRight.GetComponent<ParticleSystem>().Play();
        fireJumpLeft.GetComponent<ParticleSystem>().Stop();
        fireJumpRight.GetComponent<ParticleSystem>().Stop();
        fireDashLeft.SetActive(false);
        fireDashRight.SetActive(false);
        fireDJumpLeft.SetActive(false);
        fireDJumpRight.SetActive(false);
        fireStompLeft.SetActive(false);
        fireStompRight.SetActive(false);
    }

    void FlyAwayEnd()
    {
        dashEffect.GetComponent<ParticleSystem>().Play();
        dashStartL.GetComponent<ParticleSystem>().Play();
        dashStartR.GetComponent<ParticleSystem>().Play();
        fireAttackLeft.GetComponent<ParticleSystem>().Stop();
        fireAttackRight.GetComponent<ParticleSystem>().Stop();
        fireFlyAwayLeft.SetActive(true);
        fireFlyAwayRight.SetActive(true);
        shadow.SetActive(false);
    }

    void InitialStart()
    {
        fireAttackLeft.GetComponent<ParticleSystem>().Play();
        fireAttackRight.GetComponent<ParticleSystem>().Play();
    }

    void InitialMid()
    {
        fireAttackLeft.GetComponent<ParticleSystem>().Stop();
        fireAttackRight.GetComponent<ParticleSystem>().Stop();
    }

    void InitialEnd()
    {
        startEnded = true;
        startShadow.SetActive(false);
        shadow.SetActive(true);
    }
}
