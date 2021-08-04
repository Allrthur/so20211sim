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
    List<Processo> LerEntrada() {
        List<Processo> falae = new List<Processo>();

        Processo processo1 = new Processo(10,1,10,13211,3);
        Processo processo2 = new Processo(10,1,10,101,3);
        Processo processo3 = new Processo(10,1,10,1011,3);

        falae.Add(processo1);
        falae.Add(processo2);
        falae.Add(processo3);

        return falae;
    }
    List<Processo> LerEntradaTestes() // leitor do arquivo de entrada
    {
        List<Processo> falae = new List<Processo>();
        // Alguns casos possiveis de entrada, basta descomentar um dos casos que estao entre /* */ e comentar o que estava ativo
        // Passo a passo será mostrado no console

        //Caso 1: Todos entram de primeira
        /*
        Processo processo1 = new Processo(10,1,10,13211,3);
        Processo processo2 = new Processo(10,1,10,101,3);
        Processo processo3 = new Processo(10,1,10,1011,3);
        */

        //Caso 2: Os dois primeiros enchem a memoria e o terceiro tem que suspender um deles, escolhendo o maior
        /*
        Processo processo1 = new Processo(10,1,10,16211,3);
        Processo processo2 = new Processo(10,1,10,101,3);
        Processo processo3 = new Processo(10,1,10,1011,3);
        */

        //Caso 3: Primeiro processo gigante entra, segundo processo com prioridade força sua saida liberando espaço suficiente para o terceiro processo entrar de primeira
        /*
        Processo processo1 = new Processo(10,1,10,16311,3);
        Processo processo2 = new Processo(10,0,10,101,3);
        Processo processo3 = new Processo(10,1,10,1011,3);
        */

        //Caso 4: Um processo de alta prioridade de grande tamanho barra outros processos não importando qual prioridade deles
        /*
        Processo processo1 = new Processo(10,0,10,16311,3);
        Processo processo2 = new Processo(10,0,10,101,3);
        Processo processo3 = new Processo(10,1,10,1011,3);
        */

        //Caso 5: A memoria está cheia, há um processo de baixa prioridade em uma das filas anteriores ao rq0, porém seu espaço ocupado 
        //        na memoria nao é o suficiente para dar lugar ao novo processo, o forçando a continuar procurando mesmo que seja de alta prioridade
        /*
        Filas.fila_pronto_p1_rq2.Add(new Processo(10,1,10,10,3)); // o tamanho desse processo nao foi retirado do valor da memoria disponivel apenas por simplicidade de testes
        Processo processo1 = new Processo(10,1,10,16211,3);
        Processo processo2 = new Processo(10,1,10,101,3);
        Processo processo3 = new Processo(10,0,10,1011,3);
        */
        //Caso 6: Um processo novo ao tentar suspender outro vai sempre começar procurando na lista rq2, depois na rq1 e depois na rq0

        Filas.fila_pronto_p1_rq1.Add(new Processo(10,1,10,1400,3)); // o tamanho desse processo nao foi retirado do valor da memoria disponivel apenas por simplicidade de testes
        Processo processo1 = new Processo(10,1,10,16211,3);
        Processo processo2 = new Processo(10,1,10,101,3);
        Processo processo3 = new Processo(10,1,10,1011,3);

        falae.Add(processo1);
        falae.Add(processo2);
        falae.Add(processo3);
        return falae;
    }

    // Metodos com proxy

    public void Admitir() // Escalonador de Longo Prazo // Rick
    { 
        //elp.Admitir(LerEntrada());
        elp.Admitir(LerEntradaTestes());
    }

    public (int,int) LiberarMP(int mem, int prioridade) // Escalonador de Medio Prazo // Rick
    { 
        return emp.LiberarMP(mem, prioridade);// suspender processos quando a memoria encher.
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
