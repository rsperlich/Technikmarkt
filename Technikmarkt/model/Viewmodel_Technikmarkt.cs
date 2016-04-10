﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Technikmarkt.model;
using WpfApplication_3e_dbmodell1.model;

namespace Technikmarkt.model {
    public class Viewmodel_Technikmarkt : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        TechnikmarktEntities db = new TechnikmarktEntities();

        public IEnumerable<a_anbieter> AlleAnbieter {
            get {
                return (from a in db.a_anbieter
                        orderby a.a_anbietername
                        select a).ToList();
            }
        }

        public IEnumerable<h_haendler> AlleHaendler {
            get {
                return (from h in db.h_haendler
                        orderby h.h_haendlername
                        select h).ToList();
            }
        }

        string gewaelterAnbieter;
        public string GewaelterAnbieter {
            get { return GewaelterAnbieter; }
            set {
                gewaelterAnbieter = value;
                Console.Write(gewaelterAnbieter);
                PropertyChanged(this,new PropertyChangedEventArgs("ProduktegewählterAnbieter"));//string noch hinzufügen
            }
        }


        public IEnumerable<p_produkt> ProduktegewählterAnbieter {
            get {
                return (from p in db.p_produkt
                        where p.a_anbieter.Equals(gewaelterAnbieter)
                        select p).ToList();
            }
        }
        ICommand saveinsertstundeCommand;
        public ICommand SaveInsertstundeCommand {
            get {
                if (saveinsertstundeCommand == null)
                    saveinsertstundeCommand =
                        new DelegateCommand(SaveExecuted,
                                            SaveCanExecute);
                return saveinsertstundeCommand;
            }
        }

        public bool SaveCanExecute(object param) {
            if (param == null)
                return false;
            else
                return true;
        }

        public void SaveExecuted(object param) {
            if (param is a_anbieter) {   //  open window to get more information about anbieter
                a_anbieter anbieter1 = param as a_anbieter;
                AnbieterView v1 = new AnbieterView();
                //v1.DataContext = anbieter1;
                v1.ShowDialog();
            }

            if (param is h_haendler) {   //  edit existing  stunde
                h_haendler m1 = param as h_haendler;
                HaendlerView v1 = new HaendlerView();
                //v1.DataContext = m1;
                v1.ShowDialog();
                }

            }
        }
    }

