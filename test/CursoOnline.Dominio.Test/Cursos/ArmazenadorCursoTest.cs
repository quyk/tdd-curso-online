using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.DTO;
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
        private readonly ArmazenadorCurso _armazenadorCurso;

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
            _armazenadorCurso = new ArmazenadorCurso(_cursoRepositorioMock.Object);
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            _armazenadorCurso.Armazenar(_cursoDto);

            //cursoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Curso>()));
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
            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorCurso.Armazenar(_cursoDto)).ComMensagem(Resource.PublicoAlvoInvalido);
        }

        [Fact]
        public void NaoDeveAdicionarCursoComNomeExistente()
        {
            var cursoExistente = CursoBuilder.Instancia().ComNome(_cursoDto.Nome).Build();
            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoExistente);

            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorCurso.Armazenar(_cursoDto)).ComMensagem(Resource.CursoExistente);
        }
    }
}