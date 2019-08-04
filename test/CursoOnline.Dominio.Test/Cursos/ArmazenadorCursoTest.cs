using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Entidades.Cursos;
using CursoOnline.Dominio.Interface;
using CursoOnline.Dominio.Test.Builders;
using CursoOnline.Dominio.Test.Util;
using Moq;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class ArmazenadorCursoTest
    {
        private readonly CursoDto _cursoDto;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;

        public ArmazenadorCursoTest()
        {
            var faker = new Faker();

            _cursoDto = new CursoDto
            {
                Nome = faker.Name.FullName(),
                Descricao = faker.Lorem.Paragraph(6),
                CargaHoraria = faker.Random.Double(50, 150),
                PublicoAlvo = "Estudante",
                Valor = faker.Random.Double(50, 500)
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            _armazenadorDeCurso.Armazenar(_cursoDto);
            _cursoRepositorioMock.Verify(r => r.Adicionar(
                It.Is<Curso>(
                    x => x.Nome == _cursoDto.Nome && x.Descricao == _cursoDto.Descricao
                )
            ));
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            _cursoDto.PublicoAlvo = "Medico";
            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeCurso.Armazenar(_cursoDto)).ComMensagem(Resource.PublicoAlvoInvalido);
        }

        [Fact]
        public void NaoDeveAdicionarCursoComNomeExistente()
        {
            var cursoExistente = CursoBuilder.Instancia().ComId(432).ComNome(_cursoDto.Nome).Build();
            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoExistente);

            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeCurso.Armazenar(_cursoDto)).ComMensagem(Resource.CursoExistente);
        }

        [Fact]
        public void DeveAlterarDadosDoCurso()
        {
            _cursoDto.Id = 233;
            var curso = CursoBuilder.Instancia().Build();

            _cursoRepositorioMock.Setup(x => x.ObterPorId(_cursoDto.Id)).Returns(curso);

            _armazenadorDeCurso.Armazenar(_cursoDto);

            Assert.Equal(_cursoDto.Nome, curso.Nome);
            Assert.Equal(_cursoDto.Valor, curso.Valor);
            Assert.Equal(_cursoDto.CargaHoraria, curso.CargaHoraria);
        }

        [Fact]
        public void NaoDeveAdicionarNoRepositorioQuandoCursoJaExiste()
        {
            _cursoDto.Id = 233;
            var curso = CursoBuilder.Instancia().Build();

            _cursoRepositorioMock.Setup(x => x.ObterPorId(_cursoDto.Id)).Returns(curso);

            _armazenadorDeCurso.Armazenar(_cursoDto);

            _cursoRepositorioMock.Verify(v => v.Adicionar(It.IsAny<Curso>()), Times.Never);
        }
    }
}