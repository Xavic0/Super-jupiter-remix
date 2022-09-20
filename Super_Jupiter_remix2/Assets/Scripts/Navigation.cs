using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
[SerializeField] AudioSource _son;//donner la source du son
[SerializeField] AudioClip _clickSon;//donner le son du click de bouton
[SerializeField] GameObject _Btn;//donner le bouton


private string _nav;//variable pour le nom des sènes
public void clic(string scene){//trouver le nom des boutons de navigation
    _nav=scene;
    _son.PlayOneShot(_clickSon);//jouer le son de click
     GetComponent<Button>().interactable = false;//rendre le bouton inactivable
    Invoke("Delait",0.4f);//appelle la fonction delai en une demi seconde
    if(_nav=="menu"){//si un bouton vers le menu est activer. pour que la musique ne se stop pas d'une scene a l'autre
    var musicIntro = GameObject.FindGameObjectWithTag("musique").GetComponent<AudioSource>();//trouve l'objet avec le tag music
    musicIntro.mute=false;//le demute
    DontDestroyOnLoad(musicIntro);//ne détruit pas l'objet
    }
    if(_nav=="Jeu"||_nav=="Jeu2"||_nav=="Jeu3")//si un bouton vers l'écran jeu est presser
    {
    var musicIntro = GameObject.FindGameObjectWithTag("musique").GetComponent<AudioSource>();//trouve l'audio source dans l'ovjet qui n'est pas détruit on load
    musicIntro.mute=true;//mute
    }
   
}
private void Delait()//fonction qui fait changer la scène après, le delais est pour que le son soit jouer au complet
{
    SceneManager.LoadScene(_nav);//change la scène
}

    
}
