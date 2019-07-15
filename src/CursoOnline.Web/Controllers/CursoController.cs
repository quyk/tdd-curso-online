using System.Collections.Generic;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Web.Util;
using Microsoft.AspNetCore.Mvc;

namespace CursoOnline.Web.Controllers
{
    public class CursoController : Controller
    {
        private readonly ArmazenadorDeCurso _armazenador;

        public CursoController(ArmazenadorDeCurso armazenador)
        {
            _armazenador = armazenador;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cursos = new List<CursoParaListagemDto>();
            return View("Index", PaginatedList<CursoParaListagemDto>.Create(cursos, Request));
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View("NovoOuEditar", new CursoDto());
        }

        [HttpPost]
        public IActionResult Salvar(CursoDto curso)
        {
            _armazenador.Armazenar(curso);
            return Ok();
        }
    }
}