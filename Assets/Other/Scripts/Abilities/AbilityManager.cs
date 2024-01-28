using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.AI;

public class AbilityManager : MonoBehaviour, IAbilityManager
{
    public List<string> AbilitiesNameList = new List<string>();
    public List<IAbility> AbilitiesList = new List<IAbility>();

    public Animator animator;
    public NavMeshAgent agent;
    public bool isAnimal;
    // Start is called before the first frame update
    void Start()
    {
        GetAbilities();

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageAbilities();
        UpdateAnimations();
        CheckAbilitiesCoolDown();
    }


    public void GetAbilities()
    {
        AbilitiesList.Clear();
        GetComponents<IAbility>(AbilitiesList);

        foreach(IAbility ability in AbilitiesList)
        {
            AbilitiesNameList.Add(ability.AbilityName);
        }
    }


    public void CheckAbilitiesCoolDown()
    {
        // Seems redundant. Maybe I'll delete this function
    }


    public void ManageAbilities()
    {
        foreach(IAbility ability in AbilitiesList)
        {
            if(ability.CheckReady() && !ability.CheckIfIgnore())
            {
                ability.RunAbility();
                StartCoroutine(RunAbilityCoroutine(ability));                
                break;
            }
        }
    }

    // Coroutine for running the ability
    IEnumerator RunAbilityCoroutine(IAbility ability)
    {
        while (ability.CheckActive())
        {
            ability.RunAbility();

            yield return null;
        }
    }

    public void UpdateAnimations()
    {
        if(animator != null && agent != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
            animator.SetBool("IsAnimal", isAnimal);
        }
    }
}
