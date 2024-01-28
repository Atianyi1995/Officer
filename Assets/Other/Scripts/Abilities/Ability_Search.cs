using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Search : MonoBehaviour, IAbility
{
    [Header("Ability Properties")]
    public Enum_Abilities abilityName;
    public string AbilityName { get { return abilityName.ToString(); } }
    public float coolDownTime = 20f;
    public bool active;
    public bool ready;
    public bool ignoreAbility;

    public bool newPosSet;
    public List<GameObject> Targets;
    public GameObject newTarget;
    public Vector3 newTargetPos;
    public float idleTimeAfterSearch = 10f;
    public bool idling;
    bool playAnims;

    public void RunAbility()
    {
        active = true;
        CheckForTargets();
        if(GetComponent<Ability_Move>() != null )
        {
            Ability_Move ability_Move = GetComponent<Ability_Move>();
            ability_Move.targetPos = newTargetPos;
            ability_Move.RunAbility();
            newPosSet = true;
        }
        if (Vector3.Distance(transform.position, newTargetPos) <= 2)
        {
            RunCooldown();
        }
    }

    void CheckForTargets()
    {
        float minDistance = Mathf.Infinity;
        if (Targets != null)
        {
            foreach (GameObject target in Targets)
            {
                if (Vector3.Distance(transform.position, target.transform.position) < minDistance)
                {
                    minDistance = Vector3.Distance(transform.position, target.transform.position);
                    newTargetPos = target.transform.position;
                    Debug.Log("Target Set");
                    newTarget = target;
                }
            }
        }
    }

    public void RunCooldown()
    {
        StartCoroutine(IdlingAfterSearch());
    }    

    IEnumerator IdlingAfterSearch()
    {
        idling = true;
        if (!playAnims)
        {
            PlayAnimations(true);
            playAnims = true;
        }

        yield return new WaitForSeconds(idleTimeAfterSearch);
        idling = false;
        PlayAnimations(false);
        StartCoroutine(CoolDown());
    }
    IEnumerator CoolDown()
    {
        active = false;
        newPosSet = false;
        ready = false;
        playAnims = false;
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

    void PlayAnimations(bool playOrNot)
    {
        if (playOrNot)
        {
            if (GetComponent<Animator>() != null)
            {
                GetComponent<Animator>().SetBool("Stop Dancing", false);
                GetComponent<Animator>().Play("Funny Dance_" + (int)Random.Range(0, 1), 1);
            }
        }
        else
        {
            if (GetComponent<Animator>() != null)
            {
                GetComponent<Animator>().SetBool("Stop Dancing",true);
            }
        }
    }

}
