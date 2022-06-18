using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFornecedor
{
    [TestClass]
    public class RepositorioFornecedorEmBancoDadosTest
    {
        public RepositorioFornecedorEmBancoDadosTest()
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
        public void Deve_inserir_fornecedor()
        {
            Fornecedor novoFornecedor = new Fornecedor(
                "Nome Fornecedor", 
                "7070707070",
                "forcer@email.com",
                "Lages",
                "Feliz"
                );

            var repositorio = new RepositorioFornecedorEmBancoDados();

            repositorio.Inserir(novoFornecedor);

            Fornecedor fornecedorEncontrado = repositorio.SelecionarPorNumero(novoFornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(novoFornecedor.Id, fornecedorEncontrado.Id);
            Assert.AreEqual(novoFornecedor.Nome, fornecedorEncontrado.Nome);
            Assert.AreEqual(novoFornecedor.Telefone, fornecedorEncontrado.Telefone);
            Assert.AreEqual(novoFornecedor.Email, fornecedorEncontrado.Email);
            Assert.AreEqual(novoFornecedor.Cidade, fornecedorEncontrado.Cidade);
            Assert.AreEqual(novoFornecedor.Estado, fornecedorEncontrado.Estado);
        }

        [TestMethod]
        public void Deve_editar_fornecedor()
        {
            Fornecedor novoFornecedor = new Fornecedor(
                "Nome Fornecedor",
                "7070707070",
                "fornecer@email.com",
                "Lages",
                "Feliz"
                );

            var repositorio = new RepositorioFornecedorEmBancoDados();

            repositorio.Inserir(novoFornecedor);

            Fornecedor fornecedorAtualizado = repositorio.SelecionarPorNumero(novoFornecedor.Id);
            fornecedorAtualizado.Nome = "Nome atualizado";
            fornecedorAtualizado.Telefone = "Telefone atualizado";
            fornecedorAtualizado.Email = "Email atualizado";
            fornecedorAtualizado.Cidade = "Cidade atualizada";
            fornecedorAtualizado.Estado = "Estado atualizado";

            repositorio.Editar(fornecedorAtualizado);

            Fornecedor fornecedorEncontrado = repositorio.SelecionarPorNumero(novoFornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(fornecedorAtualizado.Id, fornecedorEncontrado.Id);
            Assert.AreEqual(fornecedorAtualizado.Nome, fornecedorEncontrado.Nome);
            Assert.AreEqual(fornecedorAtualizado.Telefone, fornecedorEncontrado.Telefone);
            Assert.AreEqual(fornecedorAtualizado.Email, fornecedorEncontrado.Email);
            Assert.AreEqual(fornecedorAtualizado.Cidade, fornecedorEncontrado.Cidade);
            Assert.AreEqual(fornecedorAtualizado.Estado, fornecedorEncontrado.Estado);
        }

        // TODO Validar exclusão de fornecedor
        [TestMethod]
        public void Deve_excluir_fornecedor()
        {
            Fornecedor novoFornecedor = new Fornecedor(
                "Nome Fornecedor",
                "7070707070",
                "fornecer@email.com",
                "Lages",
                "Feliz"
                );

            var repositorio = new RepositorioFornecedorEmBancoDados();

            repositorio.Inserir(novoFornecedor);

            repositorio.Excluir(novoFornecedor);

            Fornecedor fornecedorEncontrado = repositorio.SelecionarPorNumero(novoFornecedor.Id);

            Assert.IsNull(fornecedorEncontrado);
        }

        [TestMethod]
        public void Nao_deve_excluir_fornecedor_atrelado_a_um_medicamento()
        {
            var fornecedor = new Fornecedor("Nome Fornecedor", "7070707070", "fornecer@email.com", "Lages", "Feliz");
            var repositorioFornecedor = new RepositorioFornecedorEmBancoDados();
            repositorioFornecedor.Inserir(fornecedor);

            Medicamento novoMedicamento = new Medicamento("BoaDor", "Descricao - Doí ", "Lote123", DateTime.Now.Date, 20, fornecedor);
            var repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            repositorioMedicamento.Inserir(novoMedicamento);

            var retorno = repositorioFornecedor.Excluir(fornecedor);

            Assert.AreEqual(retorno.Errors[0].ToString(), "Não foi possível remover o registro,há medicamentos atrelados a ele");
        }

        [TestMethod]
        public void Deve_selecionar_um_fornecedor()
        {
            Fornecedor novoFornecedor = new Fornecedor(
                "Nome Fornecedor",
                "7070707070",
                "fornecer@email.com",
                "Lages",
                "Feliz"
                );

            var repositorio = new RepositorioFornecedorEmBancoDados();

            repositorio.Inserir(novoFornecedor);

            Fornecedor fornecedorEncontrado = repositorio.SelecionarPorNumero(novoFornecedor.Id);

            Assert.IsNotNull(fornecedorEncontrado);
            Assert.AreEqual(novoFornecedor.Id, fornecedorEncontrado.Id);
            Assert.AreEqual(novoFornecedor.Nome, fornecedorEncontrado.Nome);
            Assert.AreEqual(novoFornecedor.Telefone, fornecedorEncontrado.Telefone);
            Assert.AreEqual(novoFornecedor.Email, fornecedorEncontrado.Email);
            Assert.AreEqual(novoFornecedor.Cidade, fornecedorEncontrado.Cidade);
            Assert.AreEqual(novoFornecedor.Estado, fornecedorEncontrado.Estado);
        }

        [TestMethod]
        public void Deve_selecionar_todos_fornecedores()
        {
            var repositorio = new RepositorioFornecedorEmBancoDados();

            Fornecedor fornecedor1 = new Fornecedor(
                "Nome Fornecedor1",
                "7070707070",
                "fornecer@email.com",
                "Lages",
                "Feliz"
                );
            repositorio.Inserir(fornecedor1);

            Fornecedor fornecedor2 = new Fornecedor(
                "Nome Fornecedor22",
                "7070707070",
                "fornecer@email.com",
                "Lages",
                "Feliz"
                );
            repositorio.Inserir(fornecedor2);

            Fornecedor fornecedor3 = new Fornecedor(
                "Nome Fornecedor333",
                "7070707070",
                "fornecer@email.com",
                "Lages",
                "Feliz"
                );
            repositorio.Inserir(fornecedor3);

            var fornecedores = repositorio.SelecionarTodos();

            Assert.AreEqual(3, fornecedores.Count);

            Assert.AreEqual("Nome teste 1", fornecedores[0].Nome);
            Assert.AreEqual("Nome teste 2", fornecedores[1].Nome);
            Assert.AreEqual("Nome teste 3", fornecedores[2].Nome);
        }

    }
}
