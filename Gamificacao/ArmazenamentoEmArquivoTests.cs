using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamificacao
{
    public class ArmazenamentoEmArquivoTests : IDisposable
    {
        private readonly string _arquivoTeste = "armazenamento_test.json";

        public ArmazenamentoEmArquivoTests()
        {
            if (File.Exists(_arquivoTeste))
                File.Delete(_arquivoTeste);
        }

        [Fact]
        public void Deve_armazenar_e_recuperar_pontos()
        {
            var armazenamento = new ArmazenamentoEmArquivo(_arquivoTeste);

            armazenamento.AdicionarPontos("guerra", TipoPonto.Estrela, 10);
            var pontos = armazenamento.ObterPontos("guerra", TipoPonto.Estrela);

            Assert.Equal(10, pontos);
        }

        [Fact]
        public void Deve_retornar_lista_de_usuarios()
        {
            var armazenamento = new ArmazenamentoEmArquivo(_arquivoTeste);
            armazenamento.AdicionarPontos("guerra", TipoPonto.Moeda, 5);
            armazenamento.AdicionarPontos("joao", TipoPonto.Curtida, 2);

            var usuarios = armazenamento.ObterUsuarios();

            Assert.Contains("guerra", usuarios);
            Assert.Contains("joao", usuarios);
        }

        [Fact]
        public void Deve_retornar_tipos_de_ponto()
        {
            var armazenamento = new ArmazenamentoEmArquivo(_arquivoTeste);
            armazenamento.AdicionarPontos("guerra", TipoPonto.Topico, 7);
            armazenamento.AdicionarPontos("guerra", TipoPonto.Moeda, 3);

            var tipos = armazenamento.ObterTiposDePonto();

            Assert.Contains(TipoPonto.Topico, tipos);
            Assert.Contains(TipoPonto.Moeda, tipos);
        }

        public void Dispose()
        {
            if (File.Exists(_arquivoTeste))
                File.Delete(_arquivoTeste);
        }
    }
}
