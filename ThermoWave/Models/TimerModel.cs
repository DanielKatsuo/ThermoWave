namespace ThermoWave.Models
{
	public class TimerModel
	{
		public TimeOnly TempoAtual { get; set; } = TimeOnly.FromDateTime(DateTime.MinValue);
		public int PotenciaAtual { get; set; } = 10;
	}
}
