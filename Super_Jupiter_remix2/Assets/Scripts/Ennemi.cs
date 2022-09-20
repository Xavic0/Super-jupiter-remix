using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour
{
    [SerializeField] private GameObject _preFabLazerEnnemi;//donne le prefab lazer ennemi
    [SerializeField]private GameObject _vaisseauEnnemi;//donne le vaisseau ennemi au vaisseau ennemi
    [SerializeField] private GameObject _blindage;//donne le blindage
    [SerializeField] private AudioClip[] _sonLazerEnnemi;//les sont de tires et d'explosion
    [SerializeField] private AudioSource _sonJouer;//source de son
    [SerializeField] Animator _animBoum;//donne l'animation de l'explosion
    float _limiteY = 5 ;//la limite Y
    float _limiteX = 12;//la lmitie X
    float _speed=1f;   //la vitesse 
    float _cadenceDeTire=0f; //définire la variable de la cadance de tire a zero
    float _RNG;//variable du facteur chance 
    int _PV;//les pv du vaisseau ennemi
   
    void Update()
    {
        
        transform.Translate(new Vector3(-1,_RNG,0)*_speed*Time.deltaTime, Space.World);//déplacement a chaque fram
        _cadenceDeTire= _cadenceDeTire+ Time.deltaTime;   //variable qui compte la vitesse des coups de feux
          if(transform.position.x <= -_limiteX)//si dépasse les limites
          {
            Delait();//appelle la fonction delait
          } 
          if(_cadenceDeTire>=1f)//si la variable cadance de tire atteind 500 frames
          {
              Tirer();//appelle la fonction tirer pour tirer un projectile
              _cadenceDeTire=0;
          }
          if(transform.position.y<= -_limiteY||transform.position.y>= _limiteY){//si touche une des limites y
              _RNG=-_RNG;//change la dirrection y pour celle inverse, pour rebondire sur le mur
          }

    }
       public void Recycle()//fonction pour recycler les vaisseau ennemis
    {       
            _speed=1f; //redonne un vitesse
            _RNG=Random.Range(-0.78f,0.78f);//donne un trajectoire aléatoire
            gameObject.SetActive(true);//active l'objet
            transform.position= new Vector3(0,5f,0);// replace l'objet au spawn point
            float posDroite= Random.Range( -_limiteY, _limiteY);//replace l'objet au spawn point au y
            transform.position= new Vector3(_limiteX,posDroite,0);//replace l'objet au spawn point au x
    }

    private void Tirer()//fonction de tire de projectile
    {
        _sonJouer.PlayOneShot(_sonLazerEnnemi[1]);//jou le son le tire de lazer ennemi
        
        Instantiate(_preFabLazerEnnemi, _vaisseauEnnemi.transform.position, Quaternion.identity);//génère le prefab lazer
    }

      void OnTriggerEnter2D(Collider2D other)//en colision avec un autre objet...
    {
        if (other.gameObject.CompareTag("lazer")||other.gameObject.CompareTag("joueur"))//si est en colision avec un joueur ou un lazer...
        {
            _PV++;//gagne un unitié de pv
            if(_PV>=3)//si les pv sont a égales a 3
            {
                _PV=0;//réduit les pv a zero
                _animBoum.SetTrigger("meurt");//active l'animation d'explosion
                _sonJouer.PlayOneShot(_sonLazerEnnemi[0]);//jou le son de lazerEnnemi
                Instantiate(_blindage, _vaisseauEnnemi.transform.position, Quaternion.identity);//fait aparaitre un bonus de blindage
              _speed=0;//met la vitesse a 0 pour l'animation d'explosion
            }
        }
    }
    void Delait()//fonction qui delait le respawn du vaisseau
    {
        gameObject.SetActive(false);//fait disparaitre le vaisseau
            float respawnTime=Random.Range(2f, 10f);//génère une variable aléatoire entre 2 et 10 secondes
            Invoke("Recycle",respawnTime);//fait réaparaitre le vaisseau apres le nob
    }
}
      
