using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureScrap : MonoBehaviour
{
     private float _limiteY = 5 ;//défini les limites y 
    private float _limiteX = 12;// déffini les limites en x
    private float _speed=3f;//défini la vitesse
    

    void Update()//fait déplacer l'objet
    {
        transform.Translate(Vector3.left*_speed*Time.deltaTime, Space.World);//déplacer d'un unité a chaque frame
          if(transform.position.x <= -_limiteX)//si la position dépasse la limite, fait disparaitre l'objet
          {
            Destroy(gameObject);
          }
          
    }
    
    
    void OnTriggerEnter2D(Collider2D other)//si rentre en colision 
    {
        if (other.gameObject.CompareTag("joueur"))//avec le vaisseau
        {
           Destroy(gameObject);//détruit l'objet
        }
        
    }

  
    
}
