using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ELongoPrazo
{
    private Escalonador pai;
    private int pCounter = 0;
    
    public ELongoPrazo (Escalonador pai){this.pai = pai;}
    
    public void Admitir(List<Processo> dados)
    {
    
        foreach(var item in dados){
            //Debug.Log("---NOVO PROCESSO---");
            item.SetId(GerarId());
            if(item.GetMem() < pai.PMemDisp){
                if (item.GetPrioridade() == 1)
                {
                    Filas.fila_pronto_p1_rq0.Add(item); // Se prioridade for 1, coloca no rq0 de feedback
                    pai.uc.CPrint(item.GetId()+" Foi admitido na fila RQ0 de Feedback");
                } 
                else{
                    Filas.fila_pronto_p0.Add(item); // Se prioridade for 0, coloca na fila de FCFS
                    pai.uc.CPrint(item.GetId()+" Foi admitido na fila de prioridade FCFS");//Debug.Log("Entrou na fila de prioridade");
                }
                pai.PMemDisp -= item.GetMem();
                //Debug.Log("Admitiu de primeira");
                //Debug.Log("Foi admitido depois de suspender um processo - FALSE");
                continue;
                //Debug.Log(PMemDisp);
            }
            else{
                var retorno = pai.LiberarMP(item.GetMem(), item.GetPrioridade());   // tenta suspender algum processo e retorna o valor liberado na MP já contando 
                int valor = retorno.Item1;                                          // com a entrada do novo processo e retornando também de qual fila foi suspendido
                if(valor == int.MinValue) {
                    pai.uc.CPrint(item.GetId()+" Nao pode ser admitido por falta de memoria");//Debug.Log("Tentou liberar mas não deu");
                    continue;
                }
                pai.PMemDisp += valor;

                if(item.GetPrioridade() == 0){
                    Filas.fila_pronto_p0.Add(item);
                    pai.uc.CPrint(item.GetId()+" Foi admitido na fila de prioridade FCFS");//Debug.Log("Entrou na fila de prioridade");
                }
                else{
                    Filas.fila_pronto_p1_rq0.Add(item);
                    switch (retorno.Item2)
                    {
                        case 0:
                            //Debug.Log("Entrou  na rq0 após suspender um processo na fila rq0");
                            break;
                        case 1:
                            //Debug.Log("Entrou  na rq0 após suspender um processo na fila rq1");
                            break;
                        case 2:
                            //Debug.Log("Entrou  na rq0 após suspender um processo na fila rq2");
                            break;
                        default:
                            //Debug.Log("Essa linha não deveria ser printada, houve algum erro");
                            return;
                    }
                }
                pai.uc.CPrint(item.GetId()+" Foi admitido na fila RQ0 de Feedback");//Debug.Log("Foi admitido depois de suspender um processo - TRUE");
                continue;

            }
        }
        //Debug.Log("Entrada vazia");
    }

    private string GerarId()
    {
        pCounter += 1;
        if(pCounter < 10)return "000" + pCounter.ToString();
        if(pCounter < 100)return "00" + pCounter.ToString();
        if(pCounter < 1000)return "0" + pCounter.ToString();
        else return pCounter.ToString();
    }
}
