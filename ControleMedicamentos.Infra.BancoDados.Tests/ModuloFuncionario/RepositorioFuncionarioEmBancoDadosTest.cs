using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFuncionario
{
    [TestClass]
    public class RepositorioFuncionarioEmBancoDadosTest
    {
        public RepositorioFuncionarioEmBancoDadosTest()
        {
            string sql =
                @"DELETE FROM TBFUNCIONARIO;
                  DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)";

            Database.ExecutarSql(sql);
        }

        [TestMethod]
        public void Deve_inserir_funcionario()
        {
            Funcionario novoFuncionario = new Funcionario("Nome", "Login", "Senha321");

            var repositorio = new RepositorioFuncionarioEmBancoDados();

            repositorio.Inserir(novoFuncionario);

            Funcionario funcionarioEncontrado = repositorio.SelecionarPorNumero(novoFuncionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(novoFuncionario.Id, funcionarioEncontrado.Id);
            Assert.AreEqual(novoFuncionario.Nome, funcionarioEncontrado.Nome);
            Assert.AreEqual(novoFuncionario.Login, funcionarioEncontrado.Login);
            Assert.AreEqual(novoFuncionario.Senha, funcionarioEncontrado.Senha);
        }

        [TestMethod]
        public void Deve_editar_funcionario()
        {
            Funcionario novoFuncionario = new Funcionario("Nome", "Login", "Senha321");
            var repositorio = new RepositorioFuncionarioEmBancoDados();
            repositorio.Inserir(novoFuncionario);

            Funcionario funcionarioAtualizado = repositorio.SelecionarPorNumero(novoFuncionario.Id);
            funcionarioAtualizado.Nome = "Nome atualizado";
            funcionarioAtualizado.Login = "Login Atualizado";
            funcionarioAtualizado.Senha = "Senha atualizada";

            repositorio.Editar(funcionarioAtualizado);

            Funcionario funcionarioEncontrado = repositorio.SelecionarPorNumero(novoFuncionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(funcionarioAtualizado.Id, funcionarioEncontrado.Id);
            Assert.AreEqual(funcionarioAtualizado.Nome, funcionarioEncontrado.Nome);
            Assert.AreEqual(funcionarioAtualizado.Login, funcionarioEncontrado.Login);
            Assert.AreEqual(funcionarioAtualizado.Senha, funcionarioEncontrado.Senha);
        }

        [TestMethod]
        public void Deve_excluir_funcionario()
        {
            Funcionario novoFuncionario = new Funcionario("Nome", "Login", "Senha321");
            var repositorio = new RepositorioFuncionarioEmBancoDados();
            repositorio.Inserir(novoFuncionario);

            repositorio.Excluir(novoFuncionario);

            Funcionario funcionarioEncontrado = repositorio.SelecionarPorNumero(novoFuncionario.Id);
            Assert.IsNull(funcionarioEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_um_funcionario()
        {
            Funcionario novoFuncionario = new Funcionario("Nome", "Login", "Senha321");
            var repositorio = new RepositorioFuncionarioEmBancoDados();
            repositorio.Inserir(novoFuncionario);

            Funcionario funcionarioEncontrado = repositorio.SelecionarPorNumero(novoFuncionario.Id);

            Assert.IsNotNull(funcionarioEncontrado);
            Assert.AreEqual(novoFuncionario.Id, funcionarioEncontrado.Id);
            Assert.AreEqual(novoFuncionario.Nome, funcionarioEncontrado.Nome);
            Assert.AreEqual(novoFuncionario.Login, funcionarioEncontrado.Login);
            Assert.AreEqual(novoFuncionario.Senha, funcionarioEncontrado.Senha);
        }

        [TestMethod]
        public void Deve_selecionar_todos_funcionarios()
        {
            var repositorio = new RepositorioFuncionarioEmBancoDados();

            Funcionario funcionario1 = new Funcionario("Nome1", "Login1", "Senha1");
            repositorio.Inserir(funcionario1);

            Funcionario funcionario2 = new Funcionario("Nome22", "Login22", "Senha22");
            repositorio.Inserir(funcionario2);

            Funcionario funcionario3 = new Funcionario("Nome333", "Login333", "Senha333");
            repositorio.Inserir(funcionario3);

            var funcionarios = repositorio.SelecionarTodos();

            Assert.AreEqual(3, funcionarios.Count);

            Assert.AreEqual("Nome1", funcionarios[0].Nome);
            Assert.AreEqual("Nome22", funcionarios[1].Nome);
            Assert.AreEqual("Nome333", funcionarios[2].Nome);
        }

    }
}
