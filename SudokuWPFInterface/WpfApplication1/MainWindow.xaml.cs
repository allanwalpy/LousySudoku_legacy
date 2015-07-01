﻿using System;
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
using System.Data;
using System.Xml.Linq;
namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<MyTextBox> textBox = new List<MyTextBox>() { }; //коллекция коробок

        List<int> AdmissibleValues = new List<int> { }; // Содерждит колекцию допустимых значений


        public MainWindow()
        {
            InitializeComponent();
        }


        private MyTextBox TextBox_9_9Add(int row, int column)
        {
            MyTextBox tb = new MyTextBox();
            tb.row = row; tb.column = column;
            tb.Name = "_" + Convert.ToString(row) + "_" + Convert.ToString(column);
            tb.TextAlignment = TextAlignment.Center;
            tb.FontSize = 30;
            tb.Background = new SolidColorBrush(Colors.Transparent);
            tb.BorderBrush = new SolidColorBrush(Colors.Black);
            tb.BorderThickness = new Thickness(2);

            if ((column +1) % 3 == 0)
            {
                tb.Margin = new Thickness(0, 0, 10, 0);
            }
            else if ((row + 1) % 3 == 0)
                tb.Margin = new Thickness(0, 0, 0, 10);
            if ((column +1) % 3 == 0 && ((row +1) % 3 == 0))
                tb.Margin = new Thickness(0, 0, 10, 10);
                
            tb.Text = "";
            tb.TextChanged += textChangedEventHandler;
            tb.LostFocus += lostFocus;
            sudokuGrid.Children.Add(tb);

            Grid.SetColumn(tb, column);
            Grid.SetRow(tb, row);
            return tb;
        }
        
        private MyTextBox TextBox_16_16Add(int row, int column)
        {
            MyTextBox tb = new MyTextBox();
            tb.row = row; tb.column = column;
            tb.Name = "_" + Convert.ToString(row) + "_" + Convert.ToString(column);
            tb.TextAlignment = TextAlignment.Center;
            tb.FontSize = 30;
            tb.Background = new SolidColorBrush(Colors.Transparent);
            tb.BorderBrush = new SolidColorBrush(Colors.Black);
            tb.BorderThickness = new Thickness(2);

            if ((column + 1) % 4 == 0)
            {
                tb.Margin = new Thickness(0, 0, 10, 0);
            }
            else if ((row + 1) % 4 == 0)
                tb.Margin = new Thickness(0, 0, 0, 10);
            if ((column + 1) % 4 == 0 && ((row + 1) % 4 == 0))
                tb.Margin = new Thickness(0, 0, 10, 10);

            tb.Text = "";
            tb.TextChanged += textChangedEventHandler;
            tb.LostFocus += lostFocus;
            sudokuGrid.Children.Add(tb);

            Grid.SetColumn(tb, column);
            Grid.SetRow(tb, row);
            return tb;
        }

        private void textChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            MyTextBox myTextBox = (MyTextBox)sender;
            for (int i = 0; i < myTextBox.Text.Length; i++)
            {
                if (myTextBox.Text[i] < '1' || myTextBox.Text[i] > '9')
                {
                    myTextBox.Text = myTextBox.PastText;
                    return;
                }

            }
        }

        private void lostFocus(object sender, RoutedEventArgs e)
        {
            MyTextBox myTextBox = (MyTextBox)sender;

            for(int i = 0; i < myTextBox.Text.Length; i++)
            {
                if (myTextBox.Text[i] < '1' || myTextBox.Text[i] > '9')
                {
                    myTextBox.Text = myTextBox.PastText;
                    return;
                }
            }
            if (myTextBox.Text != "")
            {
                bool IsAdmissibleValue = true;
                foreach (int number in AdmissibleValues)
                {
                    if (Convert.ToInt32(myTextBox.Text) != number)
                        IsAdmissibleValue = false;
                    else
                    {
                        IsAdmissibleValue = true;
                        break;
                    }
                }
                if (!IsAdmissibleValue)
                {
                    MessageBox.Show("Введено недопустимое число", "Внимание", MessageBoxButton.OK);
                    myTextBox.Text = myTextBox.PastText;
                    return;
                }
            }
            myTextBox.PastText = myTextBox.Text;
        }

        private void _9x9_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Height = 720 + 2 * 10;
            mainWindow.Width = 750 + 2 * 10;

            sudokuGrid.Children.Clear();
            sudokuGrid.Columns = 9;
            sudokuGrid.Rows = 9;

            if (AdmissibleValues.Count != 0)
                AdmissibleValues.Clear();

            for (int i = 1; i < 10; i++)
                AdmissibleValues.Add(i);
            
            foreach (TextBox tb in textBox)
                sudokuGrid.Children.Remove(tb);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    textBox.Add(TextBox_9_9Add(i, j));
                }
            }
            ///////////////////////////////////////////////////////////









        }

        private void _16x16_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Height = 700 + 4 * 10;
            mainWindow.Width = 725 + 4 * 10;

            sudokuGrid.Children.Clear();

            sudokuGrid.Rows = 16;
            sudokuGrid.Columns = 16;

            if (AdmissibleValues.Count != 0)
                AdmissibleValues.Clear();

            for (int i = 0; i < 16; i++)
                AdmissibleValues.Add(i);

            foreach (TextBox tb in textBox)
                sudokuGrid.Children.Remove(tb);

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    textBox.Add(TextBox_16_16Add(i, j));
                }
            }
            ///////////////////////////////////////////////////////////////////////










        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
 }