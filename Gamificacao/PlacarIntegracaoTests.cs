using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamificacao
{
    public class PlacarIntegracaoTests : IDisposable
    {
        private readonly string _arquivo = "placar_integracao.json";

        public PlacarIntegracaoTests()
        {
            if (File.Exists(_arquivo))
                File.Delete(_arquivo);
        }

        [Fact]
        public void Deve_registrar_e_listar_pontos_em_integração()
        {
            var armazenamento = new ArmazenamentoEmArquivo(_arquivo);
            var placar = new Placar(armazenamento);

            placar.RegistrarPontos("guerra", TipoPonto.Estrela, 15);
            placar.RegistrarPontos("guerra", TipoPonto.Moeda, 5);

            var pontos = placar.GetPontos("guerra");

            Assert.Contains(pontos, p => p.tipo == TipoPonto.Estrela && p.quantidade == 15);
            Assert.Contains(pontos, p => p.tipo == TipoPonto.Moeda && p.quantidade == 5);
        }

        public void Dispose()
        {
            if (File.Exists(_arquivo))
                File.Delete(_arquivo);
        }
    }
}
