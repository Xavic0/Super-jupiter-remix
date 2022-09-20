using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem _lazerExplo;
    
    private float _vitesse = 7f;//la vitesse
    private float _limiteX = 9f;//la limite X
    void Start(){
        
    }

    void Update()//le déplacement du lazer
    {
        
         transform.Translate(Vector3.right * _vitesse * Time.deltaTime, Space.World);//sa fait beaucoup de répétition, va tu le lire ?
        if (transform.position.x > _limiteX)//si la position est hors des limites, détruit le lazer
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)//si est en colision avec un objet dangereux
    {
            
        if (other.gameObject.CompareTag("dangereux") || other.gameObject.CompareTag("meteore") )//si est en colision avec un objet dangereux
        {
            Instantiate(_lazerExplo, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);//détruit le lazer(et l'ennemi ;) je suis fatiguer, j'ai beaucoup trop travailler sur ce devoirs)
        }
    }
    
}
