using HelperExtensions;
using System;
using System.Text;
using System.Windows;
using System.Windows.Media;
using HelperMethods;

namespace PathResizer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        PtArrow.Fill = PtArrow.Stroke = new SolidColorBrush(
            Color.FromRgb((byte)NumberMethods.GetRandomDouble(1, 256),
                (byte)NumberMethods.GetRandomDouble(1, 256),
                (byte)NumberMethods.GetRandomDouble(1, 256)));
    }

    #region Events

    #region BtnCopy_OnClick
    /// <summary>
    /// Copies the converted path text into the Clipboard.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnCopy_OnClick(object sender, RoutedEventArgs e)
    {
        if (!TxbResizedData.Text.IsNullOrWhiteSpace())
        {
            TxbResizedData.SelectAll();
            TxbResizedData.Copy();
        }
    }
    #endregion

    #region BtnResize_OnClick
    /// <summary>
    /// Generates the resized path.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnResize_OnClick(object sender, RoutedEventArgs e)
    {
        if (Double.TryParse(TxbRatio.Text, out double ratio) && !TxbOriginalData.Text.IsNullOrWhiteSpace())
        {
            StringBuilder finalPath = new();
            string number = String.Empty;

            TxbOriginalData.Text.ForEach(_ =>
            {
                if (Char.IsNumber(_) || _ == '.')
                    number += _;
                else
                {
                    finalPath.Append(ConvertNumber(number, ratio));
                    number = String.Empty;
                    finalPath.Append(_);
                }
            });

            finalPath.Append(ConvertNumber(number, ratio));
            TxbResizedData.Text = finalPath.ToString();
        }
    }
    #endregion

    #endregion

    #region Private Methods

    #region ConvertNumber
    /// <summary>
    /// Converts the specified number according to the specified ratio.
    /// </summary>
    /// <param name="originalNumber"></param>
    /// <param name="ratio"></param>
    /// <returns></returns>
    private string ConvertNumber(string originalNumber, double ratio)
    {
        if (originalNumber != String.Empty)
        {
            double convertedNumber = Double.Parse(originalNumber);
            return convertedNumber == 1 ? "1" : (convertedNumber * ratio).ToString("0.##");
        }

        return String.Empty;
    }
    #endregion

    #endregion
}
