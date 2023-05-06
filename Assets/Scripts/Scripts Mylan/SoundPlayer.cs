using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour {

    public AudioClip[] sounds; // Liste des sons à jouer
    private int currentSoundIndex = 0; // Indice du son courant
    public float delay = 30f; // Délai entre chaque lecture (en secondes)
    private AudioSource audioSource; // Composant AudioSource pour jouer les sons

    void Start () {
        // Récupère le composant AudioSource de l'objet
        audioSource = GetComponent<AudioSource>();
        // Lance la première lecture de son après le délai initial
        Invoke("PlaySound", delay);
    }

    void PlaySound() {
        // Joue le son courant de la liste
        audioSource.clip = sounds[currentSoundIndex];
        audioSource.Play();
        // Passe au son suivant dans la liste
        currentSoundIndex = (currentSoundIndex + 1) % sounds.Length;
        // Lance la prochaine lecture de son après le délai
        Invoke("PlaySound", delay);
    }
}