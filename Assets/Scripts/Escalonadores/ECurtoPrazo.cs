using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECurtoPrazo
{
    private Escalonador pai; // aponta para o antigo escalonador    
    public ECurtoPrazo(Escalonador pai){this.pai = pai;} //construtor de classe

    // Despachante eh chamado toda vez que uma CPU estiver livre, vide metodo Executar no Escalonador

    public void Despachar(int CPU)
    {
        // ------------------------------------------------------------------//
        //  ATENCAO: A politica de escalonamento ainda nao esta implementada //
        // ------------------------------------------------------------------//
        foreach(Processo p in Filas.fila_pronto_p0) // para cada processo na fila de pronto p0 
        {
            if(DespacharProcesso(p, CPU))
            {
                Filas.fila_pronto_p0.Remove(p);
                return;
            }
        }

        foreach(Processo p in Filas.fila_pronto_p1_rq0) // para cada processo na fila de pronto p1, em rq0 
        {
            if(DespacharProcesso(p, CPU))
            {
                Filas.fila_pronto_p1_rq0.Remove(p);
                return;
            }
        }

        foreach(Processo p in Filas.fila_pronto_p1_rq1) // para cada processo na fila de pronto p1, em rq1 
        {
            if(DespacharProcesso(p, CPU))
            {
                Filas.fila_pronto_p1_rq1.Remove(p);
                return;
            }
        }

        foreach(Processo p in Filas.fila_pronto_p1_rq2) // para cada processo na fila de pronto p0, em rq2 
        {
            if(DespacharProcesso(p, CPU))
            {
                Filas.fila_pronto_p1_rq2.Remove(p);
                return;
            }
        }
    }

    private bool DespacharProcesso(Processo p, int CPU) // metodo usado pelo despachar para analisar um processo por vez
    {
        if(p.GetDisc() <= ContarDiscosLivres()) // se o processo precisa de menos processos do que há em discos livres
        {
            int discsneeded = p.GetDisc(); // coloca o processo nos discos livres

            if(pai.DISC1 == null && discsneeded > 0) {pai.DISC1 = p; discsneeded--;}
            if(pai.DISC2 == null && discsneeded > 0) {pai.DISC2 = p; discsneeded--;}
            if(pai.DISC3 == null && discsneeded > 0) {pai.DISC3 = p; discsneeded--;}
            if(pai.DISC4 == null && discsneeded > 0) {pai.DISC4 = p; discsneeded--;}

            // e coloca o processo na cpu pedida
            switch(CPU)
            {
                case 1:
                    pai.CPU1 = p;
                    //Debug.Log(p.ToString() + " foi despachado para a CPU1");
                break;
                case 2:
                    pai.CPU2 = p;
                    //Debug.Log(p.ToString() + " foi despachado para a CPU2");
                break;
                case 3:
                    pai.CPU3 = p;
                    //Debug.Log(p.ToString() + " foi despachado para a CPU3");
                break;
                case 4:
                    pai.CPU4 = p;
                    //Debug.Log(p.ToString() + " foi despachado para a CPU4");
                break;
            }
            return true;
        }
        else return false;
    } 

    private int ContarDiscosLivres(){
        int discoslivres = 0;
        if(pai.DISC1 == null){discoslivres++;}
        if(pai.DISC2 == null){discoslivres++;}
        if(pai.DISC3 == null){discoslivres++;}
        if(pai.DISC4 == null){discoslivres++;}
        return discoslivres;
    }

}
