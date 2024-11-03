using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log_Spawn : MonoBehaviour
{
    public GameObject spookyLog;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnLog(spookyLog);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnLog(GameObject resource)
    {
        Instantiate(resource, transform.position, transform.rotation);
    }

    
}
