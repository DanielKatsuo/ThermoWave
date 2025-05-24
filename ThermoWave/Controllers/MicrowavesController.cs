using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThermoWave.Domain.Interfaces;
using ThermoWave.Web.Models;

namespace ThermoWave.Web.Controllers
{
	public class Microwaves(IMicrowavesService microwavesService) : Controller
	{
		private readonly IMicrowavesService _microwaves = microwavesService;

		[HttpGet]
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult GetStatus()
		{
			_microwaves.ProcessarTick();

			var tempo = _microwaves.GetRemaingTimeFormatted;
			var progresso = _microwaves.GetStringInformativa();
			var status = _microwaves.GetStatus().ToString();
			var potencia = _microwaves.GetPotenciaExibicao();

			return Json(new
			{
				tempo, // Ex: 01:30
				progresso,
			    status,
				potencia
			});
		}

	}
}
