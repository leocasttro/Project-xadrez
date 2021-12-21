using tabuleiro;

namespace xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tab, Cor cor) : base(cor, tab)
        {

        }

        public override bool[,] movimentosPossiveis()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "C";
        }
    }
}
