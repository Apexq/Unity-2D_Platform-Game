using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yavaslatanDal : MonoBehaviour
{
    private GameObject _player;
    private MainCharacterSlow _playerSlow; // MainCharacterSlow referans�

    public float lifeTime = 5f; // Dal�n ya�am s�resi
    public float slowDuration = 2f; // Yava�latma s�resi
    public float slowMultiplier = 0.5f; // Yava�latma �arpan�

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        if (!_player)
        {
            Debug.LogError("Player bulunamad�!");
        }

        _playerSlow = _player.GetComponent<MainCharacterSlow>();
        if (!_playerSlow)
        {
            Debug.LogError("MainCharacterSlow bile�eni bulunamad�!");
        }
    }

    private void Update()
    {
        // Dal�n ya�am s�resi doldu�unda nesneyi yok et
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {

            if (_playerSlow != null)
            {
                // Yava�latma etkisini uygula
                _playerSlow.ApplySlowEffect(slowMultiplier, slowDuration);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {

            if (_playerSlow != null)
            {
                // Oyuncu dal�n d���na ��kt���nda h�z otomatik olarak eski haline d�necek
                // Bu i�lem MainCharacterSlow taraf�ndan y�netiliyor.
            }
        }
    }
}
