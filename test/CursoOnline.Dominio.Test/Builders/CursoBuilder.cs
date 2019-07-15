using Bogus;
using CursoOnline.Dominio.Entidades.Cursos;
using CursoOnline.Dominio.Enums;

namespace CursoOnline.Dominio.Test.Builders
{
    public class CursoBuilder
    {
        private string _nome;
        private double _cargaHoraria;
        private PublicoAlvo _publicoAlvo;
        private double _valor;
        private string _descricao;

        public CursoBuilder()
        {
            var faker = new Faker();

            _nome = faker.Name.FullName();
            _cargaHoraria = faker.Random.Double(50, 250);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = faker.Random.Number(0, 250);
            _descricao = faker.Lorem.Paragraph();
        }

        public static CursoBuilder Instancia()
        {
            return new CursoBuilder();
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComCargaHoraria(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public CursoBuilder ComValor(double valor)
        {
            _valor = valor;
            return this;
        }

        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public Curso Build()
        {
            return new Curso(_nome, _cargaHoraria, _publicoAlvo, _valor, _descricao);
        }
    }
}
