using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment6Group2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region CONSTANTS & VARIABLES
        const double COST_OIL_CHANGE = 26.00;
        const double COST_LUBE_JOB = 18.00;
        const double COST_RADIATOR_FLUSH = 30.00;
        const double COST_TRANSMISSION_FLUSH = 80.00;
        const double COST_INSPECTION = 15.00;
        const double COST_MUFFLER_REPLACEMENT = 100.00;
        const double COST_TIRE_ROTATION = 20.00;
        const double COST_LABOR = 20.00;
        const double TAX_MULTIPLIER = 0.06;
        const double costPart = 0.00;
        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }
        public double OilLubeCharges(bool oilChange, bool lubeJob)
        {
            if (oilChange)
            {
                if (lubeJob)
                    return COST_OIL_CHANGE + COST_LUBE_JOB;
                else
                    return COST_OIL_CHANGE;
            }
            else
            {
                if (lubeJob)
                    return COST_LUBE_JOB;
                else
                    return 0.00;
            }
        }
        public double FlushCharges(bool radiatorFlush, bool transmissionFlush)
        {
            if (radiatorFlush)
            {
                if (transmissionFlush)
                    return COST_RADIATOR_FLUSH + COST_TRANSMISSION_FLUSH;
                else
                    return COST_RADIATOR_FLUSH;
            }
            else
            {
                if (transmissionFlush)
                    return COST_TRANSMISSION_FLUSH;
                else
                    return 0.00;
            }
        }
        public double MiscCharges(bool inspection, bool mufflerReplacement, bool tireReplacement)
        {
            if (inspection)
            {
                if(mufflerReplacement)
                {
                    if (tireReplacement)
                        return COST_INSPECTION + COST_MUFFLER_REPLACEMENT + COST_TIRE_ROTATION;
                    else
                        return COST_INSPECTION + COST_MUFFLER_REPLACEMENT;

                }
                else
                {
                    if (tireReplacement)
                        return COST_INSPECTION + COST_TIRE_ROTATION;
                    else
                        return COST_INSPECTION;
                }
            }else
            {
                if (mufflerReplacement)
                {
                    if (tireReplacement)
                        return COST_MUFFLER_REPLACEMENT + COST_TIRE_ROTATION;
                    else
                        return COST_MUFFLER_REPLACEMENT;
                }else
                {
                    if (tireReplacement)
                        return COST_TIRE_ROTATION;
                    else
                        return 0.00;
                }
            }
        }
        public double OtherCharges(double parts, double labour)
        {
            if (double.TryParse(txtParts.Text, out parts) && double.TryParse(txtLabour.Text, out labour))
            {
                if (txtParts.Text == "")
                {
                    if (txtLabour.Text == "")
                        return 0.00;
                    else
                        return labour * COST_LABOR;
                }
                else
                {
                    if (txtLabour.Text == "")
                        return parts;
                    else
                        return parts + (labour * COST_LABOR);
                }
            }
            else
                return 0.00;
        }
        public double TaxCharges(double parts)
        {
            if (txtParts.Text == "")
                return 0.00;
            return TAX_MULTIPLIER * parts;
        }
        public double TotalCharges()
        {
            double costParts = Convert.ToDouble(txtParts.Text);
            double costLabour = Convert.ToDouble(txtLabour.Text);
            double totalCharges = OilLubeCharges((bool)chbxOilChange.IsChecked, (bool)chbxLubeJob.IsChecked) +
                FlushCharges((bool)chbxRadiatorFlush.IsChecked, (bool)chbxTransmissionFlush.IsChecked) +
                MiscCharges((bool)chbxInspection.IsChecked, (bool)chbxMufflerReplacement.IsChecked, (bool)chbxTireRotation.IsChecked) +
                OtherCharges(costPart, costLabour)+
                TaxCharges(double.Parse(txtParts.Text));
            return totalCharges;
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            lblResult.Content = TotalCharges();
        }
    }
}
