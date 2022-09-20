using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerEnnemi : MonoBehaviour
{
    
    private float _vitesse = 7f;//défini les limites de la zone
    private float _limiteX = 9f;//défini les limites de la zone 
    private float _RNGTire=0f;//donner une dirrection au hazard au projectile
     void Start()//au départ, 
    {
        _RNGTire=Random.Range(-0.5f,0.5f);//donne une direction aléatoire aux lazer
    }

    void Update()
    {

         transform.Translate(new Vector3(-1,_RNGTire,0) * _vitesse * Time.deltaTime, Space.World);//déplacer d'un unité a chaque frame
        if (transform.position.x < -_limiteX)//si les limites sont franchis
        {
            Destroy(gameObject);//détruit le lazer
        }
    }
     void OnTriggerEnter2D(Collider2D other)//si est en colision ...
    {
        if (other.gameObject.CompareTag("joueur")||other.gameObject.CompareTag("lazer"))//avec le joueur ou un lazer du joueur
        {
         Destroy(gameObject);//détruit le lazer
        }
    }
}
