using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void jumpMessage()
    {
        tmp.enabled = true;
        tmp.text = "Your Jump power has been increased";
        Invoke("DisableText", 5f);
    }
    public void ropeMessage()
    {
        tmp.enabled = true;
        tmp.text = "Press F to use your slingshot when attached to a surface";
        Invoke("DisableText", 5f);
    }
    void DisableText()
    { 
        tmp.enabled = false;
    }  
}
