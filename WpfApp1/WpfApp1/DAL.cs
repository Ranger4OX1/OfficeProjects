using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data.SqlClient;
using System.Windows;
using System.Data;

namespace WpfApp1
{
    internal class DAL
    {
        private DBEntities modtreeContext = null;

        public DAL()
        {
            modtreeContext = new DBEntities();
        }

        public modtree Get(int id)
        {
            return modtreeContext.modtrees.Find(id);
        }

        public int Search(string sVal)
        {
            using (var ctx = new DBEntities())
            {
                var mods = ctx.modtrees
                                .Where(s => s.s1 == sVal).Count();
                return mods;
            }
        }

        public modtree GetRecord(string sVal)
        {
            using (var ctx = new DBEntities())
            {
                var mods = ctx.modtrees

                                .Where(s => s.s1 == sVal).First();
                return mods;
            }
        }

        public void InsertSectionLvl1(modtree module, string secName, string secCode)
        {
            modtree entry = new modtree();
            entry.s1 = secName;
            entry.s2 = secCode;
            entry.s3 = "1";
            entry.s39 = "1";
            entry.s40 = module.s1.ToString().Substring(0, 2);
            entry.s100 = "SEC";
            entry.s101 = module.s101;
            entry.s102 = "SEC";
            entry.s105 = module.s105;            

            if (entry != null)
            {
                modtreeContext.modtrees.Add(entry);
                modtreeContext.SaveChanges();
            }
        }
        public void InsertSectionLvl2(modtree module, string secName, string secCode)
        {
            modtree entry = new modtree();
            entry.s1 = secName;
            entry.s2 = secCode;
            entry.s3 = "1";
            entry.s39 = "2";
            entry.s40 = module.s1.ToString().Substring(0, 4);
            entry.s100 = "SEC";
            entry.s101 = module.s101;
            entry.s102 = "SEC";
            entry.s105 = module.s105;

            if (entry != null)
            {
                modtreeContext.modtrees.Add(entry);
                modtreeContext.SaveChanges();
            }
        }

        public List<modtree> GetAll()
        {
            using (var context = new DBEntities())
            {
                //List<modtree> list ;
                var posts = context.modtrees
                                   //.OrderByDescending(p => p.n100)
                                   .Where(p => p.s102 == "MOD").ToList();
                return posts;
            }
        }

        //public List<modtree> GetModule(string sVal)
        //{
           
        //}

        public static SqlConnection GetConnection()
        {
            string sql = @"Data Source = localhost;
                            Initial Catalog = LocalMaster;
                            Integrated Security = true ";
            SqlConnection conn = new SqlConnection(sql);
            try
            {
                conn.Open();
                MessageBox.Show("Sql Connected.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                //MessageBox.Show("Module code already exits!", "", MessageBoxButton.OK, MessageBoxImage.Error);


            }
            catch (SqlException ex)
            {
                MessageBox.Show("Connection error\n" + ex.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                //throw;
            }
            return conn;
        }

        public static string[] dtToSArray(DataTable dt, char type)
        {
            string[] arr = new string[1];
            try
            {
                if (type == 'M')
                {
                    arr = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        arr[i] = dt.Rows[i][0].ToString();
                    }
                }
                else
                {
                    arr = new string[dt.Columns.Count];
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        arr[i] += dt.Rows[0][i].ToString();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error in converting dt to Array", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return arr;
        }
        public  DataTable Exec(string sql)
        {
            IDictionary<int, string> comboData = new Dictionary<int, string>();

            SqlConnection conn = GetConnection();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();

            da.Fill(dt);

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    comboData.Add(i, dt.Rows[i][0].ToString());
            //    //captionCb_sPnl.Items.Add(dt.Rows[i]["ItemName"].toString());
            //}

            return dt;
        }

        public void AddStudent(modtree modtre)
        {
            if (modtre != null)
            {
                modtreeContext.modtrees.Add(modtre);
                modtreeContext.SaveChanges();
            }
        }


        public void UpdateStudent(modtree modtre)
        {
            decimal id = modtre.n100;
            var  modtreFind= this.Get(Convert.ToInt32(id));
            if (modtreFind != null)
            {
                modtreFind.s100 = modtre.s100;
                modtreFind.s101 = modtre.s101;
                modtreFind.s102 = modtre.s102;
                modtreFind.s1 = modtre.s1;
                modtreFind.s2 = modtre.s2;
                modtreFind.s3 = modtre.s3;
                modtreFind.s8 = modtre.s8;
                modtreFind.s39 = modtre.s39;
                modtreFind.s40 = modtre.s40;
                modtreFind.n1 = modtre.n1;
                modtreeContext.SaveChanges();
            }
        }

        public void RemoveStudent(int id)
        {
            var modtreObj = modtreeContext.modtrees.Find(id);
            if (modtreObj != null)
            {
                modtreeContext.modtrees.Remove(modtreObj);
                modtreeContext.SaveChanges();
            }
        }
    }
}
