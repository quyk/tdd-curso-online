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
                .Quando(string.IsNullOrEmpty(nome), "Nome inválido")
                .Quando(cargaHoraria < 1, "Carga horaria inválida!")
                .Quando(valor < 1, "Valor inválido")
                .DispararExcessaoSeExistir();

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
            Descricao = descricao;
        }
    }
}