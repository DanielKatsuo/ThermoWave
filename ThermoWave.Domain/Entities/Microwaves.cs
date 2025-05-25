using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ThermoWave.Domain.Enums;

namespace ThermoWave.Domain.Entities
{
	public class Microwaves : IDisposable
	{
		private readonly System.Timers.Timer _timer;
		private const int TimerInterval = 1000;
		public Microwaves()
		{
			Resetar();
			_timer = new System.Timers.Timer(TimerInterval);
			_timer.Elapsed += OnTimedEvent!;
			_timer.AutoReset = true;
		}

		public int RemainingTimeInSeconds { get; private set; }
		public int Power { get; private set; } = 10;
		public HeatingStatus Status { get; private set; }
		public string? InformativeString { get; private set; }
		public string RemaingTimeFormatted { get; private set; } = "00:00";
		public char HeatingCharacter { get; private set; } = '.';

		private void OnTimedEvent(object? source, ElapsedEventArgs e)
		{
			// O timer pode disparar em uma thread diferente, então precisamos de um lock
			// para garantir que as operações de estado sejam atômicas e seguras.
			lock (this)
			{
				if (Status == HeatingStatus.Heating)
				{
					if (RemainingTimeInSeconds > 0)
					{
						RemainingTimeInSeconds--;
						AtualizarStringDeProgresso(); // Atualiza a string de progresso
					}
					else
					{
						_timer.Stop();
						Status = HeatingStatus.Finished;
						InformativeString = "Aquecimento Concluído!";
					}
				}
				else if (Status == HeatingStatus.Finished || Status == HeatingStatus.Idle)
				{
					// Se o status não é "Aquecendo", o timer não deveria estar rodando.
					// Isso é uma segurança extra para parar o timer caso ele esteja ativo indevidamente.
					_timer.Stop();
				}
			}
		}

		public void IniciarAquecimento(int tempoSegundos, int potencia)
		{
			if (tempoSegundos < 1 || tempoSegundos > 120)
				throw new ArgumentOutOfRangeException(nameof(tempoSegundos), "Tempo deve ser entre 1 e 120 segundos.");
			if (potencia < 1 || potencia > 10)
				throw new ArgumentOutOfRangeException(nameof(potencia), "Potência deve ser entre 1 e 10.");
			RemainingTimeInSeconds = tempoSegundos;
			FormatarTempoRestante();
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
			int numCaracteres = Math.Max(1, 11 - Power);
			InformativeString = new string(HeatingCharacter, numCaracteres);
		}

		public void DecrementarTempo()
		{
			if (Status != HeatingStatus.Heating) return;

			RemainingTimeInSeconds--;
			if (RemainingTimeInSeconds <= 0)
			{
				RemainingTimeInSeconds = 0;
				Status = HeatingStatus.Finished;
				InformativeString += "Aquecimento concluído";
			}
		}

		public void FormatarTempoRestante()
		{
			int minutos = RemainingTimeInSeconds / 60;
			int segundos = RemainingTimeInSeconds % 60;
			RemaingTimeFormatted = $"{minutos:D2}:{segundos:D2}";
		}

		public void AcrescentarTempo()
		{
			RemainingTimeInSeconds += 30;
		}

		public void Dispose()
		{
			_timer?.Stop();
			_timer?.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
