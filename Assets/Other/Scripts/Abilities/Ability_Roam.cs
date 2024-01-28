using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ability_Roam : MonoBehaviour, IAbility
{
    [Header("Ability Properties")]
    public Enum_Abilities abilityName;
    public string AbilityName { get { return abilityName.ToString(); } }
    public float coolDownTime = 5f;
    public bool active;
    public bool ready;
    public bool ignoreAbility;
    public float minMaxRandRangeLocation = 500f;
    public bool newPosSet;

    public Vector3 newTargetPos;

    public void RunAbility()
    {
        active = true;
        if (GetComponent<Ability_Move>()!= null)
        {
            Ability_Move ability_Move = GetComponent<Ability_Move>();
            if(!newPosSet)
            {
                newTargetPos = GenerateRandomLocation();
                newPosSet = true;
            }
            ability_Move.targetPos = newTargetPos;
            ability_Move.RunAbility();
        }
        if (Vector3.Distance(transform.position, newTargetPos) <= 1)
        {
            RunCooldown();
        }
    }

    Vector3 GenerateRandomLocation()
    {
        return new Vector3(UnityEngine.Random.Range(-minMaxRandRangeLocation,minMaxRandRangeLocation), 
            0,
            UnityEngine.Random.Range(-minMaxRandRangeLocation, minMaxRandRangeLocation));
    }

    public void RunCooldown()
    {
        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        active = false;
        newPosSet = false;
        ready = false;
        yield return new WaitForSeconds(coolDownTime);
        ready = true;
    }

    public bool CheckActive()
    {
        return active;
    }

    public bool CheckReady()
    {
        return ready;
    }

    public bool CheckIfIgnore()
    {
        return ignoreAbility;
    }

    public void EnableIgnoreAbility(bool ignore)
    {
        ignoreAbility = ignore;
    }
}