using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterSlow : MonoBehaviour
{
    private MainCharacterController _mainCharacterController;
    private bool isSlowed = false; // Yava�latma etkisinin aktif olup olmad���n� takip eder

    private void Start()
    {
        _mainCharacterController = GetComponent<MainCharacterController>();
        if (_mainCharacterController == null)
        {
            Debug.LogError("MainCharacterController bile�eni bulunamad�!");
        }
    }

    public void ApplySlowEffect(float slowMultiplier, float duration)
    {
        if (isSlowed) return; // E�er yava�latma etkisi zaten aktifse, yeni bir etki uygulanmaz

        if (_mainCharacterController != null)
        {
            isSlowed = true; // Yava�latma etkisini aktif olarak i�aretle
            float originalMagnitude = _mainCharacterController.runVectorMagnitude;

            // H�z� yava�lat
            _mainCharacterController.runVectorMagnitude *= slowMultiplier;

            // Belirli bir s�re sonra h�z� eski haline d�nd�r
            StartCoroutine(ResetRunVectorMagnitudeAfterDelay(originalMagnitude, duration));
        }
    }

    private IEnumerator ResetRunVectorMagnitudeAfterDelay(float originalMagnitude, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_mainCharacterController != null)
        {
            _mainCharacterController.runVectorMagnitude = originalMagnitude;
        }

        isSlowed = false; // Yava�latma etkisini s�f�rla
    }
}
