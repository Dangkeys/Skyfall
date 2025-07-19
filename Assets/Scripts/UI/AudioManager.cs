using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioToPlay;
    public Button startButton;
    public string gameSceneName = "GameScene";
    
    private static AudioManager instance;

    void Awake()
    {
        // Ensure this AudioManager persists across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // If another AudioManager already exists, destroy this one
            Destroy(this.gameObject);
            return;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupButtonHoverEvents();
    }

    public void OnStartButtonClicked()
    {
        if (audioToPlay != null && audioToPlay.clip != null)
        {
            audioToPlay.Play();
            Debug.Log("Click audio played successfully.");

            // Wait for audio to finish before loading scene
            StartCoroutine(WaitForAudioAndLoadScene(audioToPlay.clip.length));
        }
        else
        {
            Debug.LogError("Audio Source or its clip not set in AudioManager script!");
        }
    }

    private void SetupButtonHoverEvents()
    {
        if (startButton != null)
        {
            EventTrigger trigger = startButton.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = startButton.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
            pointerEnter.eventID = EventTriggerType.PointerEnter;
            pointerEnter.callback.AddListener((data) => { HoverStartBtnAudio(); });
            trigger.triggers.Add(pointerEnter);
        }
        else
        {
            Debug.LogError("Start Button reference not set in AudioManager!");
        }
    }

    void Update()
    {

    }

    private IEnumerator WaitForAudioAndLoadScene(float audioDuration)
    {
        yield return new WaitForSeconds(audioDuration);

        audioToPlay.Play();
        Debug.Log("Audio finished, loading scene...");
        SceneManager.LoadScene(gameSceneName);
    }

    public void PlayStartSFX()
    {
        audioToPlay.Play();
    }


    public void HoverStartBtnAudio()
    {
        if (audioToPlay != null)
        {
            audioToPlay.Play();
            Debug.Log("Hover audio played successfully.");
        }
        else
        {
            Debug.LogError("Audio Source reference not set in AudioManager script!");
        }
    }

    public void PlayStartBtnAudio()
    {
        if (audioToPlay != null)
        {
            audioToPlay.Play();
            Debug.Log("Audio played successfully.");
        }
        else
        {
            Debug.LogError("Audio Source reference not set in AudioManager script!");
        }
    }
}
