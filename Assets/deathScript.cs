using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathScript : MonoBehaviour
{

    public GameObject deathmenu;
    public GameObject pauseButton;

    public void ShowDeathMenu()
    {
        // �l�m men�s�n� g�ster
        deathmenu.SetActive(true);
        // Oyuncunun hareketini durdur
        pauseButton.SetActive(false);
    }

}
