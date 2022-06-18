﻿using ControleMedicamentos.Dominio.ModuloMedicamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{
    [TestClass]
    public class ValidadorMedicamentoTest
    {
        public ValidadorMedicamentoTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void Nome_deve_ser_obrigatorio()
        {
            var medicamento = new Medicamento();
            medicamento.Nome = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(medicamento);

            Assert.AreEqual("'Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Descricao_deve_ser_obrigatoria()
        {
            var medicamento = new Medicamento();
            medicamento.Nome = "Nome";
            medicamento.Descricao = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(medicamento);

            Assert.AreEqual("'Descricao' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Lote_deve_ser_obrigatorio()
        {
            var medicamento = new Medicamento();
            medicamento.Nome = "Nome";
            medicamento.Descricao = "Descricao";
            medicamento.Lote = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(medicamento);

            Assert.AreEqual("'Lote' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Validade_deve_ser_obrigatoria()
        {
            var medicamento = new Medicamento();
            medicamento.Nome = "Nome";
            medicamento.Descricao = "Descricao";
            medicamento.Lote = "Lote";
            medicamento.Validade = DateTime.MinValue;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(medicamento);

            Assert.AreEqual("O campo Validade é obrigatório", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void QuantidadeDisponivel_deve_ser_maior_que_0()
        {
            var medicamento = new Medicamento();
            medicamento.Nome = "Nome";
            medicamento.Descricao = "Descricao";
            medicamento.Lote = "Lote";
            medicamento.Validade = DateTime.Now;
            medicamento.QuantidadeDisponivel = -1;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(medicamento);

            Assert.AreEqual("Quantidade deve ser maior que 0", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Fornecedor_deve_ser_obrigatorio()
        {
            var medicamento = new Medicamento();
            medicamento.Nome = "Nome";
            medicamento.Descricao = "Descricao";
            medicamento.Lote = "Lote";
            medicamento.Validade = DateTime.Now;
            medicamento.QuantidadeDisponivel = 10;
            medicamento.Fornecedor = null;

            ValidadorMedicamento validador = new ValidadorMedicamento();

            var resultado = validador.Validate(medicamento);

            Assert.AreEqual("'Fornecedor' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

    }
}
