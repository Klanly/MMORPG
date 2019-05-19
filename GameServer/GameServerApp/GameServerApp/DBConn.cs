using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class DBConn
{
    public static string m_DBGameServer;
    public static string DBGameServer
    {
        get
        {
            if (string.IsNullOrEmpty(m_DBGameServer))
            {
                m_DBGameServer = System.Configuration.ConfigurationManager.ConnectionStrings["DBGameServer"].ConnectionString;
            }
            return m_DBGameServer;            
        }
    }

    public static string m_Account;
    public static string DBAccount
    {
        get
        {
            if (string.IsNullOrEmpty(m_Account))
            {
                m_Account = System.Configuration.ConfigurationManager.ConnectionStrings["DBAccount"].ConnectionString;
            }
            return m_Account;
        }
    }
}
