using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Windows.Speech;

// A struct to help in creating the Json object to be sent to the rasa server
public class PostMessageJson
{
    public string message;
    public string sender;
}

[Serializable]
// A class to extract multiple json objects nested inside a value
public class RootReceiveMessageJson
{
    public ReceiveMessageJson[] messages;
}

[Serializable]
// A class to extract a single message returned from the bot
public class ReceiveMessageJson
{
    public string recipient_id;
    public string text;
}

public class Chatbot : MonoBehaviour
{
    [Header("Attributes")]
    public string npcMsg;
    [Header("UI")]
    public TextMeshProUGUI npcText;
    public GameObject buttonVoice;
    public TMP_InputField eraseText;

    private bool isOff = true;
    
    private DictationRecognizer dictationRecognizer;
    
    private const string rasa_url = "http://localhost:5005/webhooks/rest/webhook";

    public void ChangeColorButton()
    {
        if (isOff)
        {
            buttonVoice.GetComponent<Image>().color = Color.green;
            isOff = false;
            //npcText.text = "";
            //eraseText.text = "";

            // Activate voice
            dictationRecognizer = new DictationRecognizer();
            dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;
            dictationRecognizer.Start();
        }
        else
        {
            buttonVoice.GetComponent<Image>().color = Color.red;
            isOff = true;
            eraseText.text = "";
            dictationRecognizer.Stop();
        }
        
    }

    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
    {
        eraseText.text = text;
        SendMessageToRasa(text);
    }

    public void SendMessageToRasa(string s)
    {
        Debug.Log("Input: " + s);
        // Create a json object from user message
        PostMessageJson postMessage = new PostMessageJson
        {
            sender = "user",
            message = s
        };

        string jsonBody = JsonUtility.ToJson(postMessage);
        print("User json : " + jsonBody);

        // Create a post request with the data to send to Rasa server
        StartCoroutine(PostRequest(rasa_url, jsonBody));
    }

    private IEnumerator PostRequest(string url, string jsonBody)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] rawBody = new System.Text.UTF8Encoding().GetBytes(jsonBody);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(rawBody);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            ReceiveResponse(request.downloadHandler.text);
        }
    }

    public void ReceiveResponse(string response)
    {
        // Deserialize response recieved from the bot
        RootReceiveMessageJson root = JsonUtility.FromJson<RootReceiveMessageJson>("{\"messages\":" + response + "}");

        if (root.messages != null && root.messages.Length > 0)
        {
            Debug.Log("Bot: " + root.messages[0].text);

            // Display the bot's response
            npcText.text = root.messages[0].text;
        }
        else
        {
            Debug.LogWarning("No messages received from the bot.");
        }

    }

}