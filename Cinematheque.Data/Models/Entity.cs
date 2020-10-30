using Cinematheque.Data.Utils;
using System;
using System.Collections.Generic;
using System.Data;

namespace Cinematheque.Data
{
    public class Entity<T> where T : class
    {
        internal static TableManager tm; 
        public Guid ID { get { return new Guid(row["ID"].ToString()); } }

        internal DataRow row;

        private bool isNew;

        public static List<T> AllItems
        {
            get { return GetByQuery(""); }
        }

        static Entity()
        {
            tm = new TableManager.GetTableManager(typeof(T).Name);
        }

        public Entity()
        {
            row = tm.Table.NewRow();
            row["ID"] = Guid.NewGuid();
            isNew = true;
        }

        public Entity(DataRow dr)
        {
            row = dr;
            isNew = false;
        }

        public static T GetByID(Guid id)
        {
            while(true)
            {
                try
                {
                    return (T)Activator.CreateInstance(typeof(T), new object[]
                    {
                        tm.Table.Select($"ID = '{id}'")[0]
                    });
                }
                catch
                {
                    LogWriter.Log($"No such item with ID = {id} exists in cache  of table {typeof(T).Name}");
                }

                if (tm.Recharge($"ID = '{id}'") == 0)
                {
                    return null;
                }
            }
        }

        public static List<T> GetByQuery(string query)
        {
            return GetByQuery(query, true);
        }

        public static List<T> GetByQuery(string query, bool seekInDB)
        {
            var res = new List<T>();
            var rows = new List<DataRow>();

            rows.AddRange(tm.Table.Select(query));

            if (seekInDB)
            {
                var strSel = "";

                if (rows.Count < 200)
                {
                    foreach(var row in rows)
                    {
                        strSel += $"'{row["ID"]}', ";
                    }

                    if(strSel.Length > 0)
                    {
                        strSel = strSel.Substring(0, strSel.Length - 2);
                    }

                    if(tm.Recharge(query + ((strSel == "") ? "" : 
                                               ((query == "") ? "" : " and")) 
                                               + " ID not in (" + strSel + ")") > 0)
                    {
                        rows.Clear();
                        rows.AddRange(tm.Table.Select(query));
                    }
                }
                else
                {
                    int count = 0;

                    foreach(var row in tm.GetIDs(query))
                    {
                        if(tm.Table.Select($"Id = '{row["ID"]}'").Length == 0)
                        {
                            strSel += $"ID = '{row["ID"]}', ";
                            count++;
                        }

                        if(count == 200)
                        {
                            tm.Recharge(" ID in (" + strSel.Substring(0, strSel.Length - 2) + ")");
                            strSel = "";
                            count = 0;
                        }
                    }

                    if(strSel.Length > 0)
                    {
                        tm.Recharge(" ID in (" + strSel.Substring(0, strSel.Length - 2) + ")");
                    }

                    rows.Clear();
                    rows.AddRange(tm.Table.Select(query));
                }

                foreach(var row in rows)
                {
                    res.Add((T)Activator.CreateInstance(typeof(T), new object[] { row }));
                }
            }

            return res;
        }

        public void Save()
        {
            if (isNew)
            {
                isNew = false;
                tm.Table.Rows.Add(row);
            }

            tm.Update(row);
        }

        public void Delete()
        {
            row.Delete();
            tm.Update(row);
        }
    }
}
