using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        transform.GetComponent<Transform>().position = new Vector3(transform.GetComponent<Transform>().position.x, 1.70f, transform.GetComponent<Transform>().position.z);
    }
}