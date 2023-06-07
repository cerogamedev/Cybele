using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CameraControl : MonoBehaviour
{
    public Transform oyuncu; 
    public float takipHizi = 2f; 
    public float kameraMinY = 0f; 

    private float oyuncuBaslangicY; 

    private void Start()
    {
        if (oyuncu == null)
        {
            oyuncu = GameObject.FindGameObjectWithTag("Player").transform;
        }

        oyuncuBaslangicY = oyuncu.position.y;
    }

    private void LateUpdate()
    {
        if (oyuncu != null)
        {
            float oyuncuY = oyuncu.position.y;

            if (oyuncuY > transform.position.y)
            {
                Vector3 newPosition = transform.position;
                newPosition.y = oyuncuY;
                newPosition.y = Mathf.Max(newPosition.y, kameraMinY);
                transform.position = Vector3.Lerp(transform.position, newPosition, takipHizi * Time.deltaTime);
            }
            else
            {
                Vector3 newPosition = transform.position;
                newPosition.y = Mathf.Max(newPosition.y, oyuncuBaslangicY);
                transform.position = Vector3.Lerp(transform.position, newPosition, takipHizi * Time.deltaTime);
            }

            if (transform.position.y - oyuncuY > 6f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
