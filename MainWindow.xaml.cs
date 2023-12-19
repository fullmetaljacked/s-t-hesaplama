using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace süt_hesaplama
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ItemViewModel> ItemList { get; set; }
        public MainWindow()
        {
            DataContext = this;
            ItemList = new ObservableCollection<ItemViewModel>();

            InitializeComponent();

        }

        public int ToplamSut { get; set; }
        public int Counter = 1;
        public class ItemViewModel
        {
            public int Sira { get; set; }
            public string Adi { get; set; }
            public string Irki { get; set; }
            public int Laktasyon { get; set; }
            public int Sabah { get; set; }
            public int Aksam { get; set; }
            public int Toplam { get; set; }
        }
        private static readonly Random random = new Random();

        public class LaktasyonAyVerileriHolstein
        {

            private static readonly Dictionary<int, (int Min, int Max)> laktasyonAraliklari = new Dictionary<int, (int Min, int Max)>
            {
                { 1, (18, 23) },
                { 2, (23, 28) },
                { 3, (28, 31) },
                { 4, (25, 28) },
                { 5, (22, 25) },
                { 6, (19, 22) },
                { 7, (16, 19) },
                { 8, (13, 16) },
                { 9, (10, 13) },
                { 10, (7, 10) },
                { 11, (4, 7) },

            };

            public static int GenerateLaktasyonAyVerisi(int laktasyonAy)
            {


                if (laktasyonAraliklari.TryGetValue(laktasyonAy, out var aralik))
                {
                    int laktasyonAyVerisi = random.Next(aralik.Min, aralik.Max + 1);
                    return laktasyonAyVerisi;
                }
                return 0;
            }
        }
        public class LaktasyonAyVerileriSimental
        {

            private static readonly Dictionary<int, (int Min, int Max)> laktasyonAraliklari = new Dictionary<int, (int Min, int Max)>
            {
                { 1, (11, 14) },
                { 2, (14, 18) },
                { 3, (17, 21) },
                { 4, (14, 18) },
                { 5, (10, 14) },
                { 6, (8, 11) },
                { 7, (6, 9) },
                { 8, (4, 6) },
                { 9, (3, 6) },
                { 10, (3, 5) },
                { 11, (2, 5) },


            };

            public static int GenerateLaktasyonAyVerisi(int laktasyonAy)
            {


                if (laktasyonAraliklari.TryGetValue(laktasyonAy, out var aralik))
                {
                    int laktasyonAyVerisi = random.Next(aralik.Min, aralik.Max + 1);
                    return laktasyonAyVerisi;
                }
                return 0;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (rbtHolstein.IsChecked == true)
            {
                if (txtLaktasyon.Text != "")
                {
                    if (txtAdi.Text != "")
                    {
                        if (Convert.ToInt32(txtLaktasyon.Text) < 12)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(txtAdi.Text))
                                {
                                    // İlk harfi büyük yap
                                    string adi = char.ToUpper(txtAdi.Text[0]) + txtAdi.Text.Substring(1).ToLower();
                                    txtAdi.Text = adi;
                                }

                                if (VerimTablosu.Items.Count == 0)
                                {
                                    Counter = 1;
                                }
                                else
                                {
                                    Counter = (VerimTablosu.Items.Count + 1);

                                }



                                int laktasyonAyVerisiGunlukMiktar = LaktasyonAyVerileriHolstein.GenerateLaktasyonAyVerisi(Convert.ToInt32(txtLaktasyon.Text));

                                int SabahAksamFarki = random.Next(8, 17);

                                ItemList.Add(new ItemViewModel
                                {
                                    Sira = Counter,
                                    Adi = txtAdi.Text,
                                    Irki = "Holstein",
                                    Laktasyon = Convert.ToInt32(txtLaktasyon.Text),
                                    Toplam = laktasyonAyVerisiGunlukMiktar,
                                    Sabah = (laktasyonAyVerisiGunlukMiktar / 2) + (laktasyonAyVerisiGunlukMiktar * SabahAksamFarki / 100),


                                    Aksam = laktasyonAyVerisiGunlukMiktar - (laktasyonAyVerisiGunlukMiktar / 2) - (laktasyonAyVerisiGunlukMiktar * SabahAksamFarki / 100)
                                });

                                ToplamSut = (int)ItemList.Select(item => (int)item.Toplam).Sum();
                                txtToplamMiktar.Text = Convert.ToString(ToplamSut);


                            }
                            catch (Exception)
                            {
                            }
                            finally
                            {
                                if (VerimTablosu.Items.Count > 0)
                                {
                                    VerimTablosu.ScrollIntoView(VerimTablosu.Items[VerimTablosu.Items.Count - 1]);
                                }
                                txtLaktasyon.Clear();
                                txtAdi.Clear();

                            }
                        }
                        else
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(txtAdi.Text))
                                {
                                    // İlk harfi büyük yap
                                    string adi = char.ToUpper(txtAdi.Text[0]) + txtAdi.Text.Substring(1).ToLower();
                                    txtAdi.Text = adi;
                                }


                                if (VerimTablosu.Items.Count == 0)
                                {
                                    Counter = 1;
                                }
                                else
                                {
                                    Counter = (VerimTablosu.Items.Count + 1);

                                }


                                int laktasyonValue = 11;

                                int laktasyonAyVerisiGunlukMiktar = LaktasyonAyVerileriSimental.GenerateLaktasyonAyVerisi(laktasyonValue);

                                int SabahAksamFarki = random.Next(8, 17);

                                ItemList.Add(new ItemViewModel
                                {
                                    Sira = Counter,
                                    Adi = txtAdi.Text,
                                    Irki = "Holstein",
                                    Laktasyon = Convert.ToInt32(txtLaktasyon.Text),
                                    Toplam = laktasyonAyVerisiGunlukMiktar,
                                    Sabah = (laktasyonAyVerisiGunlukMiktar / 2) + (laktasyonAyVerisiGunlukMiktar * SabahAksamFarki / 100),


                                    Aksam = laktasyonAyVerisiGunlukMiktar - (laktasyonAyVerisiGunlukMiktar / 2) - (laktasyonAyVerisiGunlukMiktar * SabahAksamFarki / 100)
                                });

                                ToplamSut = (int)ItemList.Select(item => (int)item.Toplam).Sum();
                                txtToplamMiktar.Text = Convert.ToString(ToplamSut);


                            }
                            catch (Exception)
                            {
                            }
                            finally
                            {
                                if (VerimTablosu.Items.Count > 0)
                                {
                                    VerimTablosu.ScrollIntoView(VerimTablosu.Items[VerimTablosu.Items.Count - 1]);
                                }
                                txtLaktasyon.Clear();
                                txtAdi.Clear();
                                rbtSimental.IsChecked = false; // rbtSimental'in işaretini kaldır
                                rbtHolstein.IsChecked = true; // rbtHolstein'i işaretle
                            }

                        }
                    }
                }
            }


            if (rbtSimental.IsChecked == true)
            {
                if (txtLaktasyon.Text != "")
                {
                    if (txtAdi.Text != "")
                    {
                        if (Convert.ToInt32(txtLaktasyon.Text) < 12)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(txtAdi.Text))
                                {
                                    // İlk harfi büyük yap
                                    string adi = char.ToUpper(txtAdi.Text[0]) + txtAdi.Text.Substring(1).ToLower();
                                    txtAdi.Text = adi;
                                }


                                if (VerimTablosu.Items.Count == 0)
                                {
                                    Counter = 1;
                                }
                                else
                                {
                                    Counter = (VerimTablosu.Items.Count + 1);

                                }


                                int laktasyonAyVerisiGunlukMiktar = LaktasyonAyVerileriSimental.GenerateLaktasyonAyVerisi(Convert.ToInt32(txtLaktasyon.Text));

                                int SabahAksamFarki = random.Next(8, 17);

                                ItemList.Add(new ItemViewModel
                                {
                                    Sira = Counter,
                                    Adi = txtAdi.Text,
                                    Irki = "Simental",
                                    Laktasyon = Convert.ToInt32(txtLaktasyon.Text),
                                    Toplam = laktasyonAyVerisiGunlukMiktar,
                                    Sabah = (laktasyonAyVerisiGunlukMiktar / 2) + (laktasyonAyVerisiGunlukMiktar * SabahAksamFarki / 100),


                                    Aksam = laktasyonAyVerisiGunlukMiktar - (laktasyonAyVerisiGunlukMiktar / 2) - (laktasyonAyVerisiGunlukMiktar * SabahAksamFarki / 100)
                                });

                                ToplamSut = (int)ItemList.Select(item => (int)item.Toplam).Sum();
                                txtToplamMiktar.Text = Convert.ToString(ToplamSut);


                            }
                            catch (Exception)
                            {
                            }
                            finally
                            {
                                if (VerimTablosu.Items.Count > 0)
                                {
                                    VerimTablosu.ScrollIntoView(VerimTablosu.Items[VerimTablosu.Items.Count - 1]);
                                }
                                txtLaktasyon.Clear();
                                txtAdi.Clear();
                                rbtSimental.IsChecked = false; // rbtSimental'in işaretini kaldır
                                rbtHolstein.IsChecked = true; // rbtHolstein'i işaretle
                            }
                        }
                        else
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(txtAdi.Text))
                                {
                                    // İlk harfi büyük yap
                                    string adi = char.ToUpper(txtAdi.Text[0]) + txtAdi.Text.Substring(1).ToLower();
                                    txtAdi.Text = adi;
                                }


                                if (VerimTablosu.Items.Count == 0)
                                {
                                    Counter = 1;
                                }
                                else
                                {
                                    Counter = (VerimTablosu.Items.Count + 1);

                                }
                                int laktasyonValue = 11;

                                int laktasyonAyVerisiGunlukMiktar = LaktasyonAyVerileriSimental.GenerateLaktasyonAyVerisi(laktasyonValue);

                                int SabahAksamFarki = random.Next(8, 17);

                                ItemList.Add(new ItemViewModel
                                {
                                    Sira = Counter,
                                    Adi = txtAdi.Text,
                                    Irki = "Simental",
                                    Laktasyon = Convert.ToInt32(txtLaktasyon.Text),
                                    Toplam = laktasyonAyVerisiGunlukMiktar,
                                    Sabah = (laktasyonAyVerisiGunlukMiktar / 2) + (laktasyonAyVerisiGunlukMiktar * SabahAksamFarki / 100),


                                    Aksam = laktasyonAyVerisiGunlukMiktar - (laktasyonAyVerisiGunlukMiktar / 2) - (laktasyonAyVerisiGunlukMiktar * SabahAksamFarki / 100)
                                });

                                ToplamSut = (int)ItemList.Select(item => (int)item.Toplam).Sum();
                                txtToplamMiktar.Text = Convert.ToString(ToplamSut);


                            }
                            catch (Exception)
                            {
                            }
                            finally
                            {
                                if (VerimTablosu.Items.Count > 0)
                                {
                                    Counter = VerimTablosu.Items.Count;
                                    VerimTablosu.ScrollIntoView(VerimTablosu.Items[VerimTablosu.Items.Count - 1]);
                                }
                                txtLaktasyon.Clear();
                                txtAdi.Clear();
                                rbtSimental.IsChecked = false; // rbtSimental'in işaretini kaldır
                                rbtHolstein.IsChecked = true; // rbtHolstein'i işaretle
                            }

                        }

                    }
                }
            }

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void VerimTablosu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSil.IsEnabled = VerimTablosu.SelectedItem != null;
        }

        private void btnSil_Click(object sender, RoutedEventArgs e)
        {
            if (VerimTablosu.SelectedItem != null)
            {
                ItemViewModel selectedRow = (ItemViewModel)VerimTablosu.SelectedItem;
                ItemList.Remove(selectedRow);

                // Toplam miktarı güncelle
                ToplamSut = (int)ItemList.Select(item => (int)item.Toplam).Sum();
                txtToplamMiktar.Text = Convert.ToString(ToplamSut);

                // Silindiğinde düğmeyi devre dışı bırak
                btnSil.IsEnabled = false;
            }
        }
    }
}
