using System;
using CursoOnline.Dominio._Base;
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

            ValidadorDeRegra.Novo()
                .Quando(cursoExistente != null, "Nome do curso já consta no Banco de Dados.")
                .Quando(!Enum.TryParse<PublicoAlvo>(cursoDto.PublicoAlvo, out var publicoAlvo), "Publico Alvo inválido")
                .DispararExcessaoSeExistir();

            var curso = new Curso(cursoDto.Nome, cursoDto.CargaHoraria, publicoAlvo, cursoDto.Valor, cursoDto.Descricao);
            _cursoRepositorio.Adicionar(curso);
        }
    }
}
