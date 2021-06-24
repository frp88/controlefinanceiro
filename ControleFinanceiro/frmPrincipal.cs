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
    public partial class frmPrincipal : Form {
        public frmPrincipal() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            // Instanciar um novo formulário
            frmMovimentacao frmMov = new frmMovimentacao();
            // Mostrar o novo formulário que foi criado
            //frmMov.Show();
            // Mostrar o novo formulário criado bloqueando a janela principal
            frmMov.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            // Instanciar um novo formulário
            frmMovimentacao frmMov = new frmMovimentacao();
            // Mostrar o novo formulário que foi criado
            //frmMov.Show();
            // Mostrar o novo formulário criado bloqueando a janela principal
            frmMov.ShowDialog();
        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e) {
           
        }

        private void movimentaçõesToolStripMenuItem_Click(object sender, EventArgs e) {
            // Instancia e mostra o formulário
            new frmMovimentacao().ShowDialog();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Deseja realmente sair?", "Sair do programa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                Application.Exit();
            }
        }

        private void fecharToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show("Fechando a Aplicação...");
            Application.Exit();
        }
    }
}
