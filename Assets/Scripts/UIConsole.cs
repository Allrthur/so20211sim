using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConsole : MonoBehaviour
{
    public Text ctext;
    public ScrollRect sr;
    private int z = 0;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            z = z + 1;
            ctext.text = ctext.text + "\n - Linha " + z.ToString();
            sr.velocity = new Vector2(0f, 25f);
        }
 
        
    }


}
