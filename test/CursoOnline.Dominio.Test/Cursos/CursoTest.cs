using System;
using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Entidades.Cursos;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Test.Builders;
using CursoOnline.Dominio.Test.Util;
using ExpectedObjects;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class CursoTest : IDisposable
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        public CursoTest(ITestOutputHelper outputHelper)
        {
            var faker = new Faker();

            _outputHelper = outputHelper;
            _outputHelper.WriteLine("Construtor sendo executado");

            _nome = faker.Name.FullName();
            _cargaHoraria = faker.Random.Double(50, 250);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = faker.Random.Number(0, 250);
            _descricao = faker.Lorem.Paragraph();
        }

        public void Dispose()
        {
            _outputHelper.WriteLine("Dispose sendo executado");
        }

        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor,
                Descricao = _descricao
            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor, cursoEsperado.Descricao);
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveTerNomeVazio(string nomeInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                CursoBuilder.Instancia().ComNome(nomeInvalido).Build()).ComMensagem("Nome inválido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveTerCargaHoraiaMenorQueUm(double cargaHorariaInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                CursoBuilder.Instancia().ComCargaHoraria(cargaHorariaInvalido).Build()).ComMensagem("Carga horaria inválida!");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        [InlineData(-250)]
        public void NaoDeveTerValorMenorQueUm(double valorInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                CursoBuilder.Instancia().ComValor(valorInvalido).Build()).ComMensagem("Valor inválido");
        }
    }
}
