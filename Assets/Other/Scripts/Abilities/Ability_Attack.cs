using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Ability_Attack : MonoBehaviour, IAbility
{
    [Header("Ability Properties")]
    public Enum_Abilities abilityName;
    public string AbilityName { get { return abilityName.ToString(); } }
    public float coolDownTime = 5f;
    public bool active;
    public bool ready;
    public bool ignoreAbility;

    public bool newPosSet;
    public List<GameObject> Targets;
    public GameObject newTarget;
    public Vector3 newTargetPos;

    public float attackTime = 2f;
    public float attackDamage = 10f;
    public bool attacking,CanHurt;
    bool playAnims;
    public string attackAnimName;

    public void RunAbility()
    {
        active = true;
        CheckForTargets();
        if (GetComponent<Ability_Move>() != null)
        {
            Ability_Move ability_Move = GetComponent<Ability_Move>();
            ability_Move.targetPos = newTargetPos;
            ability_Move.RunAbility();
            newPosSet = true;
        }
        Debug.Log(Vector3.Distance(transform.position, newTarget.transform.position));
        if(newTarget!=null)
        if (Vector3.Distance(transform.position, newTarget.transform.position) <= 2)
        {
            Debug.Log("Attack");
            Attack();
        }
    }

    void Attack()
    {
        // Play Attack Animation
        Debug.Log("Attack Function Triggered");        
        if (!playAnims)
        {
            PlayAnimations();
            playAnims = true;
        }
        // Damage target
        if (newTarget.GetComponent<IDamageable>() != null)
        {
            transform.LookAt(new Vector3(newTarget.transform.position.x,
                                            0,
                                            newTarget.transform.position.z));
            newTarget.GetComponent<IDamageable>().TakeDamage(attackDamage);
        }
        Debug.LogWarning("Attacking target");
        

        RunCooldown();
    }
    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            CanHurt = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            CanHurt = false;
        }
    }
    public void HurtPlayer()
    {
        if(CanHurt)
        newTarget.GetComponent<Hurt>().TakeDamage(20);
    }
    void CheckForTargets()
    {
        float minDistance = Mathf.Infinity;
        if (Targets != null)
        {
            foreach (GameObject target in Targets)
            {
                if (target != null && target.activeInHierarchy)
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
    }

    public void RunCooldown()
    {
        StartCoroutine(CoolDown());
    }
    IEnumerator AttackingTarget()
    {
        attacking = true;
        Debug.LogWarning("Attacking target (Coroutine)");
        if (!playAnims)
        {
            PlayAnimations();
            playAnims = true;
        }
        yield return new WaitForSeconds(attackTime);
        attacking = false;
        StartCoroutine(CoolDown());
    }

    IEnumerator CoolDown()
    {
        playAnims = false;
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


    void PlayAnimations()
    {       
        if (GetComponent<Animator>() != null)
        {
            attackAnimName = "Attack_" + (int)Random.Range(0, 3);
            GetComponent<Animator>().Play(attackAnimName, 1);
        }
       
    }

    public void EnableIgnoreAbility(bool ignore)
    {
        ignoreAbility = ignore;
    }
}