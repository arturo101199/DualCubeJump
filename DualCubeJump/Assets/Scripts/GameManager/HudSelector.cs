using UnityEngine;

public enum Hud { COUNT = 0, IN_GAME = 1, GAME_OVER = 2, PAUSE = 3}

public class HudSelector : MonoBehaviour
{
    [Header("Order: count, ingame, gameover, pause")]
    public GameObject[] huds;


    public void setHud(Hud hud)
    {
        for (int i = 0; i < huds.Length; i++)
        {
            if (i == (int)hud)
                huds[i].SetActive(true);
            else
                huds[i].SetActive(false);
        }
    }
}
