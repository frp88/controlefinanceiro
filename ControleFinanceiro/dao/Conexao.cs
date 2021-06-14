using System;
// Importar / usar as bibliotecas para conectar com o BD
using MySql.Data.MySqlClient;

namespace ControleFinanceiro {
    class Conexao {
        // Definir a string de conexão com o BD
        string strConexao = @"SERVER=localhost;  
                            DATABASE=bdfinanceiro;
                            UID=root; 
                            PASSWORD=;";
        // Declarar uma variável global para conectar com BD
        private MySqlConnection con;

        // Método para abrir a conexão com BD
        public string abreConexao() {
            try {
                // Criar um objeto para conectar com BD
                con = new MySqlConnection(strConexao);
                // Abrir a conexão com o BD
                con.Open();
                return "ok";
            }
            catch (Exception erro) {
                return erro.Message;
            }
        }
        
        // Método que fecha a conexão com BD
        public string fechaConexao() {
            try {
                // Fecha a conexão com BD
                con.Close();
                return "ok";
            }
            catch (Exception erro) {
                return erro.Message;
            }
        }

        // Método que retorna a conexão com o BD
        public MySqlConnection conexaoBD() {
            return con;
        }
    }
}
