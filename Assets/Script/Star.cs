using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public float speed;
    public float a;
    Vector3 velocity;
    public RectTransform target;

    public int slot = 1;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0,-1,0)*speed;

        transform.localPosition = new Vector3(-334 + slot * 140, -917, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 acer = (target.position - this.transform.position).normalized * a;
        Vector3 vec = velocity + acer*Time.deltaTime;
        if (vec.y * velocity.y < 0)
        {
            velocity = Vector3.zero;
            transform.position = transform.position + velocity * Time.deltaTime + 0.5f * acer * Time.deltaTime * Time.deltaTime;
        }
        else
        {
            velocity = vec;
            transform.position = transform.position + velocity * Time.deltaTime + 0.5f * acer * Time.deltaTime * Time.deltaTime;
        }

        if(transform.position.y > target.position.y)
        {
            HUBManger.getInstance().addStar(1);
            Destroy(this.gameObject);
        }
    }
}
