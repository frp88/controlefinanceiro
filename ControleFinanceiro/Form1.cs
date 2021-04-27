using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControleFinanceiro {
    public partial class Form1 : Form {

        // Declarar a Struct
        public struct Movimentacao {
            public string descricao;
            public double valor;
            public DateTime dataMov;
            public string tipoMov;
            public string situacao;
        }

        // Declarar uma Lista de Struct global, estática e pública
        public static List<Movimentacao> listaMov = new List<Movimentacao>(); 

        // Variável global para controle de um determinado item
        int indexSelecionado;

        public Form1() {
            InitializeComponent();
        }

        private void btnNovo_Click(object sender, EventArgs e) {
            // Definir o índice como -1
            indexSelecionado = -1;
            // Limpar a seleção do ListView
            listView1.SelectedItems.Clear();
            // Desabilitar o botão excluir
            btnExcluir.Enabled = false;
            // Altera o texto do botão "btnSalvar"
            btnSalvar.Text = "Cadastrar";
            // Limpara a caixa de texto
            //txtDescricao.Text = "";
            txtDescricao.Text = string.Empty;
            //txtDescricao.Clear();
            txtValor.Text = String.Empty;
            //txtData.Text = string.Empty;
            // Atribui a data atual para o componente
            dtDataMov.Value = DateTime.Now;
            // limpar as seleções dos RadioButtons e do CheckBox
            rbDespesa.Checked = true;
            //rbReceita.Checked = false;
            //ckbPendente.Checked = false;
            rbReceita.Checked = ckbPendente.Checked = false;
            // Colocar o foco na caixa de texto
            txtDescricao.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e) {
            // verificar se o usuário digitou alguma descrição
            if (txtDescricao.Text.Trim().Equals(string.Empty)) {
                MessageBox.Show("Digite uma descrição.");
                txtDescricao.Focus();
            }
            // Verificar se o valor é válido
            else {
                // verificar se o valor digitado é válido
                double valor;
                // tentar converter o valor digitado para double
                bool valorOk = double.TryParse(txtValor.Text, out valor);
                // Verificar se o valor digitado não foi convertido
                if (valorOk == false) {
                    MessageBox.Show("Valor digitado inválido");
                    txtValor.Focus();
                }
                else {
                    // Verificar se data é válida
                    DateTime dataValida = dtDataMov.Value;
                    // tentar converter a data digitada para DateTime
                    //bool dataOk = DateTime.TryParse(txtData.Text, out dataValida);
                    // Verificar se a data digitada não foi convertida
                    //if (dataOk == false) {
                        //MessageBox.Show("Data digitada inválida.");
                        //txtData.Focus();
                    //}
                    //else {
                        // Declara uma variável da Struct
                        Movimentacao mov;
                        // Atribuir os valores para a variável
                        mov.descricao = txtDescricao.Text;
                        mov.valor = valor;
                        mov.dataMov = dataValida;
                        mov.tipoMov = 
               (rbDespesa.Checked == true ? "Despesa" : "Receita");
                        mov.situacao = 
                     ckbPendente.Checked ? "Pendente" : "Concluído";

                        // Cadastrar uma nova despesa ou atualizar uma despesa existente
                        // Criar um objeto (item) do tipo ListViewItem
                        ListViewItem item = new ListViewItem(mov.descricao);
                            //(txtDescricao.Text);
                        // Adicionar os valores para as demais colunas deste item
                        item.SubItems.Add(mov.valor.ToString("C2"));
            item.SubItems.Add(mov.dataMov.ToShortDateString());
                        item.SubItems.Add(mov.tipoMov);
                        item.SubItems.Add(mov.situacao);
                        // Verificar o valor do variável global
                        if (indexSelecionado == -1) {
                // Inserir a variável da struct na Lista de Struct
                            listaMov.Add(mov);
                // Inserir um novo elemento (item) no ListView
                            listView1.Items.Add(item);
                        }
                        else {
                            // Alterar o elemento na lista de Struct
                            listaMov[indexSelecionado] = mov;
                            // Alterar o elemento no ListView
                            listView1.Items[indexSelecionado] = item;
                        }
                        // Executar o método do botão novo
                        btnNovo_Click(null, null);
                    //}
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e) {
            // Executar o médoto do clique do botão Novo
            btnNovo_Click(null, null);
        }

        private void btnExcluir_Click(object sender, EventArgs e) {
            // Verificar se o formulário está apto para excluir uma despesa
            if (indexSelecionado >= 0) {
                // Foi selecionado um item do ListBox, então pode excluir esse item 
                DialogResult resultado = MessageBox.Show("Deseja realmente excluir este item?", "Excluir item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Verificar se o botão clicado foi o "SIM"
                if (resultado == DialogResult.Yes) {
         // Remover o elemento (variável da Struct) da Lista de Struct
                    listaMov.RemoveAt(indexSelecionado);
                    // Remover o item do ListView
                    listView1.Items.RemoveAt(indexSelecionado);
                    // Executar o método do evento clique do botão novo
                    btnNovo_Click(null, null);
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e) {
            // Verificar se o usuário realmente quer sair
            if (MessageBox.Show("Deseja sair do programa?", "Sair do programa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                // Encerrar a aplicação
                Application.Exit();
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e) {
            // Pegar o índice do item selecionado
            indexSelecionado = listView1.SelectedItems[0].Index;
            // pegar o valor do item selecionado e colocar nas caixas de texto
            txtDescricao.Text = listView1.Items[indexSelecionado].SubItems[0].Text;
            string valor = listView1.Items[indexSelecionado].SubItems[1].Text;
            txtValor.Text = valor.Replace("R$ ", "");
            //txtData.Text = listView1.Items[indexSelecionado].SubItems[2].Text;
            dtDataMov.Value = DateTime.Parse(listView1.Items[indexSelecionado].SubItems[2].Text);
            // Verifica se o item da tabela é uma despesa ou uma receita
            if (listView1.Items[indexSelecionado].SubItems[3].Text.Trim().Equals("Despesa")) {
                rbDespesa.Checked = true;
            }
            else {
                rbReceita.Checked = true;
            }
            // Verifica se o item selecionado da tabela está pendente ou não
            ckbPendente.Checked = listView1.Items[indexSelecionado].SubItems[4].Text.Trim().Equals("Sim");
            btnSalvar.Text = "Alterar";
            btnExcluir.Enabled = true;
            txtDescricao.Focus();
        }

        private void btnTodaLista_Click(object sender, EventArgs e) {
            string lista = "--- TODAS MOVIMENTAÇÕES ---\n";
            // Percorre todos elementos da Lista de Movimentações
            foreach (Movimentacao mov in listaMov) {
                lista += "\n" + mov.descricao + " - Valor: " +
                     mov.valor.ToString("C2");
            }
            // Imprime os valores armazenados na variável string
            MessageBox.Show(lista);
        }
    }
}
