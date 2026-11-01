using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Vector3 Target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Target = transform.position;
        Target.z = -10;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Target, 0.005f);
    }

    public void SetTarget(Vector3 targ)
    {
        Target = new Vector3(targ.x, Target.y, transform.position.z);
    }
}
