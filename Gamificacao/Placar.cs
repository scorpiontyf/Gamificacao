using Gamificacao;

public class Placar
{
    private readonly IArmazenamento _armazenamento;

    public Placar(IArmazenamento armazenamento)
    {
        _armazenamento = armazenamento;
    }

    public void RegistrarPontos(string usuario, TipoPonto tipo, int qtd)
    {
        _armazenamento.AdicionarPontos(usuario, tipo, qtd);
    }

    public List<(TipoPonto tipo, int quantidade)> GetPontos(string usuario)
    {
        var tipos = _armazenamento.ObterTiposDePonto();
        var resultado = new List<(TipoPonto, int)>();

        foreach (var tipo in tipos)
        {
            var qtd = _armazenamento.ObterPontos(usuario, tipo);
            if (qtd > 0)
                resultado.Add((tipo, qtd));
        }

        return resultado;
    }

    public List<(string usuario, int quantidade)> GetRanking(TipoPonto tipo)
    {
        var usuarios = _armazenamento.ObterUsuarios();
        return usuarios
            .Select(u => (usuario: u, quantidade: _armazenamento.ObterPontos(u, tipo)))
            .Where(x => x.quantidade > 0)
            .OrderByDescending(x => x.quantidade)
            .ToList();
    }
}
