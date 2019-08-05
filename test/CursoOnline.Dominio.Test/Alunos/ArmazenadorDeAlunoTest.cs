using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Entidades.Alunos;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Interface;
using CursoOnline.Dominio.Test.Builders;
using CursoOnline.Dominio.Test.Util;
using Moq;
using Xunit;

namespace CursoOnline.Dominio.Test.Alunos
{
    public class ArmazenadorDeAlunoTest
    {
        private readonly Faker _faker;
        private readonly AlunoDto _alunoDto;
        private readonly ArmazenadorDeAluno _armazenadorDeAluno;
        private readonly Mock<IAlunoRepositorio> _alunoRepositorio;

        public ArmazenadorDeAlunoTest()
        {
            _faker = new Faker();
            _alunoDto = new AlunoDto
            {
                Nome = _faker.Person.FullName,
                Email = _faker.Person.Email,
                Cpf = _faker.Person.Cpf(),
                PublicoAlvo = PublicoAlvo.Empregador.ToString(),
            };
            _alunoRepositorio = new Mock<IAlunoRepositorio>();
            var conversorDePublicoAlvo = new Mock<IConversorDePublicoAlvo>();
            _armazenadorDeAluno = new ArmazenadorDeAluno(_alunoRepositorio.Object, conversorDePublicoAlvo.Object);
        }

        [Fact]
        public void DeveAdicionarAluno()
        {
            _armazenadorDeAluno.Armazenar(_alunoDto);
            _alunoRepositorio.Verify(r => r.Adicionar(It.Is<Aluno>(a => a.Nome == _alunoDto.Nome)));
        }

        [Fact]
        public void NaoDeveAdicionarAlunoQuandoCpfJaFoiCadastrado()
        {
            var alunoComMesmoCpf = AlunoBuilder.Novo().ComId(34).Build();

            _alunoRepositorio.Setup(r => r.ObterPeloCpf(_alunoDto.Cpf)).Returns(alunoComMesmoCpf);

            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeAluno.Armazenar(_alunoDto))
                .ComMensagem(Resource.CpfExistente);
        }

        [Fact]
        public void DeveEditarNomeDoAluno()
        {
            _alunoDto.Id = 35;
            _alunoDto.Nome = _faker.Person.FullName;

            var alunoJaSalvo = AlunoBuilder.Novo().Build();

            _alunoRepositorio.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(alunoJaSalvo);

            _armazenadorDeAluno.Armazenar(_alunoDto);

            Assert.Equal(_alunoDto.Nome, alunoJaSalvo.Nome);
        }

        [Fact]
        public void NaoDeveEditarDemaisInformacoesDoAluno()
        {
            _alunoDto.Id = 35;
            var alunoJaSalvo = AlunoBuilder.Novo().Build();
            var cpfEsperado = alunoJaSalvo.Cpf;
            var emailEsperado = alunoJaSalvo.Email;
            var publicoAlvoEsperado = alunoJaSalvo.PublicoAlvo;
            _alunoRepositorio.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(alunoJaSalvo);

            _armazenadorDeAluno.Armazenar(_alunoDto);

            Assert.Equal(cpfEsperado, alunoJaSalvo.Cpf);
            Assert.Equal(emailEsperado, alunoJaSalvo.Email);
            Assert.Equal(publicoAlvoEsperado, alunoJaSalvo.PublicoAlvo);
        }

        [Fact]
        public void NaoDeveAdicionarQuandoForEdicao()
        {
            _alunoDto.Id = 35;
            var alunoJaSalvo = AlunoBuilder.Novo().Build();
            _alunoRepositorio.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(alunoJaSalvo);

            _armazenadorDeAluno.Armazenar(_alunoDto);

            _alunoRepositorio.Verify(r => r.Adicionar(It.IsAny<Aluno>()), Times.Never);
        }
    }
}
