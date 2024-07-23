using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatHighway : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] float speed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
        if(transform.position.x < 1)
        {
            transform.position = startPos;
        }

    }
}
