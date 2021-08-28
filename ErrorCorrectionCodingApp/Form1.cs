using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace tele
{
    public partial class Form1 : Form
    {
        protected Crc _crc = new Crc();
        protected Hamming _hamming = new Hamming();
        protected Parity _parity = new Parity();
        protected Bits _dbits = new Bits();
        public Form1()
        {
            InitializeComponent();
        }
        private void h_button_koduj_Click(object sender, EventArgs e)
        {
            _hamming.setData(h_inputData.Text);
            _hamming.encode();
            h_daneZakodowane.Text = _hamming.codeToString();
            h_daneZakodowane2.Text = _hamming.codeToString();
        }

        private void h_button_generuj_Click(object sender, EventArgs e)
        {
            _dbits.generate(Int32.Parse(domainUpDown1.Text));
            h_inputData.Text = _dbits.getBitsAsString();
        }

        private void h_button_zakloc_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(h_bits_zaklocenie.Text) || h_bits_zaklocenie.Text.Equals("0"))
            {
                MessageBox.Show("Proszę wprowadzić liczbę bitów do zakłócenia");
            }
            else
            {
                _hamming.interfere(Int32.Parse(h_bits_zaklocenie.Text));
                h_daneZakodowane2.Text = _hamming.codeToString();
            }
        }

        private void h_button_dekoduj_Click(object sender, EventArgs e)
        {
            int errors = _hamming.countErrors(h_daneZakodowane.Text, h_daneZakodowane2.Text);
            _hamming.fix();
            if (h_daneZakodowane2.Text.Equals(""))
            {
                h_daneZakodowane2.Text = _hamming.codeToString();
            }
            else
            {
                _hamming.setCode(h_daneZakodowane2.Text);
                _hamming.fix();
            }
            h_input_danePoKorekcji.Text = _hamming.codeToString();
            _hamming.decode();
            h_input_daneWyjsciowe.Text = _hamming.dataToString();
            colorFixedBitsHamming(_hamming.getBitTypes());
            h_input_przeslaneBity.Text = _hamming.getDataBitsNumber().ToString();
            h_input_BityKontrolne.Text = _hamming.getControlBitsNumber().ToString();
            h_input_bledyWykryte.Text = _hamming.getDetectedErrorsNumber().ToString();
            int detected = _hamming.getDetectedErrorsNumber();
            h_input_bledySkorygowane.Text = _hamming.getFixedErrorsNumber().ToString();
            h_input_bledyNiewykryte.Text = (errors - detected).ToString();
        }

        private void p_button_generuj_Click(object sender, EventArgs e)
        {
            _dbits.generate(Int32.Parse(domainUpDown2.Text));
            p_inputData.Text = _dbits.getBitsAsString();
        }
        private void p_button_koduj_Click(object sender, EventArgs e)
        {
            _parity.setData(p_inputData.Text);
            _parity.encode();
            p_daneZakodowane.Text = _parity.codeToString();
            p_daneZakodowane2.Text = _parity.codeToString();
        }

        private void p_button_dekoduj_Click(object sender, EventArgs e)
        {
            int errors = _parity.countErrors(p_daneZakodowane.Text, p_daneZakodowane2.Text);
            _parity.fix();
            if (p_daneZakodowane2.Text.Equals(""))
            {
                p_daneZakodowane2.Text = _parity.codeToString();
            }
            else
            {
                _parity.setCode(p_daneZakodowane2.Text);
                _parity.fix();
            }
            p_input_danePoKorekcji.Text = _parity.codeToString();
            _parity.decode();
            p_input_daneWyjsciowe.Text = _parity.dataToString();
            colorFixedBitsParity(_parity.getBitTypes());
            p_input_przeslaneBity.Text = _parity.getDataBitsNumber().ToString();
            p_input_BityKontrolne.Text = _parity.getControlBitsNumber().ToString();
            int detected = _parity.getDetectedErrorsNumber();
            p_input_bledyWykryte.Text = _parity.getDetectedErrorsNumber().ToString();
            p_input_bledySkorygowane.Text = _parity.getFixedErrorsNumber().ToString();
            p_input_bledyNiewykryte.Text = (errors - detected).ToString();
        }

        private void p_button_zakloc_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(p_bits_zaklocenie.Text) || p_bits_zaklocenie.Text.Equals("0"))
            {
                MessageBox.Show("Proszę wprowadzić liczbę bitów do zakłócenia");
            }
            else
            {
                _parity.interfere(Int32.Parse(p_bits_zaklocenie.Text));
                p_daneZakodowane2.Text = _parity.codeToString();
            }
        }

        private void c_button_koduj_Click(object sender, EventArgs e)
        {
            _crc.setData(c_inputData.Text);
            _crc.encode();
            c_daneZakodowane.Text = _crc.codeToString();
            c_daneZakodowane2.Text = _crc.codeToString();
        }

        private void c_button_generuj_Click(object sender, EventArgs e)
        {
            _dbits.generate(Int32.Parse(domainUpDown3.Text));
            c_inputData.Text = _dbits.getBitsAsString();
        }

        private void c_button_dekoduj_Click(object sender, EventArgs e)
        {
            int errors = _crc.countErrors(c_daneZakodowane.Text, c_daneZakodowane2.Text);
            _crc.fix();

            if (c_daneZakodowane2.Text.Equals(""))
            {
                c_daneZakodowane2.Text = _crc.codeToString();
            }
            else
            {
                _crc.setCode(c_daneZakodowane2.Text);
                _crc.fix();
            }
            c_input_danePoKorekcji.Text = _crc.codeToString();
            _crc.decode();
            c_input_daneWyjsciowe.Text = _crc.dataToString();
            colorFixedBitsCrc(_crc.getBitTypes());
            c_input_przeslaneBity.Text = _crc.getDataBitsNumber().ToString();
            c_input_BityKontrolne.Text = _crc.getControlBitsNumber().ToString();
            c_input_bledyWykryte.Text = _crc.getDetectedErrorsNumber().ToString();
            int detected = _crc.getDetectedErrorsNumber();
            c_input_bledySkorygowane.Text = _crc.getFixedErrorsNumber().ToString();
            c_input_bledyNiewykryte.Text = (errors - detected).ToString();
        }

        private void c_button_zakloc_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(c_bits_zaklocenie.Text) || c_bits_zaklocenie.Text.Equals("0"))
            {
                MessageBox.Show("Proszę wprowadzić liczbę bitów do zakłócenia");
            }
            else
            {
                _crc.interfere(Int32.Parse(c_bits_zaklocenie.Text));
                c_daneZakodowane2.Text = _crc.codeToString();
            }
        }

        public void colorFixedBitsCrc(int[] type)
        {
            string str = c_input_danePoKorekcji.Text;
            if (str.Length == type.Length)
            {
                c_input_danePoKorekcji.Text = "";
                int l = type.Length; 
                for(int i=0; i < l; i++)
                {
                    Color color = Color.Black;
                    c_input_danePoKorekcji.SelectionColor = Color.Black;
                    switch (type[i])
                    {
                        case 0: color = Color.Green;
                            break;
                        case 1: color = Color.Red;
                            break;
                        case 2: color = Color.Orange;
                            break;
                        case 3: color = Color.Cyan;
                            break;
                        case 4: color = Color.Magenta;
                            break;
                        case 5: color = Color.Yellow;
                            break;
                    }

                    c_input_danePoKorekcji.SelectionColor = color;
                    c_input_danePoKorekcji.SelectedText += str[i];
                }
            }
        }

        public void colorFixedBitsHamming(int[] type)
        {
            string str = h_input_danePoKorekcji.Text;
            if (str.Length == type.Length)
            {
                h_input_danePoKorekcji.Text = "";
                int l = type.Length;
                for (int i = 0; i < l; i++)
                {
                    Color color = Color.Black;
                    h_input_danePoKorekcji.SelectionColor = Color.Black;
                    switch (type[i])
                    {
                        case 0:
                            color = Color.Green;
                            break;
                        case 1:
                            color = Color.Red;
                            break;
                        case 2:
                            color = Color.Orange;
                            break;
                        case 3:
                            color = Color.Cyan;
                            break;
                        case 4:
                            color = Color.Magenta;
                            break;
                        case 5:
                            color = Color.Yellow;
                            break;
                    }

                    h_input_danePoKorekcji.SelectionColor = color;
                    h_input_danePoKorekcji.SelectedText += str[i];
                }
            }
        }

        public void colorFixedBitsParity(int[] type)
        {
            string str = p_input_danePoKorekcji.Text;
            if (str.Length == type.Length)
            {
                p_input_danePoKorekcji.Text = "";
                int l = type.Length;
                for (int i = 0; i < l; i++)
                {
                    Color color = Color.Black;
                    p_input_danePoKorekcji.SelectionColor = Color.Black;
                    switch (type[i])
                    {
                        case 0:
                            color = Color.Green;
                            break;
                        case 1:
                            color = Color.Red;
                            break;
                        case 2:
                            color = Color.Orange;
                            break;
                        case 3:
                            color = Color.Cyan;
                            break;
                        case 4:
                            color = Color.Magenta;
                            break;
                        case 5:
                            color = Color.Yellow;
                            break;
                    }

                    p_input_danePoKorekcji.SelectionColor = color;
                    p_input_danePoKorekcji.SelectedText += str[i];
                }
            }
        }
    }
}
