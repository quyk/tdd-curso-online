using System.Linq;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Entidades.Cursos;
using CursoOnline.Web.Util;
using Microsoft.AspNetCore.Mvc;

namespace CursoOnline.Web.Controllers
{
    public class CursoController : Controller
    {
        private readonly ArmazenadorDeCurso _armazenador;
        private readonly IRepositorio<Curso> _cursoRepositorio;

        public CursoController(ArmazenadorDeCurso armazenador, IRepositorio<Curso> cursoRepositorio)
        {
            _armazenador = armazenador;
            _cursoRepositorio = cursoRepositorio;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cursos = _cursoRepositorio.Consultar();
            if (cursos.Any())
            {
                var cursoDto = cursos.Select(c => new CursoParaListagemDto {
                    Id = c.Id,
                    Nome = c.Nome,
                    CargaHoraria = c.CargaHoraria,
                    PublicoAlvo = c.PublicoAlvo.ToString(),
                    Valor = c.Valor
                });

                return View("Index", PaginatedList<CursoParaListagemDto>.Create(cursoDto, Request));
            }

            return View("Index", PaginatedList<CursoParaListagemDto>.Create(null, Request));
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View("NovoOuEditar", new CursoDto());
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var curso = _cursoRepositorio.ObterPorId(id);
            var cursoDto = new CursoDto
            {
                Id = curso.Id,
                Nome = curso.Nome,
                Descricao = curso.Descricao,
                CargaHoraria = curso.CargaHoraria,
                Valor = curso.Valor
            };
            return View("NovoOuEditar", cursoDto);
        }

        [HttpPost]
        public IActionResult Salvar(CursoDto curso)
        {
            _armazenador.Armazenar(curso);
            return Ok();
        }
    }
}