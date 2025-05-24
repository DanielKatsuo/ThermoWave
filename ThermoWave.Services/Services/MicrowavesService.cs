using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThermoWave.Domain.Entities;
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
	}
}
