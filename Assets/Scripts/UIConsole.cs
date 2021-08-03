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

    public Escalonador e;
    
    // Start is called before the first frame update
    void Start()
    {
        e = GetComponent<Escalonador>();
        e.Admitir();
        CPrint(" - Admissão feita com sucesso");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2")) CPrint(Filas.fila_pronto_p0.Count.ToString());

    }

    void CPrint(string msg)
    {
        z = z + 1;

        GameObject txt = Instantiate(textline, container.transform);
        txt.GetComponent<Text>().text = msg;


        //autoscroll down
        if (sr.verticalNormalizedPosition <= 0.1)
        {
            sr.verticalNormalizedPosition = 0;
            sr.velocity = new Vector2(0f, 100f);
        }
        else sr.velocity = new Vector2(0f, -10f);

    }


}
