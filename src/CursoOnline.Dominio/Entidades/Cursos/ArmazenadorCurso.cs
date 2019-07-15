using System;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Interface;


namespace CursoOnline.Dominio.Entidades.Cursos
{
    public class ArmazenadorCurso
    {
        private readonly ICursoRepositorio _cursoRepositorio;
        
        public ArmazenadorCurso(ICursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            var cursoExistente = _cursoRepositorio.ObterPeloNome(cursoDto.Nome);
            if (cursoExistente != null)
            {
                throw new ArgumentException("Nome do curso já consta no Banco de Dados.");
            }

           if(!Enum.TryParse<PublicoAlvo>(cursoDto.PublicoAlvo, out var publicoAlvo))
            {
                throw new ArgumentException("Publico Alvo inválido");
            }

            var curso = new Curso(cursoDto.Nome, cursoDto.CargaHoraria, publicoAlvo, cursoDto.Valor, cursoDto.Descricao);
            _cursoRepositorio.Adicionar(curso);
        }
    }
}
