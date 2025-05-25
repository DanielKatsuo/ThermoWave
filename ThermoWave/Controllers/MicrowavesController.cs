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

		[HttpGet]
		public JsonResult AtualizarStatus()
		{
			try
			{
				_microwaves.ProcessarTick();

				return Json(new
				{
					success = true,
					tempo = _microwaves.GetRemaingTimeFormatted(),
					potencia = _microwaves.GetPotenciaExibicao(),
					status = _microwaves.GetStatus().ToString(),
					progresso = _microwaves.GetStringInformativa()
				});
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = $"Falha ao atualizar status. ex: {ex.Message}" });
			}
		}

		[HttpPost]
		public JsonResult IniciarAquecimento(string tempoSegundos, int? potencia)
		{
			try
			{
				var potenciaValid = potencia ?? 10;
				var tempo = Helpers.ConverterMinutosESegundosParaSegundos(tempoSegundos);
				_microwaves.IniciarAquecimento(tempo, potenciaValid);
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

public static class Helpers
{
	public static int ConverterMinutosESegundosParaSegundos(string formattedTime)
	{
		// Verifica se a string está no formato esperado "MM:SS"
		if (string.IsNullOrEmpty(formattedTime) || !formattedTime.Contains(":"))
		{
			throw new ArgumentException("Formato de tempo inválido. Esperado 'MM:SS'.", nameof(formattedTime));
		}

		string[] parts = formattedTime.Split(':');

		if (parts.Length != 2)
		{
			throw new ArgumentException("Formato de tempo inválido. Esperado 'MM:SS'.", nameof(formattedTime));
		}

		if (!int.TryParse(parts[0], out int minutes))
		{
			throw new ArgumentException("Minutos inválidos na string de tempo.", nameof(formattedTime));
		}

		if (!int.TryParse(parts[1], out int seconds))
		{
			throw new ArgumentException("Segundos inválidos na string de tempo.", nameof(formattedTime));
		}

		// Calcula o total de segundos
		return (minutes * 60) + seconds;
	}
}