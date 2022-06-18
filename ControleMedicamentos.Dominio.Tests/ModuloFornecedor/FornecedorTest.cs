using ControleMedicamentos.Dominio.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloFornecedor
{
    [TestClass]
    public class FornecedorTest
    {
        public FornecedorTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void Nome_deve_ser_obrigatorio()
        {
            var fornecedor = new Fornecedor();
            fornecedor.Nome = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultado = validador.Validate(fornecedor);

            Assert.AreEqual("'Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Telefone_deve_ser_obrigatorio()
        {
            var fornecedor = new Fornecedor();
            fornecedor.Nome = "Nome";
            fornecedor.Telefone = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultado = validador.Validate(fornecedor);

            Assert.AreEqual("'Telefone' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Email_deve_ser_obrigatorio()
        {
            var fornecedor = new Fornecedor();
            fornecedor.Nome = "Nome";
            fornecedor.Telefone = "Telefone";
            fornecedor.Email = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultado = validador.Validate(fornecedor);

            Assert.AreEqual("'Email' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Cidade_deve_ser_obrigatoria()
        {
            var fornecedor = new Fornecedor();
            fornecedor.Nome = "Nome";
            fornecedor.Telefone = "Telefone";
            fornecedor.Email = "Email";
            fornecedor.Cidade = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultado = validador.Validate(fornecedor);

            Assert.AreEqual("'Cidade' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Estado_deve_ser_obrigatorio()
        {
            var fornecedor = new Fornecedor();
            fornecedor.Nome = "Nome";
            fornecedor.Telefone = "Telefone";
            fornecedor.Email = "Email";
            fornecedor.Cidade = "Cidade";
            fornecedor.Estado = null;

            ValidadorFornecedor validador = new ValidadorFornecedor();

            var resultado = validador.Validate(fornecedor);

            Assert.AreEqual("'Estado' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

    }
}
