using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BB : MonoBehaviour {

	// Use this for initialization
	void Start () {
        HttpNetManager.Instance.HttpGetAsync("http://www.baidu.com", (result, content) =>
        {
            Debug.Log(content);
        });

        KeyValuePair<string, string> phoneNumber = new KeyValuePair<string, string>("phoneNumber", "15928146616");
        KeyValuePair<string, string> passWord = new KeyValuePair<string, string>("passWord", "passWord");

        HttpNetManager.Instance.HttpFormPostAsync("http://10.2.10.157:8888/Struts2_Project/UserLoginServletAction", (result, content) =>
        {
            Debug.Log(content);
        },
        phoneNumber, passWord);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
