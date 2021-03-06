using ControleMedicamentos.Dominio.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloFuncionario
{
    [TestClass]
    public class FuncionarioTest
    {
        public FuncionarioTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void Nome_do_funcionario_deve_ser_obrigatorio()
        {
            var funcionario = new Funcionario();
            funcionario.Nome = null;

            ValidadorFuncionario validador = new ValidadorFuncionario();

            var resultado = validador.Validate(funcionario);

            Assert.AreEqual("'Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Login_do_funcionario_deve_ser_obrigatorio()
        {
            var funcionario = new Funcionario();
            funcionario.Nome = "Nome";
            funcionario.Login = null;

            ValidadorFuncionario validador = new ValidadorFuncionario();

            var resultado = validador.Validate(funcionario);

            Assert.AreEqual("'Login' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Senha_do_funcionario_deve_ser_obrigatorio()
        {
            var funcionario = new Funcionario();
            funcionario.Nome = "Nome";
            funcionario.Login = "Login";
            funcionario.Senha = null;

            ValidadorFuncionario validador = new ValidadorFuncionario();

            var resultado = validador.Validate(funcionario);

            Assert.AreEqual("'Senha' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

    }
}
