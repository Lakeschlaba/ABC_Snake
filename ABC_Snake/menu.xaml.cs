﻿using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ABC_Snake
{

    public partial class menu : Window
    {

        SoundPlayer backgroundSound = new SoundPlayer(@"C:\Users\gereo\Documents\ProgrammeC#\ABC_Snake\ABC_Snake_Game\Sounds\YiJianMei.wav"); //Soundfile für den Hintergrund

        public menu() //Konstruktor (haben keinen Rückgabewert)
        {
            InitializeComponent();
            backgroundSound.PlayLooping(); //Spielt den Sound ab und das in einem Loop
        }

        private void GameStartBtn_Click(object sender, RoutedEventArgs e) //Wird Button namens GameStartBtn geklickt wird diese Methode ausgeführt
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show(); //MainWindow Fenster wird ausgeführt
            backgroundSound.Stop(); //Stopt den Sound
            this.Close(); //Alles andere schließt sich
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e) //Wird Button namens ExitBtn geklickt wird diese Methode ausgeführt
        {
            backgroundSound.Stop(); //Stopt den Sound
            this.Close(); //Alles schließt sich
        }
    }
}
