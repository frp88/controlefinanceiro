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
    public partial class frmMovimentacao : Form {
                
        // Declarar a Struct
        public struct Movimentacao {
            public string descricao;
            public double valor;
            public DateTime dataMov;
            public string tipoMov;
            public string situacao;
        }

        // Variável global para controle de um determinado item
        int indexSelecionado;

        public frmMovimentacao() {
            InitializeComponent();
        }

        // Método que busca todas as movimentações do BD
        public void buscaMovimentacoes() {
            // Instanciar um objeto da classe Movimentações
            Movimentacao movimentacao = new Movimentacao();
            // DataTable dt = movimentacao.

            // Preencher o DataGridView (dgvMovimentacoes) com os dados retornados da consulta SQL
            // dgvMovimentacoes.DataSource = dt;
        }

        private void btnNovo_Click(object sender, EventArgs e) {
            // Definir o índice como -1
            indexSelecionado = -1;
            // Limpar a seleção do ListView
            //listView1.SelectedItems.Clear();
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
            // Adionar a aba de controle de movimentação
            tabControl1.TabPages.Add(tabPageControle);
            // Remover a aba de lista de movimentação
            tabControl1.TabPages.Remove(tabPageLista);
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
           (rbDespesa.Checked == true ? "D" : "R");
                    mov.situacao =
                 ckbPendente.Checked ? "PE" : "CO";
                    if (indexSelecionado == -1) {
                        // Chama função que insere uma nova movimentação no BD
                        //insereMovimentacao(mov);
                    }
                    else {
                        // Chamar a função que atualiza uma determinada movimentação no BD
                        // atualizaMovimentacao(mov);
                    }
                    // Executar o método do botão novo
                    //btnNovo_Click(null, null);
                    // Executar o método que retorna as movimentações cadastradas no BD
                    buscaMovimentacoes();
                    // Adicionar a aba de lista de movimentações
                    tabControl1.TabPages.Add(tabPageLista);
                    // Remover a aba de controle de movimentações
                    tabControl1.TabPages.Remove(tabPageControle);
                    //}
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e) {
            // Remover uma aba do tabControl 
            tabControl1.TabPages.RemoveAt(1);
            // Executar o médoto do clique do botão Novo
            // btnNovo_Click(null, null);
            // Executar o método que retorna os dados da tabela do BD e exibe no DataGridView
            buscaMovimentacoes();
        }

        private void btnExcluir_Click(object sender, EventArgs e) {
            // Verificar se o formulário está apto para excluir uma despesa
            if (indexSelecionado >= 0) {
                excluiMovimentacao();
                // Adicionar a aba de lista
                tabControl1.TabPages.Add(tabPageLista);
                // Remover a aba de controle
                tabControl1.TabPages.Remove(tabPageControle);
            }
        }

        private void btnSair_Click(object sender, EventArgs e) {
            // Fechar o formulário
            Close();

            // Verificar se o usuário realmente quer sair
            //if (MessageBox.Show("Deseja sair do programa?", "Sair do programa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
            //    // Encerrar a aplicação
            //    Application.Exit();
            //}
        }

        private void listView1_DoubleClick(object sender, EventArgs e) {
            // Pegar o índice do item selecionado
            //indexSelecionado = listView1.SelectedItems[0].Index;
            // pegar o valor do item selecionado e colocar nas caixas de texto
            //txtDescricao.Text = listView1.Items[indexSelecionado].SubItems[0].Text;
            //string valor = listView1.Items[indexSelecionado].SubItems[1].Text;
            //txtValor.Text = valor.Replace("R$ ", "");
            //txtData.Text = listView1.Items[indexSelecionado].SubItems[2].Text;
            //dtDataMov.Value = DateTime.Parse(listView1.Items[indexSelecionado].SubItems[2].Text);
            // Verifica se o item da tabela é uma despesa ou uma receita
            //if (listView1.Items[indexSelecionado].SubItems[3].Text.Trim().Equals("Despesa")) {
            rbDespesa.Checked = true;
            //}
            //else {
            rbReceita.Checked = true;
            //}
            // Verifica se o item selecionado da tabela está pendente ou não
            //ckbPendente.Checked = listView1.Items[indexSelecionado].SubItems[4].Text.Trim().Equals("Pendente");
            btnSalvar.Text = "Alterar";
            btnExcluir.Enabled = true;
            // Adicionar a aba de controle
            tabControl1.TabPages.Add(tabPageControle);
            // Remover a aba de lista
            tabControl1.TabPages.Remove(tabPageLista);
            txtDescricao.Focus();
        }

        private void btnTodaLista_Click(object sender, EventArgs e) {
            string lista = "--- TODAS MOVIMENTAÇÕES ---\n";
            // Percorre todos elementos da Lista de Movimentações
            //foreach (Movimentacao mov in listaMov) {
            //lista += "\n" + mov.descricao + " - Valor: " +
            //     mov.valor.ToString("C2");
            //}
            // Imprime os valores armazenados na variável string
            MessageBox.Show(lista);
        }

        private void btnCancelar_Click(object sender, EventArgs e) {
            // Adicionar a aba da Lista
            tabControl1.TabPages.Add(tabPageLista);
            // Remover a aba de controle
            tabControl1.TabPages.Remove(tabPageControle);
        }

        // Função que insere uma movimentação no BD
       

        // Função que atualiza uma movimentação na tabela do BD
        private void atualizaMovimentacao(Movimentacao mov) {
            
        }

        // Função que exclui uma movimentação na tabela do BD
        private void excluiMovimentacao() {
            DialogResult resultado = MessageBox.Show("Deseja realmente excluir este item?", "Excluir item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Verificar se o botão clicado foi o "SIM"
            if (resultado == DialogResult.Yes) {
                // Executar o método que retorna as movimentações cadastradas no BD
                buscaMovimentacoes();
            }
        }

        private void dgvMovimentacoes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            // Formatar as colunas do DataGridView
            foreach (DataGridViewColumn coluna in dgvMovimentacoes.Columns) {
                switch (coluna.Name) {
                    case "descricao":
                        coluna.Width = 350;
                        coluna.HeaderText = "Descrição";
                        break;
                    case "valor":
                        coluna.Width = 120;
                        coluna.HeaderText = "Valor";
                        coluna.DefaultCellStyle.Format = "C2";
                        coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "datamov":
                        coluna.Width = 100;
                        coluna.HeaderText = "Data";
                        coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "situacao":
                        coluna.Width = 40;
                        coluna.HeaderText = "Sit.";
                        coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "alterar":
                        coluna.Width = 30;
                        coluna.DisplayIndex = 0;
                        coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "excluir":
                        coluna.Width = 30;
                        coluna.DisplayIndex = 1;
                        coluna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    default:
                        // Oculta a coluna
                        coluna.Visible = false;
                        break;
                }
            }
            // Formatação das linhas do DataGridView
            foreach (DataGridViewRow linha in dgvMovimentacoes.Rows) {
                // Definir a cor da linha de acordo com o tipo de movimentação
                linha.DefaultCellStyle.BackColor =
                    linha.Cells["tipomov"].Value.ToString().Equals("D") ? Color.LightSalmon : Color.LightSkyBlue;
                // Mostrar a mensagem exibindo o tipo da movimentação
                linha.Cells["valor"].ToolTipText =
                    linha.Cells["tipomov"].Value.ToString().Equals("D") ? "Despesa" : "Receita";
                // Mostrar a mensagem exibindo a situação da movimentação
                linha.Cells["situacao"].ToolTipText =
                    linha.Cells["situacao"].Value.ToString().Equals("PE") ? "Situação: Pendente" : "Situação: Concluído";
                // Mostrar a mensagem explicando o significado das imagens no DGV
                linha.Cells["alterar"].ToolTipText = "Clique aqui para alterar.";
                linha.Cells["excluir"].ToolTipText = "Clique aqui para excluir.";
            }
        }

        private void dgvMovimentacoes_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            // Verifica se clicou na imagem "excluir"
            if (dgvMovimentacoes.Columns[e.ColumnIndex] ==
                dgvMovimentacoes.Columns["excluir"]) {
                // Atribui à variável o código da movimentação
                indexSelecionado = Convert.ToInt32(
                    dgvMovimentacoes.Rows[e.RowIndex].Cells["codigo"].Value.ToString().Trim());
                // Chama a função que exclui uma movimentação da tabela do BD
                excluiMovimentacao();
            }
            // Verifica se clicou na imagem "alterar"
            else if (dgvMovimentacoes.Columns[e.ColumnIndex] == dgvMovimentacoes.Columns["alterar"]) {
                // Atribui à variável o código da movimentação
                indexSelecionado = Convert.ToInt32(
                    dgvMovimentacoes.Rows[e.RowIndex].Cells["codigo"].Value.ToString().Trim());
                // Atribuir os valores do dgv para os TextBox e RadioButton
                txtDescricao.Text = dgvMovimentacoes.Rows[e.RowIndex].Cells["descricao"].Value.ToString();
                txtValor.Text = dgvMovimentacoes.Rows[e.RowIndex].Cells["valor"].Value.ToString().Trim();
                dtDataMov.Value = Convert.ToDateTime(
                    dgvMovimentacoes.Rows[e.RowIndex].Cells["datamov"].Value.ToString().Trim());

                // Seleciona a despesa ou a receita
                rbDespesa.Checked = dgvMovimentacoes.Rows[e.RowIndex].Cells["tipomov"].Value.ToString().Equals("D");
                rbReceita.Checked = dgvMovimentacoes.Rows[e.RowIndex].Cells["tipomov"].Value.ToString().Equals("R");
                // Definir se o checkbox pendente ficará marcado ou não
                ckbPendente.Checked = dgvMovimentacoes.Rows[e.RowIndex].Cells["situacao"].Value.ToString().Equals("PE");
                // Alterar o texto do botão salvar
                btnSalvar.Text = "Alterar";
                // Habilitar o botão Excluir
                btnExcluir.Enabled = true;
                // Exibe a Aba de Formulário
                tabControl1.TabPages.Add(tabPageControle);
                tabControl1.TabPages.Remove(tabPageLista);
            }
        }
    }
}
