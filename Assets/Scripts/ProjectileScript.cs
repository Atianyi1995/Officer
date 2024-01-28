using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f, movespeed = 20;

    public GameObject Slipper;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * movespeed * Time.deltaTime);
        //Slipper.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
    public void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
