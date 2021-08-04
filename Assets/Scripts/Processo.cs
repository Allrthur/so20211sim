using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Processo
{
    
    private string id;
    private int tchegada;
    private int prioridade;
    private int duracao;
    private int mem; // tamanho na memoria
    private int disc; // # de discos

    public Processo(int tchegada, int prioridade, int duracao, int mem, int disc) 
    {
        this.tchegada = tchegada;
        this.prioridade = prioridade;
        this.duracao = duracao;
        this.mem = mem;
        this.disc = disc;
    }

    public void SetId(string newid) {
        this.id = newid;
    }

    public string GetId() {
        return this.id;
    }

    public int GetTchegada() {
        return this.tchegada;
    }

    public int GetPrioridade() {
        return this.prioridade;
    }

    public int GetDuracao() {
        return this.duracao;
    }

    public int GetMem() {
        return this.mem;
    }

    public int GetDisc() {
        return this.disc;
    }

    public void SetDuracao(int dur)
    {
        this.duracao = dur;
    }

}
