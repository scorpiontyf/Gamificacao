using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamificacao
{
    public interface IArmazenamento
    {
        void AdicionarPontos(string usuario, TipoPonto tipo, int qtd);
        int ObterPontos(string usuario, TipoPonto tipo);
        List<string> ObterUsuarios();
        List<TipoPonto> ObterTiposDePonto();
    }
}
