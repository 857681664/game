using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using game.dto;
using game.entity;
using MySql.Data.MySqlClient;

namespace game.control
{
    public partial class MonsterList : Form
    {
        private static string url = "Server=localhost;Database=db;Uid=root;Pwd=13566189672";
        private static MySqlConnection connection;
        private static MySqlCommand command;
        private static MySqlDataAdapter adapter;
        private GameData gameData;
        public MonsterList(GameData gameData)
        {
            InitializeComponent();
            connection = new MySqlConnection(url);
            dataGridView1.AllowUserToAddRows = false;
            InitMonsterList();
            this.gameData = gameData;
        }

        /// <summary>
        /// 初始化怪兽列表
        /// </summary>
        public void InitMonsterList()
        {
            connection.Open();
            try
            {
                command = connection.CreateCommand();
                command.CommandText = "select * from monster";
                DataSet set = new DataSet();
                adapter = new MySqlDataAdapter(command);
                adapter.Fill(set);
                dataGridView1.DataSource = set.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[2].HeaderText = "名字";
                dataGridView1.Columns[5].HeaderText = "攻击";
                dataGridView1.Columns[6].HeaderText = "防御";
                dataGridView1.Columns[7].HeaderText = "属性";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
                command = null;
            }
        }

        /// <summary>
        /// 根据条件筛选怪兽
        /// </summary>
        /// <param name="star"></param>
        /// <param name="prop"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="name"></param>
        public void SearchMonsterByCondition(string sql, int star = 0, string prop = "", int min = 0, int max = 0)
        {
            connection.Open();
            try
            {
                command = connection.CreateCommand();
                command.CommandText = sql;
                if (star != 0)
                    command.Parameters.AddWithValue("@star", star);
                if (!prop.Equals(""))
                    command.Parameters.AddWithValue("@prop", prop);
                if (min != 0)
                    command.Parameters.AddWithValue("@min", min);
                if (max != 0)
                    command.Parameters.AddWithValue("@max", max);
                DataSet set = new DataSet();
                adapter = new MySqlDataAdapter(command);
                adapter.Fill(set);
                dataGridView1.DataSource = set.Tables[0];
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
                command = null;
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            string star = "0", prop = "";
            int min = 0, max = 0;
            foreach (RadioButton button in starGroupBox.Controls)
            {
                if (button.Checked)
                {
                    star = button.Name[button.Name.Length - 1].ToString();
                    break;
                }
            }
            foreach (RadioButton button in propGroupBox.Controls)
            {
                if (button.Checked)
                {
                    prop = button.Name;
                    break;
                }
            }
            if(!attackMinTBox.Text.Equals(""))
                min = Convert.ToInt32(attackMinTBox.Text);
            if(!attackMaxTBox.Text.Equals(""))
                max = Convert.ToInt32(attackMaxTBox.Text);
            StringBuilder sql = null;
            if (!star.Equals("0"))
            {
                sql = new StringBuilder("select * from monster where m_star = @star");
                if (!prop.Equals(""))
                    sql.Append(" and m_prop = @prop");
                if (min != 0)
                    sql.Append(" and m_attack >= @min");
                if (max != 0)
                    sql.Append(" and m_attack <= @max");
            }
            else if (!prop.Equals(""))
            {
                sql = new StringBuilder("select * from monster where m_prop = @prop");
                if (!star.Equals("0"))
                    sql.Append(" and m_star = @star");
                if (min != 0)
                    sql.Append(" and m_attack >= @min");
                if (max != 0)
                    sql.Append(" and m_attack <= @max");
            }
            else if (min != 0)
            {
                sql = new StringBuilder("select * from monster where m_attack >= @min");
                if (!star.Equals(""))
                    sql.Append(" and m_star = @star");
                if (!prop.Equals(""))
                    sql.Append(" and m_prop = @prop");
                if (max != 0)
                    sql.Append(" and m_attack <= @max");
            }
            else if (max != 0)
            {
                sql = new StringBuilder("select * from monster where m_attack <= @max");
                if (!star.Equals(""))
                    sql.Append(" and m_star = @star");
                if (!prop.Equals(""))
                    sql.Append(" and m_prop = @prop");
                if (min != 0)
                    sql.Append(" and m_attack >= @min");
            }
            SearchMonsterByCondition(sql.ToString(), Convert.ToInt32(star), prop, min, max);
        }

        /// <summary>
        /// 点击单元格的任意一行显示该单元格怪兽详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            string name = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            CardMonster card;
            if (id[0] == '2')
                card = (TwoStarMonster)GetMonster(gameData.TwoStarMonsters, name);
            else if (id[0] == '3')
                card = (ThreeStarMonster)GetMonster(gameData.ThreeStarMonsters, name);
            else
                card = (FourStarMonster)GetMonster(gameData.FourStarMonsters, name);
            monsterInfoTBox.Text = card.ToString();
        }

        /// <summary>
        /// 重置筛选条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetBtn_Click(object sender, EventArgs e)
        {
            InitMonsterList();
            foreach (RadioButton button in starGroupBox.Controls)
                button.Checked = false;
            foreach (RadioButton button in propGroupBox.Controls)
                button.Checked = false;
            attackMinTBox.Text = "";
            attackMaxTBox.Text = "";
        }

        /// <summary>
        /// 根据怪兽名字查询怪兽信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchByNameBtn_Click(object sender, EventArgs e)
        {
            string name = monsterNameTBox.Text;
            string sql = "select * from monster where m_name like '%" + name + "%'";
            SearchMonsterByCondition(sql);
        }

        /// <summary>
        /// 从给定的列表个名字获取怪兽
        /// </summary>
        /// <param name="list"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public CardMonster GetMonster(List<CardMonster> list, string name)
        {
            CardMonster card = new CardMonster();
            foreach (CardMonster m in list.Where(m => m.Name.Equals(name)))
            {
                card = m;
                break;
            }
            return card;
        }
    }
}
