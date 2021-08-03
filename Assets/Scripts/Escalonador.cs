using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Escalonador : MonoBehaviour
{
    // Variaveis que apontam para outras classes
    public ECurtoPrazo ecp;
    public EMedioPrazo emp;
    public ELongoPrazo elp;
    
    // Processos que a cpu esta executando
    private Processo CPU1;
    private Processo CPU2;
    private Processo CPU3;
    private Processo CPU4;

    // Processo que o disco esta atendendo
    private Processo DISC1;
    private Processo DISC2;
    private Processo DISC3;
    private Processo DISC4;

    // Quantum
    private int quantum;
    
    // Start is called before the first frame update
    void Start()
    {
        ecp = new ECurtoPrazo(this);
        emp = new EMedioPrazo(this);
        elp = new ELongoPrazo(this);
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

    void LiberarMP() // Escalonador de Medio Prazo // Rick
    { 
        // suspender processos quando a memoria encher.
    }

    void Despachar() // Escalonador de Curto Prazo // Juan e Theo
    {
        
    }

    void Executar(Processo p, int CPU) // Despachante cahama esse metodo para mandar uma CPU executar um processo // Juan e Theo // Arthur
    {
        //Randint de espera, simulando a execucao do codigo
        if (p.GetDisc() == 0)
        {
            if ((p.GetDuracao() - quantum) <= 0)
            {
                p = null;
                Despachar();
            }
            else
            {
                p.SetDuração(p.GetDuracao() - quantum);
                Filas.fila_pronto_p0.Add(p);
                Despachar();
            }
        }
        else // ele teve que chamar um disco, bota ele no bloqueado
        {
            Filas.bloqueados_disc_1.Add(p);
            Despachar();
            //EntradaSaida();
            //Vai pra fila de bloqueados;
        }

    }

    void EntradaSaida(Processo p, int DISCO) // Botar um processo pra segurar o disco pertinente. // Juan e Theo // Arthur
    { 
        //Randint de espera, simulando o tempo de resposta do disco
    }

}
