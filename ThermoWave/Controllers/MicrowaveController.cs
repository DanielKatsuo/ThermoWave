using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ThermoWave.Web.Controllers
{
	public class HomeController1 : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
