using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class VaisseauInfini : MonoBehaviour
{   
    [SerializeField] private GameObject _preFabLazer;//donner le prefab de lazer
    [SerializeField] private GameObject _vaisseau;//trouver le vaisseau
    [SerializeField] private Text  _affPv;//donner le Text pour afficher les PV
    [SerializeField] private Text  _affBlind;//donner le Text pour afficher le blindage
    [SerializeField] private Text  _affBar;//donner le Text pour afficher la pression
    [SerializeField] private Meteore[] _meteore;//donner acces au tableau des meteores
    [SerializeField] private Text _MsgFin;//donner le Text pour afficher le message de fin
    [SerializeField] private Text _alerte;//donner le text pour faire le message d'alerte
    [SerializeField] private GameObject _btnMenu;//donner acces au bouton vers le menu
    [SerializeField] private AudioClip[] _sonVaisseau;//donner acces a la source de son
    [SerializeField] private AudioSource _sonJouer;//donner un acces aux son
    [SerializeField] private GameObject _prefabMeteore;
    [SerializeField] private GameObject _prefabEnnemi;
    private Vaisseau _vaisseauS;//trouver l'objet
    private PolygonCollider2D _hitBox;
    float _speed= 9f;//définire la vitesse du vaisseau
    float _limiteX=8f;//déffinire les limites de la zone visible sur l'axe des X
    float _limiteY=4.5f;//déffinire les limites de la zone visible sur l'axe des y

    private bool cooldown=true;//désactiver la capacitée de tiré

    int _pression=0;//définire la pression a 0
    int _loop=0;
    float _structure=10f;//définire le blindage de base du vaisseau
    int _meteoreI=0;
    

    float _PV=100f;//définire  les points de vie
    void awake()//fonction qui active avant que le jeu ce lance
    {
        
    }
    void Start()//fonction qui s'active au lancement du jeu
    {
        _btnMenu.SetActive(false);//faire disparaitre le bouton
        Bars();//appeler la fonction qui gère la pression 
        Quaternion newQuaternion= new Quaternion();
        newQuaternion.Set(0,0,-90,1);
        Instantiate(_prefabEnnemi, new Vector3(-8,0,0), Quaternion.Euler(new Vector3(0,0,90))  );

    }

    void Update()//gère les déplacement et le tire de lazer
    {
        
        float dx= Input.GetAxis("Horizontal");//pour ce déplacer horizontalement avec de l'accélération avec  a et d 
        transform.Translate(Vector3.right*_speed*dx*Time.deltaTime, Space.World);
       
        float clampX = Mathf.Clamp(transform.position.x,-_limiteX,_limiteX);//déplacer d'un unité a chaque frame
         transform.position= new Vector3(clampX, transform.position.y,0);

         float dy= Input.GetAxis("Vertical");//pour ce déplacer verticalement avec de l'accélération avec  w et s 
        transform.Translate(Vector3.up*_speed*dy*Time.deltaTime, Space.World);
       
        float clampY = Mathf.Clamp(transform.position.y,-_limiteY,_limiteY);//déplacer d'un unité a chaque frame
         transform.position= new Vector3(transform.position.x, clampY,0);

          if (Input.GetKey(KeyCode.Space))//si la touche espace est activer
        {
           
            Tirer();//activer la fonction de tire
        }
    }

     private void Tirer()//pour tirer
    {
        
        if(cooldown){//si le cooldown est terminer
        _sonJouer.PlayOneShot(_sonVaisseau[1]);//jouer le son de tire de lazer
        Instantiate(_preFabLazer, _vaisseau.transform.position, Quaternion.identity);//faire aparaitre le prefab du lazer
        cooldown=false;//démarer le cooldown
        Invoke("reLoad", 0.5f);//faire durer l'incapacité de tirer pour 0.3 seconde en appellant la fonction

        }
    }
    private void reLoad(){//fonction avec la variable qui permet de finire le cooldown
        cooldown=true;
    }
    private void Bars()//la fonction  qui gère la pression 
    {
        _pression++;//augmente la pression de un chaque fois que la pression est augmenté
        _loop++;

        Invoke("Bars",1f);//rappeler la fonction chaque seconde
        if(_loop>=10){
            _loop=0;
            _meteoreI++;
            Instantiate(_prefabMeteore, new Vector3(-8f,0,0), Quaternion.identity);
            if(_meteoreI>=3){
                Instantiate(_prefabEnnemi, new Vector3(-8,0,0), Quaternion.Euler(new Vector3(0,0,90))  );
            }


        }

         _affBar.text=_pression+" bar de pression";//afficher 
        if(_pression>_structure)//identifier si la pression est trop élevé
        {
          float a = _pression-_structure;//savoir quel est la différence pression/blindage
          _PV=_PV-a/4;//diminuer de deux les déga prit par la pression
          Actu();//appeler actu pour actualiser les infos
          
        }
    }
    
    
    void OnTriggerEnter2D(Collider2D other)//en cas de colision, s'active
    {
        if (other.gameObject.CompareTag("dangereux"))//si est colision avec des objets dangereux
        {
          _sonJouer.PlayOneShot(_sonVaisseau[2]);//joue le son de dégat subit
           _PV=_PV-15f;//fait perdre 10 pv
           Actu();
        }
         else if(other.gameObject.CompareTag("meteore"))//si est en colision avec un objet de blindage
        {      
            _sonJouer.PlayOneShot(_sonVaisseau[2]);//joue le son de dégat subit
           _structure+=-13f;//fait perdre des points de blindage
           Actu();
        }
        else if(other.gameObject.CompareTag("blindage"))//si est en colision avec un objet de blindage
        {      
            _sonJouer.PlayOneShot(_sonVaisseau[0]);//jou le son de blindage bonus
            _structure+= 10f;//augmente le blindage de 20 points
            _PV+=10;//régénère 10 pv
            Actu();//actualise les infos
        }
        
    }
    private void Actu()//tout les infos qui doivents être actualiser dans l'UI
    {
        _structure=_structure>0?_structure:_structure=0;//si les points de pv son a 0, ne passe pas sous zero
        if(_PV<=0){//fait mourire le vaisseau
               _PV=0;//les PV ne vonts pas plus bas que zero
               Mort();//fait mourire le joueur
           }
           else if(_PV>100){//pour que les Pv ne monte pas au dessus de 100
               _PV=100;
           }
        _affPv.text=(_PV+" PV");//affiche les pv
        _affBlind.text= _structure+" mm de blindage";//affiche le blindage
    }
    private void Mort()//pour la mort
    {
           _btnMenu.SetActive(true);//fait apparaitre le bouton menu pour retourner au menu
        _MsgFin.text="vous avez survécu à "+ _pression+" Bars de pression dans l'athmosphère de Jupiter et "+_meteoreI+" meteores";//afficher le message de fin
        
        
    }

    
}
