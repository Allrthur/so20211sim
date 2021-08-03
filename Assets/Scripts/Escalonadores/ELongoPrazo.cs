using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ELongoPrazo
{
    // Memoria Principal
    private const int PMemTotal = 16384;
    private int PMemDisp = 16384;
    
    public void Admitir(List<Processo> dados)
    {
        foreach(var item in dados){
            if(item.GetMem() < this.PMemDisp){
                if (item.GetPrioridade() == 1) Filas.fila_pronto_p1_rq0.Add(item); // Se prioridade for 0, coloca no rq0 de feedback
                else Filas.fila_pronto_p0.Add(item); // Se prioridade for 1, coloca na fila de FCFS
                this.PMemDisp -= item.GetMem();
                //Debug.Log(PMemDisp);
            }
        }

    }
}
