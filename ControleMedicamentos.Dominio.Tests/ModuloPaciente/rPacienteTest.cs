using ControleMedicamentos.Dominio.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.Tests.ModuloPaciente
{
    [TestClass]
    public class rPacienteTest
    {
        public rPacienteTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("pt-BR");
        }

        [TestMethod]
        public void Nome_do_paciente_deve_ser_obrigatorio()
        {
            var paciente = new Paciente();
            paciente.Nome = null;

            ValidadorPaciente validador = new ValidadorPaciente();

            var resultado = validador.Validate(paciente);

            Assert.AreEqual("'Nome' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void CartaoSUS_do_paciente_deve_ser_obrigatorio()
        {
            var paciente = new Paciente();
            paciente.Nome = "Nome";
            paciente.CartaoSUS = null;

            ValidadorPaciente validador = new ValidadorPaciente();

            var resultado = validador.Validate(paciente);

            Assert.AreEqual("'Cartao SUS' não pode ser nulo.", resultado.Errors[0].ErrorMessage);
        }

    }
}
