using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
    public GameObject platformPrefab; // Oluþturulacak platform prefabý
    public GameObject[] enemyMob;
    public Transform oyuncu; // Oyuncu referansý
    public float yaratmaMesafesi = 50f; // Oyuncudan ne kadar yukarýya kadar platform yaratýlacaðý
    public float yaratmaAraligi = 2f; // Platformlarýn yaratma aralýðý

    private float sagSinir; // Kamera sað sýnýrý
    private float solSinir; // Kamera sol sýnýrý
    private float sonYaratilanY; // Son yaratýlan platformun y pozisyonu

    private void Start()
    {
        // Kameranýn sol ve sað sýnýrlarýný dünya koordinatlarýna dönüþtürerek sýnýrlarý belirle
        Vector3 sagKose = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        Vector3 solKose = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));

        sagSinir = sagKose.x;
        solSinir = solKose.x;

        // Baþlangýçta hiç platform oluþturulmadýðý için son yaratýlan y pozisyonunu sol sýnýra ayarla
        sonYaratilanY = solSinir;
    }

    private void Update()
    {
        if (oyuncu != null)
        {
            // Oyuncunun mevcut y pozisyonunu al
            float oyuncuY = oyuncu.position.y;

            // Oyuncu yaratmaMesafesi birim yukarýya çýktýkça ve son yaratýlan platformun y pozisyonundan uzaklaþtýkça yeni platform yarat
            for (float y = sonYaratilanY + yaratmaAraligi; y <= oyuncuY + yaratmaMesafesi; y += yaratmaAraligi)
            {
                YaratPlatform();
            }
        }
    }

    private void YaratPlatform()
    {
        // Yeni platformu yarat
        float rastgeleX = Random.Range(solSinir+3, 0);
        float rastgeleX2 = Random.Range(0, sagSinir - 3);

        Vector3 pozisyon = new Vector3(rastgeleX, sonYaratilanY, 0f);
        Vector3 pozisyon2 = new Vector3(rastgeleX2, sonYaratilanY + 2.5f, 0f);

        int randomNum = Random.Range(0, 100);
        if (randomNum<10)
        {
            int randomEnemy = Random.Range(0, enemyMob.Length);
            Instantiate(enemyMob[randomEnemy], new Vector2(pozisyon.x, pozisyon.y+1), Quaternion.identity);

        }

        Instantiate(platformPrefab, pozisyon, Quaternion.identity);
        Instantiate(platformPrefab, pozisyon2, Quaternion.identity);


        // Oluþturulan platformun y pozisyonunu son yaratýlan y pozisyonu olarak güncelle
        sonYaratilanY += yaratmaAraligi;
    }
}
