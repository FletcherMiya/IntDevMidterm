using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{

    public GameObject follow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CurrentPosition = new Vector3(follow.transform.position.x, follow.transform.position.y, -10f);
        transform.position = CurrentPosition;
    }
}
