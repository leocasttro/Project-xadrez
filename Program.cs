using System;
using tabuleiro;
using xadrez;

namespace ExercicioXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.colocarPeca(new Torre(tab, Cor.Branco), new Posicao(0, 0));
                tab.colocarPeca(new Rei(tab, Cor.Branco), new Posicao(0, 0));
                tab.colocarPeca(new Rainha(tab, Cor.Vermelho), new Posicao(1, 3));

                Tela.imprimirTabuleiro(tab);

            }

            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
