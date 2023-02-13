using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum Language
{
    uk,
    pl,
    es,
    en
}


public class ConvertTextToSpeach : MonoBehaviour
{
    public static ConvertTextToSpeach Instance;
    public Action<AudioClip> OnSuccessfullyConvertTextToAudioAction;
    [SerializeField] private Text InputText;
    [SerializeField] private Language language;
    private const string urlGoogleTranslate = "https://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q=";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GlobalEventManager.ConvertTextToSpeachEvent.AddListener(ConvertTextToAudio);
    }

    public void ConvertTextToAudio()
    {
        StartCoroutine(Converting());
    }

    private IEnumerator Converting()
    {
        string url = urlGoogleTranslate + InputText.text + "&tl=" + language.ToString();
        AudioClip audio;
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                audio = DownloadHandlerAudioClip.GetContent(www);
                OnSuccessfullyConvertTextToAudioAction?.Invoke(audio);
            }
            else
            {
                Debug.LogError("Error: " + www.error);
            }
        }

        //OutputText.text = InputText.text;
        //InputText.text = null;
    }
}
