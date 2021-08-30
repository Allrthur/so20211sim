using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UIConsole : MonoBehaviour
{
    public GameObject container;
    public GameObject textline;
    public ScrollRect sr;
    public int t = 0;    
    void Start()
    {

    }

    public void CPrint(string msg)
    {
        GameObject txt = Instantiate(textline, container.transform);
        txt.GetComponent<Text>().text = "t = " +t.ToString() +" - " +msg;

        // coisa de arquivo
        string path = "saida.txt";
        StreamWriter sw = new StreamWriter(path, true);

        // loop de leitura
        sw.WriteLine("t = " +t.ToString() +" - " +msg);

        // fechando arquivo
        sw.Close();


        //autoscroll down
        if (sr.verticalNormalizedPosition <= 0.1)
        {
            sr.verticalNormalizedPosition = 0;
            sr.velocity = new Vector2(0f, 100f);
        }
        else sr.velocity = new Vector2(0f, -10f);

    }


}
