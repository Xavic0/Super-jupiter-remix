using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteore : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioClip _sonExplo;//donner le son d'explosion
    [SerializeField] private AudioSource _sonJouer;//source du son
    [SerializeField] Animator _animBoum; //source de l'animation
    [SerializeField] CircleCollider2D _hitBox;// la hit box des meteorite
    
    private float _limiteY = 5 ;//limite y
    private float _limiteX = 12;    //limite x
    private float _speed=5f;//la vitesse de base des meteore
    public bool _filante=false;//la variable d'activation de la pluis de meteore
    private bool _detruit=false;//variable atribuer a un meteore si il est détruit
    
    void Update()
    {
        transform.Translate(Vector3.left*_speed*Time.deltaTime, Space.World);//déplacement a chaque frames vers la gauche
          if(transform.position.x <= -_limiteX){//si ateind la limite, appelle la fonction recycle
         Recycle();
          }
    }
    public void Recycle()// pour faire respawn les meteores
    {
        
    gameObject.SetActive(true);//reactive les meteores
    transform.position= new Vector3(0,10f,0);//donne la position de respawn
    float posDroite= Random.Range( -_limiteY, _limiteY);//les limites de respawn
    transform.position= new Vector3(_limiteX,posDroite,0);//la position x de respawn
    
        if(_filante)//si la variable filante est true...
        {
             float nScale = Random.Range(1.5f,3f);//réduire la taille entre 0.4 et 1.7
             transform.localScale= new Vector3(nScale, nScale, nScale);//donner la petite taille
             _speed= Random.Range(15f, 20f);//donner une nouvelle vitesse plus vite
    
        }
        else//sinon...
        {
            float nScale = Random.Range(1.7f,3.5f);//généré une taille au hazard
            transform.localScale= new Vector3(nScale, nScale, nScale);//attribuer cette taille
             _speed= Random.Range(5f, 10f);//généré une vitesse au hazard
      
        }
    }

    void OnTriggerEnter2D(Collider2D other)//si il rentre en colision avec...
    {
        if (other.gameObject.CompareTag("lazer") ||other.gameObject.CompareTag("joueur"))//...le joueur ou un lazer...
        {
            _animBoum.SetTrigger("eclate");//jouer l'animation d'explosion
            _sonJouer.PlayOneShot(_sonExplo);//jouer le son d'explosion
            _speed=0f;//réduire la vitesse a zero pour stoper le meteore quand il explose
         }
    }
    void ApresAnim()//fonction appeler quand l'animation de l'explosion du meteore est fini
    {

            float respawnTime=Random.Range(0.5f, 5f);//généré un temps aléatoire pour faire réaparaitre le meteore
            gameObject.SetActive(false);//désactive le meteore en question
            Invoke("Recycle",respawnTime);//appelle la fonction recycler apres un nombre random de secondes
    }

}
