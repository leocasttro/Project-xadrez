namespace tabuleiro
{
    abstract class Peca
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
        public void decrementarQteMovimentos()
        {
            quantidadeMovimento--;
        }
        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = movimentosPossiveis();
            for (int i = 0; i < tab.linhas; i++)
            {
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (mat[i ,j] == true)
                    {
                        return true;
                    }
                }

            }
            return false;
        } 
        public bool podeMoverPara(Posicao pos)
        {
            return movimentosPossiveis()[pos.linha, pos.coluna];
        }
        public abstract bool[,] movimentosPossiveis();

    }
}
