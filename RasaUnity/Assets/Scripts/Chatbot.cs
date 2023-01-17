using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;

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
    public Image[] ImageTec = new Image[3];

    private const string rasa_url = "http://localhost:5005/webhooks/rest/webhook";

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
		UnityWebRequest request = new UnityWebRequest(url, "POST");
		byte[] rawBody = new System.Text.UTF8Encoding().GetBytes(jsonBody);
		request.uploadHandler = (UploadHandler)new UploadHandlerRaw(rawBody);
		request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");

		yield return request.SendWebRequest();

        ReceiveResponse(request.downloadHandler.text);
    }

    public void ReceiveResponse(string response)
    {
        // Deserialize response recieved from the bot
        RootReceiveMessageJson root = JsonUtility.FromJson<RootReceiveMessageJson>("{\"messages\":" + response + "}");

        Debug.Log("Bot: " + root.messages[0].text);

        ImageAppear(root.messages[0].text);

    }

    public void ImageAppear(string text)
    {
        // Set all images to transparent
        foreach (Image img in ImageTec)
        {
            img.color = new Color(1, 1, 1, 0.05882353f);
        }

        // Modified color image RGB
        if (text.Contains("Python"))
        {
            ImageTec[0].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("C#"))
        {
            ImageTec[1].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("SQLite"))
        {
            ImageTec[2].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Apache Flink"))
        {
            ImageTec[3].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("R language"))
        {
            ImageTec[4].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Apache Kafka"))
        {
            ImageTec[5].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Swift"))
        {
            ImageTec[6].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("JavaScript"))
        {
            ImageTec[7].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("PostgreSQL"))
        {
            ImageTec[8].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Apache Storm"))
        {
            ImageTec[9].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Keras"))
        {
            ImageTec[10].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Windows"))
        {
            ImageTec[11].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Scala"))
        {
            ImageTec[12].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("PyTorch"))
        {
            ImageTec[13].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Linux"))
        {
            ImageTec[14].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Julia"))
        {
            ImageTec[15].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Mac OS"))
        {
            ImageTec[16].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("MongoDB"))
        {
            ImageTec[17].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("PHP"))
        {
            ImageTec[18].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("BeagleBone Black"))
        {
            ImageTec[19].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Java"))
        {
            ImageTec[20].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Arduino"))
        {
            ImageTec[21].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Apache Spark"))
        {
            ImageTec[22].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Unity"))
        {
            ImageTec[23].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("TensorFlow"))
        {
            ImageTec[24].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("TypeScript"))
        {
            ImageTec[25].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Raspberry Pi"))
        {
            ImageTec[26].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Apache Hadoop"))
        {
            ImageTec[27].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("MySQL"))
        {
            ImageTec[28].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("CryEngine"))
        {
            ImageTec[29].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("C++"))
        {
            ImageTec[30].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("LISP"))
        {
            ImageTec[31].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Kotlin"))
        {
            ImageTec[32].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Oracle"))
        {
            ImageTec[33].color = new Color(1, 1, 1, 1);
        }

        if (text.Contains("Unreal Engine"))
        {
            ImageTec[34].color = new Color(1, 1, 1, 1);
        }

    }
}