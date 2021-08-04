using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMedioPrazo
{
    private Escalonador pai; // aponta para o escalonador
    public EMedioPrazo(Escalonador pai){this.pai = pai;} // construtor

    public (int,int) LiberarMP(int memItem, int prioridade) // Liberar MP pede a memoria necessaria para admitir o novo processo
    {
        //Basicamente checa as listas de prontos do rq2 ao rq0 para suspender o processo que ocupa maior espaço lá
        Debug.Log("Função para liberar memoria devido a falta de espaço foi chamada");
        //Debug.Log(Filas.fila_pronto_p1_rq0.Count);
        int maior;
        if(Filas.fila_pronto_p1_rq2.Count > 0){

            maior = checaMaiorProcesso(Filas.fila_pronto_p1_rq2);                   // checa index do maior processo na lista
            if(Filas.fila_pronto_p1_rq2[maior].GetMem() >= memItem){                // checa se maior processo dessa lista é o suficiente para liberar espaço para o novo
                Suspensos.fila_pronto_p1_rq2.Add(Filas.fila_pronto_p1_rq2[maior]);  // se for coloca ele no suspenso
                int retorno = Filas.fila_pronto_p1_rq2[maior].GetMem() - memItem;   // salva a diferença do tamanho dos processos (note que somar isso ao valor da memoria disponivel significa liberar espaço)
                Filas.fila_pronto_p1_rq2.RemoveAt(maior);                           // tira o processo antigo da fila
                Debug.Log("Suspendeu o processo de maior tamanho do rq2");
                return (retorno,2);                                                 // retorna o tamanho do processo novo menos o tamanho do que acabou de ser suspendido 
            }                                                                       // junto de qual fila liberou espaço
            else{ Debug.Log("Tamanho do maior processo no rq2 nao seria suficiente para entrada de novo processo");}
        }
        if(Filas.fila_pronto_p1_rq1.Count > 0){                                     //logica se repete para outras filas
            
            maior = checaMaiorProcesso(Filas.fila_pronto_p1_rq1);
            if(Filas.fila_pronto_p1_rq1[maior].GetMem() >= memItem){ 
                Suspensos.fila_pronto_p1_rq1.Add(Filas.fila_pronto_p1_rq1[maior]);
                int retorno = Filas.fila_pronto_p1_rq1[maior].GetMem() - memItem;
                Filas.fila_pronto_p1_rq1.RemoveAt(maior);
                Debug.Log("Suspendeu o processo de maior tamanho do rq1");
                return (retorno, 1);
            }
            else{ Debug.Log("Tamanho do maior processo no rq1 nao seria suficiente para entrada de novo processo");}
        }
        if((Filas.fila_pronto_p1_rq0.Count > 1) || (prioridade == 0 && Filas.fila_pronto_p1_rq0.Count != 0)){ // Maior que 1 na checagem da rq0 pra nao deixar ficar um ciclo infinito caso seja prioridade 1, se for prioriade 0 vai dar liberar de qualquer forma
                                                                   // podemos mudar esse numero para que a politica fique mais viável para muitos processos
            maior = checaMaiorProcesso(Filas.fila_pronto_p1_rq0);
            if(Filas.fila_pronto_p1_rq0[maior].GetMem() >= memItem){ 
                Suspensos.fila_pronto_p1_rq0.Add(Filas.fila_pronto_p1_rq0[maior]);
                int retorno = Filas.fila_pronto_p1_rq0[maior].GetMem() - memItem ;
                Filas.fila_pronto_p1_rq0.RemoveAt(maior);
                Debug.Log("Suspendeu o processo de maior tamanho do rq0");
                if(Filas.fila_pronto_p1_rq0.Count <= 1 && prioridade == 0) {Debug.Log("O processo tem prioridade e suspendeu outro a força");}
                return ( retorno , 0);
            }
            else{ Debug.Log("Tamanho do maior processo no rq0 nao seria suficiente para entrada de novo processo");}
        }
        Debug.Log("Nao deu pra liberar ninguem");
        return (int.MinValue, -1);
    }

    public int checaMaiorProcesso(List<Processo> fila){
        int maior = 0;
        for (var i = 1; i < fila.Count; i++) {
            if(fila[i].GetMem() > fila[maior].GetMem()) maior = i;
        }
        return maior;
    }
}
