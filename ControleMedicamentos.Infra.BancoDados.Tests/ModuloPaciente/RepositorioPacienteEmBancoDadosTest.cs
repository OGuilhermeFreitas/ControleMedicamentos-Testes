using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloPaciente
{
    [TestClass]
    public class RepositorioPacienteEmBancoDadosTest
    {
        public RepositorioPacienteEmBancoDadosTest()
        {
            string sql =
                @"DELETE FROM TBREQUISICAO;
                  DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)

                  DELETE FROM TBPACIENTE;
                  DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)";

            Database.ExecutarSql(sql);
        }

        [TestMethod]
        public void Deve_inserir_paciente()
        {
            //arrange
            Paciente novoPaciente = new Paciente("Nomezinho", "OCartao");

            var repositorio = new RepositorioPacienteEmBancoDados();

            //action
            repositorio.Inserir(novoPaciente);

            //assert
            Paciente pacienteEncontrado = repositorio.SelecionarPorNumero(novoPaciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(novoPaciente.Id, pacienteEncontrado.Id);
            Assert.AreEqual(novoPaciente.Nome, pacienteEncontrado.Nome);
            Assert.AreEqual(novoPaciente.CartaoSUS, pacienteEncontrado.CartaoSUS);
        }

        [TestMethod]
        public void Deve_editar_paciente()
        {
            //arrange
            Paciente novoPaciente = new Paciente("Nomezinho", "OCartao");
            var repositorio = new RepositorioPacienteEmBancoDados();
            repositorio.Inserir(novoPaciente);

            Paciente pacienteAtualizado = repositorio.SelecionarPorNumero(novoPaciente.Id);
            pacienteAtualizado.Nome = "Nomezinho version 2";
            pacienteAtualizado.CartaoSUS = "Cartao Segundo";

            //action
            repositorio.Editar(pacienteAtualizado);

            //assert
            Paciente pacienteEncontrado = repositorio.SelecionarPorNumero(novoPaciente.Id);

            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(pacienteAtualizado.Id, pacienteEncontrado.Id);
            Assert.AreEqual(pacienteAtualizado.Nome, pacienteEncontrado.Nome);
            Assert.AreEqual(pacienteAtualizado.CartaoSUS, pacienteEncontrado.CartaoSUS);
        }

        [TestMethod]
        public void Deve_excluir_paciente()
        {
            //arrange
            Paciente novoPaciente = new Paciente("Nomezinho", "OCartao");

            var repositorio = new RepositorioPacienteEmBancoDados();

            repositorio.Inserir(novoPaciente);

            //action
            repositorio.Excluir(novoPaciente);

            //assert
            Paciente tarefaEncontrada = repositorio.SelecionarPorNumero(novoPaciente.Id);

            Assert.IsNull(tarefaEncontrada);
        }

        [TestMethod]
        public void Deve_selecionar_um_paciente()
        {
            //arrange
            Paciente novoPaciente = new Paciente("Nomezinho", "OCartao");

            var repositorio = new RepositorioPacienteEmBancoDados();

            repositorio.Inserir(novoPaciente);

            //action
            Paciente pacienteEncontrado = repositorio.SelecionarPorNumero(novoPaciente.Id);

            //assert
            Assert.IsNotNull(pacienteEncontrado);
            Assert.AreEqual(novoPaciente.Id, pacienteEncontrado.Id);
            Assert.AreEqual(novoPaciente.Nome, pacienteEncontrado.Nome);
            Assert.AreEqual(novoPaciente.CartaoSUS, pacienteEncontrado.CartaoSUS);
        }

        [TestMethod]
        public void Deve_selecionar_todos_pacientes()
        {
            var repositorio = new RepositorioPacienteEmBancoDados();

            Paciente paciente1 = new Paciente("Nomezinho", "OCartao");
            repositorio.Inserir(paciente1);

            Paciente paciente2 = new Paciente("Nomezinho2", "OCartao2");
            repositorio.Inserir(paciente2);

            Paciente paciente3 = new Paciente("Nomezinho3", "OCartao3");
            repositorio.Inserir(paciente3);

            //action
            var pacientes = repositorio.SelecionarTodos();

            //assert
            Assert.AreEqual(3, pacientes.Count);

            Assert.AreEqual("Nomezinho", pacientes[0].Nome);
            Assert.AreEqual("Nomezinho2", pacientes[1].Nome);
            Assert.AreEqual("Nomezinho3", pacientes[2].Nome);
        }

    }
}
