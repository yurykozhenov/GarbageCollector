using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform followed;

    Vector3 offset;
    
    void Start()
    {
        offset = followed.position - transform.position;
    }

    void Update()
    {
        transform.position = followed.position - offset;
    }
}
