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
        private readonly Faker _faker;
        private readonly ITestOutputHelper _outputHelper;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        public CursoTest(ITestOutputHelper outputHelper)
        {
            _faker = new Faker();

            _outputHelper = outputHelper;
            _outputHelper.WriteLine("Construtor sendo executado");

            _nome = _faker.Name.FullName();
            _cargaHoraria = _faker.Random.Double(50, 250);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = _faker.Random.Number(0, 250);
            _descricao = _faker.Lorem.Paragraph();
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
        public void NaoDeveTerNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                CursoBuilder.Instancia().ComNome(nomeInvalido).Build()).ComMensagem("Nome inválido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveTerCargaHorariaInvalida(double cargaHorariaInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                CursoBuilder.Instancia().ComCargaHoraria(cargaHorariaInvalido).Build()).ComMensagem("Carga horaria inválida!");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        [InlineData(-250)]
        public void NaoDeveTerValorInvalido(double valorInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                CursoBuilder.Instancia().ComValor(valorInvalido).Build()).ComMensagem("Valor inválido");
        }

        [Fact]
        public void DeveAlterarNome()
        {
            var nomeEsperado = _faker.Person.FullName;
            var curso = CursoBuilder.Instancia().Build();

            curso.AlterarNome(nomeEsperado);

            Assert.Equal(nomeEsperado, curso.Nome);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarComNomeInvalido(string nomeInvalido)
        {
            var curso = CursoBuilder.Instancia().Build();
            Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarNome(nomeInvalido)).ComMensagem("Nome inválido");
        }

        [Fact]
        public void DeveAlterarCargaHoraria()
        {
            var cargaHorariaEsperada = _faker.Random.Double(50, 250);
            var curso = CursoBuilder.Instancia().Build();

            curso.AlterarCargaHoraria(cargaHorariaEsperada);

            Assert.Equal(cargaHorariaEsperada, curso.CargaHoraria);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveAlterarComCargaHorariaInvalida(double cargaHoraria)
        {
            var curso = CursoBuilder.Instancia().Build();

            Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarCargaHoraria(cargaHoraria)).ComMensagem("Carga horaria inválida!");
        }

        [Fact]
        public void DeveAlterarValor()
        {
            var valorEsperado = _faker.Random.Number(0, 250);
            var curso = CursoBuilder.Instancia().Build();

            curso.AlterarValor(valorEsperado);

            Assert.Equal(valorEsperado, curso.Valor);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-20)]
        [InlineData(-250)]
        public void NaoDeveAlterarComValorInvalido(double valorInvalido)
        {
            var curso = CursoBuilder.Instancia().Build();

            Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarValor(valorInvalido)).ComMensagem("Valor inválido");
        }
    }
}
