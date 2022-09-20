using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
     private float _limiteY = 5 ;//défini la limite sur l'axe des Y 
    private float _limiteX = 12;//défini la limite sur l'axe des X 
    private float _speed=3f;//défini la vitesse
  
    void Update()//fait déplacer l'objet
    {
        transform.Translate(Vector3.left*_speed*Time.deltaTime, Space.World);//déplacer d'un unité a chaque frame
          if(transform.position.x <= -_limiteX)//si la position dépasse la limite, fait disparaitre l'objet
          {
            Delait();//appelle la fonction délait
          }
          
    }
     public void Recycle()//réutilise l'objet
    {
     
    gameObject.SetActive(true);//active l'objet
    transform.position= new Vector3(0,5f,0);//remet l'objet au bout de l'écran
    float posDroite= Random.Range( -_limiteY, _limiteY);//détermine les limites sur l'axe Y pour l'objet
    transform.position= new Vector3(_limiteX,posDroite,0);//détermine les limites sur l'axe X pour l'objet
     
    }
    void OnTriggerEnter2D(Collider2D other)//si rentre en colision avec un objet
    {
        if (other.gameObject.CompareTag("joueur"))//si touche le joueur
        {
         
           Delait(); //active la fonction délait
        }
        
    }

    private void Delait()//pour faire attendre un peu avant de faire  répop
    {
        gameObject.SetActive(false);//désactive l'objet
            float respawnTime=Random.Range(2f, 7f);//détermine le temps de repop au hazard
            Invoke("Recycle",respawnTime);//active la fonction recycler
    }
    
}
