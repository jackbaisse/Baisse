using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baisse.Model.Models.AppsettingModel
{
    public class DBConfig
    {
        /// <summary>
        /// mysql
        /// </summary>
        public string MySqlConnection { get; set; }
        /// <summary>
        /// oralce
        /// </summary>
        public string OracleConnection { get; set; }
        /// <summary>
        /// sqlserver
        /// </summary>
        public string SqlServerConnection { get; set; }
        /// <summary>
        /// sqlserver
        /// </summary>
        public string SqliteConnection { get; set; }
    }
}
