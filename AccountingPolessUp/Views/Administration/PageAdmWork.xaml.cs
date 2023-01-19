﻿using AccountingPolessUp.Implementations;
using AccountingPolessUp.Models;
using AccountingPolessUp.Views.Administration.EditPages;
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

namespace AccountingPolessUp.Views.Administration
{
    /// <summary>
    /// Логика взаимодействия для PageAdmWork.xaml
    /// </summary>
    public partial class PageAdmWork : Page
    {
        EmploymentService _employmentService = new EmploymentService();
        public PageAdmWork()
        {
            InitializeComponent();
            dataGrid.ItemsSource = _employmentService.Get();
        }
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0 && MessageBox.Show("Подтвердить удаление", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                for (int i = 0; i < dataGrid.SelectedItems.Count; i++)
                {
                    Employment employment = dataGrid.SelectedItems[i] as Employment;
                    if (employment != null)
                    {
                        //dataGrid.Items.Remove(dataGrid.SelectedItems[i]);

                        _employmentService.Delete(employment.Id);
                    }
                }
            }
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            EditFrame.Content = new PageEditWork();
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dataGrid.SelectedItems.Count; i++)
            {
                Employment employment = dataGrid.SelectedItems[i] as Employment;
                if (employment != null)
                {
                    EditFrame.Content = new PageEditWork(employment);

                }
            }
        }
    }
}
