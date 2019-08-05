﻿using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio.Entidades.Alunos;
using CursoOnline.Dominio.Enums;
using System;

namespace CursoOnline.Dominio.Test.Builders
{
    public class AlunoBuilder
    {
        protected int Id;
        protected string Nome;
        protected string Email;
        protected string Cpf;
        protected PublicoAlvo PublicoAlvo;

        public static AlunoBuilder Novo()
        {
            var faker = new Faker();

            return new AlunoBuilder
            {
                Nome = faker.Person.FullName,
                Email = faker.Person.Email,
                Cpf = faker.Person.Cpf(),
                PublicoAlvo = PublicoAlvo.Empreendedor
            };
        }

        public AlunoBuilder ComId(int id)
        {
            Id = id;
            return this;
        }

        public AlunoBuilder ComNome(string nome)
        {
            Nome = nome;
            return this;
        }

        public AlunoBuilder ComEmail(string email)
        {
            Email = email;
            return this;
        }

        public AlunoBuilder ComCpf(string cpf)
        {
            Cpf = cpf;
            return this;
        }

        public AlunoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            PublicoAlvo = publicoAlvo;
            return this;
        }

        public Aluno Build()
        {
            var aluno = new Aluno(Nome, Email, Cpf, PublicoAlvo);

            if(aluno.Id > 0)
            {
                var propertyInfo = aluno.GetType().GetProperty("Id");
                propertyInfo.SetValue(aluno, Convert.ChangeType(Id, propertyInfo.PropertyType), null);
            }

            return aluno;
        }
    }
}
