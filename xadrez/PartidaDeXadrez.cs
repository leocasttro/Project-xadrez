using tabuleiro;
using System.Collections.Generic;
using xadrez;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public  int turno { get; private set; }
        public Cor jogadorAtual{ get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }


        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branco;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();

        }
        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }
        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQteMovimentos();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);
            
        }
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
            }
            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            if (testeXequemate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }
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
                jogadorAtual = Cor.Azul;
            }
            else
            {
                jogadorAtual = Cor.Branco;
            }
        }
        public HashSet<Peca> pecaCapturadasCor(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> pecaEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecaCapturadasCor(cor));
            return aux;
        }
        private Cor adversaria (Cor cor)
        {
            if (cor == Cor.Branco)
            {
                return Cor.Azul;
            }
            else
            {
                return Cor.Branco;
            }
        }
        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecaEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x; 
                }
            }
            return null;
        }
        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não existe rei da " + cor + "no tabuleiro");
            }
            foreach (Peca x in pecaEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }
        public bool testeXequemate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach  (Peca x in pecaEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++)

                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i,j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCaputradaCor = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCaputradaCor);
                            if (testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public void colocarNovaPeca(char coluna, int linha, Peca peca )
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }
        private void colocarPecas()
        {
            //colocarNovaPeca('a', 1, new Torre(tab, Cor.Azul));
            //colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Azul));
            //colocarNovaPeca('c', 1, new Bispo(tab, Cor.Azul));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Azul));
            //colocarNovaPeca('e', 1, new Rainha(tab, Cor.Azul));
            //colocarNovaPeca('f', 1, new Bispo(tab, Cor.Azul));
            //colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Azul));
            //colocarNovaPeca('h', 1, new Torre(tab, Cor.Azul));
            //colocarNovaPeca('a', 2, new Peao(tab, Cor.Azul));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Branco));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Branco));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Branco));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Branco));
            colocarNovaPeca('e', 8, new Rainha(tab, Cor.Branco));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Branco));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Branco));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Branco));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Branco));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Branco));
        }
    }
}
