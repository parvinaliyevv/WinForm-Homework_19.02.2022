using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Dish_CheckedChanged(object sender, EventArgs e)
        {
            // Way 1 //

            if (sender is CheckBox cb) 
            {
                foreach (var item in CafeGroupBox.Controls)
                {
                    if (item is TextBox tb)
                    {
                        if (cb.Name.Split("CheckBox")[0].Equals(tb.Name.Split("Count")[0]))
                        {
                            tb.Enabled = (cb.Checked) ? true : false;
                            return;
                        }
                    }
                }
            }


            // Way 2

            // if (sender is CheckBox cb)
            // {
            //     if (cb.Name == nameof(HotDogCheckBox)) HotDogCount.Enabled = (cb.Checked) ? true : false;
            //     if (cb.Name == nameof(HamburgerCheckBox)) HamburgerCount.Enabled = (cb.Checked) ? true : false;
            //     if (cb.Name == nameof(FrenchFriesCheckBox)) FrenchFriesCount.Enabled = (cb.Checked) ? true : false;
            //     if (cb.Name == nameof(CocaColaCheckBox)) CocaColaCount.Enabled = (cb.Checked) ? true : false;
            // }
        }

        private void DishPrice_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                double calc = 0;

                try
                {
                    if (HotDogCount.Enabled && !string.IsNullOrWhiteSpace(HotDogCount.Text))
                        calc += Convert.ToInt32(HotDogCount.Text) * Convert.ToDouble(HotDogPrice.Text);

                    if (HamburgerCount.Enabled && !string.IsNullOrWhiteSpace(HamburgerCount.Text))
                        calc += Convert.ToInt32(HamburgerCount.Text) * Convert.ToDouble(HamburgerPrice.Text);

                    if (FrenchFriesCount.Enabled && !string.IsNullOrWhiteSpace(FrenchFriesCount.Text))
                        calc += Convert.ToInt32(FrenchFriesCount.Text) * Convert.ToDouble(FrenchFriesPrice.Text);

                    if (CocaColaCount.Enabled && !string.IsNullOrWhiteSpace(CocaColaCount.Text))
                        calc += Convert.ToInt32(CocaColaCount.Text) * Convert.ToDouble(CocaColaPrice.Text);
                }
                catch
                {
                    MessageBox.Show("You can only enter integer values!", "Invalid Input!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                DishPrice.Text = calc.ToString();
            }
        }

        private void Payment_TextChanged(object sender, EventArgs e) => TotalGroupBox.Text = (Convert.ToDouble(RefuelPrice.Text) + Convert.ToDouble(DishPrice.Text)).ToString();

        private void BuyButton_Click(object sender, EventArgs e)
        {
            if (TotalGroupBox.Text.Equals("0"))
            {
                MessageBox.Show("You haven't chosen anything!", "Invalid Operation!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Payment was successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PaymentType_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb)
            {
                if (rb.Name == nameof(CountRadioButton))
                {
                    CountEntry.Enabled = (rb.Checked) ? true : false;
                    FuelCount_TextChanged(CountEntry, null);
                }
                if (rb.Name == nameof(CashRadioButton))
                {
                    CashEntry.Enabled = (rb.Checked) ? true : false;
                    FuelCount_TextChanged(CashEntry, null);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox cb)
            {
                if (cb.SelectedItem.ToString().Equals("AI-92")) FuelPrice.Text = "0.90";
                else if (cb.SelectedItem.ToString().Equals("AI-95")) FuelPrice.Text = "1.40";
                else if (cb.SelectedItem.ToString().Equals("AI-98")) FuelPrice.Text = "1.55";

                FuelCount_TextChanged((CountRadioButton.Checked) ? CountEntry : CashEntry, null);
            }
        }

        private void FuelCount_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                try
                {
                    if (CountRadioButton.Checked && !string.IsNullOrWhiteSpace(tb.Text) && !tb.Text.EndsWith("."))
                    {
                        CashEntry.TextChanged -= FuelCount_TextChanged;
                        CashEntry.Text = (Convert.ToDouble(tb.Text) * Convert.ToDouble(FuelPrice.Text)).ToString();
                        RefuelPrice.Text = CashEntry.Text;
                        CashEntry.TextChanged += FuelCount_TextChanged;
                    }
                    else if (CashRadioButton.Checked && !string.IsNullOrWhiteSpace(tb.Text) && !tb.Text.EndsWith("."))
                    {
                        CountEntry.TextChanged -= FuelCount_TextChanged;
                        CountEntry.Text = Math.Round((Convert.ToDouble(tb.Text) / Convert.ToDouble(FuelPrice.Text)), 2).ToString();
                        RefuelPrice.Text = tb.Text;
                        CountEntry.TextChanged += FuelCount_TextChanged;
                    }
                }
                catch
                {
                    MessageBox.Show("You can only enter integer values!", "Invalid Input!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
