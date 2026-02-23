using Microsoft.AspNetCore.Mvc;
using MvcNetCoreUtilidades.Helpers;

namespace MvcNetCoreUtilidades.Controllers
{
    public class CifradosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CifradoBasico()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CifradoBasico(string contenido, string resultado, string accion)
        {
            //ciframos el contenido
            string response = HelperCriptography.EncriptarTxtBasico(contenido);

            if (accion.ToLower() == "cifrar")
            {
                ViewData["Cifrado"] = response;
            }
            else if (accion.ToLower() == "comparar")
            {
                //si se quiere comprarar
                //nos envia el txt en el resultado
                if (response != resultado)
                {
                    ViewData["Comparacion"] = "Los datos no coinciden :(";
                }
                else
                {
                    ViewData["Comparacion"] = "Los datos coinciden, bieeeeeen";
                }
            }
                return View();
        }
    }
}
