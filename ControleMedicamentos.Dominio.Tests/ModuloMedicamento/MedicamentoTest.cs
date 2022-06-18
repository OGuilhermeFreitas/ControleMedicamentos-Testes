using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{
    [TestClass]
    public class MedicamentoTest
    {
        [TestMethod]
        public void Deve_adicionar_fornecedor_no_medicamento()
        {
            var medicamento = new Medicamento();

            var fornecedor = new Fornecedor("Nome", "70707070", "Email@email.com", "Lages", "Alegre");
            medicamento.Fornecedor = fornecedor;

            Assert.IsNotNull(medicamento.Fornecedor, fornecedor.ToString());
        }
    }
}
