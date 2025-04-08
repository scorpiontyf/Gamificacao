using Moq;

namespace Gamificacao
{
    public class PlacarTests
    {
        [Fact]
        public void Deve_registrar_ponto()
        {
            var mock = new Mock<IArmazenamento>();
            var placar = new Placar(mock.Object);

            placar.RegistrarPontos("guerra", TipoPonto.Estrela, 10);

            mock.Verify(x => x.AdicionarPontos("guerra", TipoPonto.Estrela, 10), Times.Once);
        }

        [Fact]
        public void Deve_retornar_pontos_do_usuario()
        {
            var mock = new Mock<IArmazenamento>();
            mock.Setup(x => x.ObterTiposDePonto()).Returns(new List<TipoPonto> { TipoPonto.Moeda, TipoPonto.Estrela });
            mock.Setup(x => x.ObterPontos("guerra", TipoPonto.Moeda)).Returns(20);
            mock.Setup(x => x.ObterPontos("guerra", TipoPonto.Estrela)).Returns(0);

            var placar = new Placar(mock.Object);
            var pontos = placar.GetPontos("guerra");

            Assert.Contains(pontos, p => p.tipo == TipoPonto.Moeda && p.quantidade == 20);
            Assert.DoesNotContain(pontos, p => p.tipo == TipoPonto.Estrela); // pois tem 0
        }

        [Fact]
        public void Deve_retornar_ranking()
        {
            var mock = new Mock<IArmazenamento>();
            mock.Setup(x => x.ObterUsuarios()).Returns(new List<string> { "guerra", "fernandes", "rodrigo" });
            mock.Setup(x => x.ObterPontos("guerra", TipoPonto.Estrela)).Returns(25);
            mock.Setup(x => x.ObterPontos("fernandes", TipoPonto.Estrela)).Returns(19);
            mock.Setup(x => x.ObterPontos("rodrigo", TipoPonto.Estrela)).Returns(17);

            var placar = new Placar(mock.Object);
            var ranking = placar.GetRanking(TipoPonto.Estrela);

            Assert.Equal("guerra", ranking[0].usuario);
            Assert.Equal("fernandes", ranking[1].usuario);
            Assert.Equal("rodrigo", ranking[2].usuario);
        }
    }

}