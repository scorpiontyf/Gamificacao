using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamificacao
{
    public class Armazenamento
    {
        public string Usuario { get; set; }
        public Pontuacao Ponto { get; set; }
        public List<Tuple<string, Pontuacao>> ListaArmazenamento { get; set; }

        public List<string> RetornarUsuariosComPonto()
        {
            return ListaArmazenamento
                .Where(item => item.Item2.QtdPonto > 0)
                .Select(item => item.Item1) 
                .ToList();
        }
        
        public List<TipoPonto> RetornarPontosPorTipoDoUsuario(string Usuario)
        {
            return ListaArmazenamento
                .Where(item => item.Item1 == Usuario)
                .Select(item => item.Item2.TipoPonto)
                .Distinct()
                .ToList();
        }



    }

    public class Pontuacao
    {
        public TipoPonto TipoPonto { get; set; }
        public int QtdPonto { get; set; }
    }

    public enum TipoPonto
    {
        Moeda, Estrela, Topico, Comentario, Curtida, Energia
    }
}
