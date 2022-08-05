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

// NEW
using System.Data.Entity;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow : Window
    {
        DBEntities context = new DBEntities();

        CollectionViewSource modtreeViewSource;
        CollectionViewSource modViewSource;
        CollectionViewSource secL2ViewSource;

        private DAL dal = new DAL();
        private Rules rules = new Rules();

        private modtree selectedModule = new modtree();
        private modtree selectedSecLvl1 = new modtree();
        private modtree selectedSecLvl2 = new modtree();

        public MainWindow()
        {
            InitializeComponent();

            modtreeViewSource = ((CollectionViewSource)(FindResource("modtreeViewSource")));
            modViewSource = ((CollectionViewSource)(FindResource("moduleViewSource")));
            secL2ViewSource = ((CollectionViewSource)(FindResource("secLvl2ViewSource")));

            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //context.modtrees.Load();
            //modtreeViewSource.Source = context.modtrees.Local;

            //context.modtrees.Load();
            //modViewSource.Source = context.modtrees.Local;

            PopulateModTreGrid();
        }

        private void PopulateModTreGrid()
        {
            modtreeViewSource.Source = dal.Exec("select n100, s100, s101, s102, s105, s1, s2, s3, s8, s39, s40, n1 from modtree where s100 = 'MOD'");
            //modtreeViewSource.View.Refresh();         

        }

        private void PopulateModuleGrid()
        { 
            string modCode = selectedModule.s1.ToString().Substring(0, 2);
            string sql = "SELECT n100, s100, s101, s102, s105, s1, s2, s3, s8, s39, s40, n1 FROM modtree WHERE s100 = 'SEC' AND LEFT(s1,2)= '" + modCode + "' AND LEN(s1)= 4 ORDER BY s1 DESC";
            modViewSource.Source = dal.Exec(sql);
        }

        private void PopulatesecLvl2ViewSource()
        {
            string secCode = selectedSecLvl1.s1.ToString();

            string sql = "SELECT n100, s100, s101, s102, s105, s1, s2, s3, s8, s39, s40, n1 FROM modtree WHERE s100 = 'SCR' AND LEFT(s1,4)= '"+ secCode + "'";
            secL2ViewSource.Source = dal.Exec(sql);
            //modtreeViewSource.View.Refresh();         
            //
        }

        private void LastCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            modtreeViewSource.View.MoveCurrentToLast();
        }

        private void PreviousCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            modtreeViewSource.View.MoveCurrentToPrevious();
        }

        private void NextCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            modtreeViewSource.View.MoveCurrentToNext();
        }

        private void FirstCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            modtreeViewSource.View.MoveCurrentToFirst();
        }

        /// chk section 
        /// 
        private void DeleteModuleCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        private void UpdateCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {            
            modtree mt = new modtree()
            {
                n100 = Convert.ToInt32(idTextBox.Text),
                s100 = childTypeComboBox.Text,
                s101 = statusComboBox.Text,
                s102 = docTypeTextBox.Text,
                s105 = productCodeTextBox.Text,
                s1 = moduleCodeTextBox.Text,
                s2 = moduleNameTextBox.Text,
                s3 = parentComboBox.Text,
                s8 = moduleImgTextBox.Text,
                s39 = treeLvlTextBox.Text,
                s40 = prevLvlTextBox.Text,
                n1 = Convert.ToInt32(displayOrderTextBox.Text)
            };
            dal.UpdateStudent(mt);
        }

        private void RefreshCommandCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            PopulateModTreGrid();
        }
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Collapse();
            //sectionGrid.Visibility = Visibility.Collapsed;
            ModuleInfoGrid.Visibility = Visibility.Collapsed;
            creationModtreeGrid.Visibility = Visibility.Collapsed;
            existingModtreeGrid.Visibility = Visibility.Visible;
            modtreeDataGrid.Visibility = Visibility.Visible;           
        }

        private void ResetControls()
        {           
            childTypeComboBox.Text = string.Empty;
            statusComboBox.Text = string.Empty;
            docTypeTextBox.Text= string.Empty;
            productCodeTextBox.Text = string.Empty;
            moduleCodeTextBox.Text = string.Empty;
            moduleNameTextBox.Text = string.Empty;
            parentComboBox.Text = string.Empty;
            moduleImgTextBox.Text = string.Empty;
            treeLvlTextBox.Text = string.Empty;
            prevLvlTextBox.Text = string.Empty;
            displayOrderTextBox.Text = string.Empty;
            create_idTextBox.Text = string.Empty;
            create_childTypeTextBox.Text = string.Empty;
            create_statusTextBox.Text = string.Empty;
            create_docTypeTextBox.Text = string.Empty;
            create_productCodeTextBox.Text = string.Empty;
            create_moduleCodeTextBox.Text = string.Empty;
            create_moduleNameTextBox.Text = string.Empty;
            create_parentTextBox.Text = string.Empty;
            create_moduleImgTextBox.Text = string.Empty;
            create_treeLvlTextBox.Text = string.Empty;
            create_prevLvlTextBox.Text = string.Empty;
            create_displayOrderTextBox.Text = string.Empty;

        }

        //// Cancels any input into the new customer form  
        private void CancelCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Collapse();

            ResetControls();
        }

        ///Collapse Evrything HAHAH!!
        private void Collapse()
        {

            existingModtreeGrid.Visibility = Visibility.Collapsed;            
            creationModtreeGrid.Visibility = Visibility.Collapsed;

            ModuleInfoGrid.Visibility = Visibility.Collapsed;
            SectionInfoGrid.Visibility = Visibility.Collapsed;
            newSectionLvl1Grid.Visibility = Visibility.Collapsed;
            newSectionLvl2Grid.Visibility = Visibility.Collapsed;

            selctSecL1Grid.Visibility = Visibility.Collapsed;
            selctSecL2Grid.Visibility = Visibility.Collapsed;
            selSecCntlBtnGrid.Visibility = Visibility.Collapsed;
            scrnCntlBtnGrid.Visibility = Visibility.Collapsed;
            
            modtreeDataGrid.Visibility = Visibility.Collapsed;
            secLvl2DataGrid.Visibility = Visibility.Collapsed;
            moduleDataGrid.Visibility = Visibility.Collapsed;
        }
        private void SelectModuleCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            Collapse();

            existingModtreeGrid.Visibility = Visibility.Collapsed;
            creationModtreeGrid.Visibility = Visibility.Visible;    
            modtreeDataGrid.Visibility = Visibility.Visible;

        }

        private void Delete_Order(modtree mod)
        {
            dal.RemoveMod(mod.n100);
            PopulateModTreGrid();
        }

        private void DeleteOrderCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        private void childTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 

        }

        private void moduleDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cust = modViewSource.View.CurrentItem as modtree;
            if (cust == null)
            {
                MessageBox.Show("No customer selected.");
                return;
            }

            //section_sectionCodeTextBox.Text  = cust.s1.ToString();
            //
            //section_sectionNameTextBox.Text = cust.s2.ToString();

        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            
            selectedModule = dal.GetRecord(create_moduleCodeTextBox.Text);
            MessageBox.Show(create_moduleCodeTextBox.Text);

            create_idTextBox.Text = selectedModule.n100.ToString();
            create_childTypeTextBox.Text = selectedModule.s100.ToString();
            create_statusTextBox.Text = selectedModule.s101.ToString();
            create_docTypeTextBox.Text = selectedModule.s102.ToString();
            create_productCodeTextBox.Text = selectedModule.s105;
            create_moduleCodeTextBox.Text = selectedModule.s1.ToString();
            create_moduleNameTextBox.Text = selectedModule.s2.ToString();
            create_parentTextBox.Text = selectedModule.s3.ToString();
            create_moduleImgTextBox.Text = selectedModule.s8.ToString() ;
            create_treeLvlTextBox.Text = selectedModule.s39.ToString();
            create_prevLvlTextBox.Text = selectedModule.s40.ToString();
            create_displayOrderTextBox.Text = selectedModule.n1.ToString();

            //modtreeDataGrid.Visibility = Visibility.Visible;
            //PopulateGrid();
            
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            modtree temp = new modtree();
            temp.n100 = Convert.ToDecimal(create_idTextBox.Text);
        }

        private void StkPnlNavButtons_IsEnabled(bool flag)
        {
            if (flag)
            {
                btnFirst.IsEnabled = true;
                btnPrev.IsEnabled = true;
                btnNext.IsEnabled = true;
                btnLast.IsEnabled = true;
            }
            else
            {
                btnFirst.IsEnabled = false;
                btnPrev.IsEnabled = false;
                btnNext.IsEnabled = false;
                btnLast.IsEnabled = false;
            }
        }

        private void selectButton_Click(object sender, RoutedEventArgs e)
        {

            StkPnlNavButtons_IsEnabled(false);

            if (!string.IsNullOrEmpty(create_childTypeTextBox.Text) && !string.IsNullOrEmpty(create_statusTextBox.Text) && !string.IsNullOrEmpty(create_docTypeTextBox.Text) && !string.IsNullOrEmpty(create_productCodeTextBox.Text) && !string.IsNullOrEmpty(create_moduleCodeTextBox.Text) && !string.IsNullOrEmpty(create_displayOrderTextBox.Text)  && !string.IsNullOrEmpty(create_moduleNameTextBox.Text) && !string.IsNullOrEmpty(create_parentTextBox.Text) && !string.IsNullOrEmpty(create_moduleImgTextBox.Text) && !string.IsNullOrEmpty(create_treeLvlTextBox.Text) && !string.IsNullOrEmpty(create_prevLvlTextBox.Text))
            {
                //selectedModule.n100 = Convert.ToInt32(create_idTextBox.Text);
                selectedModule.s100 = create_childTypeTextBox.Text;
                selectedModule.s101 = create_statusTextBox.Text;
                selectedModule.s102 = create_docTypeTextBox.Text;
                selectedModule.s105 = create_productCodeTextBox.Text;
                selectedModule.s1 = create_moduleCodeTextBox.Text;
                selectedModule.s2 = create_moduleNameTextBox.Text;
                selectedModule.s3 = create_parentTextBox.Text;
                selectedModule.s8 = create_moduleImgTextBox.Text;
                selectedModule.s39 = create_treeLvlTextBox.Text;
                selectedModule.s40 = create_prevLvlTextBox.Text;
                selectedModule.n1 = Convert.ToDecimal(create_displayOrderTextBox.Text);

                PopulateModuleGrid();

                moduleDataGrid.Visibility = Visibility.Collapsed;
                modtreeDataGrid.Visibility = Visibility.Collapsed;
                creationModtreeGrid.Visibility = Visibility.Collapsed;

                ModuleInfoGrid.Visibility = Visibility.Visible;
                newSectionLvl1Grid.Visibility = Visibility.Visible;
                newSectionLvl2Grid.Visibility = Visibility.Visible;

                //string modCode = selectedModule.s1.ToString().Substring(0, 2);
                //string sql = "select n100,s100,s102,s1,s2 from modtree where left(s1, 2) = '" + modCode + "' and(s100 = 'MOD' or s100 = 'SEC') order by s1";

                //modViewSource.Source = dal.Exec(sql);
                //modViewSource.View.Refresh();

                edit_idTextBox.Text = selectedModule.n100.ToString();
                edit_moduleCodeTextBox.Text = selectedModule.s1.ToString();
                edit_moduleNameTextBox.Text = selectedModule.s2.ToString();
                edit_docTypeTextBox.Text = selectedModule.s102.ToString();
            }
            else
            {
                MessageBox.Show("Please dont leave any fields empty", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void addScreenButton_Click(object sender, RoutedEventArgs e)
        {

            StkPnlNavButtons_IsEnabled(true);
        }
        
        private void editScreenButton_Click(object sender, RoutedEventArgs e)
        {
        }
        private void addSecLvl1Button_Click(object sender, RoutedEventArgs e)
        {
            modViewSource.View.Refresh();
            if (!string.IsNullOrEmpty(newSectionLvl1_sectionNameTextBox.Text) && !string.IsNullOrEmpty(newSectionLvl1_sectionCodeTextBox.Text))
            {
                ///Checks if section lvl 1 code has the first 2 digits of the module code and has a len of 4 and checks if the code already exists in the database
                string modCode = selectedModule.s1.ToString().Substring(0, 2);
                if ( (newSectionLvl1_sectionCodeTextBox.Text.Substring(0,2) == modCode) && (newSectionLvl1_sectionCodeTextBox.Text.Length == 4) && (rules.IsUnique(selectedModule.s1.ToString(), newSectionLvl1_sectionCodeTextBox.Text,1) == true) )
                {
                    dal.InsertSectionLvl1(selectedModule, newSectionLvl1_sectionNameTextBox.Text, newSectionLvl1_sectionCodeTextBox.Text);
                    newSectionLvl2Grid.IsEnabled = true;
                    addScreenButton.Visibility = Visibility.Visible;

                    selectedSecLvl1.s1 = newSectionLvl1_sectionCodeTextBox.Text;
                    selectedSecLvl1.s2 = newSectionLvl1_sectionNameTextBox.Text;

                    PopulatesecLvl2ViewSource();
                    newSectionLvl1Grid.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Invalid code for section", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill the Section Name and Code first", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            secL2ViewSource.View.Refresh();

        }

        private void addSecLvl2Button_Click(object sender, RoutedEventArgs e)
        { 

            if (!string.IsNullOrEmpty(newSectionLvl2_sectionNameTextBox.Text) && !string.IsNullOrEmpty(newSectionLvl2_sectionCodeTextBox.Text))
            {
                ///Checks if section lvl 2 code has the first 4 digits of sec lvl 1 and has a len of 6
                string modCode = newSectionLvl1_sectionCodeTextBox.Text;
                if ( (newSectionLvl2_sectionCodeTextBox.Text.Substring(0, 4) == modCode) && (newSectionLvl2_sectionCodeTextBox.Text.Length == 6 ) && (rules.IsUnique(selectedModule.s1.ToString(), newSectionLvl2_sectionCodeTextBox.Text, 2) == true) )
                {
                    dal.InsertSectionLvl2(selectedModule, newSectionLvl2_sectionNameTextBox.Text, newSectionLvl2_sectionCodeTextBox.Text);
                }
                else
                {
                    MessageBox.Show("Invalid code for section", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill the Section Name and Code first", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void createSectionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addModuleButton_Click(object sender, RoutedEventArgs e)
        {
            if (
                !string.IsNullOrEmpty(productCodeTextBox.Text) && !string.IsNullOrEmpty(childTypeComboBox.Text)
                && !string.IsNullOrEmpty(statusComboBox.Text) && !string.IsNullOrEmpty(docTypeTextBox.Text)
                && !string.IsNullOrEmpty(moduleCodeTextBox.Text) && !string.IsNullOrEmpty(moduleNameTextBox.Text)
                && !string.IsNullOrEmpty(parentComboBox.Text) && !string.IsNullOrEmpty(moduleImgTextBox.Text)
                && !string.IsNullOrEmpty(treeLvlTextBox.Text) && !string.IsNullOrEmpty(prevLvlTextBox.Text)
                && !string.IsNullOrEmpty(displayOrderTextBox.Text)
                )
            {
                try
                {
                    modtree mt = new modtree()
                    {
                        //n100 = Convert.ToInt32(idTextBox.Text),
                        s100 = childTypeComboBox.Text,
                        s101 = statusComboBox.Text,
                        s102 = docTypeTextBox.Text,
                        s105 = productCodeTextBox.Text,
                        s1 = moduleCodeTextBox.Text,
                        s2 = moduleNameTextBox.Text,
                        s3 = parentComboBox.Text,
                        s8 = moduleImgTextBox.Text,
                        s39 = treeLvlTextBox.Text,
                        s40 = prevLvlTextBox.Text,
                        n1 = Convert.ToInt32(displayOrderTextBox.Text)
                    };
                    if (rules.modtreeValidation(mt) == true)
                    {
                        dal.AddStudent(mt);
                        MessageBox.Show("New record successfully saved.");
                        ResetControls();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured unable to save record!\n" + ex.Message, "", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                finally
                {
                    PopulateModTreGrid();
                }
            }
            else
            {
                MessageBox.Show("Please dont leave any fields empty", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            modtree mt = new modtree()
            {
                n100 = Convert.ToInt32(idTextBox.Text),
                s100 = childTypeComboBox.Text,
                s101 = statusComboBox.Text,
                s102 = docTypeTextBox.Text,
                s105 = productCodeTextBox.Text,
                s1 = moduleCodeTextBox.Text,
                s2 = moduleNameTextBox.Text,
                s3 = parentComboBox.Text,
                s8 = moduleImgTextBox.Text,
                s39 = treeLvlTextBox.Text,
                s40 = prevLvlTextBox.Text,
                n1 = Convert.ToInt32(displayOrderTextBox.Text)
            };
            dal.UpdateStudent(mt);
        }

        private void sec_addButton_Click(object sender, RoutedEventArgs e)
        {
            Collapse();

            secLvl2DataGrid.Visibility = Visibility.Visible;
            selctSecL1Grid.Visibility = Visibility.Visible;
            selctSecL2Grid.Visibility = Visibility.Visible;
            selSecCntlBtnGrid.Visibility = Visibility.Visible;
            scrnCntlBtnGrid.Visibility = Visibility.Visible;

            string modCode = selectedModule.s1.ToString().Substring(0, 2);
            string sql = "SELECT n100, s100, s101, s102, s105, s1, s2, s3, s8, s39, s40, n1 FROM modtree WHERE s100 = 'SEC' AND LEFT(s1,2)= '" + modCode + "' AND LEN(s1)= 4 ORDER BY s1 DESC";
            modViewSource.Source = dal.Exec(sql);
            modViewSource.View.Refresh();

            moduleDataGrid.Visibility = Visibility.Visible;
            



        }

        ///// ednds heree
    }
}
