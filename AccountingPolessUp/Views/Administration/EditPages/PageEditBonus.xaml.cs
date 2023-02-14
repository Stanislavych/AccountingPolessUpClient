﻿using AccountingPolessUp.Helpers;
using AccountingPolessUp.Implementations;
using AccountingPolessUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AccountingPolessUp.Views.Administration.EditPages
{
    /// <summary>
    /// Логика взаимодействия для PageEditBonus.xaml
    /// </summary>
    public partial class PageEditBonus : Page
    {

        Page _parent;
        BonusService _bonusService = new BonusService();
        RankService _RankService = new RankService();
        RankBonusService _rankBonusService = new RankBonusService();
        List<Rank> _ranks;
        Bonus _bonus;
        public PageEditBonus(Bonus bonus, Page parent)
        {
            InitializeComponent();
            ButtonSaveEdit.Visibility = Visibility.Visible;
            ButtonAdd.Visibility = Visibility.Hidden;
            _ranks = _RankService.Get();
            DataContext = bonus;
            _bonus = bonus;
            _parent = parent;
            BoxRank.ItemsSource = _ranks;
        }
        public PageEditBonus(Page parent)
        {
            InitializeComponent();
            ButtonSaveEdit.Visibility = Visibility.Hidden;
            ButtonAdd.Visibility = Visibility.Visible;
            _bonus = new Bonus();
            _ranks = _RankService.Get();
            _parent = parent;
            BoxRank.ItemsSource = _ranks;
            _parent = parent;
            ButtonSaveRankBonus.IsEnabled = false;
            ButtonDeleteRankBonus.IsEnabled = false;
        }
        private void OpenRank_Click(object sender, RoutedEventArgs e)
        {
            DataNavigator.ChangePage = this;
            DataNavigator.NameBox = BoxRank.Name;
            _parent.NavigationService.Content = new PageAdmRanks(_ranks);
        }
        private void ButtonSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WriteData();
                if (FormValidator.AreAllElementsFilled(this))
                    throw new Exception();
                _bonusService.Update(_bonus);
                DataGridUpdater.AdmBonus.UpdateDataGrid();
                this.NavigationService.GoBack();
            }
            catch (Exception)
            {
                MessageBox.Show("Заполните все поля корректно!");
            }
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WriteData();
                if (FormValidator.AreAllElementsFilled(this))
                    throw new Exception();
                _bonusService.Create(_bonus);
                DataGridUpdater.AdmBonus.UpdateDataGrid();
                this.NavigationService.GoBack();
            }
            catch (Exception)
            {
                MessageBox.Show("Заполните все поля корректно!");
            }
        }
        private void ButtonSaveRankBonus_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                _rankBonusService.Create(new RankBonus { RankId = _ranks.FirstOrDefault(i => i == BoxRank.SelectedItem).Id, BonusId = _bonus.Id });
                DataGridUpdater.AdmBonus.UpdateDataGrid();

            }
            catch (Exception)
            {

                MessageBox.Show("Ошибка при создании связи");
            }

        }
        private void ButtonDeleteRankBonus_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                _rankBonusService.Delete(_ranks.FirstOrDefault(i => i == BoxRank.SelectedItem).Id, _bonus.Id);
                DataGridUpdater.AdmBonus.UpdateDataGrid();

            }
            catch (Exception)
            {

                MessageBox.Show("Ошибка при удалении связи");
            }

        }
        private void WriteData()
        {
            _bonus.BonusName = BonusName.Text;
            _bonus.BonusDescription = BonusDescription.Text;
        }
    }
}
