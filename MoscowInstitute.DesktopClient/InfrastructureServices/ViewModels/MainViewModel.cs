using MoscowTrafficRestriction.ApplicationServices.GetTrafficRestrictionListUseCase;
using MoscowTrafficRestriction.ApplicationServices.Ports;
using MoscowTrafficRestriction.DomainObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MoscowTrafficRestriction.DesktopClient.InfrastructureServices.ViewModels 
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IGetTrafficRestrictionListUseCase _getTrafficRestrictionListUseCase;

        public MainViewModel(IGetTrafficRestrictionListUseCase getTrafficRestrictionListUseCase)
            => _getTrafficRestrictionListUseCase = getTrafficRestrictionListUseCase;

        private Task<bool> _loadingTask;
        private TrafficRestriction _currentTrafficRestriction;
        private ObservableCollection<TrafficRestriction> _TrafficRestrictiont;

        public event PropertyChangedEventHandler PropertyChanged;

        public TrafficRestriction CurrentTrafficRestriction
        {
            get => _currentTrafficRestriction;
            set
            {
                if (_currentTrafficRestriction != value)
                {
                    _currentTrafficRestriction = value;
                    OnPropertyChanged(nameof(CurrentTrafficRestriction));
                }
            }
        }

        private async Task<bool> LoadTrafficRestrictions()
        {
            var outputPort = new OutputPort();
            bool result = await _getTrafficRestrictionListUseCase.Handle(GetTrafficRestrictionListUseCaseRequest.CreateAllTrafficRestrictionRequest(), outputPort);
            if (result)
            {
                TrafficRestrictions = new ObservableCollection<TrafficRestriction>(outputPort.TrafficRestrictions);
            }
            return result;
        }

        public ObservableCollection<TrafficRestriction> TrafficRestrictions
        {
            get
            {
                if (_loadingTask == null)
                {
                    _loadingTask = LoadTrafficRestrictions();
                }

                return _TrafficRestrictiont;
            }
            set
            {
                if (_TrafficRestrictiont != value)
                {
                    _TrafficRestrictiont = value;
                    OnPropertyChanged(nameof(TrafficRestrictions));
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private class OutputPort : IOutputPort<GetTrafficRestrictionListUseCaseResponse>
        {
            public IEnumerable<TrafficRestriction> TrafficRestrictions { get; private set; }

            public void Handle(GetTrafficRestrictionListUseCaseResponse response)
            {
                if (response.Success)
                {
                    TrafficRestrictions = new ObservableCollection<TrafficRestriction>(response.TrafficRestriction);
                }
            }
        }
    }
}
