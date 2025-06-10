using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Mobile.Models;

namespace Mobile.ViewModels
{
    public class TesteViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Pergunta> Perguntas { get; } = new();
        public List<Resposta> Respostas { get; } = new();

        private Pergunta _perguntaAtual;
        public Pergunta PerguntaAtual
        {
            get => _perguntaAtual;
            set
            {
                if (_perguntaAtual != value)
                {
                    _perguntaAtual = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _todasRespondidas;
        public bool TodasRespondidas
        {
            get => _todasRespondidas;
            set
            {
                if (_todasRespondidas != value)
                {
                    _todasRespondidas = value;
                    OnPropertyChanged();
                }
            }
        }

        public void IniciarQuestionario()
        {
            if (Perguntas.Count > 0)
            {
                PerguntaAtual = Perguntas[0];
            }
        }

        public void IrParaProximaPergunta()
        {
            var index = Perguntas.IndexOf(PerguntaAtual);

            if (index >= 0 && index < Perguntas.Count - 1)
            {
                PerguntaAtual = Perguntas[index + 1];
            }
        }

        public bool EhUltimaPergunta()
        {
            var index = Perguntas.IndexOf(PerguntaAtual);
            return index == Perguntas.Count - 1;
        }

        public void VerificarCompletude()
        {
            // Cada pergunta deve ter uma resposta com ValorResposta preenchida (não null / vazio)
            TodasRespondidas = Perguntas.All(p =>
                Respostas.Any(r =>
                    r.IdPergunta == p.Id &&
                    !double.IsNaN(r.ValorResposta) && // protege contra valores inválidos
                    r.ValorResposta.ToString() != ""   // protege contra Entry vazio
                )
            );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
