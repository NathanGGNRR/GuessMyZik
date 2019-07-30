using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using GuessMyZik.Pages.Frames;
using Windows.ApplicationModel.Core;
using Windows.UI;
using GuessMyZik.Classes;
using GuessMyZik.Classes.ArtistClasses;
using GuessMyZik.Classes.AlbumClasses;
using GuessMyZik.Classes.TrackClasses;
using GuessMyZik.Classes.CategoryClasses;
using Windows.UI.Xaml.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;


namespace GuessMyZik.Pages.Frames
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class SecondStepSoloCategoryFrame : Page
    {
        public SecondStepSoloCategoryFrame()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        #region Variables
        private Frame rootFrame;
        private Users connectedUser;
        private Frame gameFrame;

        private APIConnect apiConnect = new APIConnect();

        private int pageFound = 0;
        private int pageAdd = 0;
        #endregion

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            FrameParameters parameter = (FrameParameters)e.Parameter;
            rootFrame = parameter.rootFrame;
            gameFrame = parameter.secondFrame;
            connectedUser = parameter.connectedUser;
            InitializeFrame();
        }


        private async void InitializeFrame()
        {
            ProgressAnimation(true, progressFound, textEmptyFound);
            string response = await apiConnect.GetAsJsonAsync("https://api.deezer.com/genre");
            listCategories = deserializeCategory(response); //Deserialize the response of the php files to class Artist.
            ProgressAnimation(false, progressFound, textEmptyFound);
            if (listCategories.data.Count != 0)
            {
                ShowListView(true, gridFound, textEmptyFound);
                chooseClass();
            }
            else
            {
                ShowListView(false, gridFound, textEmptyFound);
                textEmptyFound.Text = "NO RESULT FOUND";
            }
        }

        #region Event Button

        private void BtnValid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathValid, (sender as Button), "LastColor");
        }

        private void BtnValid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathValid, (sender as Button), "WriteColor");
        }

        private void BtnBack_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloEntered(pathBack, (sender as Button), "ExitColor");
        }

        private void BtnBack_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ChooseGame.AnimationColorBtnSoloExited(pathBack, (sender as Button), "WriteColor");
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            gameFrame.GoBack();
        }

        private void BtnBackFound_Click(object sender, RoutedEventArgs e)
        {
            chooseClassNext(false, true);
        }

        private void BtnNextFound_Click(object sender, RoutedEventArgs e)
        {
            chooseClassNext(true, true);
        }

        private void BtnBackAdd_Click(object sender, RoutedEventArgs e)
        {
            chooseClassNext(false, false);
        }

        private void BtnNextAdd_Click(object sender, RoutedEventArgs e)
        {
            chooseClassNext(true, false);
        }

        private void chooseClass()
        {
            LoadCategoriesFound();
        }

        private void chooseClassNext(bool action, bool listViewFound)
        {
            if (listViewFound)
            {
                LoadCategoriesFound(action);
            }
            else
            {
                LoadCategoriesAdd(action);
            }
        }
        #endregion

        #region Event ListView
        private void ListViewFound_ItemClick(object sender, ItemClickEventArgs e)
        {
            listViewFoundClickCategory(e);
        }


        private void ListViewAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            listViewAddClickCategory(e);
        }
        #endregion

        #region Animation
        private void ProgressAnimation(bool action, ProgressRing progressRing, TextBlock textEmpty)
        {
            if (action)
            {
                textEmpty.Visibility = Visibility.Collapsed;
                progressRing.Visibility = Visibility.Visible;
                progressRing.IsActive = true;
            }
            else
            {
                progressRing.Visibility = Visibility.Collapsed;
                progressRing.IsActive = false;
            }
        }

        private void ShowListView(bool action, Grid grid, TextBlock textBlock)
        {

            if (action)
            {
                textBlock.Visibility = Visibility.Collapsed;
                grid.Visibility = Visibility.Visible;
            }
            else
            {
                grid.Visibility = Visibility.Collapsed;
                textBlock.Visibility = Visibility.Visible;
            }
        }


        #endregion

        #region Category

        #region Variables
        private Categories listCategories;
        private Categories listCategoriesAdd = new Categories();
        private ObservableCollection<Category> categoryShowing;
        private ObservableCollection<Category> categoryAdd = new ObservableCollection<Category>();
        #endregion

        #region LoadingFound
        public void LoadCategoriesFound()
        {
            if (this.listCategories.data.Count > 5)
            {
                if ((pageFound * 5) + 5 > this.listCategories.data.Count)
                {
                    int count = this.listCategories.data.Count - (pageFound * 5);
                    categoryShowing = new ObservableCollection<Category>(this.listCategories.data.GetRange(pageFound * 5, count));
                    btnNextFound.IsEnabled = false;
                }
                else
                {
                    categoryShowing = new ObservableCollection<Category>(this.listCategories.data.GetRange(pageFound * 5, 5));
                    if ((pageFound * 5) + 5 == this.listCategories.data.Count)
                    {
                        btnNextFound.IsEnabled = false;
                    }
                    else
                    {
                        btnNextFound.IsEnabled = true;
                    }
                }
                listViewFound.ItemsSource = categoryShowing;
            }
            else
            {
                btnNextFound.IsEnabled = false;
                categoryShowing = new ObservableCollection<Category>(this.listCategories.data.GetRange(pageFound * 5, this.listCategories.data.Count));
                listViewFound.ItemsSource = categoryShowing;
            }
        }

        public void LoadCategoriesFound(int count)
        {
            categoryShowing = new ObservableCollection<Category>(this.listCategories.data.GetRange(pageFound * 5, count));
            listViewFound.ItemsSource = categoryShowing;
        }

        private void LoadCategoriesFound(bool nextPage)
        {
            if (nextPage)
            {
                if (((pageFound + 1) * 5) + 5 >= this.listCategories.data.Count)
                {
                    int count = this.listCategories.data.Count - (pageFound * 5);
                    pageFound++;
                    LoadCategoriesFound();
                    btnNextFound.IsEnabled = false;

                }
                else
                {
                    pageFound++;
                    LoadCategoriesFound();
                    btnNextFound.IsEnabled = true;
                }
                btnBackFound.IsEnabled = true;
            }
            else
            {
                if (pageFound - 1 >= 0)
                {
                    btnNextFound.IsEnabled = true;
                    pageFound--;
                    if (pageFound == 0)
                    {
                        btnBackFound.IsEnabled = false;
                    }
                    LoadCategoriesFound();
                }
                else
                {
                    btnBackFound.IsEnabled = false;
                }
            }
        }
        #endregion

        #region LoadingAdd
        public async void LoadCategoriesAdd()
        {
            await Task.Delay(1);
            if (this.listCategoriesAdd.data.Count > 5)
            {
                if ((pageAdd * 5) + 5 > this.listCategoriesAdd.data.Count)
                {
                    int count = this.listCategoriesAdd.data.Count - (pageAdd * 5);
                    categoryAdd = new ObservableCollection<Category>(this.listCategoriesAdd.data.GetRange(pageAdd * 5, count));
                    btnNextAdd.IsEnabled = false;
                }
                else
                {
                    categoryAdd = new ObservableCollection<Category>(this.listCategoriesAdd.data.GetRange(pageAdd * 5, 5));
                    if ((pageAdd * 5) + 5 == this.listCategoriesAdd.data.Count)
                    {
                        btnNextAdd.IsEnabled = false;
                    }
                    else
                    {
                        btnNextAdd.IsEnabled = true;
                    }
                }
                listViewAdd.ItemsSource = categoryAdd;
            }
            else
            {
                if (pageAdd == 1)
                {
                    pageAdd--;
                }
                btnNextAdd.IsEnabled = false;
                categoryAdd = new ObservableCollection<Category>(this.listCategoriesAdd.data.GetRange(pageAdd * 5, this.listCategoriesAdd.data.Count));
                listViewAdd.ItemsSource = categoryAdd;
            }
        }

        public void LoadCategoriesAdd(int count)
        {
            categoryAdd = new ObservableCollection<Category>(this.listCategoriesAdd.data.GetRange(pageAdd * 5, count));
            listViewAdd.ItemsSource = categoryAdd;
        }

        private void LoadCategoriesAdd(bool nextPage)
        {
            if (nextPage)
            {
                if (((pageAdd + 1) * 5) + 5 >= this.listCategories.data.Count)
                {
                    int count = this.listCategoriesAdd.data.Count - (pageAdd * 5);
                    pageAdd++;
                    LoadCategoriesAdd();
                    btnNextAdd.IsEnabled = false;

                }
                else
                {
                    pageAdd++;
                    LoadCategoriesAdd();
                    btnNextAdd.IsEnabled = true;
                }
                btnBackAdd.IsEnabled = true;
            }
            else
            {
                if (pageAdd - 1 >= 0)
                {
                    btnNextAdd.IsEnabled = true;
                    pageAdd--;
                    if (pageAdd == 0)
                    {
                        btnBackAdd.IsEnabled = false;
                    }
                    LoadCategoriesAdd();
                }
                else
                {
                    btnBackAdd.IsEnabled = false;
                }
            }
        }
        #endregion

        private Categories deserializeCategory(string response)
        {
            return this.listCategories = JsonConvert.DeserializeObject<Categories>(response); //Deserialize the response of the php files to a dictionary.
        }

        private void listViewFoundClickCategory(ItemClickEventArgs e)
        {
            categoryAdd.Insert(0, e.ClickedItem as Category);
            listCategoriesAdd.data.Insert(0, e.ClickedItem as Category);
            categoryShowing.Remove(e.ClickedItem as Category);
            listCategories.data.Remove(e.ClickedItem as Category);
            LoadCategoriesFound();
            LoadCategoriesAdd();
            if (categoryShowing.Count == 0 && listCategories.data.Count > 0)
            {
                if (pageFound > 0)
                {
                    LoadCategoriesFound(false);
                }
            }
            if (categoryAdd.Count > 5)
            {

                btnNextAdd.IsEnabled = true;
            }
            else
            {
                btnBackAdd.IsEnabled = false;
                btnNextAdd.IsEnabled = false;
                if (listCategoriesAdd.data.Count == 1)
                {
                    ShowListView(true, gridAdd, textEmptyAdd);
                }
            }
        }

        private void listViewAddClickCategory(ItemClickEventArgs e)
        {
            categoryShowing.Insert(0, e.ClickedItem as Category);
            listCategories.data.Insert(0, e.ClickedItem as Category);
            categoryAdd.Remove(e.ClickedItem as Category);
            listCategoriesAdd.data.Remove(e.ClickedItem as Category);
            LoadCategoriesFound();
            LoadCategoriesAdd();

            if (categoryAdd.Count > 5)
            {
                btnNextAdd.IsEnabled = true;
            }
            else
            {
                btnNextAdd.IsEnabled = false;
                btnBackAdd.IsEnabled = false;
                if (listCategoriesAdd.data.Count == 0)
                {
                    ShowListView(false, gridAdd, textEmptyAdd);
                }
            }
        }

        #endregion
    }
}
