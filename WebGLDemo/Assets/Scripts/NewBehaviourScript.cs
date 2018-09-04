using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    private string baseUrl = "https://www.baidu.com";

    private HttpClient httpClient = new HttpClient();

    private void Awake()
    {
        Debug.Log("--->1");
        Task<string> task = GetHttpResponse();
        Debug.Log("--->3");
    }

    // Use this for initialization
    void Start () {
       // StartCoroutine(GetAsync());
	}
	
	// Update is called once per frame
	void Update () {

	}


    private async Task<string> GetHttpResponse()
    {
        httpClient.BaseAddress = new System.Uri(baseUrl);
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        Debug.Log("--->2");
        HttpResponseMessage respones = await httpClient.GetAsync(baseUrl);
        Debug.Log("--->4");
        if (respones.IsSuccessStatusCode)
        {
            string content = await respones.Content.ReadAsStringAsync();
            Debug.Log(content);
            return content;
        }
        else
        {
            return "";
        }
    }


    private IEnumerator GetAsync() {
        WWW www = new WWW(baseUrl);
        yield return www;
        if (www.isDone && www.error == null) {
            Debug.Log(www.text);
        }
    }
}
