using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Importar / usar as bibliotecas para conectar com o BD
using MySql.Data.MySqlClient;

namespace ControleFinanceiro {
    public class FormaDePagamento {
        // Definição da lista de atributos (propriedades)
        public int codigo { get; set; }
        public string formaDePagamento { get; set; }
        
        // Definição dos Métodos Construtores
        public FormaDePagamento() { 
        }

        public FormaDePagamento(string formaDePagamento) {
            this.formaDePagamento = formaDePagamento;
        }
       
        // Método para Inserir uma nova forma de pagamento no BD
        public string insert() {
            // Instanciar um objeto da classe de Conexão
            Conexao c = new Conexao();
            try {
                // Abrir a conexão com o BD
                c.abreConexao();
                // Definir o comando SQL (INSERT) que insere uma nova movimentação no BD
                MySqlCommand cmd = new MySqlCommand(@"INSERT INTO tblformadepagamento(formadepagamento) VALUES(@formadepagamento);", c.conexaoBD());
                // Define os valores para os parâmetros 
                cmd.Parameters.AddWithValue("@formadepagamento", this.formaDePagamento);
                // Executar o comando SQL (insert). Esse método retorna o total de linhas afetas no BD
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception erro) {
                return erro.Message;
            }
            finally {
                // Fecha a conexão com o BD
                c.fechaConexao();
            }
        }

        // Método Selecionar todas as formas de pagamento cadastradas no BD
        public DataTable select() {
            // Criar uma tabela genérica 
            DataTable tabela = new DataTable();
            // Criar um objeto da classe de conexão com BD
            Conexao conexao = new Conexao();
            try {
                conexao.abreConexao();
                // Definir o comando SQL (SELECT) e o BD que o comando será executado
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tblformadepagamento;", conexao.conexaoBD());
                // Executar a consulta SQL e armazenar os dados retornados
                MySqlDataReader dados = cmd.ExecuteReader();
                // Verificar se a consulta retornou algum registro da tabela do BD
                if (dados.HasRows == true) {
                    // Carregar os dados do DataReader para o DataTable
                    tabela.Load(dados);
                }
                else {
                    tabela = new DataTable("erro");
                    tabela.Rows.Add("Nenhuma forma de pagamento encontrada");
                }
                return tabela;
            }
            catch (Exception erro) {
                tabela = new DataTable("erro");
                tabela.Rows.Add("Erro: " + erro.Message);
                return tabela;
            }
            finally {
                conexao.fechaConexao();
            }
        }
        // Método para Atualizar uma determinada forma de pagamento
        public string update() {
            //MySqlConnection con = new MySqlConnection(strConexao);
            Conexao c = new Conexao();
            try {
                //con.Open();
                c.abreConexao();
                MySqlCommand cmd = new MySqlCommand(@"
                UPDATE tblformadepagamento SET formadepagamento = @formadepagamento WHERE codigo = @codigo", c.conexaoBD());
                cmd.Parameters.AddWithValue("@formadepagamento", this.formaDePagamento);
                cmd.Parameters.AddWithValue("@codigo", this.codigo);
                // Executar o comando SQL (UPDATE)
                cmd.ExecuteNonQuery();
                //MessageBox.Show("Movimentação atualizada com sucesso!");
                return "ok";
            }
            catch (Exception erro) {
                //MessageBox.Show("Erro: " + erro.Message);
                return "Erro: " + erro.Message;
            }
            finally {
                //con.Close();
                c.fechaConexao();
            }
        }
        // Método para Excluir uma determinada forma de pagamento
        public string delete(int codigo) {
            //MySqlConnection con = new MySqlConnection(strConexao);
            Conexao c = new Conexao();
            try {
                //con.Open();
                c.abreConexao();
                MySqlCommand cmd = new MySqlCommand(@"
                DELETE FROM tblformadepagamento 
                WHERE codigo = " + codigo, c.conexaoBD());
                cmd.ExecuteNonQuery();
                //MessageBox.Show("Movimentação excluída com sucesso!");
                return "ok";
            }
            catch (Exception erro) {
                // MessageBox.Show("Erro: " + erro.Message);
                return "Erro: " + erro.Message;
            }
            finally {
                // con.Close();
                c.fechaConexao();
            }
        }
    }
}
