using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECurtoPrazo
{
    private Escalonador pai; // aponta para o antigo escalonador    
    public ECurtoPrazo(Escalonador pai){this.pai = pai;} //construtor de classe

    // Despachante eh chamado toda vez que uma CPU estiver livre, vide metodo Executar no Escalonador

    void Despachar(int CPU)
    {

    }
    
    /*
    private int c;
    
    void Despachar() // Escalonador de Curto Prazo // Juan e Theo
    {
        // Escolhe qual das filas de prioridade vao executar e em quais CPUs e qual processo pega qual disco.
        if(fila_pronto_p0 != NULL)
        {
            if (reservado_disc() >= Filas.fila_pronto_p0[0].disco)
            {
                reserva_disc(Filas.fila_pronto_p0[0].disco);
                c = reservado_cpu();
                reserva_cpu(c);
                executar(fila_pronto_p0[0],c);             
            }
        }

        else
        {
            if (reservado_disc()>= Filas.fila_pronto_p1_rq0.disco)
            {
                reserva_disc(Filas.fila_pronto_p1_rq0[0].disco);
                c = reservado_cpu();
                reserva_cpu(c);
                executar(Filas.fila_pronto_p1_rq0[0],c);
            }
        }
    }

    void reserva_disc(int qnt_disc){    // Muda o status do disco 

    }

    int reservado_disc(){ // Retorna qnt discos livres

    }
    
    int reservado_cpu(){ // Retorna CPU livre

    }
    void reserva_cpu(int c){  // Muda status 1(UMA) CPU [c] 

    }
    */    

}
