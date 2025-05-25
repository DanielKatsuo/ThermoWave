using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThermoWave.Domain.Entities;
using ThermoWave.Domain.Enums;
using ThermoWave.Domain.Interfaces;

namespace ThermoWave.Services.Services
{
	public class MicrowavesService : IMicrowavesService
	{
		private readonly Microwaves _microwaves;

		public MicrowavesService()
		{
			_microwaves = new Microwaves();
		}

		public void IniciarAquecimento(int tempoSegundos, int potencia)
		{ 
			 _microwaves.IniciarAquecimento(tempoSegundos, potencia);
		}

		public void InicioRapido()
		{
			_microwaves.IniciarAquecimento(30, 10);
		}

		public void AcrescentarTempo()
		{
			if (_microwaves.Status == HeatingStatus.Heating)
			{
				_microwaves.AcrescentarTempo(); // Atualiza diretamente a propriedade
			}
		}

		public void PausarOuCancelar()
		{
			if (_microwaves.Status == HeatingStatus.Heating)
			{
				_microwaves.Pausar();
			}
			else if (_microwaves.Status == HeatingStatus.Idle)
			{
				_microwaves.Cancelar(); // Cancela se estiver pausado e botão for pressionado novamente
			}
			else if (_microwaves.Status == HeatingStatus.Idle || _microwaves.Status == HeatingStatus.Finished)
			{
				_microwaves.Cancelar(); // Limpa se estiver parado
			}
		}

		public void ContinuarAquecimento()
		{
			if (_microwaves.Status == HeatingStatus.Idle)
			{
				_microwaves.Continuar();
			}
		}

		public void ProcessarTick()
		{
			if (_microwaves.Status == HeatingStatus.Heating)
			{
				_microwaves.DecrementarTempo();
				_microwaves.AtualizarStringDeProgresso();
			}
		}

		public int GetTempoRestante() => _microwaves.RemainingTimeInSeconds;
		public string GetStringInformativa() => _microwaves.InformativeString ?? string.Empty;
		public string GetPotenciaExibicao() => _microwaves.Power.ToString();
		public HeatingStatus GetStatus() => _microwaves.Status;
		public string GetRemaingTimeFormatted() => _microwaves.RemaingTimeFormatted ?? string.Empty;
		public void LimparDados() => _microwaves.Cancelar();
	}
}
