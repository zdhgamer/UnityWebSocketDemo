using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class HttpNetManager : MonoBehaviour {

    private static HttpNetManager instance;

    private static HttpClient httpClient = new HttpClient();

    public static HttpNetManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 异步调用GET方式获取http服务
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callBack"></param>
    private static async void HttpGetAsync(string url, Action<bool,string> callBack)
    {
        httpClient.BaseAddress = new System.Uri(url);
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage respones = await httpClient.GetAsync(url);

        if (respones.IsSuccessStatusCode)
        {
            string content = await respones.Content.ReadAsStringAsync();
            callBack?.Invoke(true, content);
        }
        else
        {
            callBack?.Invoke(false, "");
        }
    }




}
