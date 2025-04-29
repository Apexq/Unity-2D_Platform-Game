using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    AudioManagerMainMenu audioManager; // Ses y�neticisi referans�
    public RectTransform settingsPanel; // Settings panelinin RectTransform referans�
    public GameObject mainMenuButtons; // Ana men�deki tu�lar�n GameObject grubu
    public Vector2 targetPosition = new Vector2(200, 0); // Panelin hedef pozisyonu
    public Vector2 previousPosition = new Vector2(-1420, 0); // Panelin �nceki pozisyonu
    public float slideDuration = 1.0f; // Kayma s�resi (saniye)

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerMainMenu>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManagerMainMenu bulunamad�!");
        }
    }
    public void OnStartButtonPressed()
    {
        SceneManager.LoadScene("lvl1");
        audioManager.playButtonTouch();
    }

    public void OnSettingsButtonPressed()
    {
        Debug.Log("Settings menu a��l�yor...");
        StartCoroutine(HideMainMenuButtonsWithDelay());
        StartCoroutine(SlidePanelToTarget(targetPosition));
        audioManager.playButtonTouch();

    }

    public void OnCloseSettingsButtonPressed()
    {
        Debug.Log("Settings menu kapan�yor...");
        StartCoroutine(SlidePanelToTargetAndShowMainMenu(previousPosition));
        audioManager.playButtonTouch();

    }

    public void OnExitButtonPressed()
    {
        Debug.Log("Oyun kapat�l�yor...");
        audioManager.playButtonTouch();
        Application.Quit();
    }

    private IEnumerator HideMainMenuButtonsWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // Yar�m saniye bekle
        mainMenuButtons.SetActive(false); // Ana men� tu�lar�n� gizle
    }

    private IEnumerator SlidePanelToTarget(Vector2 destination)
    {
        Vector2 startPosition = settingsPanel.anchoredPosition; // Panelin ba�lang�� pozisyonu
        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / slideDuration;
            settingsPanel.anchoredPosition = Vector2.Lerp(startPosition, destination, t); // Pozisyonu yava��a de�i�tir
            yield return null;
        }

        settingsPanel.anchoredPosition = destination; // Hedef pozisyona tam olarak yerle�tir
    }

    private IEnumerator SlidePanelToTargetAndShowMainMenu(Vector2 destination)
    {
        yield return SlidePanelToTarget(destination); // Paneli hedef pozisyona kayd�r
        mainMenuButtons.SetActive(true); // Ana men� tu�lar�n� tekrar g�ster
    }
}
