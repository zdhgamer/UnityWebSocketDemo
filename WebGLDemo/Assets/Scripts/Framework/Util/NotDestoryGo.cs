using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotDestoryGo : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
