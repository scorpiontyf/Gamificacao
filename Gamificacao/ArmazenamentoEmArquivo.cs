using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamificacao
{
    public class ArmazenamentoEmArquivo : IArmazenamento
    {
        private readonly string _caminhoArquivo;
        private Dictionary<string, Dictionary<string, int>> _dados;

        public ArmazenamentoEmArquivo(string caminhoArquivo)
        {
            _caminhoArquivo = caminhoArquivo;
            _dados = CarregarDoArquivo();
        }

        public void AdicionarPontos(string usuario, TipoPonto tipo, int qtd)
        {
            var tipoStr = tipo.ToString();

            if (!_dados.ContainsKey(usuario))
                _dados[usuario] = new Dictionary<string, int>();

            if (!_dados[usuario].ContainsKey(tipoStr))
                _dados[usuario][tipoStr] = 0;

            _dados[usuario][tipoStr] += qtd;
            SalvarNoArquivo();
        }

        public int ObterPontos(string usuario, TipoPonto tipo)
        {
            var tipoStr = tipo.ToString();

            if (_dados.TryGetValue(usuario, out var pontosDoUsuario) &&
                pontosDoUsuario.TryGetValue(tipoStr, out var qtd))
            {
                return qtd;
            }

            return 0;
        }

        public List<string> ObterUsuarios()
        {
            return _dados.Keys.ToList();
        }

        public List<TipoPonto> ObterTiposDePonto()
        {
            return _dados.Values
                .SelectMany(p => p.Keys)
                .Distinct()
                .Select(tipoStr => Enum.Parse<TipoPonto>(tipoStr))
                .ToList();
        }

        private Dictionary<string, Dictionary<string, int>> CarregarDoArquivo()
        {
            if (!File.Exists(_caminhoArquivo))
                return new Dictionary<string, Dictionary<string, int>>();

            var json = File.ReadAllText(_caminhoArquivo);
            return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, int>>>(json)
                   ?? new Dictionary<string, Dictionary<string, int>>();
        }

        private void SalvarNoArquivo()
        {
            var json = JsonConvert.SerializeObject(_dados, Formatting.Indented);
            File.WriteAllText(_caminhoArquivo, json);
        }
    }
}
