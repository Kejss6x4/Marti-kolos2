using System;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp11
{
    public partial class Form1 : Form
    {
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Ustawienie zakresu dla TrackBar
            trackBar1.Minimum = 1;
            trackBar1.Maximum = 5;
            trackBar1.Value = 1;

            // Inicjalizacja DataGridView
            dataGridView1.ColumnCount = 5;
            dataGridView1.RowCount = 5;

            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            trackBar1.ValueChanged += TrackBar1_ValueChanged;
            textBox2.TextChanged += TextBox2_TextChanged;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "generuj")
            {
                GenerateRandomNumbers();
            }
            else if (comboBox1.SelectedItem.ToString() == "oblicz")
            {
                CalculateResult();
            }
        }

        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
            label1.Text = "";
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            label1.Text = "";
        }

        private void GenerateRandomNumbers()
        {
            if (double.TryParse(textBox1.Text, out double maxRange))
            {
                double[] values = new double[25];
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = random.NextDouble() * (maxRange + 2) - 2; // Random double between -2 and maxRange
                }

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        dataGridView1[j, i].Value = values[i * 5 + j];
                    }
                }

                double average = values.Average();
                MessageBox.Show($"Średnia: {average}");
            }
            else
            {
                MessageBox.Show("Wprowadź prawidłową liczbę do TextBox1");
            }
        }

        private void CalculateResult()
        {
            if (double.TryParse(textBox2.Text, out double inputValue))
            {
                double trackBarValue = trackBar1.Value;
                double result;

                if (trackBarValue % 2 == 0)
                {
                    result = inputValue - trackBarValue;
                    label1.Text = $"{inputValue} - {trackBarValue} = {result}";
                }
                else
                {
                    result = inputValue + trackBarValue;
                    label1.Text = $"{inputValue} + {trackBarValue} = {result}";
                }

                SaveToFile(trackBarValue, inputValue, result);
            }
            else
            {
                MessageBox.Show("Wprowadź prawidłową liczbę do TextBox2");
            }
        }

        private void SaveToFile(double trackBarValue, double inputValue, double result)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files|*.txt";
                saveFileDialog.Title = "Zapisz wynik";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string operation = trackBarValue % 2 == 0 ? "-" : "+";
                    string content = $"{inputValue} {operation} {trackBarValue} = {result}";
                    System.IO.File.WriteAllText(saveFileDialog.FileName, content);
                }
            }
        }
    }
}
