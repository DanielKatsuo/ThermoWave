using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThermoWave.Domain.Entities;

namespace ThermoWave.Domain.Interfaces
{
	public interface IMicrowavesService 
	{
		void IniciarAquecimento(int tempoSegundos, int potencia);
	}
}
