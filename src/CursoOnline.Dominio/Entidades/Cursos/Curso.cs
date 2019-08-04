using System;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Enums;

namespace CursoOnline.Dominio.Entidades.Cursos
{
    public class Curso : Entidade
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }


        private Curso() { }

        public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor, string descricao)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
                .Quando(cargaHoraria < 1, Resource.CargaHorariaInvalida)
                .Quando(valor < 1, Resource.ValorInvalido)
                .DispararExcessaoSeExistir();

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
            Descricao = descricao;
        }

        public void AlterarNome(string nome)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
                .DispararExcessaoSeExistir();

            Nome = nome;
        }

        public void AlterarCargaHoraria(double cargaHoraria)
        {
            ValidadorDeRegra.Novo()
               .Quando(cargaHoraria < 1, Resource.CargaHorariaInvalida)
               .DispararExcessaoSeExistir();

            CargaHoraria = cargaHoraria;
        }

        public void AlterarValor(double valor)
        {
            ValidadorDeRegra.Novo()
                .Quando(valor < 1, Resource.ValorInvalido)
                .DispararExcessaoSeExistir();

            Valor = valor;
        }
    }
}