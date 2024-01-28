using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour
{
    public float Life;
    public Animator MyLife;
    public string hurt,die;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        Life -= damage;
        if (Life <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        MyLife.Play(die);
    } 
    public void Hurting()
    {
        MyLife.Play(hurt);
    }
}
