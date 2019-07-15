using System;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Entidades.Cursos;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Interface;

namespace CursoOnline.Dominio.Cursos
{
    public class ArmazenadorDeCurso
    {
        private readonly ICursoRepositorio _cursoRepositorio;

        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }

        public void Armazenar(CursoDto curso)
        {
            var cursoJaSalvo = _cursoRepositorio.ObterPeloNome(curso.Nome);

            if (cursoJaSalvo != null)
            {
                throw new ArgumentException("Nome do curso já consta no banco de dados");
            }

            if (!Enum.TryParse<PublicoAlvo>(curso.PublicoAlvo, out var publicoAlvo))
            {
                throw new ArgumentException("Publico Alvo inválido");
            }

            var novoCurso = new Curso(curso.Nome, curso.CargaHoraria, publicoAlvo, curso.Valor, curso.Descricao);

            _cursoRepositorio.Adicionar(novoCurso);
        }
    }
}
