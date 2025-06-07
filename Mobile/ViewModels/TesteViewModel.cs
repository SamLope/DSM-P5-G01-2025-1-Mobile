using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Mobile.Models;

namespace Mobile.ViewModels;

public class TesteViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Pergunta> Perguntas { get; } = new();
    public List<Resposta> Respostas { get; } = new();

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

    public void VerificarCompletude()
    {
        TodasRespondidas = Respostas.Count == Perguntas.Count;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


}