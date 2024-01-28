using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    public string AbilityName { get; }
    public void RunAbility();
    public void RunCooldown();
    public bool CheckActive();
    public bool CheckReady();
    public bool CheckIfIgnore();

}
