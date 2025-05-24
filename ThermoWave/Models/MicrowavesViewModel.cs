namespace ThermoWave.Web.Models
{
	public class MicrowavesViewModel
	{
		public string TimeInput { get; set; } = "00:00";
		public string PowerInput { get; set; } = "10";
		public string TimeShow { get; set; } = "00:00";
		public string PowerShow { get; set; } = "10";
	    public string ErrorMessage { get; set; } = string.Empty;
		public string StringProcess { get; set; } = string.Empty;
		public Status StatusNow { get; set; }
	}

	public enum Status
	{
		Idle,
		Heating,
		Finished,
		Error
	}
}
