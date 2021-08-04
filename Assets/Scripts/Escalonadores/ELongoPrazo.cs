using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ELongoPrazo
{
    private Escalonador pai;
    
    // Memoria Principal
    private const int PMemTotal = 16384;
    private int PMemDisp = 16384;
    
    public ELongoPrazo (Escalonador pai){this.pai = pai;}    
    
    public void Admitir(List<Processo> dados)
    {
    
        foreach(var item in dados){
            Debug.Log("---NOVO PROCESSO---");
            if(item.GetMem() < this.PMemDisp){
                if (item.GetPrioridade() == 1) Filas.fila_pronto_p1_rq0.Add(item); // Se prioridade for 1, coloca no rq0 de feedback
                else Filas.fila_pronto_p0.Add(item); // Se prioridade for 0, coloca na fila de FCFS
                this.PMemDisp -= item.GetMem();
                Debug.Log("Admitiu de primeira");
                continue;
                //Debug.Log(PMemDisp);
            }
            else{
                var retorno = pai.LiberarMP(item.GetMem(), item.GetPrioridade());
                int valor = retorno.Item1;
                Debug.Log(valor);
                if(valor == int.MinValue) {
                    Debug.Log("Tentou liberar mas não deu");
                    continue;
                }
                PMemDisp += valor;

                if(item.GetPrioridade() == 0){
                    Filas.fila_pronto_p0.Add(item);
                    Debug.Log("Entrou na fila de prioridade");
                }
                else{
                    switch (retorno.Item2)
                    {
                        case 0:
                            Filas.fila_pronto_p1_rq0.Add(item);
                            Debug.Log("Entrou na fila rq0");
                            break;
                        case 1:
                            Filas.fila_pronto_p1_rq1.Add(item);
                            Debug.Log("Entrou na fila rq1");
                            break;
                        case 2:
                            Filas.fila_pronto_p1_rq2.Add(item);
                            Debug.Log("Entrou na fila rq2");
                            break;
                        default:
                            Debug.Log("Essa linha não deveria ser printada, houve algum erro");
                            return;
                    }
                }
                Debug.Log("Sendo que foi admitido depois de suspender um processo");
                continue;

            }
        }
        Debug.Log("Entrada vazia");
    }
}
