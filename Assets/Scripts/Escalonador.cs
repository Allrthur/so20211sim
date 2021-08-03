using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalonador : MonoBehaviour
{

    // Variaveis de referencia a outros scripts
    private ELongoPrazo elp = new ELongoPrazo();
    
    // Processos que a cpu esta executando
    private Processo CPU1 = null;
    private Processo CPU2 = null;
    private Processo CPU3 = null;
    private Processo CPU4 = null;

    // Processo que o disco esta atendendo
    private Processo DISC1 = null;
    private Processo DISC2 = null;
    private Processo DISC3 = null;
    private Processo DISC4 = null;


    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //---------------------------------------------------//

    List<Processo> LerEntrada() // leitor do arquivo de entrada
    {
        List<Processo> falae = new List<Processo>();
        Processo processo = new Processo(10,0,10,101,3);
        Processo processo2 = new Processo(10,0,10,1011,3);
        falae.Add(processo);
        falae.Add(processo2);
        return falae;
    }

    public void Admitir() // Escalonador de Longo Prazo // Rick
    { 
        elp.Admitir(LerEntrada());
    }

    void LiberarMP() // Escalonador de M�dio Prazo // Rick
    { 
        // suspender processos quando a mem�ria encher.
    }

    void Despachar() // Escalonador de Curto Prazo // Juan e Theo
    {
        // Escolhe qual das filas de prioridade v�o executar e em quais CPUs e qual processo pega qual disco.
    }

    void Executar(Processo p, int CPU) // Despachante cahama esse m�todo para mandar uma CPU executar um processo // Juan e Theo // Arthur
    {
        //Randint de espera, simulando a execu��o do c�digo
        if (p.GetDisc() != 0)
        {

        }
        else // ele teve que chamar um disco, bota ele no bloqueado
        {
            //EntradaSaida();
            //Vai pra fila de bloqueados;
        }

    }

    void EntradaSaida(Processo p, int DISCO) // Botar um processo pra segurar o disco pertinente. // Juan e Theo // Arthur
    { 
        //Randint de espera, simulando o tempo de resposta do disco
    }
}
