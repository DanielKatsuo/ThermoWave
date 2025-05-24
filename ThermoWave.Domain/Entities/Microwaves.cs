using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThermoWave.Domain.Enums;

namespace ThermoWave.Domain.Entities
{
	public class Microwaves
	{
		public Microwaves()
		{
			Resetar();
		}

		public int RemainingTimeInSeconds { get; set; }
		public int Power { get; set; } = 10;
		public HeatingStatus Status { get; set; }
		public string? InformativeString { get; set; }
		public string RemaingTimeFormatted { get; set; } = "00:00";
		public char HeatingCharacter { get; set; } = '.';

		public void IniciarAquecimento(int tempoSegundos, int potencia)
		{
			if (tempoSegundos < 1 || tempoSegundos > 120)
				throw new ArgumentOutOfRangeException(nameof(tempoSegundos), "Tempo deve ser entre 1 e 120 segundos.");
			if (potencia < 1 || potencia > 10)
				throw new ArgumentOutOfRangeException(nameof(potencia), "Potência deve ser entre 1 e 10.");

			RemainingTimeInSeconds = tempoSegundos;
			Power = potencia;
			Status = HeatingStatus.Heating;
			InformativeString = "";
			HeatingCharacter = '.';
		}

		public void Pausar()
		{
			if (Status == HeatingStatus.Heating)
			{
				Status = HeatingStatus.Idle;
			}
		}

		public void Continuar()
		{
			if (Status == HeatingStatus.Idle)
			{
				Status = HeatingStatus.Heating;
			}
		}

		public void Cancelar()
		{
			Resetar();
		}

		private void Resetar()
		{
			RemainingTimeInSeconds = 0;
			Power = 10; // Padrão
			Status = HeatingStatus.Finished;
			InformativeString = "";
			HeatingCharacter = '.';
		}

		public void AtualizarStringDeProgresso()
		{
			if (Status == HeatingStatus.Heating)
			{
				for (int i = 0; i < Power; i++)
				{
					InformativeString += HeatingCharacter;
				}
			}
		}

		public void FormatarTempoRestante()
		{
			int minutos = RemainingTimeInSeconds / 60;
			int segundos = RemainingTimeInSeconds % 60;
			InformativeString = $"{minutos:D2}:{segundos:D2}";
		}
	}
}
