using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Cinematique_Principale : MonoBehaviour
{
    public VideoPlayer videoPlayer; // référence au composant VideoPlayer
    public string nextSceneName; // nom de la prochaine scène à charger

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnded; // ajout d'un événement qui se déclenche à la fin de la vidéo
    }

    void OnVideoEnded(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName); // chargement de la prochaine scène
    }
}