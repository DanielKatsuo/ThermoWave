using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ThermoWave.Web.Controllers
{
	public class Microwaves : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
