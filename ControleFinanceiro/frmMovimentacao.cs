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
                
        // Variável global para controle de um determinado item
        int indexSelecionado;

        public frmMovimentacao() {
            InitializeComponent();
        }

        // Método que busca todas as formas de pagamento do BD e preenche o ComboBox de Formas de pagamento
        public void buscaFormasDePagamento() {
            FormaDePagamento formaDePag = new FormaDePagamento();
            DataTable dt = formaDePag.select();
            // Atribui todos os registros para o comboBox
            cbFormaDePagamento.DataSource = dt;
            // Definir qual coluna da tabela "tblformadepagamento" será exibida
            cbFormaDePagamento.DisplayMember = "formadepagamento";
            // Definir qual a coluna da tabela será manipulada via código
            cbFormaDePagamento.ValueMember = "codigo";
        }

        // Método que busca todas as movimentações do BD
        public void buscaMovimentacoes() {
            // Instanciar um objeto da classe Movimentações
            Movimentacao movimentacao = new Movimentacao();
            DataTable dt = movimentacao.select();
            // Preencher o DataGridView (dgvMovimentacoes) com os dados retornados da consulta SQL
            dgvMovimentacoes.DataSource = dt;
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
            // ComboBox
            cbFormaDePagamento.SelectedIndex = 0;
            cbFormaDePagamento.Text = "Sel. uma forma de pgto.";
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
                    // Instanciar um objeto classe Movimentação e definir os valores para os atributos
                    Movimentacao mov = new Movimentacao (
                        txtDescricao.Text, valor, dtDataMov.Value,
                        rbDespesa.Checked == true ? "D" : "R",
                        ckbPendente.Checked ? "PE" : "CO", Convert.ToInt32(cbFormaDePagamento.SelectedValue.ToString()));              
                    // Verifica se é um cadastro ou uma atualização
                    if (indexSelecionado == -1) {
                        // Chama o método do objeto que insere uma nova movimentação no BD
                        string ultimoId = mov.insert();
                        MessageBox.Show("ID da Movimentação: " + ultimoId);
                    }
                    else {
                        // Definir o valor para o atributo "codigo"
                        mov.codigo = indexSelecionado;
                        // Chama o método do obejto que atualiza uma determinada movimentação no BD
                        mov.update();
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
            // Executar o método que retorna todos os registros da tabela "tblformadepagamento" e exibe no ComboBox
            buscaFormasDePagamento();
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


        private void btnCancelar_Click(object sender, EventArgs e) {
            // Adicionar a aba da Lista
            tabControl1.TabPages.Add(tabPageLista);
            // Remover a aba de controle
            tabControl1.TabPages.Remove(tabPageControle);
        }

        // Função que exclui uma movimentação na tabela do BD
        private void excluiMovimentacao() {
            DialogResult resultado = MessageBox.Show("Deseja realmente excluir este item?", "Excluir item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Verificar se o botão clicado foi o "SIM"
            if (resultado == DialogResult.Yes) {
                // Instanciar um objeto da classe "Movimentacao"
                Movimentacao movimentacao = new Movimentacao();
                // Executar o método do objeto que exclui uma movimentação na tabela do BD
                movimentacao.delete(indexSelecionado);

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
                // Exibir a forma de pagamento da movimentação
                cbFormaDePagamento.SelectedValue = dgvMovimentacoes.Rows[e.RowIndex].Cells["codformadepag"].Value;
            }
        }
    }
}
