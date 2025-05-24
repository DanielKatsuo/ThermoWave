using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThermoWave.Domain.Entities;
using ThermoWave.Domain.Enums;

namespace ThermoWave.Domain.Interfaces
{
	public interface IMicrowavesService 
	{
		void IniciarAquecimento(int tempoSegundos, int potencia);
		void InicioRapido();
		void AcrescentarTempo();
		void PausarOuCancelar();
		void ContinuarAquecimento();
		void ProcessarTick();
		int GetTempoRestante();
		string GetRemaingTimeFormatted();
		string GetStringInformativa();
		string GetPotenciaExibicao();
		HeatingStatus GetStatus();
		void LimparDados();
	}
}
