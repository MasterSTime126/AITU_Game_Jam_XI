using UnityEngine;

public class BackgroundStars : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime);
    }
}
