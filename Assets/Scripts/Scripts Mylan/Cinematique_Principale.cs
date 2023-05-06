using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Cinematique_Principale : MonoBehaviour
{
    public VideoPlayer videoPlayer; // r�f�rence au composant VideoPlayer
    public string nextSceneName; // nom de la prochaine sc�ne � charger

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnded; // ajout d'un �v�nement qui se d�clenche � la fin de la vid�o
    }

    void OnVideoEnded(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName); // chargement de la prochaine sc�ne
    }
}