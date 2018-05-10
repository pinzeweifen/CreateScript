using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.Text;
using System.Collections.Generic;

namespace Database
{
    public enum DataType
    {
        /// <summary>
        /// 一个 NULL 值
        /// </summary>
        NULL,
        /// <summary>
        /// 一个带符号的整数，根据值的大小存储在 1、2、3、4、6 或 8 字节中
        /// </summary>
        INTEGER,
        /// <summary>
        /// 一个浮点值，存储为 8 字节的 IEEE 浮点数字
        /// </summary>
        REAL,
        /// <summary>
        /// 一个文本字符串，使用数据库编码（UTF-8、UTF-16BE 或 UTF-16LE）存储
        /// </summary>
        TEXT,
        /// <summary>
        /// 一个 blob 数据，完全根据它的输入存储
        /// </summary>
        BLOB,
        /// <summary>
        /// 如果不能转为其他类型，那么它是TEXT
        /// </summary>
        NUMERIC,
        /// <summary>
        /// aaaa
        /// </summary>
        NONE
    }

    public class DbAccess
    {
        private SqliteConnection dbConnection;
        private SqliteCommand dbCommand;
        private SqliteDataReader reader;

        public DbAccess() { }
        public DbAccess(string connectionString)
        {
            OpenDB(connectionString);
        }
        /// <summary>
        /// 连接数据库
        /// </summary>
        public void OpenDB(string connectionString)
        {
            try
            {
                dbConnection = new SqliteConnection(string.Format("data source={0}.db", connectionString));
                dbConnection.Open();
                Debug.Log("Connected to db");
            }
            catch (Exception e)
            {
                throw new SqliteException(e.ToString());
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void CloseSqlConnection()
        {
            if (dbCommand != null)
            {
                dbCommand.Dispose();
            }

            dbCommand = null;
            if (reader != null)
            {
                reader.Dispose();
            }

            reader = null;
            if (dbConnection != null)
            {
                dbConnection.Close();
            }

            dbConnection = null;

            Debug.Log("Disconnected from db.");

        }

        /// <summary>
        /// 执行命令行
        /// </summary>
        public SqliteDataReader ExecuteQuery(string sqlQuery)
        {
            try
            {
                dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = sqlQuery;
                reader = dbCommand.ExecuteReader();
                return reader;
            }
            catch (Exception e)
            {
                CloseSqlConnection();
                throw new SqliteException(e.ToString());
            }
        }

        /// <summary>
        /// 是否已创建表
        /// </summary>
        public bool IsCreateTable(string name)
        {
            var query = ExecuteQuery(string.Format("select count(*) from sqlite_master where type='table' and name='{0}'", name));
            query.Read();
            bool isTrue;
            try
            {
                isTrue = query.GetBoolean(0);
            }
            catch (Exception e)
            {
                CloseSqlConnection();
                throw new SqliteException(e.ToString());
            }
            return isTrue;
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="colName"></param>
        /// <param name="colType"></param>
        /// <param name="keyIndex">主键索引</param>
        /// <param name="notNull">不能为空的索引</param>
        /// <returns></returns>
        public SqliteDataReader CreateTable(string name, string[] colName, DataType[] colType, int keyIndex = -1, int[] notNull = null)
        {
            if (colName.Length != colType.Length)
            {
                throw new SqliteException("columns.Length != colType.Length");
            }

            bool[] not = new bool[colName.Length];
            for (int i = 0; i < not.Length; i++)
            {
                not[i] = false;
                if (notNull != null)
                {
                    for (int j = 0; j < notNull.Length; j++)
                    {
                        if (i == notNull[j])
                        {
                            not[i] = true; break;
                        }
                    }
                }
            }

            StringBuilder tmp = new StringBuilder();
            for (int i = 0; i < colName.Length; i++)
            {
                tmp.AppendFormat("{0} {1}", colName[i], colType[i].ToString());
                if (keyIndex == i)
                    tmp.Append(" PRIMARY KEY");
                if (not[i])
                {
                    tmp.Append(" NOT NULL");
                }
                tmp.Append(",");
            }
            tmp.Length = tmp.Length - 1;
            return ExecuteQuery(string.Format("CREATE TABLE {0} ({1})", name, tmp));
        }

        /// <summary>
        /// 搜索表
        /// </summary>
        /// <returns></returns>
        public List<string> ShowTables()
        {
            var data = ExecuteQuery(@"select name from sqlite_master where type='table' order by name");
            List<string> list=new List<string>();
            while(data.Read()){ 
                list.Add(data.GetString(0));
            }
            return list;
        }

        /// <summary>
        /// 清空表
        /// </summary>
        public SqliteDataReader DeleteContents(string tableName)
        {
            return ExecuteQuery("DELETE FROM " + tableName);
        }

        /// <summary>
        /// 删除表
        /// </summary>
        public SqliteDataReader DropTable(string name)
        {
            return ExecuteQuery("DROP TABLE " + name);
        }

        /// <summary>
        /// 读取表所有数据
        /// </summary>
        public SqliteDataReader ReadFullTable(string tableName)
        {
            return ExecuteQuery("SELECT * FROM " + tableName);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        public SqliteDataReader InsertInto(string tableName, string[] values)
        {
            return ExecuteQuery(string.Format("INSERT INTO {0} VALUES ({1})", tableName, ArrayToString(values, true)));
        }

        /// <summary>
        /// 按列插入数据
        /// </summary>
        public SqliteDataReader InsertIntoSpecific(string tableName, string[] cols, string[] values)
        {
            if (cols.Length != values.Length)
            {
                throw new SqliteException("columns.Length != values.Length");
            }
            return ExecuteQuery(string.Format("INSERT INTO {0} ({1}) VALUES({2})", tableName, ArrayToString(cols), ArrayToString(values, true)));

        }

        /// <summary>
        /// 修改数据
        /// </summary>
        public SqliteDataReader UpdateInto(string tableName, string[] keys, string[] values, Operation operation)
        {
            StringBuilder query = new StringBuilder();
            query.AppendFormat("UPDATE {0} SET ", tableName);

            for (int i = 0; i < keys.Length; i++)
            {
                query.AppendFormat("{0}={1},", keys[i], values[i]);
            }
            query.Length = query.Length - 1;
            query.AppendFormat(" WHERE {0}", operation.ToString());

            return ExecuteQuery(query.ToString());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public SqliteDataReader Delete(string tableName, Operation operation)
        {
            return ExecuteQuery(string.Format("DELETE FROM {0} WHERE {1}", tableName, operation.ToString()));
        }



        /// <summary>
        /// 按条件查找对应列数据
        /// </summary>
        public SqliteDataReader SelectWhere(string tableName, string[] columns, Operation operation)
        {
            return ExecuteQuery(string.Format("SELECT {0} FROM {1} WHERE {2}",
                ArrayToString(columns), tableName, operation.ToString()));
        }

        /// <summary>
        /// 查找对应列数据
        /// </summary>
        public SqliteDataReader Select(string tableName, string[] columns)
        {
            return ExecuteQuery(string.Format("SELECT {0} FROM {1}", ArrayToString(columns), tableName));
        }

        private string ArrayToString(string[] values, bool isAddColon = false)
        {
            StringBuilder tmp = new StringBuilder();
            var len = values.Length;
            for (int i = 0; i < len; i++)
            {
                if (isAddColon)
                    tmp.AppendFormat("'{0}',", values[i]);
                else
                    tmp.AppendFormat("{0},", values[i]);
            }
            tmp.Length = tmp.Length - 1;
            return tmp.ToString();
        }
    }

    public abstract class Operation
    {
        private object m_Obj, m_Obj2;
        public Operation(string str1, string str2) { m_Obj = str1; m_Obj2 = str2; }
        public Operation(Operation operation1, Operation operation2) { m_Obj = operation1; m_Obj2 = operation2; }
        public new string ToString()
        {
            if (m_Obj is Operation)
            {
                return string.Format(" {0} {1} {2} ", (m_Obj as Operation).ToString(), Character(), (m_Obj2 as Operation).ToString());
            }
            else
            {
                return string.Format(" {0} {1} '{2}' ", m_Obj, Character(), m_Obj2);
            }
        }
        protected abstract string Character();
    }

    /// <summary>
    /// 比较运算【等于】
    /// </summary>
    public class DbCompareEqual : Operation
    {
        public DbCompareEqual(string str1, string str2) : base(str1, str2) { }
        protected override string Character() { return "=="; }
    }
    /// <summary>
    /// 比较运算【不等于】
    /// </summary>
    public class DbCompareNotEqual : Operation
    {
        public DbCompareNotEqual(string str1, string str2) : base(str1, str2) { }
        protected override string Character() { return "!="; }
    }
    /// <summary>
    /// 比较运算【大于】
    /// </summary>
    public class DbCompareGreaterThan : Operation
    {
        public DbCompareGreaterThan(string str1, string str2) : base(str1, str2) { }
        protected override string Character() { return ">"; }
    }
    /// <summary>
    /// 比较运算【小于】
    /// </summary>
    public class DbCompareLessThan : Operation
    {
        public DbCompareLessThan(string str1, string str2) : base(str1, str2) { }
        protected override string Character() { return "<"; }
    }
    /// <summary>
    /// 比较运算【小于等于】
    /// </summary>
    public class DbCompareLessThanAndEqual : Operation
    {
        public DbCompareLessThanAndEqual(string str1, string str2) : base(str1, str2) { }
        protected override string Character() { return "<="; }
    }
    /// <summary>
    /// 比较运算【大于等于】
    /// </summary>
    public class DbCompareGreaterThanAndEqual : Operation
    {
        public DbCompareGreaterThanAndEqual(string str1, string str2) : base(str1, str2) { }
        protected override string Character() { return ">="; }
    }
    /// <summary>
    /// 逻辑运算【且】
    /// </summary>
    public class DbLogicalAND : Operation
    {
        public DbLogicalAND(string str1, string str2) : base(str1, str2) { }
        public DbLogicalAND(Operation operation1, Operation operation2) : base(operation1, operation2) { }
        protected override string Character() { return "AND"; }
    }
    /// <summary>
    /// 逻辑运算【或】
    /// </summary>
    public class DbLogicalOR : Operation
    {
        public DbLogicalOR(string str1, string str2) : base(str1, str2) { }
        public DbLogicalOR(Operation operation1, Operation operation2) : base(operation1, operation2) { }
        protected override string Character() { return "OR"; }
    }
    /// <summary>
    /// 逻辑运算【不】
    /// </summary>
    public class DbLogicalNOT : Operation
    {
        public DbLogicalNOT(string str1, string str2) : base(str1, str2) { }
        public DbLogicalNOT(Operation operation1, Operation operation2) : base(operation1, operation2) { }
        protected override string Character() { return "NOT"; }
    }
    /// <summary>
    /// 逻辑运算【不是】
    /// </summary>
    public class DbLogicalISNOT : Operation
    {
        public DbLogicalISNOT(string str1, string str2) : base(str1, str2) { }
        public DbLogicalISNOT(Operation operation1, Operation operation2) : base(operation1, operation2) { }
        protected override string Character() { return "IS NOT"; }
    }
    /// <summary>
    /// 逻辑运算【是空】
    /// </summary>
    public class DbLogicalISNULL : Operation
    {
        public DbLogicalISNULL(string str1, string str2) : base(str1, str2) { }
        public DbLogicalISNULL(Operation operation1, Operation operation2) : base(operation1, operation2) { }
        protected override string Character() { return "IS NULL"; }
    }
    /// <summary>
    /// 逻辑运算【把某个值与使用通配符运算符的相似值进行比较】
    /// </summary>
    public class DbLogicalLIKE : Operation
    {
        public DbLogicalLIKE(string str1, string str2) : base(str1, str2) { }
        public DbLogicalLIKE(Operation operation1, Operation operation2) : base(operation1, operation2) { }
        protected override string Character() { return "LIKE"; }
    }
    /// <summary>
    /// 逻辑运算【把某个值与使用通配符运算符的相似值进行比较】->对大小写敏感
    /// </summary>
    public class DbLogicalGLOB : Operation
    {
        public DbLogicalGLOB(string str1, string str2) : base(str1, str2) { }
        public DbLogicalGLOB(Operation operation1, Operation operation2) : base(operation1, operation2) { }
        protected override string Character() { return "GLOB"; }
    }
    /// <summary>
    /// 逻辑运算【是】
    /// </summary>
    public class DbLogicalIS : Operation
    {
        public DbLogicalIS(string str1, string str2) : base(str1, str2) { }
        public DbLogicalIS(Operation operation1, Operation operation2) : base(operation1, operation2) { }
        protected override string Character() { return "IS"; }
    }
    /// <summary>
    /// 逻辑运算【把某个值与一系列指定列表的值进行比较】
    /// </summary>
    public class DbLogicalIN : Operation
    {
        public DbLogicalIN(string str1, string str2) : base(str1, str2) { }
        public DbLogicalIN(Operation operation1, Operation operation2) : base(operation1, operation2) { }
        protected override string Character() { return "IN"; }
    }
    /// <summary>
    /// 逻辑运算【把某个值与不在一系列指定列表的值进行比较】
    /// </summary>
    public class DbLogicalNOTIN : Operation
    {
        public DbLogicalNOTIN(string str1, string str2) : base(str1, str2) { }
        public DbLogicalNOTIN(Operation operation1, Operation operation2) : base(operation1, operation2) { }
        protected override string Character() { return "NOT IN"; }
    }
    /// <summary>
    /// 逻辑运算【在给定最小值和最大值范围内的一系列值中搜索值】
    /// </summary>
    public class DbLogicalBETWEEN : Operation
    {
        public DbLogicalBETWEEN(string str1, string str2) : base(str1, str2) { }
        public DbLogicalBETWEEN(Operation operation1, Operation operation2) : base(operation1, operation2) { }
        protected override string Character() { return "BETWEEN"; }
    }
}