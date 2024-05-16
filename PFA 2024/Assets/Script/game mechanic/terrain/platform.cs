using Unity.VisualScripting;
using UnityEngine;

public class platform : MonoBehaviour
{
    [SerializeField] private int speed = 2;
    private int actualSpeed;
    public int PlatformSpeed {  get {  return speed; } } 
    private bool direction;
    public bool Side { get { return direction;}}

    void Start()
    {
        Vector3 rotation = transform.rotation.eulerAngles;

        actualSpeed = speed;
        if (transform.position.x > 0)
        {
            direction = false;
            rotation.y = 180;
            transform.rotation = Quaternion.Euler(rotation);
        }
        else
        {
            direction = true;
        }
    }

    void Update()
    {
        transform.position += transform.right * actualSpeed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
