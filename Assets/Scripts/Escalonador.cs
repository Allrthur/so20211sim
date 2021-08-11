using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Escalonador : MonoBehaviour
{
    
    
    // Variaveis que apontam para outras classes
    public UIConsole uc;
    public ECurtoPrazo ecp;
    public EMedioPrazo emp;
    public ELongoPrazo elp;
    
    // Processos que a cpu esta executando
    public Processo CPU1;
    public Processo CPU2;
    public Processo CPU3;
    public Processo CPU4;

    // Processo que o disco esta atendendo
    // Isso faz o mesmo que a fila de bloqueados. Posso tornar isso aq o tempo q o processador leva para liberar?
    public Processo DISC1;
    public Processo DISC2;
    public Processo DISC3;
    public Processo DISC4;

    // Tempo em unidades de tempo
    private int t = 0;
    private int quantum = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        uc = GetComponent<UIConsole>();
        ecp = new ECurtoPrazo(this);
        emp = new EMedioPrazo(this);
        elp = new ELongoPrazo(this);

    }

    void FixedUpdate() // Este eh o loop do simulador, executado a cada unidade de tempo.
    {
        //Debug.Log("t = "+ t.ToString() +" ------------------------------------------------");
        Admitir(); // Admite os processos da vez, se houver, liberando MP caso necessario via o EMedioPrazo
        
        // Executa os processos nas CPUs
        if(CPU1 != null)Executar(CPU1, 1); else {ecp.Despachar(1); Executar(CPU1, 1);}
        if(CPU2 != null)Executar(CPU2, 2); else {ecp.Despachar(2); Executar(CPU2, 2);}
        if(CPU3 != null)Executar(CPU3, 3); else {ecp.Despachar(3); Executar(CPU3, 3);}
        if(CPU4 != null)Executar(CPU4, 4); else {ecp.Despachar(4); Executar(CPU4, 4);}
        
        // incrementa o contador de tempo
        t += 1;
        uc.t = t;
    }

    // Metodos de execução
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
        
        Processo processo1 = new Processo(10,1,10,13211,0);
        Processo processo2 = new Processo(10,1,10,101,3);
        Processo processo3 = new Processo(10,1,10,1011,1);
        

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
        /*
        Filas.fila_pronto_p1_rq1.Add(new Processo(10,1,10,1400,3)); // o tamanho desse processo nao foi retirado do valor da memoria disponivel apenas por simplicidade de testes
        Processo processo1 = new Processo(10,1,10,16211,3);
        Processo processo2 = new Processo(10,1,10,101,3);
        Processo processo3 = new Processo(10,1,10,1011,3);
        */
        falae.Add(processo1);
        falae.Add(processo2);
        falae.Add(processo3);
        return falae;
    }
    
    // Metodos com proxy

    public void Admitir() // Escalonador de Longo Prazo // Rick
    { 
        
        List<Processo> davez = new List<Processo>(); // Criar lista de entrada com os processo que chegam no tempo t.
        foreach(Processo p in LerEntradaTestes()) if(p.GetTchegada() == t) davez.Add(p); // loop de filtragem
        if(davez.Count != 0)elp.Admitir(davez); // passar davez para o admitir desta linha
        davez = new List<Processo>();
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
        if(p == null)return;
        uc.CPrint("Executando o processo " + p.ToString() + " na CPU " + CPU.ToString()); //Debug.Log("Executando o processo " + p.ToString() + " na CPU " + CPU.ToString());
        if (p.GetDisc() == 0) // se o processo nao pedir discos, execute normalmente ate o fim da fatia de tempo ou fim do processo
        {
            p.SetDuracao(p.GetDuracao() - 1);
            if (p.GetDuracao() <= 0) // se a execucao do processo acabou, tira da cpu
            {
                uc.CPrint("O processo " + p.ToString() + " finalizou sua execucao");
                RemoverProcessoDaCPU(CPU);
            }
            // se a fatia de tempo do quantum acabou, e o processo tem prioridade 1, incrementa a prioridade
            // prioridade 2, significa que o processo vai para a fila rq1
            // prioridade 3, para a fila rq2
            // prioridade 4 ou maior fica na fila rq2
            else if ((p.GetPrioridade() > 0) && (t % quantum == 0) && t != 0) //quantum nao acaba em t=0
            {
                p.SetPrioridade(p.GetPrioridade() + 1);
                switch(p.GetPrioridade())
                {
                    case 1:
                        Debug.Log("Incrementei um processo de prioridade 0, corrigindo"); // isso nao devia acontecer nunquinha
                        p.SetPrioridade(0);
                    break;
                    case 2:
                        Filas.fila_pronto_p1_rq1.Add(p);
                        uc.CPrint("O processo " + p.ToString() + " foi para RQ1 por fatia de tempo");
                        RemoverProcessoDaCPU(CPU);
                    break;
                    default:
                        Filas.fila_pronto_p1_rq2.Add(p);
                        uc.CPrint("O processo " + p.ToString() + " foi para RQ2 por fatia de tempo");
                        RemoverProcessoDaCPU(CPU);
                    break;
                }
            }
            
            
        }
        else // ele teve que chamar um disco, bota ele no bloqueado
        {
            Filas.bloqueados_disc_1.Add(p);
            uc.CPrint("O processo " + p.ToString() + " foi bloqueado por entrada / saida");
            RemoverProcessoDaCPU(CPU);
        }

    }

    private void RemoverProcessoDaCPU(int CPU) // Chamado pelo executar, tira o processo da CPU e despacha (chama o proximo da fila)
    {
        switch(CPU)
                {
                    case 1:
                        CPU1 = null;
                    break;
                    case 2:
                        CPU2 = null;
                    break;
                    case 3:
                        CPU3 = null;
                    break;
                    case 4: 
                        CPU4 = null;
                    break;
                }
                Despachar(CPU);
                return;
    }
}
