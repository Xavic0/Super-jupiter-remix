using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuggeur : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<ParticleSystem>().enableEmission=true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
