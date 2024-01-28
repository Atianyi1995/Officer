using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbilityManager
{
    public void GetAbilities();
    public void ManageAbilities();
    public void CheckAbilitiesCoolDown();
    public void UpdateAnimations();
}
