﻿using AccountingPolessUp.Helpers;
using AccountingPolessUp.Implementations;
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
    /// Логика взаимодействия для PageAdmFinalProject.xaml
    /// </summary>
    public partial class PageAdmFinalProject : Page
    {
        FinalProjectService _finalProjectService = new FinalProjectService();
        Employment _employment;
        public PageAdmFinalProject(Employment employment)
        {
            InitializeComponent();
            _employment = employment;
            DataGridUpdater.UpdateDataGrid(_finalProjectService.GetByEmployment(employment.Id), this);
        }
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0 && MessageBox.Show("Подтвердить удаление", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                for (int i = 0; i < dataGrid.SelectedItems.Count; i++)
                {
                    FinalProject FinalProject = dataGrid.SelectedItems[i] as FinalProject;
                    if (FinalProject != null)
                    {

                        _finalProjectService.Delete(FinalProject.Id);
                    }
                }
            }
            DataGridUpdater.UpdateDataGrid(_finalProjectService.GetByEmployment(_employment.Id), this);

        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            EditFrame.Content = new PageEditFinalProject(_employment, this);
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dataGrid.SelectedItems.Count; i++)
            {
                FinalProject FinalProject = dataGrid.SelectedItems[i] as FinalProject;
                if (FinalProject != null)
                {

                    EditFrame.Content = new PageEditFinalProject(FinalProject, _employment, this);

                }
            }
        }
        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            FilterManager.ConfirmFilter(dataGrid, _finalProjectService.Get().Where(x => x.EmploymentId == _employment.Id), TextBoxName.Text,
                TextBoxDescription.Text, TextBoxGitHub.Text, TextBoxLink.Text, DateStart.Text, DateEnd.Text);
        }
        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }
        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
        private void Clear()
        {
            TextBoxName.Text = string.Empty;
            TextBoxDescription.Text = string.Empty;
            TextBoxGitHub.Text = string.Empty;
            TextBoxLink.Text = string.Empty;
            DateEnd.Text = string.Empty;
            DateStart.Text = string.Empty;
            dataGrid.ItemsSource = null;
            dataGrid.Items.Clear();
            dataGrid.ItemsSource = _finalProjectService.Get().Where(x => x.EmploymentId == _employment.Id);
        }
       
    }
}
