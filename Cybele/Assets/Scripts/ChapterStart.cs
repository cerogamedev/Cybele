using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ChapterStart : MonoBehaviour
{
    public AudioClip[] audioFiles;
    public string[] textToDisplay;
    public string nextSceneName;
    public TextMeshProUGUI displayText;
    private int currentAudioIndex = 0;
    private AudioSource audioSource;

    public float letterDisplaySpeed = 0.1f;

    private Coroutine displayCoroutine;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayAudio();
    }

    void PlayAudio()
    {
        if (currentAudioIndex >= audioFiles.Length)
        {
            SceneManager.LoadScene(nextSceneName);
            return;
        }

        string currentText = textToDisplay[currentAudioIndex];
        displayCoroutine = StartCoroutine(DisplayTextLetters(currentText));

        audioSource.clip = audioFiles[currentAudioIndex];
        audioSource.Play();

        StartCoroutine(WaitForAudioClip(audioSource.clip.length));
    }

    IEnumerator WaitForAudioClip(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentAudioIndex++;
        PlayAudio();
    }

    IEnumerator DisplayTextLetters(string text)
    {
        displayText.SetText(""); 

        foreach (char letter in text)
        {
            displayText.text += letter; 
            yield return new WaitForSeconds(letterDisplaySpeed); 
        }
    }

    void StopDisplayCoroutine()
    {
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
        }
    }

    private void OnDestroy()
    {
        StopDisplayCoroutine();
    }
}
