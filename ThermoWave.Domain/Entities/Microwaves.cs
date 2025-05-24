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
		public int RemainingTimeInSeconds { get; set; }
		public TimeOnly StartTime { get; set; } = TimeOnly.FromDateTime(DateTime.MinValue);
		public int Power { get; set; } = 10;
		public HeatingStatus Status { get; set; }
		public string? InformativeString { get; set; }
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
	}


}
