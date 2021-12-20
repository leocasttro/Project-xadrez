using System;
using tabuleiro;
using System.Text;

namespace xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base (cor, tab)
        {
            
        }
        public override string ToString()
        {
            return "R";
        }
    }
    
    }

