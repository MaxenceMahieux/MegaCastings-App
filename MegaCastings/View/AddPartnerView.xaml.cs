using MegaCastings.DBLib.Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
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

namespace MegaCastings.View
{
    /// <summary>
    /// Logique d'interaction pour AddPartnerView.xaml
    /// </summary>
    public partial class AddPartnerView : Page
    {
        public ObservableCollection<BigCategory> BigCategories { get; set; }
        public ObservableCollection<Pack> Packs { get; set; }

        public AddPartnerView()
        {
            InitializeComponent();
            DataContext = this;
            BigCategories = GetBigCategories();
            Packs = GetPackCategories();
        }

        private ObservableCollection<BigCategory> GetBigCategories()
        {
            // Obtenez les catégories depuis la base de données
            using (MegaProductionContext context = new MegaProductionContext())
            {
                return new ObservableCollection<BigCategory>(context.BigCategories.ToList());
            }
        }

        private ObservableCollection<Pack> GetPackCategories()
        {
            using (MegaProductionContext context = new MegaProductionContext())
            {
                return new ObservableCollection<Pack>(context.Packs.ToList());
            }
        }





        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Label = label.Text;
            string Siret = siret.Text;
            string Desc = desc.Text;
            DateTime? selectedDate = date.SelectedDate;
            int CheckBoxIsActive = checkboxisactive.IsChecked == true ? 1 : 0;


            if (!string.IsNullOrEmpty(Label) && !string.IsNullOrEmpty(Siret) && !string.IsNullOrEmpty(Desc) && selectedDate.HasValue)
            {
                if (DropdownBigCategories.SelectedItem is BigCategory selectedBigCategory &&
                    DropdownPack.SelectedItem is Pack selectedPack)
                {
                    Partner newPartner = new Partner
                    {
                        Label = Label,
                        Siret = Siret,
                        Desc = Desc,
                        Datetime = selectedDate,
                        Isactive = CheckBoxIsActive,
                        Bigcategoryid = selectedBigCategory.Id,
                        Packid = selectedPack.Id,
                    };

                    using (MegaProductionContext context = new MegaProductionContext())
                    {
                        context.Partners.Add(newPartner);
                        context.SaveChanges();
                    }

                    MessageBox.Show("Utilisateur ajouté avec succès.");
                    PartnerView PartnerView = new PartnerView();
                    NavigationService?.RemoveBackEntry(); 
                    NavigationService?.Navigate(PartnerView);
                }

            }
            else
            {
                MessageBox.Show("Veuillez saisir le prénom et le nom de l'utilisateur.");
            }
        }
    }
}
