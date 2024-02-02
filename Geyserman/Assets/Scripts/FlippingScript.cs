using UnityEngine;

public class FlippingScript : MonoBehaviour
{
    public bool turningLeft=true;
    public bool turningRight=false;

    void Update()
    {
        if (Input.GetKeyDown("d") && turningLeft)
        {
            turningLeft = false;
            turningRight = true;
            transform.localScale = new Vector3(-0.15f, 0.15f, 0.15f);
        }
        if(Input.GetKeyDown("a") && turningRight)
        {
            turningRight = false;
            turningLeft = true;
            transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        }
    }
}
