﻿using AccountingPolessUp.Helpers;
using AccountingPolessUp.Implementations;
using AccountingPolessUp.Models;
using AccountingPolessUp.Views.Administration.EditPages;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AccountingPolessUp.Views.Administration
{
    /// <summary>
    /// Логика взаимодействия для PageAdmDepartments.xaml
    /// </summary>
    public partial class PageAdmDepartments : Page
    {
        private readonly DepartmentService _departmentService = new DepartmentService();
        ParticipantsService _participantsService = new ParticipantsService();
        List<Department> _departments;
        public PageAdmDepartments()
        {
            InitializeComponent();
            DataGridUpdater.AdmDepartments = this;
            BoxDirector.ItemsSource = _participantsService.Get();

            UpdateDataGrid();
            FilterComboBox.SetBoxOrganizations(BoxOrganizations);
        }
        public PageAdmDepartments(List<Department> departments)
        {
            InitializeComponent();
            DataGridUpdater.AdmDepartments = this;
            ColumSelect.Visibility = Visibility.Visible;
            _departments = departments;
            ButtonAdd.Visibility = Visibility.Hidden;
            ColumDelete.Visibility = Visibility.Hidden;
            ColumEdit.Visibility = Visibility.Hidden;
            FilterComboBox.SetBoxOrganizations(BoxOrganizations);
            DataGridUpdater.UpdateDataGrid(_departments, this);

        }
        private void ButtonRight_Click(object sender, RoutedEventArgs e)
        {
            DataNavigator.LineRight(scroll);
        }
        private void ButtonLeft_Click(object sender, RoutedEventArgs e)
        {
            DataNavigator.LineLeft(scroll);
        }
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteSelectedDepartments();
        }
        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectSelectedDepartments();
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            EditFrame.Content = new PageEditDepartments(this);
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            EditSelectedDepartments();
        }
        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();

            string directorId = string.IsNullOrEmpty(BoxDirector.Text) ? "" : _participantsService.GetByParticipantName(BoxDirector.Text).Id.ToString();

            FilterManager.ConfirmFilter(dataGrid, _departments, FullName.Text, Description.Text, DateStart.Text, DateEnd.Text, BoxStatus.Text, BoxOrganizations.Text, directorId);
        }
        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            FilterManager.ClearControls(panel);
            UpdateDataGrid();
        }
        private void DeleteSelectedDepartments()
        {

            if (dataGrid.SelectedItems.Count > 0 && MessageBox.Show("Подтвердить удаление", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (Department department in dataGrid.SelectedItems)
                {
                        _departmentService.Delete(department.Id);
                }
            }
            UpdateDataGrid();
        }
        private void SelectSelectedDepartments()
        {
            foreach (Department department in dataGrid.SelectedItems)
            {
                DataNavigator.UpdateValueComboBox(_departments.FirstOrDefault(x => x.Id == department.Id));
            }
            this.NavigationService.GoBack();
        }
        private void EditSelectedDepartments()
        {
            foreach (Department department in dataGrid.SelectedItems)
            {
                    EditFrame.Content = new PageEditDepartments(department, this);
            }
        }
        public void UpdateDataGrid()
        {
            _departments = _departmentService.Get();
              if (RoleValidator.User.Role.Name != "Admin")
                _departments = _departments.Where(x => RoleValidator.RoleChecker((int)x.DirectorId) == true).ToList();
            DataGridUpdater.UpdateDataGrid(_departments, this);
        }
        private void ButtonPositions_Click(object sender, RoutedEventArgs e)
        {
            OpenPositions();
        }
        private void OpenPositions()
        {
            foreach (Department department in dataGrid.SelectedItems)
            {
                this.NavigationService.Content = new PageAdmPosition(department);
            }
        }
        private void Number_PreviewDateInput(object sender, TextCompositionEventArgs e)
        {
            NumberValidator.DateValidator(e);
        }
    }
}
