﻿namespace tabuleiro
{
    class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int quantidadeMovimento { get; protected set; }
        public Tabuleiro tab { get; protected set; }


        public Peca( Cor cor, Tabuleiro tab)
        {
            this.posicao = null;
            this.cor = cor;
            this.tab = tab;
            this.quantidadeMovimento = 0;
        }
        public void incrementarQteMovimentos()
        {
            quantidadeMovimento++;
        }
    }
}
