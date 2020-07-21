using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public AudioManager audioManager;
    void Start()
    {
        audioManager.PlayMenuMusic();
    }

}
