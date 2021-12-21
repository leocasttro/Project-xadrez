using tabuleiro;

namespace xadrez
{
    class Peao : Peca 
    {
        public Peao(Tabuleiro tab, Cor cor) :base(cor, tab)
        {

        }
        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;
        }
        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.colunas, tab.linhas];
            Posicao pos = new Posicao(0, 0);
            
            //ACIMA

            pos.definirValores(posicao.linha - 2, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            pos.linha = pos.linha - 1;
            return mat;
        }

        public override string ToString()
        {
            return "P";
        }

 
    }
}
