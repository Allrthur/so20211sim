using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Filas
{
    /*
     * O primeiro da fila � o elemento 0
    */

    public static List<Processo> fila_pronto_p1_rq2 = new List<Processo>();
    public static List<Processo> fila_pronto_p1_rq1 = new List<Processo>();
    public static List<Processo> fila_pronto_p1_rq0 = new List<Processo>();
    public static List<Processo> fila_pronto_p0 = new List<Processo>();

    public static List<Processo> bloqueados  = new List<Processo>();

}
