using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsTriggerCerato
{
    public partial class Form1 : Form
    {
        public static readonly string workingDirectory = Environment.CurrentDirectory;
        public static readonly string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        public static readonly string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + projectDirectory + @"\Clienti.mdf;Integrated Security=True;Connect Timeout=30";

        private BindingSource bsClienti = new BindingSource();
        private BindingSource bsStoricoCancellazioni = new BindingSource();
        private BindingSource bsStoricoAggiornamenti = new BindingSource();
        private SqlDataAdapter daClienti, daStoricoCancellazioni, daStoricoAggiornamenti;
        private DataTable dtClienti, dtStoricoCancellazioni, dtStoricoAggiornamenti;

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateDgv(0);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            daClienti.Update(dtClienti);
            PopulateDgv(0);
        }

        private void dgvDriver_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            daClienti.Update(dtClienti);
            PopulateDgv(3);
        }

        private void PopulateDgv(int tabella)
        {
            switch (tabella)
            {
                case 0:
                    //riempimento tabella clienti
                    dgvDriver.DataSource = bsClienti;
                    Queryable("SELECT * FROM Clienti", out daClienti);
                    if (daClienti != null)
                    {
                        dtClienti = new DataTable();
                        daClienti.Fill(dtClienti);
                        bsClienti.DataSource = dtClienti;
                    }
                    dgvDriver.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                    //riempimento tabella cancellazioni
                    dgvStoricoCancellazioni.DataSource = bsStoricoCancellazioni;
                    Queryable("SELECT * FROM StoricoCancellazioni", out daStoricoCancellazioni);
                    if (daStoricoCancellazioni != null)
                    {
                        dtStoricoCancellazioni = new DataTable();
                        daStoricoCancellazioni.Fill(dtStoricoCancellazioni);
                        bsStoricoCancellazioni.DataSource = dtStoricoCancellazioni;
                    }
                    dgvStoricoCancellazioni.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                    //riempimento tabella aggiornamenti
                    dgvStoricoAggiornamenti.DataSource = bsStoricoAggiornamenti;
                    Queryable("SELECT * FROM StoricoAggiornamenti", out daStoricoAggiornamenti);
                    if (daStoricoAggiornamenti != null)
                    {
                        dtStoricoAggiornamenti = new DataTable();
                        daStoricoAggiornamenti.Fill(dtStoricoAggiornamenti);
                        bsStoricoAggiornamenti.DataSource = dtStoricoAggiornamenti;
                    }
                    dgvStoricoAggiornamenti.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                    break;

                case 1:
                    dgvDriver.DataSource = bsClienti;
                    Queryable("SELECT * FROM Clienti", out daClienti);
                    if (daClienti != null)
                    {
                        dtClienti = new DataTable();
                        daClienti.Fill(dtClienti);
                        bsClienti.DataSource = dtClienti;
                    }
                    dgvDriver.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                    break;

                case 2:
                    //X Aggiornamenti
                    dgvStoricoAggiornamenti.DataSource = bsStoricoAggiornamenti;
                    Queryable("SELECT * FROM StoricoAggiornamenti", out daStoricoAggiornamenti);
                    if (daStoricoAggiornamenti != null)
                    {
                        dtStoricoAggiornamenti = new DataTable();
                        daStoricoAggiornamenti.Fill(dtStoricoAggiornamenti);
                        bsStoricoAggiornamenti.DataSource = dtStoricoAggiornamenti;
                    }
                    dgvStoricoAggiornamenti.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                    break;

                case 3:
                    //X Cancellazioni
                    dgvStoricoCancellazioni.DataSource = bsStoricoCancellazioni;
                    Queryable("SELECT * FROM StoricoCancellazioni", out daStoricoCancellazioni);
                    if (daStoricoCancellazioni != null)
                    {
                        dtStoricoCancellazioni = new DataTable();
                        daStoricoCancellazioni.Fill(dtStoricoCancellazioni);
                        bsStoricoCancellazioni.DataSource = dtStoricoCancellazioni;
                    }
                    dgvStoricoCancellazioni.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

                    break;
            }
        }

        private void Queryable(string selectCommand, out SqlDataAdapter da)
        {
            da = null;
            try
            {
                //Creo un nuovo data adapter basato su selectCommand
                da = new SqlDataAdapter(selectCommand, CONNECTION_STRING);

                //Creo un command builder per generare un comando SQL update, insert, delete
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(da);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
