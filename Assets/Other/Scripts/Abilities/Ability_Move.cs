using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ability_Move : MonoBehaviour, IAbility
{
    [Header("Ability Properties")]
    public Enum_Abilities abilityName;
    public string AbilityName { get { return abilityName.ToString(); } }
    public float coolDownTime = 0f;
    public bool active;
    public bool ready;
    public bool ignoreAbility = true;

    public Vector3 targetPos;
    public bool targetPosReached;
    NavMeshAgent navMeshAgent;
   
    public void RunAbility()
    {
        active = true;
        if (navMeshAgent == null)
        if (GetComponent<NavMeshAgent>() != null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        if (navMeshAgent != null)
        {
            navMeshAgent.SetDestination(new Vector3(targetPos.x, transform.position.y, targetPos.z));
            if (Vector3.Distance(targetPos, transform.position) <= 1)
            {
                targetPosReached = true;
            }
        }
        else
        {
            Debug.LogError("Navmesh Agent missing! (Add navmesh agent component to "+this.gameObject.name);
        }
    }

    public void RunCooldown() // Not needed for this ability
    {
        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        active = false;
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
}
