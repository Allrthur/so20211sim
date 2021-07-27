using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConsole : MonoBehaviour
{
    public GameObject container;
    public GameObject textline;
    public ScrollRect sr;
    private int z = 0;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2")) CPrint();

    }

    void CPrint()
    {
        z = z + 1;

        GameObject txt = Instantiate(textline, container.transform);
        txt.GetComponent<Text>().text = " - Linha " + z.ToString();


        //autoscroll down
        if (sr.verticalNormalizedPosition <= 0.1)
        {
            sr.verticalNormalizedPosition = 0;
            sr.velocity = new Vector2(0f, 100f);
        }
        else sr.velocity = new Vector2(0f, -10f);

    }


}
