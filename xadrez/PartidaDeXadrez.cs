using tabuleiro;
using System;
using xadrez;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public  int turno { get; private set; }
        public Cor jogadorAtual{ get; private set; }
        public bool terminada { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branco;
            colocarPecas();
        }
        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
        }
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);
            turno++;
            mudaJogador();
        }
        public void validarPosicaoOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe nenhuma peça nessa posilção!");
            }
            if (jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A cor peça selecioanda é do adversário!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("A peça está bloqueada para a jogada");
            }
        }
        public void validarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino invalido!");
            }
        }
        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branco)
            {
                jogadorAtual = Cor.Vermelho;
            }
            else
            {
                jogadorAtual = Cor.Branco;
            }
        }
        private void colocarPecas()
        {
            tab.colocarPeca(new Torre(tab, Cor.Branco), new PosicaoXadrez('a', 1).toPosicao());
            tab.colocarPeca(new Cavalo(tab, Cor.Branco), new PosicaoXadrez('b', 1).toPosicao());
            tab.colocarPeca(new Bispo(tab, Cor.Branco), new PosicaoXadrez('c', 1).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Branco), new PosicaoXadrez('d', 1).toPosicao());
            tab.colocarPeca(new Rainha(tab, Cor.Branco), new PosicaoXadrez('e', 1).toPosicao());
            tab.colocarPeca(new Bispo(tab, Cor.Branco), new PosicaoXadrez('f', 1).toPosicao());
            tab.colocarPeca(new Cavalo(tab, Cor.Branco), new PosicaoXadrez('g', 1).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Branco), new PosicaoXadrez('h', 1).toPosicao());
            tab.colocarPeca(new Peao(tab, Cor.Branco), new PosicaoXadrez('a', 2).toPosicao());



            tab.colocarPeca(new Torre(tab, Cor.Vermelho), new PosicaoXadrez('a', 8).toPosicao());
            tab.colocarPeca(new Cavalo(tab, Cor.Vermelho), new PosicaoXadrez('b', 8).toPosicao());
            tab.colocarPeca(new Bispo(tab, Cor.Vermelho), new PosicaoXadrez('c', 8).toPosicao());
            tab.colocarPeca(new Rei(tab, Cor.Vermelho), new PosicaoXadrez('d', 8).toPosicao());
            tab.colocarPeca(new Rainha(tab, Cor.Vermelho), new PosicaoXadrez('e', 8).toPosicao());
            tab.colocarPeca(new Bispo(tab, Cor.Vermelho), new PosicaoXadrez('f', 8).toPosicao());
            tab.colocarPeca(new Cavalo(tab, Cor.Vermelho), new PosicaoXadrez('g', 8).toPosicao());
            tab.colocarPeca(new Torre(tab, Cor.Vermelho), new PosicaoXadrez('h', 8).toPosicao());
        }
    }
}
