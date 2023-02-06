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
    /// Interaction logic for PageAdmScheduleOfClasses.xaml
    /// </summary>
    public partial class PageAdmScheduleOfClasses : Page
    {
        ScheduleOfClassesService _scheduleOfClassesService = new ScheduleOfClassesService();
        public PageAdmScheduleOfClasses()
        {
            InitializeComponent();
            DataGridUpdater.UpdateDataGrid(_scheduleOfClassesService.Get(), this);
        }
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0 && MessageBox.Show("Подтвердить удаление", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                for (int i = 0; i < dataGrid.SelectedItems.Count; i++)
                {
                    ScheduleOfСlasses Schedule = dataGrid.SelectedItems[i] as ScheduleOfСlasses;
                    if (Schedule != null)
                    {
                        _scheduleOfClassesService.Delete(Schedule.Id);
                    }
                }
            }
            DataGridUpdater.UpdateDataGrid(_scheduleOfClassesService.Get(), this);
        }
        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            FilterManager.ConfirmFilter(dataGrid, _scheduleOfClassesService.Get(), Description.Text, DateStart.Text, DateEnd.Text,WorkSpaceLink.Text,BoxTrainingCourses.Text);
        }
        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            FilterManager.ClearControls(Panel);
            DataGridUpdater.UpdateDataGrid(_scheduleOfClassesService.Get(), this);
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            EditFrame.Content = new PageEditScheduleOfClasses(this);
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dataGrid.SelectedItems.Count; i++)
            {
                ScheduleOfСlasses Schedule = dataGrid.SelectedItems[i] as ScheduleOfСlasses;
                if (Schedule != null)
                {
                    EditFrame.Content = new PageEditScheduleOfClasses(Schedule, this);
                }
            }
        }
        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            NumberValidator.Validator(e);
        }
    }
}
