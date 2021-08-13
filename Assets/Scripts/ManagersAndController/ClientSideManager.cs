using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClientSideManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Dropdown serverSelect;
    public static string ServerLocation="in";
    string[] serverNames = new string[]{ "in", "eu", "us" };
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void HandleInputData(int val)
    {
        Debug.Log(val);
        ServerLocation = serverNames[val];
        Debug.Log(serverNames[val]);
    }
}
