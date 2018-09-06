using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using LitJson;
using System.Text;

public class HttpNetManager : MonoBehaviour
{

    private static HttpNetManager instance;

    private HttpClient httpClient = new HttpClient();

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
        InitHttpClient();
    }

    /// <summary>
    /// 初始化HttpClient
    /// </summary>
    private void InitHttpClient()
    {
        httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

    }

    /// <summary>
    /// 异步调用GET方式获取http服务
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callBack"></param>
    public async void HttpGetAsync(string url, Action<bool, string> callBack)
    {
        httpClient.BaseAddress = new System.Uri(url);

        HttpResponseMessage response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            callBack?.Invoke(true, content);
        }
        else
        {
            callBack?.Invoke(false, "");
        }
    }

    /// <summary>
    /// 异步调用POST方法提交表单内容
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callBack"></param>
    /// <param name="forms"></param>
    public async void HttpFormPostAsync(string url, Action<bool, string> callBack, params KeyValuePair<string, string>[] forms)
    {
        httpClient.BaseAddress = new System.Uri(url);

        List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>();

        if (forms != null && forms.Length > 0)
        {
            for (int i = 0; i < forms.Length; i++)
            {
                keyValuePairs.Add(forms[i]);
            }
        }

        HttpContent httpContent = new FormUrlEncodedContent(keyValuePairs);
        httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        HttpResponseMessage response = await httpClient.PostAsync(new Uri(url), httpContent);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            callBack?.Invoke(true, content);
        }
        else
        {
            callBack?.Invoke(false, "");
        }
    }

    /// <summary>
    /// 提交一个对象
    /// </summary>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="callBack"></param>
    public async void HttpObjectPostAsync(string url, object data, Action<bool, string> callBack)
    {
        httpClient.BaseAddress = new System.Uri(url);

        string dataJson = JsonUtility.ToJson(data);

        HttpContent httpContent = new StringContent(dataJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PostAsync(new Uri(url), httpContent);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            callBack?.Invoke(true, content);
        }
        else
        {
            callBack?.Invoke(false, "");
        }

    }

}
