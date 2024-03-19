using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTilee : MonoBehaviour
{
    public AudioSource colors;
    public AudioClip s;
    // Start is called before the first frame update
    public void Start()
    {
        colors = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
