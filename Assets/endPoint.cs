using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endPoint : MonoBehaviour
{
    [Header("Main Menu Scene Name")]
    public string mainMenuSceneName = "MainMenu"; // Ana men� sahnesinin ad�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �arp��an nesnenin tag'ini kontrol et
        if (collision.gameObject.tag.Equals("Player"))
        {
            // Ana men� sahnesine ge�i� yap
            SceneManager.LoadScene(0);
        }
    }
}
