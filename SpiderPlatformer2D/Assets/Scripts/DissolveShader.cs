using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveShader : MonoBehaviour
{
    // Start is called before the first frame update
    Material material;
    private float fade=1;
    public float dissolveSpeed;
public bool isDissolving;
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        isDissolving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDissolving)
        {
            fade -= Time.deltaTime* dissolveSpeed;
            if (fade <= 0f)
            {
                fade = 0f;
                isDissolving = false;

            }
            //Debug.Log("fade" + fade);
            //Debug.Log("_Fade"+material.GetFloat("fade"));
            material.SetFloat("_fade", fade);
        }
    }
    public void isDisolveTrue()//animasyonda event ile çağırılacak
    {
        isDissolving = true;
    }
}
