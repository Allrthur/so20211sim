using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escalonador
{

    // Processos que a cpu est� executando
    private Processo CPU1;
    private Processo CPU2;
    private Processo CPU3;
    private Processo CPU4;

    // Processo que o disco est� atendendo
    private Processo DISC1;
    private Processo DISC2;
    private Processo DISC3;
    private Processo DISC4;

    // Mem�ria Principal
    public int PMemTotal = 16384;
    private int PMemDisp = 16384;
    
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
        // parte leitora de arquivo? via LerEntrada()
        // nomear processo com SetId();
        List<Processo> dados = this.LerEntrada();
        foreach(var item in dados){
            if(item.GetMem() < this.PMemDisp){
                if (item.GetPrioridade() == 0) Filas.fila_pronto_p1_rq0.Add(item); // Se prioridade for 0, coloca no rq0 de feedback
                else Filas.fila_pronto_p0.Add(item); // Se prioridade for 1, coloca na fila de FCFS
                this.PMemDisp -= item.GetMem();
                Debug.Log(PMemDisp);
            }
        }
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
