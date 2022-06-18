using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloMedicamento
{
    [TestClass]
    public class RepositorioMedicamentoEmBancoDadosTest
    {
        public RepositorioMedicamentoEmBancoDadosTest()
        {
            string sql =
                @"DELETE FROM TBREQUISICAO;
                DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)

                DELETE FROM TBMEDICAMENTO;
                DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)

                DELETE FROM TBFORNECEDOR;
                DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)";

            Database.ExecutarSql(sql);
        }

        [TestMethod]
        public void Deve_inserir_medicamento()
        {
            var fornecedor = new Fornecedor("NomeFornecedor", "49-7070-7070", "junin@email.com", "Lages", "Este Aqui");
            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFornecedor.Inserir(fornecedor);

            Medicamento novoMedicamento = new Medicamento("Dor", "Causa Dor", "Lote123", DateTime.Now.Date, 10, fornecedor);
            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();            
            repositorioMedicamento.Inserir(novoMedicamento);

            Medicamento medicamentoEncontrado = repositorioMedicamento.SelecionarPorNumero(novoMedicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(novoMedicamento.Id, medicamentoEncontrado.Id);
            Assert.AreEqual(novoMedicamento.Nome, medicamentoEncontrado.Nome);
            Assert.AreEqual(novoMedicamento.Descricao, medicamentoEncontrado.Descricao);
            Assert.AreEqual(novoMedicamento.Lote, medicamentoEncontrado.Lote);
            Assert.AreEqual(novoMedicamento.Validade, medicamentoEncontrado.Validade);
            Assert.AreEqual(novoMedicamento.Fornecedor.Id, medicamentoEncontrado.Fornecedor.Id);
        }

        [TestMethod]
        public void Deve_editar_medicamento()
        {
            var fornecedor = new Fornecedor("NomeFornecedor", "49-7070-7070", "junin@email.com", "Lages", "Este Aqui");
            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFornecedor.Inserir(fornecedor);

            Medicamento novoMedicamento = new Medicamento("Dor", "Causa Dor", "Lote123", DateTime.Now.Date, 10, fornecedor);
            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            repositorioMedicamento.Inserir(novoMedicamento);

            Medicamento medicamentoAtualizado = repositorioMedicamento.SelecionarPorNumero(novoMedicamento.Id);
            medicamentoAtualizado.Nome = "Dor alterada";
            medicamentoAtualizado.Descricao = "Descricao da dor alterada";
            medicamentoAtualizado.Lote = "Lote dolorasamente alterado";
            medicamentoAtualizado.Validade = DateTime.Now.AddDays(10).Date;
            medicamentoAtualizado.QuantidadeDisponivel = 20;
            medicamentoAtualizado.Fornecedor = repositorioFornecedor.SelecionarPorNumero(1);

            repositorioMedicamento.Editar(medicamentoAtualizado);

            Medicamento medicamentoEncontrado = repositorioMedicamento.SelecionarPorNumero(novoMedicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(medicamentoAtualizado.Id, medicamentoEncontrado.Id);
            Assert.AreEqual(medicamentoAtualizado.Nome, medicamentoEncontrado.Nome);
            Assert.AreEqual(medicamentoAtualizado.Descricao, medicamentoEncontrado.Descricao);
            Assert.AreEqual(medicamentoAtualizado.Lote, medicamentoEncontrado.Lote);
            Assert.AreEqual(medicamentoAtualizado.Validade, medicamentoEncontrado.Validade);
            Assert.AreEqual(medicamentoAtualizado.Fornecedor.Id, medicamentoEncontrado.Fornecedor.Id);
        }

        [TestMethod]
        public void Deve_excluir_medicamento()
        {
            var fornecedor = new Fornecedor("NomeFornecedor", "49-7070-7070", "junin@email.com", "Lages", "Este Aqui");
            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFornecedor.Inserir(fornecedor);

            Medicamento novoMedicamento = new Medicamento("Dor", "Causa Dor", "Lote123", DateTime.Now.Date, 10, fornecedor);
            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            repositorioMedicamento.Inserir(novoMedicamento);

            repositorioMedicamento.Excluir(novoMedicamento);

            Medicamento medicamentoEncontrado = repositorioMedicamento.SelecionarPorNumero(novoMedicamento.Id);
            Assert.IsNull(medicamentoEncontrado);
        }

        [TestMethod]
        public void Deve_selecionar_um_medicamento()
        {
            var fornecedor = new Fornecedor("NomeFornecedor", "49-7070-7070", "junin@email.com", "Lages", "Este Aqui");
            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFornecedor.Inserir(fornecedor);

            Medicamento novoMedicamento = new Medicamento("Dor", "Causa muita Dor", "Lote123", DateTime.Now.Date, 10, fornecedor);
            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            repositorioMedicamento.Inserir(novoMedicamento);

            Medicamento medicamentoEncontrado = repositorioMedicamento.SelecionarPorNumero(novoMedicamento.Id);

            Assert.IsNotNull(medicamentoEncontrado);
            Assert.AreEqual(novoMedicamento.Id, medicamentoEncontrado.Id);
            Assert.AreEqual(novoMedicamento.Nome, medicamentoEncontrado.Nome);
            Assert.AreEqual(novoMedicamento.Descricao, medicamentoEncontrado.Descricao);
            Assert.AreEqual(novoMedicamento.Lote, medicamentoEncontrado.Lote);
            Assert.AreEqual(novoMedicamento.Validade, medicamentoEncontrado.Validade);
            Assert.AreEqual(novoMedicamento.Fornecedor.Id, medicamentoEncontrado.Fornecedor.Id);
        }

        [TestMethod]
        public void Deve_selecionar_todos_medicamentos()
        {
            var fornecedor = new Fornecedor("NomeFornecedor", "49-7070-7070", "junin@email.com", "Lages", "Este Aqui");
            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFornecedor.Inserir(fornecedor);

            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            Medicamento medicamento1 = new Medicamento("Dor1", "Causa Dor do tipo 1", "Lote1", DateTime.Now.Date, 10, fornecedor);
            repositorioMedicamento.Inserir(medicamento1);

            Medicamento medicamento2 = new Medicamento("Dor2", "Causa Dor do tipo 2", "Lote22", DateTime.Now.Date, 20, fornecedor);
            repositorioMedicamento.Inserir(medicamento2);

            Medicamento medicamento3 = new Medicamento("Dor3", "Causa Dor do tipo 3", "Lote333", DateTime.Now.Date, 30, fornecedor);
            repositorioMedicamento.Inserir(medicamento3);

            var medicamentos = repositorioMedicamento.SelecionarTodos();

            Assert.AreEqual(3, medicamentos.Count);

            Assert.AreEqual("Nome1", medicamentos[0].Nome);
            Assert.AreEqual("Nome22", medicamentos[1].Nome);
            Assert.AreEqual("Nome333", medicamentos[2].Nome);
        }

        [TestMethod]
        public void Deve_selecionar_medicamentos_com_pouco_estoque()
        {
            var fornecedor = new Fornecedor("NomeFornecedor", "49-7070-7070", "junin@email.com", "Lages", "Este Aqui");
            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFornecedor.Inserir(fornecedor);

            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            Medicamento medicamento1 = new Medicamento("Dor1", "Causa Dor do tipo 1", "Lote1", DateTime.Now.Date, 60, fornecedor);
            repositorioMedicamento.Inserir(medicamento1);

            Medicamento medicamento2 = new Medicamento("Dor2", "Causa Dor do tipo 2", "Lote22", DateTime.Now.Date, 40, fornecedor);
            repositorioMedicamento.Inserir(medicamento2);

            Medicamento medicamento3 = new Medicamento("Dor3", "Causa Dor do tipo 3", "Lote333", DateTime.Now.Date, 20, fornecedor);
            repositorioMedicamento.Inserir(medicamento3);

            var medicamentos = repositorioMedicamento.SelecionarMedicamentosComPoucoEstoque();

            Assert.AreEqual(2, medicamentos.Count);

            Assert.AreEqual("Nome1", medicamentos[0].Nome);
            Assert.AreEqual("Nome22", medicamentos[1].Nome);
        }

    }
}
