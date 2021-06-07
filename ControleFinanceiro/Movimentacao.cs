using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Importar / usar as bibliotecas para conectar com o BD
using MySql.Data.MySqlClient;

namespace ControleFinanceiro {
    public class Movimentacao {
        // Definição da lista de atributos (propriedades)
        public int codigo { get; set; }
        public string descricao { get; set; }
        public double valor { get; set; }
        public DateTime dataMov { get; set; }
        public string tipoMov { get; set; }
        public string situacao { get; set; }

        // Definição dos Métodos Construtores
        public Movimentacao() { 
        }

        public Movimentacao(string descricao, double valor, DateTime dataMov, string tipoMov, string situacao) {
            this.descricao = descricao;
            this.valor = valor;
            this.dataMov = dataMov;
            this.tipoMov = tipoMov;
            this.situacao = situacao;
        }
        public Movimentacao(string descricao, double valor, DateTime dataMov, string tipoMov, string situacao, int codigo) {
            this.descricao = descricao;
            this.valor = valor;
            this.dataMov = dataMov;
            this.tipoMov = tipoMov;
            this.situacao = situacao;
            this.codigo = codigo;
        }

        // Método para Inserir uma nova movimentação no BD
        public string insert() {
            // Instanciar um objeto da classe de Conexão
            Conexao c = new Conexao();
            try {
                // Abrir a conexão com o BD
                c.abreConexao();
                // Definir o comando SQL (INSERT) que insere uma nova movimentação no BD
                MySqlCommand cmd = new MySqlCommand(@"INSERT INTO tblmovimentacao(descricao, valor, datamov, tipomov, situacao) VALUES(@descricao, @valor, @datamov, @tipomov, @situacao);", c.conexaoBD());
                // Define os valores para os parâmetros 
                cmd.Parameters.AddWithValue("@descricao", this.descricao);
                cmd.Parameters.AddWithValue("@valor", this.valor);
                cmd.Parameters.AddWithValue("@datamov", this.dataMov);
                cmd.Parameters.AddWithValue("@tipomov", this.tipoMov);
                cmd.Parameters.AddWithValue("@situacao", this.situacao);

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

        // Método Selecionar todas as movimentações cadastradas no BD
        public DataTable select() {
            // Criar uma tabela genérica 
            DataTable tabela = new DataTable();
            // Criar um objeto da classe de conexão com BD
            Conexao conexao = new Conexao();
            try {
                conexao.abreConexao();
                // Definir o comando SQL (SELECT) e o BD que o comando será executado
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tblmovimentacao", conexao.conexaoBD());
                // Executar a consulta SQL e armazenar os dados retornados
                MySqlDataReader dados = cmd.ExecuteReader();
                // Verificar se a consulta retornou algum registro da tabela do BD
                if (dados.HasRows == true) {
                    // Carregar os dados do DataReader para o DataTable
                    tabela.Load(dados);
                }
                else {
                    tabela = new DataTable("erro");
                    tabela.Rows.Add("Nenhuma movimentação encontrada");
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
        // Método para Atualizar uma determinada movimentação
        public string update() {
            //MySqlConnection con = new MySqlConnection(strConexao);
            Conexao c = new Conexao();
            try {
                //con.Open();
                c.abreConexao();
                MySqlCommand cmd = new MySqlCommand(@"
                UPDATE tblmovimentacao SET descricao = @descricao,
                valor = @valor, datamov = @datamov, tipomov = @tipomov,
                situacao = @situacao WHERE codigo = @codigo", c.conexaoBD());
                cmd.Parameters.AddWithValue("@descricao", this.descricao);
                cmd.Parameters.AddWithValue("@valor", this.valor);
                cmd.Parameters.AddWithValue("@datamov", this.dataMov);
                cmd.Parameters.AddWithValue("@tipomov", this.tipoMov);
                cmd.Parameters.AddWithValue("@situacao", this.situacao);
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
        // Método para Excluir uma determinada movimentação
        public string delete(int codigo) {
            //MySqlConnection con = new MySqlConnection(strConexao);
            Conexao c = new Conexao();
            try {
                //con.Open();
                c.abreConexao();
                MySqlCommand cmd = new MySqlCommand(@"
                DELETE FROM tblmovimentacao 
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
