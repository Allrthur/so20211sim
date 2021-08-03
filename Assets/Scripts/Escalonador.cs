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
    // Isso faz o mesmo que a fila de bloqueados. Posso tornar isso aq o tempo q o processador leva para liberar?
    private Processo DISC1;
    private Processo DISC2;
    private Processo DISC3;
    private Processo DISC4;

    // Tempo em unidades de tempo
    private int t = 0;
    private int quantum = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        ecp = new ECurtoPrazo(this);
        emp = new EMedioPrazo(this);
        elp = new ELongoPrazo(this);
    }

    void FixedUpdate() // Este eh o loop do simulador, executado a cada unidade de tempo.
    {
        
        Admitir(); // Admite os processos da vez, se houver, liberando MP caso necessario via o ECurtoPrazo
        // Executa os processos nas CPUs
        Executar(CPU1, 1);
        Executar(CPU2, 2); 
        Executar(CPU3, 3); 
        Executar(CPU4, 4);  
        t += 1;
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

    // Metodos com proxy

    public void Admitir() // Escalonador de Longo Prazo // Rick
    { 
        elp.Admitir(LerEntrada());
    }

    public void LiberarMP(int mem) // Escalonador de Medio Prazo // Rick
    { 
        emp.LiberarMP(mem);// suspender processos quando a memoria encher.
    }

    void Despachar(int CPU) // Escalonador de Curto Prazo // Juan e Theo
    {
        ecp.Despachar(CPU);
    }

    // Metodo Executar, chama Despachar do escalonador de curto prazo

    void Executar(Processo p, int CPU) // Despachante chama esse metodo para mandar uma CPU executar um processo // Juan e Theo // Arthur
    {
        if (p.GetDisc() == 0) // se o processo nao pedir discos, execute normalmente ate o fim da fatia de tempo ou fim do processo
        {
            if ((p.GetDuracao() - quantum) <= 0)
            {
                p = null;
                Despachar(CPU);
            }
            else
            {
                p.SetDuração(p.GetDuracao() - quantum);
                Filas.fila_pronto_p0.Add(p);
                Despachar(CPU);
            }
        }
        else // ele teve que chamar um disco, bota ele no bloqueado
        {
            Filas.bloqueados_disc_1.Add(p);
            Despachar(CPU);
            //EntradaSaida();
            //Vai pra fila de bloqueados;
        }

    }

}
