using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Openning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(JumpToScene());
    }

    // Update is called once per frame
    IEnumerator JumpToScene()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("ExampleLevel");
    }
   
}
