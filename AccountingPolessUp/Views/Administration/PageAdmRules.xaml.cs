﻿using AccountingPolessUp.Helpers;
using AccountingPolessUp.Implementations;
using AccountingPolessUp.Models;
using AccountingPolessUp.Views.Administration.EditPages;
using AccountingPolessUp.Views.TextViews;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AccountingPolessUp.Views.Administration
{
    /// <summary>
    /// Логика взаимодействия для PageAdmRules.xaml
    /// </summary>
    public partial class PageAdmRules : Page
    {
        private readonly RegulationService _regulationService = new RegulationService();
        List<Regulation> _regulations;
        public PageAdmRules()
        {
            InitializeComponent();
            _regulations = _regulationService.Get();
            UpdateDataGrid();
            FilterComboBox.SetBoxOrganizations(BoxOrganization);
        }
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteSelectedRegulations();
        }
        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            FilterManager.ConfirmFilter(dataGrid, _regulations, BoxOrganization.Text, Name.Text, Text.Text, Description.Text);
        }
        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            FilterManager.ClearControls(Panel);
            UpdateDataGrid();
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            EditFrame.Content = new PageEditRules(this);
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            EditSelectedRules();
        }
        private void UpdateDataGrid()
        {
            DataGridUpdater.UpdateDataGrid(_regulations, this);
        }
        private void DeleteSelectedRegulations()
        {
            if (dataGrid.SelectedItems.Count > 0 && MessageBox.Show("Confirm deletion", "Deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (Regulation regulation in dataGrid.SelectedItems)
                {
                    _regulationService.Delete(regulation.Id);
                }
            }
            UpdateDataGrid();
        }
        private void EditSelectedRules()
        {
            foreach (Regulation regulation in dataGrid.SelectedItems)
            {
                EditFrame.Content = new PageEditRules(regulation, this);
                break;
            }
        }
        private void ButtonText_Click(object sender, RoutedEventArgs e)
        {
            foreach (Regulation regulation in dataGrid.SelectedItems)
            {
                WindowRegulationText windowText = new WindowRegulationText(regulation);
                windowText.Show();
            }
            
        }
    
    }
}
