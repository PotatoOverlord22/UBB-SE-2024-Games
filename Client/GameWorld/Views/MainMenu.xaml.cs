﻿using System.Windows;
using GameWorld.Resources.Utils;
using GameWorld.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameWorld.Views
{
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void PlayHarvestHavenButton_Click(object sender, RoutedEventArgs e)
        {
            HarvestHavenMainMenu harvestHavenMainMenu = new HarvestHavenMainMenu(DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<IHarvestHavenMainService>());
            harvestHavenMainMenu.Show();
            this.Close();
        }
        private void PlayPokerButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage(DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<ICasinoPokerMainService>());
            loginPage.Show();
            /* MenuWindow mainMenu = new MenuWindow(GameStateManager.GetCurrentUser(), DependencyInjectionConfigurator.ServiceProvider.GetRequiredService<ICasinoPokerMainService>());
             mainMenu.Show();*/
            this.Close();
        }
    }
}