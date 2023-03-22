﻿using AccountingPolessUp.Helpers;
using AccountingPolessUp.Implementations;
using AccountingPolessUp.Models;
using AccountingPolessUp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace AccountingPolessUp.Views.Administration.EditPages
{
    /// <summary>
    /// Логика взаимодействия для PageEditUser.xaml
    /// </summary>
    public partial class PageEditUser : Page
    {
        private UserService _userService = new UserService();
        private RoleService _roleService = new RoleService();
        private List<Role> roles;
        private User user;
        private Page _parent;

        public PageEditUser(User user, Page parent)
        {
            InitializeComponent();
            Password.IsEnabled = false;
            FillDataContext(user);
            ButtonSaveEdit.Visibility = Visibility.Visible;
            _parent = parent;
        }
        public PageEditUser(User user, bool changePassword, Page parent)
        {
            InitializeComponent();
            _parent = parent;
            FillDataContext(user);
            ButtonEditPassword.Visibility = Visibility.Visible;
            Login.IsEnabled = false;
            BoxRole.IsEnabled = false;
        }
        public PageEditUser(Page parent)
        {
            InitializeComponent();
            this.user = new User();
            ButtonAdd.Visibility = Visibility.Visible;
            CheckRole();
            BoxRole.ItemsSource = roles;
            BoxRole.SelectedIndex = roles.IndexOf(roles.FirstOrDefault(x => x.Id == user.RoleId));
            _parent = parent;
        }
        private void FillDataContext(User user)
        {
            this.user = user;
            DataContext = user;
            CheckRole();
            BoxRole.ItemsSource = roles;
            BoxRole.SelectedIndex = roles.IndexOf(roles.FirstOrDefault(x => x.Id == user.RoleId));
        }
        private void CheckRole()
        {
            roles = _roleService.Get();
            if (RoleValidator.User.Role.Name != "Admin")
                roles = roles.Where(x => x.Name == "User").ToList();
        }
        private void ButtonSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WriteData();
                DataAccess.Update(this, user);
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
                if (FormValidator.AreAllElementsFilled(this))
                    throw new Exception();
                RegisterDto registerDto = new RegisterDto();
                registerDto.Login = Login.Text;
                registerDto.Password = Password.Password;
                registerDto.RoleId = roles.FirstOrDefault(x => x == BoxRole.SelectedItem).Id;
                _userService.Create(registerDto);
                DataGridUpdater.AdmUsers.UpdateDataGrid();
                CancelFrameChecker.CreateData = true;
                MessageBox.Show("Пользователь добавлен!");
            }
            catch (Exception)
            {
                MessageBox.Show("Заполните все поля корректно!");
            }
        }
        private void ButtonEditPassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Password.Password))
                    throw new Exception();

                UpdatePasswordDto upPassword = new UpdatePasswordDto();
                upPassword.Id = user.Id;
                upPassword.Password = Password.Password;
                _userService.UpdatePassword(upPassword);
                DataGridUpdater.AdmUsers.UpdateDataGrid();
                MessageBox.Show("Пароль изменен!");
            }
            catch (Exception)
            {
                MessageBox.Show("Заполните все поля корректно!");
            }
        }
        private void WriteData()
        {
            user.Login = Login.Text;
            user.RoleId = roles.FirstOrDefault(x => x == BoxRole.SelectedItem).Id;
        }
    }
}
