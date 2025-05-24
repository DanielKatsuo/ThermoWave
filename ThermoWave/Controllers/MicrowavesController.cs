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
			var model = new MicrowavesViewModel
			{
				TimeShow = _microwaves.GetRemaingTimeFormatted(),
				StringProcess = _microwaves.GetStringInformativa(),
				StatusNow = _microwaves.GetStatus().ToString(),
				PowerShow = _microwaves.GetPotenciaExibicao()
			};
			return View(model);
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
				tempo,
				progresso,
			    status,
				potencia
			});
		}

		[HttpPost]
		public JsonResult IniciarAquecimento(int tempoSegundos, int? potencia)
		{
			try
			{
				var potenciaValid = potencia ?? 10;
				_microwaves.IniciarAquecimento(tempoSegundos, potenciaValid);
				return Json(new
				{
					success = true,
					message = "Aquecimento iniciado!",
					tempo = _microwaves.GetRemaingTimeFormatted(),
					potencia = _microwaves.GetPotenciaExibicao(),
					status = _microwaves.GetStatus().ToString(),
					progresso = _microwaves.GetStringInformativa()
				});
			}
			catch (ArgumentException ex)
			{
				return Json(new { success = false, message = ex.Message, tempo = _microwaves.GetRemaingTimeFormatted(), status = _microwaves.GetStatus().ToString(), progresso = _microwaves.GetStringInformativa() });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = $"Ocorreu um erro ao iniciar o aquecimento: {ex.Message}" });
			}
		}

		[HttpPost]
		public JsonResult InicioRapido()
		{
			try
			{
				_microwaves.InicioRapido();
				return Json(new
				{
					success = true,
					message = "Início rápido ativado!",
					tempo = _microwaves.GetRemaingTimeFormatted(),
					potencia = _microwaves.GetPotenciaExibicao(),
					status = _microwaves.GetStatus().ToString(),
					progresso = _microwaves.GetStringInformativa()
				});
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = $"Ocorreu um erro no início rápido. Erro: {ex.Message}" });
			}
		}

		[HttpPost]
		public JsonResult AcrescentarTempo()
		{
			try
			{
				_microwaves.AcrescentarTempo();
				return Json(new
				{
					success = true,
					message = "Tempo acrescentado!",
					tempo = _microwaves.GetRemaingTimeFormatted(),
					potencia = _microwaves.GetPotenciaExibicao(),
					status = _microwaves.GetStatus().ToString(),
					progresso = _microwaves.GetStringInformativa()
				});
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = $"Não foi possível acrescentar tempo. Erro: {ex.Message}" });
			}
		}

		[HttpPost]
		public JsonResult PausarOuCancelar()
		{
			try
			{
				_microwaves.PausarOuCancelar();
				string acao = (_microwaves.GetStatus().ToString() == "Idle") ? "Pausado" : "Cancelado";
				return Json(new
				{
					success = true,
					message = $"Aquecimento {acao}!",
					tempo = _microwaves.GetRemaingTimeFormatted(),
					potencia = _microwaves.GetPotenciaExibicao(),
					status = _microwaves.GetStatus().ToString(),
					progresso = _microwaves.GetStringInformativa()
				});
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = $"Ocorreu um erro ao pausar/cancelar. Erro: {ex.Message}" });
			}
		}
	}
}
